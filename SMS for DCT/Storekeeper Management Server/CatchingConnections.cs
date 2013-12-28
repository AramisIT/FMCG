﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Threading;


namespace StorekeeperManagementServer
    {
    public class CatchingConnections
        {
        private readonly TcpListener TCPServer;
        private readonly List<DataTerminalSession> StorekeeperSessions;
        private readonly PrintingConnectionsInfoDelegate PrintingAddresses;
        private readonly UpdateCompleteDelegate InformAboutUpdateComplete;

        public List<KeyValuePair<Guid, int>> GetPdtSessions()
            {
            var sessions = new List<KeyValuePair<Guid, int>>();

            lock (this)
                {
                foreach (DataTerminalSession session in StorekeeperSessions)
                    {
                    if (session.CurrentUserId > 0)
                        {
                        sessions.Add(new KeyValuePair<Guid, int>(session.SessionId, session.CurrentUserId));
                        }
                    }
                }

            return sessions;
            }

        /// <summary>
        /// Key - ip address, value guid (pdt id)
        /// </summary>
        private readonly Dictionary<string, string> AllowIpList;
        private ArrayList NeedToUpdateIpList;
        private string FullUpdatePath;
        private readonly ReceiveMessage receiveMessage;

        public CatchingConnections(TcpListener MyTCPServer, PrintingConnectionsInfoDelegate MyPrintingDelegate, ArrayList allowIpList, ArrayList NeedToUpdateIpList, UpdateCompleteDelegate InformAboutUpdateComplete, string FullUpdatePath, string Server1CConnectionString, ReceiveMessage receiveMessage)
            {

            TCPServer = MyTCPServer;
            PrintingAddresses = MyPrintingDelegate;
            StorekeeperSessions = new List<DataTerminalSession>();
            this.AllowIpList = new Dictionary<string, string>();
            this.NeedToUpdateIpList = NeedToUpdateIpList;
            this.InformAboutUpdateComplete = InformAboutUpdateComplete;
            this.FullUpdatePath = FullUpdatePath;
            this.receiveMessage = receiveMessage;


            foreach (string allowIPAddress in allowIpList)
                {
                AllowIpList.Add(allowIPAddress, Guid.NewGuid().ToString());
                }
            Console.WriteLine();
            }

        public void Start(object state)
            {
            while (true)
                {
                AcceptConnection();
                }
            }

        public void RefreshUpdateStatusClients(ArrayList NewNeedToUpdateIpList, string fullUpdatePath)
            {
            NeedToUpdateIpList = NewNeedToUpdateIpList;
            foreach (DataTerminalSession SS in StorekeeperSessions)
                {
                SS.NeedToUpdate = NeedToUpdateIpList.IndexOf(SS.GetClientIP()) != -1;
                SS.FullUpdatePath = fullUpdatePath;
                FullUpdatePath = fullUpdatePath;
                }
            }

        public void PingingUpdate(bool IsPing)
            {
            foreach (DataTerminalSession SS in StorekeeperSessions)
                {
                SS.NeedToPing = IsPing;
                }
            }

        private void DeleteStorekeeperSession(DataTerminalSession Session)
            {
            try
                {
                lock (this)
                    {
                    StorekeeperSessions.Remove(Session);
                    PrintingAddresses(StorekeeperSessions);
                    }
                }
            catch { }
            }

        private void AcceptConnection()
            {
            #region Defining variables
            TcpClient NewTCPClient;
            NetworkStream NewTCPStream;
            string newClientIP;
            #endregion

            string guidStr = null;
            #region Getting connection
            try
                {
                // Here this thread will be waiting till anybody connect to him
                NewTCPClient = TCPServer.AcceptTcpClient();

                newClientIP = ((System.Net.IPEndPoint)(NewTCPClient.Client.RemoteEndPoint)).Address.ToString();

                if (!AllowIpList.TryGetValue(newClientIP, out guidStr))
                    {
                    // Запрет подключения левых IP
                    NewTCPClient.Close();
                    Console.WriteLine("Refused connection from not allow IP: " + newClientIP);
                    return;
                    }

                // Getting the network stream                
                NewTCPStream = NewTCPClient.GetStream();

                // Отправка уведомления клиенту
                byte[] HiMessage = new byte[10];
                HiMessage = Encoding.GetEncoding(1251).GetBytes("$M$_$ERVER");
                NewTCPStream.Write(HiMessage, 0, HiMessage.Length);
                lock (this)
                    {
                    for (int i = StorekeeperSessions.Count - 1; i >= 0; i--)
                        {
                        DataTerminalSession WorkerSession = StorekeeperSessions[i];
                        if (WorkerSession.IPAddress == newClientIP)
                            {
                            WorkerSession.CloseChannels();
                            }
                        }
                    }
                }
            catch (Exception exp)
                {
                Console.WriteLine("Error during creating TCP client: " + exp.Message);
                return;
                }
            #endregion

            #region Creating new thread
            // Here this thread has to create the new thread and give to him new connection


            DataTerminalSession NewStorekeeperSession = new DataTerminalSession(NewTCPClient, NewTCPStream, DeleteStorekeeperSession, InformAboutUpdateComplete, receiveMessage, new Guid(guidStr));
            AddSession(NewStorekeeperSession);
            //NewStorekeeperSession.Server1CAgent = OneCConnections[newClientIP];
            NewStorekeeperSession.IPAddress = newClientIP;
            NewStorekeeperSession.NeedToUpdate = NeedToUpdateIpList.IndexOf(newClientIP) != -1;
            NewStorekeeperSession.FullUpdatePath = FullUpdatePath;
            // Caling delegate for drawing information for system administrator
            PrintingAddresses(StorekeeperSessions);

            Thread NewStorekeeperThread = new Thread(NewStorekeeperSession.Start);
            NewStorekeeperSession.StorekeeperThread = NewStorekeeperThread;
            NewStorekeeperThread.Name = AllowIpList[newClientIP];
            NewStorekeeperThread.IsBackground = true;
            NewStorekeeperThread.Start();
            #endregion
            }

        public void PressKeyOnTDC(int key)
            {
            StorekeeperSessions.ForEach(x => x.KeyPress = key);
            }

        public void PressKeyOnTDC(string barcode)
            {
            StorekeeperSessions.ForEach(x => x.SendBarcode = barcode);
            }

        private void AddSession(DataTerminalSession NewSession)
            {
            string SessionIP = NewSession.GetClientIP();
            DataTerminalSession session;
            lock (this)
                {
                StorekeeperSessions.Add(NewSession);
                }
            }
        }
    }
