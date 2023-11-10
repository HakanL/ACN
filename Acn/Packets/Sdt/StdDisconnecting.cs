using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class StdDisconnecting : SdtPdu
    {
        public StdDisconnecting()
            : base(StdVectors.Disconnecting)
        {
        }

        #region Packet Contents

        public int ProtocolId { get; set; }
        
        public ReasonCodes Reason { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ProtocolId = data.ReadOctet4();
            Reason = (ReasonCodes)data.ReadByte();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteOctet(ProtocolId);
            data.Write((byte)Reason);
        }

        #endregion
    }
}
