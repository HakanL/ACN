using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Dmp
{
    public class DmpSubscribeAccept : DmpPdu
    {
        public DmpSubscribeAccept() : base(DmpMessages.SubscribeAccept)
        { }

        public override void ReadData(AcnBinaryReader data)
        {
            base.ReadData(data);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            base.WriteData(data);
        }
    }
}
