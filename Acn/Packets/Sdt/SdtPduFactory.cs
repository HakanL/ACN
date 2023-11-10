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
            RegisterPacketType(StdVectors.Ack, typeof(StdAck));
            RegisterPacketType(StdVectors.ChannelParameters, typeof(StdChannelParams));
            RegisterPacketType(StdVectors.Connect, typeof(StdConnect));
            RegisterPacketType(StdVectors.ConnectAccept, typeof(StdConnectAccept));
            RegisterPacketType(StdVectors.ConnectRefuse, typeof(StdConnectRefuse));
            RegisterPacketType(StdVectors.Disconnect, typeof(StdDisconnect));
            RegisterPacketType(StdVectors.Disconnecting, typeof(StdDisconnecting));
            RegisterPacketType(StdVectors.GetSessions, typeof(StdGetSessions));
            RegisterPacketType(StdVectors.Join, typeof(StdJoin));
            RegisterPacketType(StdVectors.JoinAccept, typeof(StdJoinAccept));
            RegisterPacketType(StdVectors.JoinRefuse, typeof(StdJoinRefuse));
            RegisterPacketType(StdVectors.Leave, typeof(StdLeave));
            RegisterPacketType(StdVectors.Leaving, typeof(StdLeaving));
            RegisterPacketType(StdVectors.Nak, typeof(StdNak));
            RegisterPacketType(StdVectors.ReliableWrapper, typeof(StdReliableWrapper));
            RegisterPacketType(StdVectors.Sessions, typeof(StdSessions));
            RegisterPacketType(StdVectors.UnreliableWrapper, typeof(StdUnreliableWrapper));
        }

        private static Dictionary<StdVectors, Type> packetStore = new Dictionary<StdVectors, Type>();

        public static void RegisterPacketType(StdVectors vector, Type packetType)
        {

            if (packetStore.ContainsKey(vector))
                throw new InvalidOperationException(string.Format("The packet is already registered for {1}.", vector.ToString()));

            packetStore[vector] = packetType;
        }

        public static SdtPdu Build(AcnPduHeader header)
        {
            Type packetType;
            if (packetStore.TryGetValue((StdVectors)header.Vector, out packetType))
            {
                SdtPdu pdu = (SdtPdu)Activator.CreateInstance(packetType);
                return pdu;
            }
            return null;
        }
    }
}
