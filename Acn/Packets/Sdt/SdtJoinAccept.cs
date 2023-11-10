using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtJoinAccept: SdtPdu
    {
        public SdtJoinAccept()
            : base(SdtVectors.JoinAccept)
        {
        }

        #region Packet Contents

        public Guid ComponentId { get; set; }

        public short ChannelNumber { get; set; }

        public short MemberId { get; set; }

        public int ReliableSequenceNumber { get; set; }

        public short ReciprocalChannel { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ComponentId = NetworkGuid.FromPacket(data.ReadBytes(16));
            ChannelNumber = data.ReadOctet2();
            MemberId = data.ReadOctet2();
            ReliableSequenceNumber = data.ReadOctet4();
            ReciprocalChannel = data.ReadOctet2();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.Write(ComponentId.ToNetworkByteArray());
            data.WriteOctet(ChannelNumber);
            data.WriteOctet(MemberId);
            data.WriteOctet(ReliableSequenceNumber);
            data.WriteOctet(ReciprocalChannel);
        }

        #endregion
    }
}
