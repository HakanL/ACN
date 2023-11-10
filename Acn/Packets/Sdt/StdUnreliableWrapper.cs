using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class StdUnreliableWrapper:StdReliableWrapper
    {
        public StdUnreliableWrapper()
            : base(StdVectors.UnreliableWrapper)
        {
        }

        #region Packet Contents

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            base.ReadData(data);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            base.WriteData(data);
        }

        #endregion
    }
}
