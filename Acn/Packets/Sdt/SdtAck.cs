using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtAck : SdtPdu
    {
        public SdtAck()
            : base(SdtVectors.Ack)
        {
        }

        #region Packet Contents

        public int ReliableSequenceNumber { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ReliableSequenceNumber = data.ReadOctet4();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteOctet(ReliableSequenceNumber);
        }

        #endregion
    }
}
