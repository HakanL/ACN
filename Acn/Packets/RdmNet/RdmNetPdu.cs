using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;
using System.IO;
using Acn.Rdm;

namespace Acn.Packets.RdmNet
{
    public class RdmNetPdu : AcnPdu
    {
        public RdmNetPdu()
        {
            Header = new AcnPduHeader((int)DmxStartCodes.RDM);
        }

        public RdmNetPdu(DmxStartCodes vector)
        {
            Header = new AcnPduHeader((int)vector);
        }

        #region PDU Contents

        private byte[] rdmData = null;

        public byte[] RdmData
        {
            get { return rdmData; }
            set { rdmData = value; }
        }

        #endregion

        #region Read and Write

        public override void ReadData(AcnBinaryReader data)
        {
            RdmData = data.ReadBytes(Header.Length - 3);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            if (RdmData != null)
                data.Write(RdmData);
        }

        #endregion

    }
}
