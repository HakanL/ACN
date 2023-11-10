using Acn.IO;
using Acn.Rdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Acn.Packets.Sdt
{
    internal class SdtPduFactory
    {

        static SdtPduFactory()
        {
            RegisterPacketType(SdtVectors.Ack, typeof(SdtAck));
            RegisterPacketType(SdtVectors.ChannelParameters, typeof(SdtChannelParams));
            RegisterPacketType(SdtVectors.Connect, typeof(SdtConnect));
            RegisterPacketType(SdtVectors.ConnectAccept, typeof(SdtConnectAccept));
            RegisterPacketType(SdtVectors.ConnectRefuse, typeof(SdtConnectRefuse));
            RegisterPacketType(SdtVectors.Disconnect, typeof(SdtDisconnect));
            RegisterPacketType(SdtVectors.Disconnecting, typeof(SdtDisconnecting));
            RegisterPacketType(SdtVectors.GetSessions, typeof(SdtGetSessions));
            RegisterPacketType(SdtVectors.Join, typeof(SdtJoin));
            RegisterPacketType(SdtVectors.JoinAccept, typeof(SdtJoinAccept));
            RegisterPacketType(SdtVectors.JoinRefuse, typeof(SdtJoinRefuse));
            RegisterPacketType(SdtVectors.Leave, typeof(SdtLeave));
            RegisterPacketType(SdtVectors.Leaving, typeof(SdtLeaving));
            RegisterPacketType(SdtVectors.Nak, typeof(SdtNak));
            RegisterPacketType(SdtVectors.ReliableWrapper, typeof(SdtReliableWrapper));
            RegisterPacketType(SdtVectors.Sessions, typeof(SdtSessions));
            RegisterPacketType(SdtVectors.UnreliableWrapper, typeof(SdtUnreliableWrapper));
        }

        private static Dictionary<SdtVectors, Type> packetStore = new Dictionary<SdtVectors, Type>();

        public static void RegisterPacketType(SdtVectors vector, Type packetType)
        {

            if (packetStore.ContainsKey(vector))
                throw new InvalidOperationException(string.Format("The packet is already registered for {1}.", vector.ToString()));

            packetStore[vector] = packetType;
        }

        public static SdtPdu Build(AcnPduHeader header)
        {
            Type packetType;
            if (packetStore.TryGetValue((SdtVectors)header.Vector, out packetType))
            {
                SdtPdu pdu = (SdtPdu)Activator.CreateInstance(packetType);
                return pdu;
            }
            return null;
        }
    }
}
