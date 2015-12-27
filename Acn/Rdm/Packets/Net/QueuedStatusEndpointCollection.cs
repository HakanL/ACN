﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acn.Rdm.Packets.Status;

namespace Acn.Rdm.Packets.Net
{
    public class QueuedStatusEndpointCollection
    {
        public class Set : RdmRequestPacket
        {
            public Set()
                : base(RdmCommands.Set, RdmParameters.QueuedStatusEndpointCollection)
            {
            }

            public short EndpointID { get; set; }

            public StatusTypes StatusType { get; set; }

            protected override void ReadData(RdmBinaryReader data)
            {
                EndpointID = data.ReadNetwork16();
                StatusType = (StatusTypes) data.ReadByte();
            }

            protected override void WriteData(RdmBinaryWriter data)
            {
                data.WriteNetwork(EndpointID);
                data.Write((byte) StatusType);
            }
        }

        public class SetReply : RdmResponsePacket
        {
            public SetReply()
                : base(RdmCommands.SetResponse, RdmParameters.QueuedStatusEndpointCollection)
            {
            }

            protected override void ReadData(RdmBinaryReader data)
            {
                //Parameter Data Empty
            }

            protected override void WriteData(RdmBinaryWriter data)
            {
                //Parameter Data Empty
            }
        }
    }
}
