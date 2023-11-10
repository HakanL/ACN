using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;
using System.IO;

namespace Acn.Packets.RdmNet
{
    public enum RdmNetProtocolIds
    {
        RdmNet = 1,
        Status = 2,
        Controller = 3,
        ChangeNotification = 4
    }

    public class RdmNetFramingPdu : AcnPdu
    {
        public RdmNetFramingPdu()
        {
            Header = new AcnPduHeader((int)RdmNetProtocolIds.RdmNet);
        }

        public RdmNetFramingPdu(RdmNetProtocolIds protocolId)
        {
            Header = new AcnPduHeader((int)protocolId);
        }

        #region PDU Contents

        public string SourceName { get; set; } = string.Empty;

        public int SequenceNumber { get; set; } = 0;

        public short EndpointID { get; set; } = 0;

        #endregion

        #region Read and Write

        public override void ReadData(AcnBinaryReader data)
        {
            SourceName = data.ReadUtf8String(64);
            SequenceNumber = data.ReadOctet4();
            EndpointID = data.ReadOctet2();
            data.BaseStream.Seek(1, SeekOrigin.Current);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteUtf8String(SourceName, 64);
            data.WriteOctet(SequenceNumber);
            data.WriteOctet(EndpointID);
            data.BaseStream.Seek(1, SeekOrigin.Current);
        }

        #endregion

    }
}
