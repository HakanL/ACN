using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class StdLeaving: SdtPdu
    {
        public StdLeaving()
            : base(StdVectors.Leaving)
        {
        }

        #region Packet Contents

        public Guid LeaderId { get; set; }

        public short ChannelNumber { get; set; }

        public short MemberId { get; set; }

        public int ReliableSequenceNumber { get; set; }

        public ReasonCodes ReasonCode { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            LeaderId = NetworkGuid.FromPacket(data.ReadBytes(16));
            ChannelNumber = data.ReadOctet2();
            MemberId = data.ReadOctet2();
            ReliableSequenceNumber = data.ReadOctet4();
            ReasonCode = (ReasonCodes)data.ReadByte();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.Write(LeaderId.ToNetworkByteArray());
            data.WriteOctet(ChannelNumber);
            data.WriteOctet(MemberId);
            data.WriteOctet(ReliableSequenceNumber);
            data.Write((byte)ReasonCode);
        }

        #endregion
    }
}
