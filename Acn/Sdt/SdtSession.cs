using Acn.Packets.Dmp;
using Acn.Packets.Sdt;
using Acn.Packets.Sdt.Addresses;
using Acn.Std;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Sdt
{
    public class SessionChannel
    {
        public short ChannelNumber { get; private set; }
        public int SequenceNumber { get; set; }
        public Guid ComponentId { get; set; }
        public ChannelParameterBlock ParameterBlock { get; set; }
        public SessionChannel(Guid ComponentId)
        {
            this.ComponentId = ComponentId;
            Random rng = new Random();
            ChannelNumber = (short)rng.Next(short.MinValue, short.MaxValue);
            SequenceNumber = (int)rng.Next(0, ushort.MaxValue);
            ParameterBlock = ChannelParameterBlock.Build();
        }
        public SessionChannel(Guid ComponentId, ChannelParameterBlock ParameterBlock, short ChannelNumber, int SequenceNumber)
        {
            this.ComponentId = ComponentId;
            this.ParameterBlock = ParameterBlock;
            this.ChannelNumber = ChannelNumber;
            this.SequenceNumber = SequenceNumber;
        }
    }

    public class SdtSession : IDisposable
    {
        public SessionChannel LocalChannel { get; private set; }
        public SessionChannel RemoteChannel { get; private set; }

        public short MemberId { get; private set; }
        public IPEndPoint remoteClient { get; private set; }
        public SdtServer sdtServer { get; private set; }

        public SdtSession(SdtServer sdtServer, IPEndPoint remoteClient, short memberId = 1)
        {
            this.sdtServer = sdtServer;
            this.remoteClient = remoteClient;
            LocalChannel = new SessionChannel(sdtServer.SenderId);
            MemberId = memberId;
            TxJoin();
        }

        internal SdtSession(SdtServer sdtServer, IPEndPoint remoteClient, SdtJoin joinRequest)
        {
            this.sdtServer = sdtServer;
            this.remoteClient = remoteClient;
            LocalChannel = new SessionChannel(sdtServer.SenderId);
            RemoteChannel = new SessionChannel(joinRequest.rootLayer.SenderId, joinRequest.ChannelParameterBlock, joinRequest.ChannelNumber, joinRequest.ReliableSequenceNumber);
            MemberId = joinRequest.MemberId;
            RxJoin(joinRequest);
        }

        internal void RxJoinAccept(SdtJoinAccept joinAccept)
        {
            Debug.WriteLine("rx JoinAccept");
            TxAck(joinAccept.ReliableSequenceNumber);
        }

        public void TxJoinAccept(SdtJoin joinRequest, short reciprocalChannel)
        {
            SdtJoinAccept joinAccept = new SdtJoinAccept();
            joinAccept.ChannelNumber = joinRequest.ChannelNumber;
            joinAccept.ReciprocalChannel = reciprocalChannel;

            joinAccept.ComponentId = joinRequest.rootLayer.SenderId;
            joinAccept.MemberId = joinRequest.MemberId;
            this.MemberId = joinRequest.MemberId;
            joinAccept.ReliableSequenceNumber = joinRequest.ReliableSequenceNumber;

            pendingACK.Add(joinAccept.ReliableSequenceNumber, joinAccept);

            Debug.WriteLine("tx JoinAccept");
            sdtServer.SendPacket(joinAccept, remoteClient);
        }
        public void TxJoin()
        {
            SdtJoin join = new SdtJoin();

            join.ChannelNumber = LocalChannel.ChannelNumber;
            join.ChannelParameterBlock = LocalChannel.ParameterBlock;
            join.MemberId = MemberId;
            join.TotalSequenceNumber = LocalChannel.SequenceNumber++;
            join.ReliableSequenceNumber = join.TotalSequenceNumber;
            join.DestinationAddress = new NullAddress();
            join.AdHocExpiry = 255;
            if (RemoteChannel != null)
            {
                join.ReciprocalChannel = RemoteChannel.ChannelNumber;
                join.ComponentId = RemoteChannel.ComponentId;
            }
            Debug.WriteLine("tx Join");
            sdtServer.SendPacket(join, remoteClient);
        }

        public void RxJoin(SdtJoin joinRequest)
        {
            Debug.WriteLine("rx Join");
            TxJoinAccept(joinRequest, LocalChannel.ChannelNumber);
            if (joinRequest.ReciprocalChannel == 0)
                TxJoin();
        }

        Dictionary<int, SdtPdu> pendingACK = new Dictionary<int, SdtPdu>();

        public void RxAck(SdtReliableWrapper wrapper)
        {
            Debug.WriteLine("rx ACK");
            int ACKID = wrapper.ReliableSequenceNumber;
            if (!pendingACK.ContainsKey(ACKID))
                return;
            if (pendingACK[ACKID].Header.Vector == (int)SdtVectors.JoinAccept)
                RaiseChannelEstablished();
            if (pendingACK.ContainsKey(ACKID))
                pendingACK.Remove(ACKID);
            if (wrapper.LastMemberToAck == -1 && wrapper.FirstMemberToAck == -1)
                return;
            if (wrapper.LastMemberToAck <= MemberId && wrapper.FirstMemberToAck >= MemberId)
                TxAck(wrapper.TotalSequenceNumber);

            Debug.WriteLine(" ======= UNHANDLED ACK ========= ");
        }

        private void TxAck(int SequenceNumber)
        {
            SdtAck ack = new SdtAck();
            ack.ReliableSequenceNumber = RemoteChannel.SequenceNumber++;

            SdtUnreliableWrapper wrapper = new SdtUnreliableWrapper();
            wrapper.ReliableSequenceNumber = SequenceNumber;
            wrapper.OldestAvailableWrapper = SequenceNumber;
            wrapper.TotalSequenceNumber = SequenceNumber + 1;
            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = -1;
            wrapper.WrappedHeader = new SdtWrappedHeader();
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;
            wrapper.Pdu.Add(ack);

            Debug.WriteLine("tx ACK");

            sdtServer.SendPacket(wrapper, remoteClient);
        }


        public void RxLeave(SdtReliableWrapper wrapper)
        {
            Debug.WriteLine("rx Leave");
            TxAck(wrapper.ReliableSequenceNumber);
        }

        public void RxConnect(SdtReliableWrapper wrapper, SdtConnect reqPdu)
        {
            Debug.WriteLine("rx Connect");
            TxConnectAccept(reqPdu);
        }

        public void TxConnect()
        {

            SdtConnect connect = new SdtConnect((int)ProtocolIds.DMP);

            SdtReliableWrapper wrapper = new SdtReliableWrapper();
            wrapper.Pdu.Add(connect);

            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;

            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = 0;

            wrapper.WrappedHeader = new SdtWrappedHeader();
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = 0; //gratuitous, no association

            Debug.WriteLine("tx Connect");
            sdtServer.SendPacket(wrapper, remoteClient);
        }

        public void TxConnectAccept(SdtConnect reqPdu)
        {
            SdtConnectAccept accept = new SdtConnectAccept();
            accept.ProtocolId = reqPdu.ProtocolId;

            SdtReliableWrapper wrapper = new SdtReliableWrapper();
            wrapper.Pdu.Add(accept);

            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;

            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = 0;

            wrapper.WrappedHeader = new SdtWrappedHeader();
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;

            Debug.WriteLine("tx ConnectAccept");

            sdtServer.SendPacket(wrapper, remoteClient);
        }

        public delegate void ChannelEstablishedEventHandler(SdtSession sender);

        public event ChannelEstablishedEventHandler ChannelEstablished;
        void RaiseChannelEstablished()
        {
            if (ChannelEstablished != null)
                ChannelEstablished(this);
        }

        public delegate void GetPropertyEventHandler(SdtSession sender, int Address, ref byte[] Value);
        public event GetPropertyEventHandler GetProperty;

        public void RxDmpGetProperty(SdtReliableWrapper wrapper)
        {
            Debug.WriteLine("rx GetProperties");
            List<DmpGetPropertyReply> retPdu = new List<DmpGetPropertyReply>();
            if (GetProperty != null)
            {
                bool first = true;
                int lengthEstimate = 0x47;
                foreach (DmpGetProperty reqPdu in wrapper.Pdu)
                {
                    byte[] returnData = null;
                    GetProperty(this, reqPdu.ActualAddress, ref returnData);
                    if (returnData == null)
                        throw new ArgumentNullException("returnData", string.Format("Address 0x{0} didn't have a value", reqPdu.ActualAddress.ToString("x8")));
                    if (lengthEstimate + returnData.Length + 6 > 1480)
                    {
                        TxDmpGetPropertyReply(retPdu);
                        retPdu = new List<DmpGetPropertyReply>();
                        first = true;
                        lengthEstimate = 0x47;
                    }
                    DmpGetPropertyReply reply = new DmpGetPropertyReply();
                    reply.Header = new AcnPduHeader((int)DmpMessages.GetPropertyReply, 1, PduFlags.Data); lengthEstimate += 1;
                    if (first)
                    {
                        reply.Header = new AcnPduHeader((int)DmpMessages.GetPropertyReply, 1); lengthEstimate += 4;
                    }
                    reply.AddressType = reqPdu.AddressType; lengthEstimate += 1;
                    reply.ActualAddress = reqPdu.ActualAddress; lengthEstimate += 4;
                    reply.Data = returnData; lengthEstimate += returnData.Length;
                    retPdu.Add(reply);
                    first = false;
                }
                TxDmpGetPropertyReply(retPdu);
            }
        }

        public void TxDmpGetPropertyReply(List<DmpGetPropertyReply> retPdu)
        {
            SdtReliableWrapper wrapper = new SdtReliableWrapper();
            wrapper.Pdu.AddRange(retPdu.ToArray());

            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;

            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = -1;

            wrapper.WrappedHeader = new SdtWrappedHeader(ProtocolIds.DMP);
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;

            Debug.WriteLine("tx GetPropertiesReply");

            sdtServer.SendPacket(wrapper, remoteClient);
        }

        public delegate void PropertySubscribeEventHandler(SdtSession sender, int Address);
        public event PropertySubscribeEventHandler PropertySubscribe;

        public void RxDmpSubscribe(SdtReliableWrapper wrapper)
        {
            Debug.WriteLine("rx Subscribe");
            List<DmpSubscribeAccept> retPdu = new List<DmpSubscribeAccept>();
            if (GetProperty != null)
            {
                bool first = true;
                foreach (DmpSubscribe reqPdu in wrapper.Pdu)
                {
                    PropertySubscribe(this, reqPdu.ActualAddress);
                    DmpSubscribeAccept reply = new DmpSubscribeAccept();
                    reply.Header = new AcnPduHeader((int)DmpMessages.SubscribeAccept, 1, PduFlags.Data); 
                    if (first)
                    {
                        reply.Header = new AcnPduHeader((int)DmpMessages.SubscribeAccept, 1); 
                    }
                    reply.AddressType = reqPdu.AddressType;
                    reply.ActualAddress = reqPdu.ActualAddress;
                    retPdu.Add(reply);
                    first = false;
                }
                TxDmpSubscribeAccept(retPdu);
            }
        }

        public void TxDmpSubscribeAccept(List<DmpSubscribeAccept> retPdu)
        {
            SdtReliableWrapper wrapper = new SdtReliableWrapper();
            wrapper.Pdu.AddRange(retPdu.ToArray());

            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;

            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = -1;

            wrapper.WrappedHeader = new SdtWrappedHeader(ProtocolIds.DMP);
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;

            Debug.WriteLine("tx SubscribeAccept");

            sdtServer.SendPacket(wrapper, remoteClient);
        }

        public void TxEvent(int Address, byte[] Data)
        {
            DmpEvent pdu = new DmpEvent();
            pdu.Data = Data;
            pdu.AddressType = AddressFlags.Size_4Octets;
            pdu.ActualAddress = Address;

            SdtUnreliableWrapper wrapper = new SdtUnreliableWrapper();
            wrapper.Pdu.Add(pdu);

            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;

            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = -1;

            wrapper.WrappedHeader = new SdtWrappedHeader(ProtocolIds.DMP);
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;

            //Debug.WriteLine("tx Event");

            if (remoteClient != null)
                sdtServer.SendPacket(wrapper, remoteClient);
        }

        public void RxHeartbeat(SdtReliableWrapper wrapper)
        {
            Debug.WriteLine("rx Heartbeat");
            TxHeartbeatAck(wrapper.ReliableSequenceNumber);
        }

        public void TxHeartbeatAck(int SequenceNumber)
        {
            SdtAck ack = new SdtAck();
            ack.ReliableSequenceNumber = SequenceNumber;
            RemoteChannel.SequenceNumber = SequenceNumber;

            SdtUnreliableWrapper wrapper = new SdtUnreliableWrapper();
            wrapper.ReliableSequenceNumber = LocalChannel.SequenceNumber;
            wrapper.OldestAvailableWrapper = LocalChannel.SequenceNumber;
            wrapper.TotalSequenceNumber = ++LocalChannel.SequenceNumber;
            wrapper.ChannelNumber = LocalChannel.ChannelNumber;
            wrapper.FirstMemberToAck = -1;
            wrapper.LastMemberToAck = -1;
            wrapper.WrappedHeader = new SdtWrappedHeader();
            wrapper.WrappedHeader.MemberId = MemberId;
            wrapper.WrappedHeader.Association = RemoteChannel.ChannelNumber;
            wrapper.Pdu.Add(ack);

            Debug.WriteLine("tx HeartbeatACK");

            sdtServer.SendPacket(wrapper, remoteClient);
        }

        public void Dispose()
        {
            pendingACK.Clear();
            pendingACK = null;
            remoteClient = null;
            sdtServer = null;
        }

    }
}
