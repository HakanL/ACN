using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;
using Acn.Packets.RdmNet;

namespace Acn.Packets.sAcn
{
    public class RdmNetStatusPacket : AcnPacket
    {
        public RdmNetStatusPacket()
            : base(ProtocolIds.RdmNet)
        {
        }

        #region Packet Contents

        public RdmNetFramingPdu Framing { get; } = new RdmNetFramingPdu(RdmNetProtocolIds.Status);

        public RdmNetStatusPdu RdmNet { get; } = new RdmNetStatusPdu();

        #endregion

        #region Read/Write

        protected override void ReadData(AcnBinaryReader data)
        {
            Framing.Header.ReadPdu(data);
            Framing.ReadData(data);
            RdmNet.Header.ReadPdu(data);
            RdmNet.ReadData(data);
        }

        protected override void WriteData(AcnBinaryWriter data)
        {
            Framing.Header.WritePdu(data);
            Framing.WriteData(data);
            RdmNet.Header.WritePdu(data);
            RdmNet.WriteData(data);
            RdmNet.Header.WriteLength(data);
            Framing.Header.WriteLength(data);
        }

        #endregion
    }
}
