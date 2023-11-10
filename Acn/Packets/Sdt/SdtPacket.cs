using Acn.IO;
using Acn.Packets.RdmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtPacket : AcnPacket
    {
        public SdtPacket() : base(ProtocolIds.SDT) { }

        #region Packet Contents

        public AcnPduHeader Header { get; } = new AcnPduHeader(0, 1);

        #endregion

        #region Read/Write

        protected override void ReadData(AcnBinaryReader data)
        {
            Header.ReadPdu(data);
        }

        protected override void WriteData(AcnBinaryWriter data)
        {
            //Header.WritePdu(data);
            //RdmNet.WritePdu(data);
            //RdmNet.WriteLength(data);
            //Header.WriteLength(data);
        }

        #endregion
    }


}
