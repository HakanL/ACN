using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    public class SdtWrappedHeader
    {
        public SdtWrappedHeader()
        {
            Flags = PduFlags.All;
        }

        public SdtWrappedHeader(ProtocolIds protocol)
        {
            Flags = PduFlags.All;
            ProtocolId = protocol;
        }

        #region Packet Contents

        public PduFlags Flags { get; protected set; }

        private int length = 0;

        public int Length
        {
            get { return length; }
            set
            {
                if (length != value)
                {
                    length = value;

                    if (length > 4096)
                        Flags |= PduFlags.Length;
                    else
                        Flags &= ~PduFlags.Length;
                }
            }
        }

        public ProtocolIds ProtocolId { get; private set; } = ProtocolIds.SDT;

        public short MemberId { get; set; }

        public short Association { get; set; }

        #endregion

        #region Read and Write

        public virtual void ReadData(AcnBinaryReader data)
        {
            //Read PDU Header
            Length = data.ReadOctet2();
            Flags = (PduFlags)((Length & 0xF000) >> 12);
            Length &= 0xFFF;
            MemberId = data.ReadOctet2();
            ProtocolId = (ProtocolIds)data.ReadOctet4();
            Association = data.ReadOctet2();
        }

        long lengthPosition = 0;

        public virtual void WriteData(AcnBinaryWriter data)
        {
            //Save length and skip
            //We save the stream position of the length so we can come back later and write it.
            lengthPosition = data.BaseStream.Position;
            data.BaseStream.Seek(2, System.IO.SeekOrigin.Current);
            //also skipping over flags, as this is embeded in the same bytes

            data.WriteOctet(MemberId);
            data.WriteOctet((int)ProtocolId);
            data.WriteOctet(Association);
        }

        public void WriteLength(AcnBinaryWriter data)
        {
            //Return to Length and update
            long endPosition = data.BaseStream.Position;
            Length = (int)(endPosition - lengthPosition);

            //Write PDU length.
            data.BaseStream.Seek(lengthPosition, System.IO.SeekOrigin.Begin);
            data.WriteOctet((short)((Length & 0x0FFF) + ((int)Flags << 12)));

            //Return to original stream position at end of PDU.
            data.BaseStream.Seek(endPosition, System.IO.SeekOrigin.Begin);
        }

        #endregion
    }
}
