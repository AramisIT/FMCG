﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Threading;


namespace StorekeeperManagementServer
{
    class CatchingConnections
    {
        private readonly TcpListener TCPServer;
        private readonly SortedDictionary<String, object> OneCConnections;
        private readonly List<DataTerminalSession> StorekeeperSessions;
        private readonly PrintingConnectionsInfoDelegate PrintingAddresses;
        private readonly UpdateCompleteDelegate InformAboutUpdateComplete;
        private readonly ArrayList AllowIpList;
        private ArrayList NeedToUpdateIpList;
        private string FullUpdatePath;
        private readonly ReceiveMessage receiveMessage;

        public CatchingConnections(TcpListener MyTCPServer, PrintingConnectionsInfoDelegate MyPrintingDelegate, ArrayList AllowIpList, ArrayList NeedToUpdateIpList, UpdateCompleteDelegate InformAboutUpdateComplete, string FullUpdatePath, string Server1CConnectionString, ReceiveMessage receiveMessage)
        {

            TCPServer = MyTCPServer;
            PrintingAddresses = MyPrintingDelegate;
            StorekeeperSessions = new List<DataTerminalSession>();
            this.AllowIpList = AllowIpList;
            this.NeedToUpdateIpList = NeedToUpdateIpList;
            this.InformAboutUpdateComplete = InformAboutUpdateComplete;
            this.FullUpdatePath = FullUpdatePath;
            this.receiveMessage = receiveMessage;

            OneCConnections = new SortedDictionary<string, object>();

            foreach (string AllowIPAddress in AllowIpList)
            {
                if (!OneCConnections.ContainsKey(AllowIPAddress))
                {
                    Console.Write("Creating 1C connection for IP: {0} {1}", AllowIPAddress," - complated !");
                    OneCConnections.Add(AllowIPAddress, null);
                }
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
            catch {}
        }

        private void AcceptConnection()
        {
            #region Defining variables
            TcpClient NewTCPClient;
            NetworkStream NewTCPStream;
            string newClientIP;
            #endregion

            #region Getting connection
            try
            {
                // Here this thread will be waiting till anybody connect to him
                NewTCPClient = TCPServer.AcceptTcpClient();

                newClientIP = ((System.Net.IPEndPoint)(NewTCPClient.Client.RemoteEndPoint)).Address.ToString();


                // Запрет подключения левых IP
                if (AllowIpList.IndexOf(newClientIP) == -1)
                {
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

                for (int i = StorekeeperSessions.Count - 1; i >= 0; i--)
                {
                    DataTerminalSession WorkerSession = StorekeeperSessions[i];
                    if (WorkerSession.IPAddress == newClientIP)
                    {
                        WorkerSession.CloseChannels();
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


            DataTerminalSession NewStorekeeperSession = new DataTerminalSession(NewTCPClient, NewTCPStream, DeleteStorekeeperSession, InformAboutUpdateComplete, receiveMessage);
            AddSession(NewStorekeeperSession);
            //NewStorekeeperSession.Server1CAgent = OneCConnections[newClientIP];
            NewStorekeeperSession.IPAddress = newClientIP;
            NewStorekeeperSession.NeedToUpdate = NeedToUpdateIpList.IndexOf(newClientIP) != -1;
            NewStorekeeperSession.FullUpdatePath = FullUpdatePath;
            // Caling delegate for drawing information for system administrator
            PrintingAddresses(StorekeeperSessions);

            Thread NewStorekeeperThread = new Thread(NewStorekeeperSession.Start);
            NewStorekeeperSession.StorekeeperThread = NewStorekeeperThread;
            NewStorekeeperThread.Name = String.Format("StorekeeperThread{0}", StorekeeperSessions.Count);
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
                for (int i = 0; i < StorekeeperSessions.Count; i++)
                {
                    //do
                    //{
                    //    session = StorekeeperSessions[i];
                    //    if (session.GetClientIP() == SessionIP)
                    //    {
                    //        DeleteStorekeeperSession(session);
                    //    }
                    //} while (i < StorekeeperSessions.Count && StorekeeperSessions[i].GetClientIP() == SessionIP);
                    // this "do while" cycle ensuring that we won't skip any session 
                    // (after caling DeleteStorekeeperSession "StorekeeperSessions[i]" point for next element)
                }
            }
            StorekeeperSessions.Add(NewSession);
        }
    }
}
