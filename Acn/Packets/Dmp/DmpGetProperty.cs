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
        Type_RangeMixedMembers = 0x30,
        Type_RangeEqualMembers = 0x20,
        Type_RangeSingle = 0x10,
        Reserved = 0xC,
        Size_Reserved = 0x3,
        Size_4Octets = 0x2,
        Size_2Octets = 0x1,
        Actual__Absolute__Type_NonRangeSingle__Size_1Octet = 0x00,
        Actual = 0x00,
        Absolute = 0x00,
        Size_1Octet = 0x00
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
