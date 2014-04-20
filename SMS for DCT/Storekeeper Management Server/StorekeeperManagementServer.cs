using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace StorekeeperManagementServer
    {
    public delegate void PrintingConnectionsInfoDelegate(List<DataTerminalSession> StorekeepersList);

    public delegate void UpdateCompleteDelegate(string IpAddress);

    public class AramisTCPServer : IDisposable
        {
        #region Fields
        private const int SERVER_PORT = 8609;
        private string IpAddress;
        private string StringConnection;
        private readonly string SETTINGS_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) +
                                                     @"\Aramis Warehouse Server";
        private bool disposed;
        private readonly Thread CatchingConnectionThread;
        private readonly CatchingConnections ClientConnector;
        private readonly TcpListener TCPServer;

        private ArrayList AllowIpList;
        private ArrayList NeedToUpdateIpList;
        private string FullUpdatePath;
        private volatile bool serverActive;
        #endregion

        public CatchingConnections CatchingConnection
            {
            get { return ClientConnector; }
            }

        public bool ServerActive
            {
            get { return serverActive; }
            }

        public AramisTCPServer(ReceiveMessage receiveMessage, ArrayList allowedIP, string serverIP)
            {
            serverActive = false;
            NeedToUpdateIpList = new ArrayList();

            if (!SetConfiguration(allowedIP, serverIP))
                {
                return;
                }

            try
                {
                Console.WriteLine("\n\t***\tTCP Server [ {0} ] is ready.\n\n\n", IpAddress);
                TCPServer = new TcpListener(IPAddress.Parse(IpAddress), SERVER_PORT);

                }
            catch (Exception exp)
                {
                Console.WriteLine("Error during server creating: {0}.", exp.Message);
                Console.ReadLine();
                Application.Exit();
                return;
                }

            #region Creating CatchingConnection, Starting server

            try
                {
                ClientConnector = new CatchingConnections(TCPServer, AllowIpList, NeedToUpdateIpList,
                                                          WriteToFileAboutUpdate, FullUpdatePath, StringConnection,
                                                          receiveMessage);

                }
            catch (Exception e)
                {
                Console.WriteLine("Error CatchingConnections:" + e.Message);
                return;
                }

            CatchingConnectionThread = new Thread(ClientConnector.Start);
            CatchingConnectionThread.Name = "CatchingConnectionsThread";
            CatchingConnectionThread.IsBackground = true;

            try
                {
                TCPServer.Start();
                }
            catch (Exception exp)
                {
                Console.WriteLine("Error during server starting: {0}.", exp.Message);
                return;
                }

            CatchingConnectionThread.Start();

            #endregion

            serverActive = true;
            }

        private bool SetConfiguration(ArrayList allowedIP, string serverIP)
            {
            StringConnection = @"C:\Documents and Settings\D\My Documents\InfoBase3";

            IpAddress = serverIP;
            AllowIpList = new ArrayList();
            AllowIpList = allowedIP;

            if (AllowIpList.Count == 0 || IpAddress == null || StringConnection == null)
                {
                MessageBox.Show(
                    "Не определены обязательные поля (UpdateFolderName, AllowIpList, IpAddress, StringConnection).\r\nСервер не может быть запущен!",
                    "Error during starting SM server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                }


            string UpdateFileName = SETTINGS_FILE_PATH + @"\update_info.txt";
            if (File.Exists(UpdateFileName))
                {
                StreamReader readerUpdateInfo = File.OpenText(UpdateFileName);

                FullUpdatePath = readerUpdateInfo.ReadLine();
                FullUpdatePath = FullUpdatePath == null ? "" : FullUpdatePath.Trim();

                if (FullUpdatePath == "")
                    {
                    readerUpdateInfo.Close();
                    return true;
                    }

                string input; // now input equal null       
                while ((input = readerUpdateInfo.ReadLine()) != null)
                    {
                    NeedToUpdateIpList.Add(input.Trim());
                    }

                readerUpdateInfo.Close();
                }

            return true;
            }



        public void PressKeyOnTDC(int key)
            {
            ClientConnector.PressKeyOnTDC(key);
            }

        public void PressKeyOnTDC(string barcode)
            {
            ClientConnector.PressKeyOnTDC(barcode);
            }

        public void WriteToFileAboutUpdate(string IpAddress)
            {
            lock (this)
                {
                Console.WriteLine("Inside WriteToFileAboutUpdate(\"{1}\"). Thread #{0}",
                                  Thread.CurrentThread.GetHashCode(), IpAddress);
                StreamWriter writer;
                if (IpAddress != null)
                    {
                    if (NeedToUpdateIpList.IndexOf(IpAddress) == -1)
                        {
                        if (IpAddress == "$FileDeleted$")
                            {
                            writer = File.CreateText(SETTINGS_FILE_PATH + @"\update_info.txt");
                            writer.Close();
                            NeedToUpdateIpList.Clear();
                            ClientConnector.RefreshUpdateStatusClients(NeedToUpdateIpList, "");
                            }
                        return;
                        }
                    NeedToUpdateIpList.Remove(IpAddress);
                    }

                try
                    {
                    writer = File.CreateText(SETTINGS_FILE_PATH + @"\update_info.txt");
                    writer.WriteLine(FullUpdatePath);
                    foreach (string ipAddress in NeedToUpdateIpList)
                        {
                        writer.WriteLine(ipAddress);
                        }

                    writer.Close();
                    }
                catch (Exception e)
                    {
                    Console.WriteLine(e.Message);
                    }

                // write to file about update !
                }
            }

        #region Финализация объекта

        public void Dispose()
            {
            terminateThis(true);
            GC.SuppressFinalize(this);
            }

        private void terminateThis(bool disposing)
            {
            if (!disposed)
                {
                // если disposing == false, вызов осуществляется сборщиком мусора
                if (disposing)
                    {
                    try
                        {
                        // Здесь нужно отправить сообщения клиентам, что сервер останавливается, остановить все соединения,
                        // но это делать не обязательно, т.к. уже предусмотрена неустойчивая связь

                        if (TCPServer != null) TCPServer.Stop();
                        }
                    catch (Exception exp)
                        {
                        Console.WriteLine("Error: " + exp.Message);
                        }
                    }
                }
            disposed = true;
            }

        ~AramisTCPServer()
            {
            terminateThis(false);
            }

        public void PingingUpdate(bool NeedToPing)
            {
            ClientConnector.PingingUpdate(NeedToPing);
            }

        #endregion

        }
    }