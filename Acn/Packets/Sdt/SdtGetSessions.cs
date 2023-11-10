using Acn.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtGetSessions: SdtPdu
    {
        public SdtGetSessions()
            : base(SdtVectors.GetSessions)
        {
        }

        #region Packet Contents

        public Guid ComponentId { get; set; }

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            ComponentId = NetworkGuid.FromPacket(data.ReadBytes(16));
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.Write(ComponentId.ToNetworkByteArray());
        }

        #endregion
    }
}
