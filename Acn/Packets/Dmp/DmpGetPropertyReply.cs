using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;

namespace Acn.Packets.Dmp
{
    public class DmpGetPropertyReply : DmpPdu
    {
        public DmpGetPropertyReply()
            : base(DmpMessages.GetPropertyReply)
        {
        }

        #region Packet Contents

        public byte[] Data { get; set; } = new byte[] { 0 };

        #endregion

        #region Read/Write

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

        #endregion
    }
}
