using Acn.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Dmp
{
    public abstract class DmpPdu : AcnPdu
    {
        public DmpPdu()
        {
            Header = new AcnPduHeader((int)ProtocolIds.DMP);
        }

        public DmpPdu(DmpMessages vector)
        {
            Header = new AcnPduHeader((int)vector);
        }

        public DmpPdu(DmpMessages vector, int vectorLength)
        {
            Header = new AcnPduHeader((int)vector, vectorLength);
        }

        public AddressFlags AddressType { get; set; } = 0;

        public int ActualAddress { get; set; } = 0;

        public override void ReadData(AcnBinaryReader data)
        {
            if (Header.Flags.HasFlag(PduFlags.Header))
                AddressType = (AddressFlags)data.ReadByte();
            if (AddressType.HasFlag(AddressFlags.Size_4Octets))
                ActualAddress = data.ReadOctet4();
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            if (Header.Flags.HasFlag(PduFlags.Header))
                data.Write((byte)AddressType);
            if (AddressType.HasFlag(AddressFlags.Size_4Octets))
                data.WriteOctet((int)ActualAddress);
        }

        public static DmpPdu Create(AcnPduHeader header)
        {
            switch((DmpMessages)header.Vector)
            {
                case DmpMessages.GetProperty: return (DmpPdu)Activator.CreateInstance(typeof(DmpGetProperty)); 
                case DmpMessages.Subscribe: return (DmpPdu)Activator.CreateInstance(typeof(DmpSubscribe));
                case DmpMessages.SubscribeAccept: return (DmpPdu)Activator.CreateInstance(typeof(DmpSubscribeAccept));
                case DmpMessages.Event: return (DmpPdu)Activator.CreateInstance(typeof(DmpEvent));
                default:
                    throw new NotImplementedException();
            }
            return null;
            //return DmpPduFactory.Build(header);
        }
    }
}
