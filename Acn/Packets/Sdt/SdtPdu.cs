using Acn.IO;
using Acn.Packets.sAcn;
using Acn.Rdm;
using Acn.Rdm.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public abstract class SdtPdu : AcnPdu
    {

        public SdtPdu()
        {
            Header = new AcnPduHeader((int)ProtocolIds.SDT, 1);
        }

        public SdtPdu(StdVectors vector)
        {
            Header = new AcnPduHeader((int)vector, 1);
        }

        public static SdtPdu Create(AcnPduHeader header)
        {
            return SdtPduFactory.Build(header);
        }
    }
}
