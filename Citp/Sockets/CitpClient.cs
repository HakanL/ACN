using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Citp.Packets;
using System.IO;
using Citp.Packets.Msex;
using Citp.Sockets;
using System.Net;
using Citp.IO;

namespace Citp.Sockets
{
    public class CitpClient:IDisposable
    {
        private TcpClient client = null;

        public event UnhandledExceptionEventHandler UnhandledException;
        public event EventHandler<CitpNewPacketEventArgs> NewPacket;
        public event EventHandler Disconnected;

        public CitpClient(TcpClient client)
        {
            this.client = client;            
        }

        public virtual void Start()
        {
            StartReceive(new CitpReceiveData());
        }

        protected void RaiseDisconnected()
        {
            if (Disconnected != null)
                Disconnected(this, EventArgs.Empty);
        }

        protected void StartReceive(CitpReceiveData receiveState)
        {
            try
            {
                receiveState.SetLength(receiveState.ReadPosition + receiveState.ReadNibble);
                client.Client.BeginReceive(receiveState.GetBuffer(), receiveState.ReadPosition, receiveState.ReadNibble, SocketFlags.None, new AsyncCallback(OnReceive), receiveState);
            }
            catch (Exception ex)
            {
                OnUnhandledException(new ApplicationException("An error ocurred while trying to start recieving CITP.", ex));
            }
        }

        private void OnReceive(IAsyncResult state)
        {
            CitpPacket newPacket;
            bool restartReceive = false;

            CitpReceiveData receiveState = (CitpReceiveData)(state.AsyncState);

            try
            {
                if (receiveState != null && client != null && client.Connected)
                {
                    receiveState.SetLength((receiveState.Length - receiveState.ReadNibble) + client.Client.EndReceive(state));

                    if (receiveState.Length > 0 && !IsDisposed())
                    {
                        //We want to start the receive again to listen for more data.
                        //Only do this when the client is in a position to do so.
                        restartReceive = true;

                        if (NewPacket != null)
                        {
                            while (CitpPacketBuilder.TryBuild(receiveState, out newPacket))
                            {
                                receiveState.ReadPosition += (int)((CitpHeader)newPacket).MessageSize;

                                //Packet has been read successfully.
                                NewPacket(this, new CitpNewPacketEventArgs((IPEndPoint)client.Client.LocalEndPoint, (IPEndPoint) client.Client.RemoteEndPoint, newPacket));
                            }
                        }
                    }
                }
            }
            catch (SocketException)
            {
                //Connect has been closed.
            }
            catch (Exception ex)
            {
                OnUnhandledException(ex);
            }
            finally
            {
                //Attempt to receive another packet.
                if (restartReceive)
                    StartReceive(receiveState);
                else
                    RaiseDisconnected();                   
            }
        }

        public void BeginSend(CitpPacket citpMessage)
        {
            MemoryStream data = new MemoryStream();
            CitpBinaryWriter writer = new CitpBinaryWriter(data);

            citpMessage.WriteData(writer);
            ((CitpHeader)citpMessage).WriteMessageSize(writer);

            client.Client.BeginSend(data.GetBuffer(), 0, (int) data.Length, SocketFlags.None, new AsyncCallback(OnSendCompleted), null);
        }

        private void OnSendCompleted(IAsyncResult result)
        {
        }
           
        protected void OnUnhandledException(Exception ex)
        {
            if (UnhandledException != null) UnhandledException(this, new UnhandledExceptionEventArgs((object)ex, false));
        }

        private bool disposed = false;

        public bool IsDisposed()
        {
            return disposed;
        }

        public virtual void Dispose()
        {
            disposed = true;
            if (client != null)
            {
                client.Close();
                client = null;
            }            
        }
    }
}
