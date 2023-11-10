using Acn.IO;
using Acn.Packets.Dmp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class StdReliableWrapper : SdtPdu
    {
        public StdReliableWrapper()
            : base(StdVectors.ReliableWrapper)
        {
        }

        protected StdReliableWrapper(StdVectors vector)
            : base(vector)
        {
        }

        #region Packet Contents

        public short ChannelNumber { get; set; }

        public int TotalSequenceNumber { get; set; }

        public int ReliableSequenceNumber { get; set; }

        public int OldestAvailableWrapper { get; set; }

        public short FirstMemberToAck { get; set; }

        public short LastMemberToAck { get; set; }

        public short MAKThreshold { get; set; }

        public SdtWrappedHeader WrappedHeader { get; set; }

        public List<AcnPdu> Pdu { get; set; } = new List<AcnPdu>();

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ChannelNumber = data.ReadOctet2();
            TotalSequenceNumber = data.ReadOctet4();
            ReliableSequenceNumber = data.ReadOctet4();
            OldestAvailableWrapper = data.ReadOctet4();
            FirstMemberToAck = data.ReadOctet2();
            LastMemberToAck = data.ReadOctet2();
            MAKThreshold = data.ReadOctet2();
            //if (rootLayer.Length < data.BaseStream.Position + 6)
            //    return;
            WrappedHeader = new SdtWrappedHeader();
            WrappedHeader.ReadData(data);

            AcnPduHeader header = new AcnPduHeader(0, 1);
            header.ReadPdu(data);
            bool first = true;
            switch (WrappedHeader.ProtocolId)
            {
                case ProtocolIds.SDT:
                    do
                    {
                        SdtPdu tempPdu = SdtPduFactory.Build(header);
                        tempPdu.Header = header;
                        if (!first) tempPdu.Header.ReadFlags(data);
                        tempPdu.ReadData(data);
                        Pdu.Add(tempPdu);
                        first = false;
                    } while (data.BaseStream.Position < rootLayer.Length + 16);
                    break;
                case ProtocolIds.DMP:
                    do
                    {
                        DmpPdu tempPdu = DmpPdu.Create(header);
                        tempPdu.Header = header;
                        if (!first)
                        {
                            tempPdu.AddressType = ((DmpPdu)Pdu[0]).AddressType;
                            tempPdu.Header.ReadFlags(data);
                        }
                        tempPdu.ReadData(data);
                        Pdu.Add(tempPdu);
                        first = false;
                    } while (data.BaseStream.Position < rootLayer.Length + 16);
                    break;
            }
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteOctet(ChannelNumber);
            data.WriteOctet(TotalSequenceNumber);
            data.WriteOctet(ReliableSequenceNumber);
            data.WriteOctet(OldestAvailableWrapper);
            data.WriteOctet(FirstMemberToAck);
            data.WriteOctet(LastMemberToAck);
            data.WriteOctet(MAKThreshold);
            WrappedHeader.WriteData(data);
            foreach (AcnPdu pdu in Pdu)
            {
                pdu.Header.WritePdu(data);
                pdu.WriteData(data);
                pdu.Header.WriteLength(data);
            }
            WrappedHeader.WriteLength(data);
        }

        #endregion
    }
}
