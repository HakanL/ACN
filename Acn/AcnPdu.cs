using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn
{
    public abstract class AcnPdu
    {
        internal AcnRootLayer rootLayer { get; set; }

        public AcnPduHeader Header;

        public abstract void ReadData(AcnBinaryReader data);

        public abstract void WriteData(AcnBinaryWriter data);

       
    }
}
