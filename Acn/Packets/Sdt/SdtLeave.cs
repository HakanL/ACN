using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtLeave : SdtPdu
    {
        public SdtLeave()
            : base(SdtVectors.Leave)
        {
        }

        #region Packet Contents

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
        }

        public override void WriteData(AcnBinaryWriter data)
        {
        }

        #endregion
    }
}
