﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Acn.IO;

namespace Acn
{
    public abstract class AcnPacket
    {
        public AcnPacket(int protocolId)
        {
            Root = new AcnRootLayer();
            Root.ProtocolId = protocolId;
        }

        public AcnPacket(ProtocolIds protocolId)
            : this((int)protocolId)
        {
        }


        #region Packet Contents

        public AcnRootLayer Root { get; set; }

        #endregion

        #region Read and Write

        protected abstract void ReadData(AcnBinaryReader data);

        protected abstract void WriteData(AcnBinaryWriter data);

        public static AcnPacket ReadPacket(AcnBinaryReader data)
        {
            AcnRootLayer rootLayer = new AcnRootLayer();
            rootLayer.ReadData(data,false);

            return ReadPacket(rootLayer, data); ;
        }

        public static AcnPacket ReadTcpPacket(AcnBinaryReader data)
        {
            AcnRootLayer rootLayer = new AcnRootLayer();
            rootLayer.ReadData(data, true);

            return ReadPacket(rootLayer, data); ;
        }

        public static AcnPacket ReadPacket(AcnRootLayer header, AcnBinaryReader data)
        {
            AcnPacket packet = AcnPacket.Create(header);
            if (packet != null)
            {
                packet.Root = header;
                packet.ReadData(data);
            }

            return packet;
        }

        public static void WritePacket(AcnPacket packet, AcnBinaryWriter data)
        {
            packet.Root.WriteData(data,false);
            packet.WriteData(data);
            packet.Root.WriteLength(data);
        }

        public static void WriteTcpPacket(AcnPacket packet, AcnBinaryWriter data)
        {
            packet.Root.WriteData(data, true);
            packet.WriteData(data);
            packet.Root.WriteLength(data);
        }

        #endregion

        public static AcnPacket Create(AcnRootLayer header)
        {
            return AcnPacketFactory.Build(header);
        }

        public static AcnPacket Create(AcnRootLayer rootLayer, Type packetType)
        {
            AcnPacket packet = (AcnPacket)Activator.CreateInstance(packetType);
            packet.Root = rootLayer;
            return packet;
        }
        
    }
}
