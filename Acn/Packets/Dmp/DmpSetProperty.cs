using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.IO;

namespace Acn.Packets.Dmp
{
    public class DmpSetProperty : DmpPdu
    {
        public DmpSetProperty()
            : base(DmpMessages.SetProperty)
        {
        }

        #region Packet Contents

        public short AddressIncrement { get; set; } = 0;

        public byte[] Data { get; set; } = null;

        #endregion

        #region Read/Write

        public override void ReadData(AcnBinaryReader data)
        {
            base.ReadData(data);
            AddressIncrement = data.ReadOctet2();
            int propertyLength = data.ReadOctet2();
            Data = data.ReadBytes(propertyLength);
        }

        public override void WriteData(AcnBinaryWriter data)
        {
            base.WriteData(data);
            data.WriteOctet(AddressIncrement);
            data.WriteOctet(Data.Length);
            WriteContent(data);
        }

        /// <summary>
        /// Writes the Property data to the packet stream.
        /// </summary>
        /// <remarks>
        /// This allows customization of the property data.
        /// </remarks>
        /// <param name="data">The packet data stream.</param>
        protected virtual void WriteContent(AcnBinaryWriter data)
        {
            data.Write(Data);
        }

        #endregion
    }
}
