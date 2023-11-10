using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;
using System.IO;
using Acn.Packets.sAcn;
using Acn.Packets.Dmp;

namespace Acn.Packets.sAcn
{
    public abstract class StreamingAcnPacket : AcnPacket
    {
        public StreamingAcnPacket(ProtocolIds protocolId)
            : base(protocolId)
        {
        }

        #region Packet Contents

        private StreamingAcnFramingPdu framing = new StreamingAcnFramingPdu();

        public StreamingAcnFramingPdu Framing
        {
            get { return framing; }
        }

        private StreamingAcnDmxPdu dmx = new StreamingAcnDmxPdu();

        public StreamingAcnDmxPdu Dmx
        {
            get { return dmx; }
        }

        #endregion

        #region Read/Write

        protected override void ReadData(AcnBinaryReader data)
        {
            Framing.Header.ReadPdu(data);
            Framing.ReadData(data);
            Dmx.Header.ReadPdu(data);
            Dmx.ReadData(data);
        }

        protected override void WriteData(AcnBinaryWriter data)
        {
            Framing.Header.WritePdu(data);
            Framing.WriteData(data);
            dmx.Header.WritePdu(data);
            Dmx.WriteData(data);
            Dmx.Header.WriteLength(data);
            Framing.Header.WriteLength(data);
        }

        #endregion
    }
}
