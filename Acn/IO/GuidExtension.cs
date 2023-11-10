using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acn.IO
{
    public static class GuidExtension
    {
        //[__DynamicallyInvokable]
        public static byte[] ToNetworkByteArray(this Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            return new byte[16]
            {
                bytes[3],
                bytes[2],
                bytes[1],
                bytes[0],
                bytes[5],
                bytes[4],
                bytes[7],
                bytes[6],
                bytes[8],
                bytes[9],
                bytes[10],
                bytes[11],
                bytes[12],
                bytes[13],
                bytes[14],
                bytes[15]
            };
        }


    }

    public class NetworkGuid
    {
        public static System.Guid FromPacket(byte[] b)
        {

            if (b == null)
            {
                throw new ArgumentNullException("b");
            }

            if (b.Length != 16)
            {
                throw new ArgumentException("invalid length");
            }

            byte[] o = new byte[16]
              {
                b[3],
                b[2],
                b[1],
                b[0],
                b[5],
                b[4],
                b[7],
                b[6],
                b[8],
                b[9],
                b[10],
                b[11],
                b[12],
                b[13],
                b[14],
                b[15]
              };

            return new System.Guid(o);

        }
    }

}