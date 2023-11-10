using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class ChannelParameterBlock
    {
        public byte expiry { get; set; }
        public byte flags { get; set; }
        public short NAK_holdoff { get; set; }
        public short NAK_modulus { get; set; }
        public short NAK_maxWait { get; set; }

        public static ChannelParameterBlock ReadData(AcnBinaryReader data)
        {
            ChannelParameterBlock block = new ChannelParameterBlock();
            block.expiry = data.ReadByte();
            block.flags = data.ReadByte();
            block.NAK_holdoff = data.ReadOctet2();
            block.NAK_modulus = data.ReadOctet2();
            block.NAK_maxWait = data.ReadOctet2();
            return block;
        }

        public static ChannelParameterBlock Build()
        {
            ChannelParameterBlock block = new ChannelParameterBlock();
            block.expiry = 5;
            block.flags = 0;
            block.NAK_holdoff = 2;
            block.NAK_modulus = 50;
            block.NAK_maxWait = 20;
            return block;
        }
 
        public void WriteData(AcnBinaryWriter data)
        {
            data.Write(expiry);
            data.Write(flags);  
            data.WriteOctet(NAK_holdoff);
            data.WriteOctet(NAK_modulus);
            data.WriteOctet(NAK_maxWait);
        }
    }
}
