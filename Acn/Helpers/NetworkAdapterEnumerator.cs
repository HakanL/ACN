using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Acn.Helpers
{
    public static class NetworkAdapterEnumerator
    {
        public static IEnumerable<IPAddress> GetIpv4AddressFromInterfaceTypes(params NetworkInterfaceType[] interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
            {
                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (adapter.SupportsMulticast && adapter.NetworkInterfaceType == interfaceType &&
                        adapter.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProperties = adapter.GetIPProperties();

                        foreach (var ipAddress in ipProperties.UnicastAddresses)
                        {
                            if (ipAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                yield return ipAddress.Address;
                        }
                    }
                }
            }
        }

        public static IPAddress[] GetAllLocalAddresses()
        {
            var ipAddresses = GetIpv4AddressFromInterfaceTypes(
                NetworkInterfaceType.Ethernet,
                NetworkInterfaceType.Wireless80211);

            return ipAddresses.ToArray();
        }

        public static IPAddress GetFirstBindAddress()
        {
            return GetAllLocalAddresses().First();
        }
    }
}
