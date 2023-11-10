using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;
using System.IO;
using Acn.Rdm;

namespace Acn.Packets.RdmNet
{
    public class RdmNetStatusPdu : AcnPdu
    {
        public RdmNetStatusPdu()
        {
            Header = new AcnPduHeader((int)DmxStartCodes.RDM);
        }

        #region PDU Contents

        public short StatusCode { get; set; } = 0;

        public string StatusMessage { get; set; } = string.Empty;

        #endregion

        #region Read and Write

        public override void ReadData(AcnBinaryReader data)
        {
            StatusCode = data.ReadOctet2();
            StatusMessage = data.ReadUtf8String(Header.Length - 4);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            data.WriteOctet(StatusCode);
            data.WriteUtf8String(StatusMessage, StatusMessage.Length);
        }

        #endregion

    }
}
