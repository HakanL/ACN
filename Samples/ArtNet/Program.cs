using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acn.Sockets;
using Acn.ArtNet;
using Acn.ArtNet.Sockets;
using Acn.ArtNet.Packets;

namespace ArtNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var artnet = new ArtNetSocket();
            artnet.EnableBroadcast = true;

            artnet.Open(IPAddress.Any, IPAddress.Broadcast);

            var dmxDataPerUniverse = new Dictionary<short, byte[]>();

            artnet.NewPacket += (object sender, NewPacketEventArgs<ArtNetPacket> e) =>
            {
                if (e.Packet.OpCode == ArtNetOpCodes.Dmx)
                {
                    var packet = e.Packet as ArtNetDmxPacket;

                    byte[] dmxData;
                    if (!dmxDataPerUniverse.TryGetValue(packet.Universe, out dmxData))
                    {
                        dmxData = new byte[512];
                        dmxDataPerUniverse.Add(packet.Universe, dmxData);
                    }

                    if (!packet.DmxData.SequenceEqual(dmxData))
                    {
                        Console.WriteLine("New DMX data for universe {0}", packet.Universe);

                        for (int y = 0; y < 16; y++)
                        {
                            var sb = new StringBuilder();
                            for (int x = 0; x < 16; x++)
                            {
                                int pos = y * 16 + x;

                                if (sb.Length != 0)
                                    sb.Append(' ');

                                sb.AppendFormat("{0:X2}", packet.DmxData[pos]);
                            }

                            Console.WriteLine("{0:X2} - {1}", y * 16, sb.ToString());
                        }

                        Console.WriteLine();

                        if (dmxData.Length < packet.DmxData.Length)
                        {
                            dmxData = new byte[packet.DmxData.Length];
                            dmxDataPerUniverse[packet.Universe] = dmxData;
                        }

                        Array.Copy(packet.DmxData, dmxData, dmxData.Length);
                    }
                }
            };

            Console.ReadLine();

        }
    }
}
