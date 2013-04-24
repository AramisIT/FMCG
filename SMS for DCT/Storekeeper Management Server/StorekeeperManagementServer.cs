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

    internal class StorekeeperManagementServer : IDisposable
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
        private string UpdateFolderName;
        private ArrayList AllowIpList;
        private ArrayList NeedToUpdateIpList;
        private string FullUpdatePath;
        private readonly Label InfoLabel;
        #endregion

        public StorekeeperManagementServer(PrintingConnectionsInfoDelegate PrintingDelegate, Label InfoLabel,
                                           ReceiveMessage receiveMessage, ArrayList allowedIP, string serverIP,
                                           string updFolder, out bool success)
            {
            success = false;
            NeedToUpdateIpList = new ArrayList();
            this.InfoLabel = InfoLabel;

            if (!SetConfiguration(allowedIP, serverIP, updFolder))
                {
                return;
                }

            #region Creating watcher for update folder

            FileSystemWatcher updateFolderWatcher = new FileSystemWatcher();
            try
                {
                updateFolderWatcher.Path = UpdateFolderName;
                }
            catch (Exception ex)
                {
                MessageBox.Show("Ошибка при создании \"file watcher\":" + ex.Message, "Error during starting SM server",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                }
            updateFolderWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            updateFolderWatcher.Filter = "W*";
            updateFolderWatcher.Created += NewUpdateDetected;
            updateFolderWatcher.EnableRaisingEvents = true;

            #endregion

            #region Creating TCP listener

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

            #endregion

            #region Creating CatchingConnection, Starting server

            try
                {
                ClientConnector = new CatchingConnections(TCPServer, PrintingDelegate, AllowIpList, NeedToUpdateIpList,
                                                          WriteToFileAboutUpdate, FullUpdatePath, StringConnection,
                                                          receiveMessage);
                }
            catch (Exception e)
                {
                Console.WriteLine("Error CatchingConnections:" + e.Message);
                Console.ReadLine();
                Application.Exit();
                return;
                }

            // Creating the new background thread for CatchingConnections
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
                Console.WriteLine("Check accessibility of IP address.");
                Console.ReadLine();
                throw new Exception(string.Format("Error during server starting: {0}.", exp.Message));
                }

            CatchingConnectionThread.Start();

            #endregion

            success = true;
            }

        private bool SetConfiguration(ArrayList allowedIP, string serverIP, string updFolder)
            {
            StringConnection = @"C:\Documents and Settings\D\My Documents\InfoBase3";
            UpdateFolderName = updFolder;
            IpAddress = serverIP;
            AllowIpList = new ArrayList();
            AllowIpList = allowedIP;

            if (UpdateFolderName == null || AllowIpList.Count == 0 || IpAddress == null || StringConnection == null)
                {
                MessageBox.Show(
                    "Не определены обязательные поля (UpdateFolderName, AllowIpList, IpAddress, StringConnection).\r\nСервер не может быть запущен!",
                    "Error during starting SM server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                }

            InfoLabel.Text = String.Format("Start time: {0}     1C Server's parameters: {1}", DateTime.Now.ToString(),
                                           StringConnection);
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

        private void NewUpdateDetected(object source, FileSystemEventArgs e)
            {

            #region Checking of possibility to change file
            bool fileNotAccessible = true;
            while (fileNotAccessible)
                {
                try
                    {
                    FileStream FileForTransmit = File.Open(e.FullPath, FileMode.Open, FileAccess.Read);
                    FileForTransmit.Close();
                    fileNotAccessible = false;
                    }
                catch
                    {
                    Thread.Sleep(1000);
                    }
                }

            #endregion

            Console.WriteLine("NewUpdateDetected(). Thread #{0}", Thread.CurrentThread.GetHashCode());
            Console.WriteLine("File {0} {1}!", e.FullPath, e.ChangeType);
            NeedToUpdateIpList = (ArrayList) AllowIpList.Clone();
            FullUpdatePath = e.FullPath;
            WriteToFileAboutUpdate(null);
            ClientConnector.RefreshUpdateStatusClients(NeedToUpdateIpList, FullUpdatePath);
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

        ~StorekeeperManagementServer()
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