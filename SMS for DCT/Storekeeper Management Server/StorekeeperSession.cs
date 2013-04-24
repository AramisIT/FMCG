﻿﻿using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;
using WMS_client;

namespace StorekeeperManagementServer
{
    public delegate void CloseConnectionDelegate(DataTerminalSession storekeeperSession);
    public delegate object[] ReceiveMessage(string procedure, object[] parameters);

    public class DataTerminalSession
    {
        private readonly ReceiveMessage receiveMessage;
        #region Private Fields

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public bool NeedToPing = false;
        private readonly TcpClient TCPClient;
        private readonly NetworkStream TCPStream;
        private readonly CloseConnectionDelegate DeleteConnection;
        private readonly UpdateCompleteDelegate InformAboutUpdateComplete;
        private readonly long lastTimePingSent = DateTime.Now.Ticks;
        public Thread StorekeeperThread;
        private const string ClientCode = "";
        private bool PingSent;
        private bool NewConnection = true;

        #endregion

        #region Public Fields

        public bool NeedToUpdate;
        public string FullUpdatePath;
        public string IPAddress;
        public int KeyPress;
        public string SendBarcode;

        #endregion

        #region Public methods

        public DataTerminalSession(TcpClient MyTCPClient, NetworkStream MyTCPStream, CloseConnectionDelegate MyCloseConnectionMethod, UpdateCompleteDelegate InformAboutUpdateComplete, ReceiveMessage receiveMessage)
        {
            TCPClient = MyTCPClient;
            TCPStream = MyTCPStream;
            DeleteConnection = MyCloseConnectionMethod;
            this.receiveMessage = receiveMessage;


            //Server1CAgent = new HandlingVia1CServer(@"Srvr=""localhost""; Ref=""newwms"";");
            //Server1CAgent = new HandlingVia1CServer(@"Srvr=""localhost""; Ref=""iboya81""; Usr=""Оборский Д.В.""; Pwd=""123456""");
            //Server1CAgent = new HandlingVia1CServer(@"File=""C:\Documents and Settings\Reshifa\My Documents\InfoBase9""; Usr=""Denis.V.O""; Pwd=""123""");
            this.InformAboutUpdateComplete = InformAboutUpdateComplete;
        }

        public void Start(object state)
        {
            Console.Write("Устройство '{0}' подключено!", IPAddress);

            #region Defining variables

            string StorekeeperQueryHead = "";
            
            #endregion

            while (true)
            {
                #region Checking topicality of connection, closing the thread if connection dead

                if (!TCPClient.Connected)
                {
                    CloseChannels();

                    return;
                }

                #endregion

                #region Reading storekeeper query from network stream

                string StorekeeperQuery;

                if (PackageViaWireless.isCompletelyPackage(StorekeeperQueryHead))
                {
                    StorekeeperQuery = StorekeeperQueryHead;
                }
                else
                {
                    StorekeeperQuery = ReadStream(StorekeeperQueryHead);
                    
                    if (StorekeeperQuery == null)
                    {
                        continue;
                    }
                }
                #endregion

                PackageViaWireless package = new PackageViaWireless();
                if (!package.SetPackage(StorekeeperQuery, out StorekeeperQueryHead))
                {
                    // We have recived package with wrong data format or uncompletely package
                    continue;
                }

                object[] ResultParameters = PackageConvertation.GetPatametersFromStr(package.Parameters);
                object[] resultArray = receiveMessage(package.QueryName, ResultParameters);

                string resultStr = PackageConvertation.GetStrPatametersFromArray(resultArray);
                package.DefineQueryAndParams("Answer", resultStr);
                WritePackage(package);
                //#region Handling via 1C server

                //bool registration = false;
                //PackageViaWireless package = new PackageViaWireless();
                //if (!package.SetPackage(StorekeeperQuery, out StorekeeperQueryHead))
                //{
                //    // We have recived package with wrong data format or uncompletely package
                //    continue;
                //}

                //if (package.QueryName == "PingReply")
                //{
                //    double delay = ((Double)(DateTime.Now.Ticks - Convert.ToInt64(PackageConvertation.GetPatametersFromStr(package.Parameters)[0]))) / 10000000;
                //    package.Parameters = "3" + System.Math.Round(delay, 2).ToString();
                //    WriteStream(package.GetPackage());
                //    lastTimePingSent = DateTime.Now.Ticks;
                //    PingSent = false;
                //    continue;
                //}

                //#region Playing
                //if (package.QueryName == "PageDown")
                //{
                //    KeyEmulation.PressKey(System.Windows.Forms.Keys.PageDown);
                //    continue;
                //}

                //if (package.QueryName == "PageUp")
                //{
                //    KeyEmulation.PressKey(System.Windows.Forms.Keys.PageUp);
                //    continue;
                //}

                //if (package.QueryName == "MouseMove")
                //{
                //    object[] XYParameters = PackageConvertation.GetPatametersFromStr(package.Parameters);
                //    System.Drawing.Point P = System.Windows.Forms.Cursor.Position;
                //    P.X += 3 * Convert.ToInt32(XYParameters[0]);
                //    P.Y += 3 * Convert.ToInt32(XYParameters[1]);
                //    System.Windows.Forms.Cursor.Position = P;
                //    continue;
                //}

                //if (package.QueryName == "DoMouseClick")
                //{
                //    DoMouseClick();
                //    continue;
                //}



                //#endregion

                //if (Updating)
                //{
                //    if (package.QueryName == "FileAccepted")
                //    {
                //        // Файл принят успешно                        
                //        InformAboutUpdateComplete(GetClientIP());
                //        Updating = false;
                //        Console.WriteLine(String.Format("Update has recived by machine: [{0}] ", IPAddress));
                //        continue;
                //    }
                //}

                //// Console.WriteLine(String.Format("id: {0}\t{1}", package.PackageID, DateTime.Now.ToString()));
                //Trace1C.WriteParameters(package.ClientCode.ToString(), package.QueryName, package.Parameters.Replace("\r", "^"));
                //if (package.QueryName == "ConnectionRecovery")
                //{
                //    ClientCode = Convert.ToString(package.ClientCode);
                //    continue;
                //}

                //if (package.QueryName == "BreakConnection")
                //{
                //    Console.WriteLine(String.Format("Machine [{0}] disconnected", IPAddress));
                //    CloseChannels();
                //    return;
                //}

                //if (package.QueryName == "Message")
                //{
                //    int MessageId = Convert.ToInt32(package.Parameters);
                //    Server1CAgent.QueryTo1CServer(ClientCode, "SetMessageStatus", "2" + MessageId.ToString() + "\r22");
                //    continue;

                //registration = package.QueryName == "Registration";
                //string Result = Server1CAgent.QueryTo1CServer(package.ClientCode.ToString(), package.QueryName, package.Parameters);
                //if (result == null)
                //{
                //    package.DefineQueryAndParams("Answer", "#ERROR:1C_AGENT_DISABLE#");
                //    WriteStream(package.GetPackage());
                //    continue;
                //}

                //if (package.QueryName == "UserGetOut")
                //{
                //    continue;
                //}

                //if (package.QueryName == "PrintLabel" || package.QueryName == "RepeatPrinting" || package.QueryName == "SlittingManagement")
                //{
                //    int StartIndex = Result.IndexOf("P#$$");
                //    // P#$$ - чтобы не возникло никаких случайностей, защита от совпадения с другой строкой
                //    if (StartIndex != -1)
                //    {
                //        string PrintingParameters = Result.Substring(StartIndex + 4);
                //        Result = Result.Substring(0, StartIndex);
                //        if (PrintingParameters.Length > 0)
                //        {
                //            System.Diagnostics.Process.Start("ftp", PrintingParameters);
                //        }
                //    }
                //}

                //if (registration)
                //{
                //    object[] ResultParameters = PackageConvertation.GetPatametersFromStr(Result);
                //    if ((int)ResultParameters[0] == 0)
                //    {
                //        this.ClientCode = ((int)ResultParameters[1]).ToString();
                //    }
                //}
                //package.DefineQueryAndParams("Answer", Result);
                ////Console.WriteLine("{1} - StorekeeperQueryHead.Length = {0}", StorekeeperQueryHead.Length, DateTime.Now.ToString());


                //#endregion
            }
        }

        private string ReadStream(string line)
        {
            Byte[] recivedData = new Byte[1024];
            Byte[] emptyData = Encoding.GetEncoding(1251).GetBytes("");

            try
            {
                if (TCPClient.Available > 0)
                {
                    StringBuilder SB = new StringBuilder();
                    while (TCPClient.Available > 0)
                    {
                        int streamLength = TCPStream.Read(recivedData, 0, recivedData.Length);
                        SB.Append(Encoding.GetEncoding(1251).GetString(recivedData, 0, streamLength));
                    }
                    string sbResult = SB.ToString();

                    if(!string.IsNullOrEmpty(line))
                    {
                        return string.Concat(line, sbResult);
                    }

                    return sbResult;
                }

                    TCPStream.Write(emptyData, 0, emptyData.Length);
                    // if anybody comment next string, CPU will die with five or more client connections

                    if (NeedToUpdate)
                    {
                        SendUpdate();
                        return null;
                    }


                    long MSecDiff = (DateTime.Now.Ticks - lastTimePingSent) / 10000;
                    if (
                        //false &&    
                        (NeedToPing && !PingSent && MSecDiff >= 700))
                    {

                        // TestString length is 100 bytes
                        //string TestString = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
                        const string TestString = "Ибоя работает!  Ибоя работает!  Ибоя работает!";

                        StringBuilder sb = new StringBuilder(TestString);
                        for (int i = 0; i < 0; i++)
                        {
                            sb.Append(TestString);
                        }

                        WriteStream(PackageViaWireless.BuildPackage(0, "Ping", PackageConvertation.GetStrPatametersFromArray(DateTime.Now.Ticks.ToString(), sb.ToString()), false));
                        PingSent = true;
                        return null;
                    }

                    //MSecDiff = (DateTime.Now.Ticks - lastTimeMessageCheck) / 10000;
                    if (ClientCode == "")
                    {   // Еще не известно для кого проверять наличие сообщения
                        Byte[] answer;
                        if (NewConnection)
                        {
                            answer = PackageViaWireless.BuildPackage(0, "TimeSynchronization", PackageConvertation.GetStrPatametersFromArray(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")), false);
                            // Sending answer 
                            WriteStream(answer);

                            NewConnection = false;
                        }
                        if (KeyPress != 0)
                        {
                            answer = PackageViaWireless.BuildPackage(0, "KeyPressing", PackageConvertation.GetStrPatametersFromArray(KeyPress), false);
                            WriteStream(answer);
                            KeyPress = 0;
                        }

                        if (SendBarcode != null)
                        {
                            answer = PackageViaWireless.BuildPackage(0, "BarcodeEvent", PackageConvertation.GetStrPatametersFromArray(SendBarcode), false);
                            WriteStream(answer);
                            SendBarcode = null;
                        }

                        Thread.Sleep(100);
                        return null;
                    }
            }
            catch (Exception exp)
            { /* I just was unable to find other way for inform TcpClient about connection death. 
                        /* Without attempt of writing empty string to stream the property "Available" return 
                        /* wrong value, for example "TRUE" when client went to dining room :) */

                Console.Write("Disconnecting for machine [ {0} ], thread {1}. Exp:{2}", IPAddress, Thread.CurrentThread.GetHashCode(), exp.Message);
                CloseChannels();
                return "";
            }
        }


        /// <summary>
        /// Отправить пакет клиенту (ТСД)
        /// </summary>
        /// <param name="package"></param>
        private void WritePackage(PackageViaWireless package)
        {
            WriteStream(package.GetPackage());
        }

        private void WriteStream(Byte[] answer)
        {
            try
            {
                TCPStream.Write(answer, 0, answer.Length);
            }
            catch
            {
            }
        }

        public void CloseChannels()
        {
            try { TCPStream.Close(); }
            catch { }

            try { TCPClient.Close(); }
            catch { }

            try { DeleteConnection(this); }
            catch { }


            StorekeeperThread.Abort();
        }

        public string GetClientIP()
        {
            if (TCPClient != null)
            {
                if (TCPClient.Connected)
                {
                    return ((System.Net.IPEndPoint)(TCPClient.Client.RemoteEndPoint)).Address.ToString();
                }

                    CloseChannels();
            }
            return "EMPTY CONNECTION";
        }

        #endregion

        private void SendUpdate()
        {
            try
            {

                FileTransmitViaWireless FTVW = new FileTransmitViaWireless(FullUpdatePath);
                Byte[] answer = FTVW.CreateAlertAboutFile().GetPackage();
                Console.WriteLine("FileTransmit. Thread: {0}", Thread.CurrentThread.GetHashCode());


                const int BlockSize = FileTransmitViaWireless.BlockSize;
                Byte[] buffer = new Byte[BlockSize];

                // Чтение и передача файла
                int totalRead = 100;
                lock (this)
                {
                    FileStream FileForTransmit = File.Open(FullUpdatePath, FileMode.Open, FileAccess.Read);
                    // Отправка предупреждения клиенту о том, что будет передаваться файл
                    WriteStream(answer);
                    while (totalRead > 0)
                    {
                        try
                        {
                            totalRead = FileForTransmit.Read(buffer, 0, BlockSize);
                            TCPStream.Write(buffer, 0, totalRead);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error 1 File Transmit. Thread: {0} \t Message: {1}", Thread.CurrentThread.GetHashCode(), e.Message);
                            FileForTransmit.Close();
                            return;
                        }
                    }
                    FileForTransmit.Close();
                    NeedToUpdate = false;
                    Console.WriteLine("Update has sent to machine: [{0}] ", IPAddress);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error 2 File Transmit. Thread: {0}\t  Message: {1}", Thread.CurrentThread.GetHashCode(), e.Message);
                if (!File.Exists(FullUpdatePath))
                {
                    InformAboutUpdateComplete("$FileDeleted$");
                    NeedToUpdate = false;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        public void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            int X = System.Windows.Forms.Cursor.Position.X;
            int Y = System.Windows.Forms.Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
    }
}
