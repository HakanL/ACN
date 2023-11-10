using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtConnect : SdtPdu
    {
        public SdtConnect()
            : base(SdtVectors.Connect)
        {
        }

        public SdtConnect(int ProtocolId)
            : base(SdtVectors.Connect)
        {
            this.ProtocolId = ProtocolId;   
        }

        #region Packet Contents

        public int ProtocolId { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ProtocolId = data.ReadOctet4();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteOctet(ProtocolId);
        }

        #endregion
    }
}
