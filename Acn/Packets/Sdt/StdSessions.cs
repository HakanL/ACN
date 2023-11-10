using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class StdSessions : SdtPdu
    {
        public StdSessions()
            : base(StdVectors.Sessions)
        {
        }

        #region Packet Contents
        
        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            throw new NotImplementedException();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
