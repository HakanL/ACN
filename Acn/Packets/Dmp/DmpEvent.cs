using Acn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Dmp
{
    public class DmpEvent : DmpPdu
    {
        public DmpEvent() : base(DmpMessages.Event, 1)
        { }

        #region Packet Contents

        public byte[] Data { get; set; } = new byte[] { 0 };

        #endregion

        public override void ReadData(AcnBinaryReader data)
        {
            base.ReadData(data);
            Data = data.ReadBytes(base.Header.Length - 6); //kind of... TODO: actually work this out programatically, as it depends on flags
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            base.WriteData(data);
            data.Write(Data);
        }
    }
}
