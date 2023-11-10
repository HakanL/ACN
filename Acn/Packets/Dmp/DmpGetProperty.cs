using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Acn.IO;

namespace Acn.Packets.Dmp
{
    [Flags]
    public enum AddressFlags : byte
    {
        Virtual = 0x80,
        Relative = 0x40,
        Type3 = 0x30,
        Type2 = 0x20,
        Type1 = 0x10,
        Reserved = 0xC,
        Size_Unknown3 = 0x3,
        Size_4Octets = 0x2,
        Size_Unknown1 = 0x1,
        Actual__Absolute__Type_NonRangeSingleItem__Size_Unknown0 = 0x00,
    }
    public class DmpGetProperty : DmpPdu
    {
        public DmpGetProperty()
            : base(DmpMessages.GetProperty)
        {
        }

        #region Packet Contents


        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            base.ReadData(data);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            base.WriteData(data);
        }

        #endregion
    }
}
