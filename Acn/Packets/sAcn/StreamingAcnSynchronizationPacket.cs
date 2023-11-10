using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acn.Packets.sAcn
{
    public class StreamingAcnSynchronizationPacket:AcnPacket
    {
        public StreamingAcnSynchronizationPacket()
            : base(ProtocolIds.sACN)
        {
        }

        #region Packet Contents

        public StreamingAcnFramingPdu Framing { get; } = new StreamingAcnFramingPdu();

        #endregion

        #region Read/Write

        protected override void ReadData(AcnBinaryReader data)
        {
            Framing.Header.ReadPdu(data);
            Framing.ReadData(data);
        }

        protected override void WriteData(AcnBinaryWriter data)
        {
            Framing.Header.WritePdu(data);
            Framing.WriteData(data);
            Framing.Header.WriteLength(data);
        }

        #endregion
    }
}
