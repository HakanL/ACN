using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Security.Policy;
using Acn.Sockets;
using Acn.IO;
using System.Diagnostics;
using Acn.Packets.Sdt;
using Acn.Rdm;
using System.Net.Sockets;
using System.IO;
using Acn.Packets.sAcn;
using Acn.Sdt;
using System.Runtime.InteropServices;
using Acn.Packets.Dmp;

namespace Acn.Std
{
    public class SdtServer : AcnSocket, IProtocolFilter
    {



        /// <summary>
        /// Starts a new Sdt Session listener
        /// </summary>
        /// <param name="localEndPoint"></param>
        /// <param name="interfaceIdentifier"></param>
        public SdtServer(IPEndPoint localEndPoint, Guid interfaceIdentifier) : base(interfaceIdentifier)
        {
            RegisterProtocolFilter(this);
            Open(localEndPoint);
        }

        int IProtocolFilter.ProtocolId
        {
            get { return (int)ProtocolIds.SDT; }
        }

        void IProtocolFilter.ProcessPacket(IPEndPoint remoteClient, AcnRootLayer rootHeader, AcnBinaryReader data)
        {
            SdtPacket sdtPacket = AcnPacket.ReadPacket(rootHeader, data) as SdtPacket;

            if (sdtPacket == null)
                return;

            if (NewSdtPacket != null)
                NewSdtPacket(this, new NewPacketEventArgs<SdtPacket>(remoteClient, sdtPacket));

            SdtPdu body = SdtPduFactory.Build(sdtPacket.Header);
            body.rootLayer = rootHeader;
            body.ReadData(data);

            if (sdtPacket.Header.Vector == (int)SdtVectors.UnreliableWrapper || sdtPacket.Header.Vector == (int)SdtVectors.ReliableWrapper)
            {
                SdtReliableWrapper wrapper = body as SdtReliableWrapper;
                if (wrapper.Pdu.Count == 0)
                {
                    RaiseClientHeartbeat(remoteClient, wrapper as SdtUnreliableWrapper);
                    return;
                }
                SdtSession session = sdtSessions.Where(s => s.remoteClient.ToString() == remoteClient.ToString() && s.MemberId == wrapper.WrappedHeader.MemberId).FirstOrDefault();
                if (session == null) return;

                if (wrapper.WrappedHeader.ProtocolId == ProtocolIds.SDT)
                {
                    
                    foreach (AcnPdu pdu in wrapper.Pdu)
                    {
                        switch (pdu.Header.Vector)
                        {
                            case (int)SdtVectors.Leave:
                                session.RxLeave(wrapper);
                                break;
                            case (int)SdtVectors.Connect:
                                session.RxConnect(wrapper, (SdtConnect)pdu);
                                break;
                            case (int)SdtVectors.Ack:
                                session.RxAck(wrapper);
                                break;
                        }
                    }
                    return;
                }
                if (wrapper.WrappedHeader.ProtocolId == ProtocolIds.DMP)
                {
                    switch (wrapper.Pdu[0].Header.Vector)
                    {
                        case (int)DmpMessages.GetProperty:
                            session.RxDmpGetProperty(wrapper);
                            return;
                        case (int)DmpMessages.Subscribe:
                            session.RxDmpSubscribe(wrapper);
                            return;
                    }
                    Debug.WriteLine(string.Format( "Unhandled DMP vector {0}", (DmpMessages)wrapper.Pdu[0].Header.Vector));
                    return;
                }
                Debug.WriteLine(string.Format("Unhandled SDT wrapped protocol {0}", wrapper.WrappedHeader.ProtocolId));
                return;
            }
            switch (sdtPacket.Header.Vector)
            {
                case (int)SdtVectors.Join:
                    RaiseClientJoin(remoteClient, body as SdtJoin);
                    return;
                case (int)SdtVectors.JoinAccept:
                    RaiseClientJoinAccept(remoteClient, body as SdtJoinAccept);
                    return;
                case (int)SdtVectors.JoinRefuse:
                    RaiseClientJoinRefuse(remoteClient, body as SdtJoinRefuse);
                    return;
                case (int)SdtVectors.Leaving:
                    RaiseClientLeaving(remoteClient, body as SdtLeaving);
                    return;
            }
            Debug.WriteLine(string.Format("Unhandled SDT packet {0}", sdtPacket.Header.Vector));

        }

        public delegate void ClientJoinEventHandler(object sender, IPEndPoint remoteClient, ref bool Reject);
        public event ClientJoinEventHandler ClientJoinEvent;
        public event EventHandler<NewPacketEventArgs<SdtPacket>> NewSdtPacket;
        public event EventHandler<NewPacketEventArgs<SdtPacket>> SdtPacketSent;
        public event SdtSession.ChannelEstablishedEventHandler ChannelEstablished;
        public delegate void ClientLeaveEventHandler(SdtSession session, ReasonCodes reason);
        public event ClientLeaveEventHandler ClientLeave;
        public event SdtSession.GetPropertyEventHandler GetProperty;
        public event SdtSession.PropertySubscribeEventHandler PropertySubscribe;

        private List<SdtSession> sdtSessions = new List<SdtSession>();

        void RaiseClientJoin(IPEndPoint remoteClient, SdtJoin sdtRequest)
        {
            bool Reject = false;
            if (ClientJoinEvent != null)
                ClientJoinEvent(this, remoteClient, ref Reject);
            if (Reject)
            {
                SendJoinRefuse(remoteClient, sdtRequest);
                return;
            }
            SdtSession session = new SdtSession(this, remoteClient, sdtRequest);
            session.ChannelEstablished += ChannelEstablished;
            session.GetProperty += GetProperty;
            session.PropertySubscribe += PropertySubscribe;
            sdtSessions.Add(session);
        }

        void RaiseClientJoinAccept(IPEndPoint remoteClient, SdtJoinAccept sdtRequest)
        {
            SdtSession session = sdtSessions.Where(s => s.remoteClient.ToString() == remoteClient.ToString() && s.MemberId == sdtRequest.MemberId).FirstOrDefault();
            if (session == null) return;
            session.RxJoinAccept(sdtRequest);
        }

        void RaiseClientJoinRefuse(IPEndPoint remoteClient, SdtJoinRefuse sdtRequest)
        {

        }

        protected void SendJoinRefuse(IPEndPoint remoteClient, SdtJoin sdtRequest)
        {
            SdtJoinRefuse sdtPacket = new SdtJoinRefuse();
            this.SendPacket(sdtPacket, remoteClient);
        }

        void RaiseClientLeaving(IPEndPoint remoteClient, SdtLeaving sdtRequest)
        {
            SdtSession session = sdtSessions.Where(s => s.remoteClient.ToString() == remoteClient.ToString() && s.MemberId == sdtRequest.MemberId).FirstOrDefault();
            if (session == null) return;
            Debug.WriteLine(string.Format("rx Leaving: {0}", sdtRequest.ReasonCode));
            sdtSessions.Remove(session);
            if (ClientLeave != null)
                ClientLeave(session, sdtRequest.ReasonCode);
            session.Dispose();
            session = null;
        }

        void RaiseClientHeartbeat(IPEndPoint remoteClient, SdtUnreliableWrapper wrapper)
        {
            if (wrapper.LastMemberToAck != wrapper.FirstMemberToAck) return;
            SdtSession session = sdtSessions.Where(s => s.remoteClient.ToString() == remoteClient.ToString() && s.MemberId == wrapper.LastMemberToAck).FirstOrDefault();
            if (session == null) return;
            session.RxHeartbeat(wrapper);
        }

        void RaiseClientDMPGetProperty(IPEndPoint remoteclient, SdtSession session, SdtReliableWrapper wrapper)
        {

        }

        internal void SendPacket(SdtPdu pdu, IPEndPoint destination)
        {
            SdtPacket packet = new SdtPacket();
            packet.Root.SenderId = SenderId;

            MemoryStream data = new MemoryStream();
            AcnBinaryWriter writer = new AcnBinaryWriter(data);

            AcnPacket.WritePacket(packet, writer);
            pdu.Header.WritePdu(writer);
            pdu.WriteData(writer);
            pdu.Header.WriteLength(writer);
            packet.Root.WriteLength(writer);

            base.SendTo(data.ToArray(), destination);
        }

    }
}
