﻿using System;
﻿using System.Diagnostics;
﻿using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;

namespace StorekeeperManagementServer
    {
    public delegate void CloseConnectionDelegate(DataTerminalSession storekeeperSession);
    public delegate object[] ReceiveMessage(string procedure, object[] parameters, int userId);

    public class DataTerminalSession
        {
        private readonly ReceiveMessage receiveMessage;
        #region Private Fields

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private int currentUserId;

        public int CurrentUserId
            {
            get
                {
                lock (this)
                    {
                    return currentUserId;
                    }
                }

            set
                {
                lock (this)
                    {
                    currentUserId = value;
                    }
                }
            }

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

        public DataTerminalSession(TcpClient MyTCPClient, NetworkStream MyTCPStream, CloseConnectionDelegate MyCloseConnectionMethod, UpdateCompleteDelegate InformAboutUpdateComplete, ReceiveMessage receiveMessage, Guid sessionId)
            {
            TCPClient = MyTCPClient;
            TCPStream = MyTCPStream;
            DeleteConnection = MyCloseConnectionMethod;
            this.receiveMessage = receiveMessage;

            this.SessionId = sessionId;

            //Server1CAgent = new HandlingVia1CServer(@"Srvr=""localhost""; Ref=""newwms"";");
            //Server1CAgent = new HandlingVia1CServer(@"Srvr=""localhost""; Ref=""iboya81""; Usr=""Оборский Д.В.""; Pwd=""123456""");
            //Server1CAgent = new HandlingVia1CServer(@"File=""C:\Documents and Settings\Reshifa\My Documents\InfoBase9""; Usr=""Denis.V.O""; Pwd=""123""");
            this.InformAboutUpdateComplete = InformAboutUpdateComplete;
            }

        public void SendLastPack()
            {
            WriteStream(lastPackage, lastPackageId);
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


                object[] ResultParameters = Aramis.Net.PackageConvertation.GetPatametersFromStr(package.Parameters);

                if (package.QueryName == PACKAGE_CONFIRMATION_NAME)
                    {
                    string packageId = ResultParameters[0] as string;
                    bool delivered = (bool)(ResultParameters[1]);

                    Trace.WriteLine(string.Format("Pack id: {0};\tquery = {1};\t{2}", packageId, PACKAGE_CONFIRMATION_NAME, DateTime.Now.ToString("mm:ss")));

                    if (packageId.Equals(lastPackageId))
                        {
                        if (delivered)
                            {
                            waitForConfirmationReply = false;
                            }
                        else
                            {
                            RepeateLastPackage();
                            }
                        }
                    else
                        {
                        // если пакет потерян, то его нельзя переслать
                        waitForConfirmationReply = false;
                        }

                    continue;
                    }

                Trace.WriteLine(string.Format("Pack id: {0};\tquery = {1};\t{2}", package.PackageID, package.QueryName, DateTime.Now.ToString("mm:ss")));

                CurrentUserId = package.ClientCode;
                object[] resultArray = receiveMessage(package.QueryName, ResultParameters, package.ClientCode);

                string resultStr = Aramis.Net.PackageConvertation.GetStrPatametersFromArray(resultArray);
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

        private static readonly string PACKAGE_CONFIRMATION_NAME = "DELIVERY_CONFIRMATION";

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

                    if (!string.IsNullOrEmpty(line))
                        {
                        return string.Concat(line, sbResult);
                        }

                    return sbResult;
                    }
                else if (waitForConfirmationReply)
                    {
                    lock (this)
                        {
                        int milisecondsElapsed = (int)((TimeSpan)(DateTime.Now - actualLastPackageTime)).TotalMilliseconds;
                        if (milisecondsElapsed > 500)
                            {
                            //Trace.WriteLine(string.Format("WaitForConfirmationReply package.PackageID = {0};\t{1}",
                            //    lastPackageId, DateTime.Now));

                            TCPStream.Write(packageConfirmation, 0, packageConfirmation.Length);
                            actualLastPackageTime = DateTime.Now;
                            }
                        }
                    }

                lock (this)
                    {
                    TCPStream.Write(emptyData, 0, emptyData.Length);
                    }
                // if anybody comment next string, CPU will die with five or more client connections

                if (NeedToUpdate)
                    {
                    SendUpdate();
                    return null;
                    }

                string packageId;
                long MSecDiff = (DateTime.Now.Ticks - lastTimePingSent) / 10000;
                if (
                    //false &&    
                    (NeedToPing && !PingSent && MSecDiff >= 700))
                    {

                    // TestString length is 100 bytes
                    const string TestString = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

                    StringBuilder sb = new StringBuilder(TestString);
                    for (int i = 0; i < 0; i++)
                        {
                        sb.Append(TestString);
                        }


                    WriteStream(PackageViaWireless.BuildPackage(0, "Ping", Aramis.Net.PackageConvertation.GetStrPatametersFromArray(DateTime.Now.Ticks.ToString(), sb.ToString()), false, out packageId), packageId);
                    PingSent = true;
                    return null;
                    }

                //MSecDiff = (DateTime.Now.Ticks - lastTimeMessageCheck) / 10000;
                if (ClientCode == "")
                    {   // Еще не известно для кого проверять наличие сообщения
                    Byte[] answer;
                    if (NewConnection)
                        {
                        answer = PackageViaWireless.BuildPackage(0, "TimeSynchronization", Aramis.Net.PackageConvertation.GetStrPatametersFromArray(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")), false, out packageId);
                        // Sending answer 
                        WriteStream(answer, packageId);

                        NewConnection = false;
                        }
                    if (KeyPress != 0)
                        {
                        answer = PackageViaWireless.BuildPackage(0, "KeyPressing", Aramis.Net.PackageConvertation.GetStrPatametersFromArray(KeyPress), false, out packageId);
                        WriteStream(answer, packageId);
                        KeyPress = 0;
                        }

                    if (SendBarcode != null)
                        {
                        answer = PackageViaWireless.BuildPackage(0, "BarcodeEvent", Aramis.Net.PackageConvertation.GetStrPatametersFromArray(SendBarcode), false, out packageId);
                        WriteStream(answer, packageId);
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

        private Byte[] lastPackage;
        private string lastPackageId;
        private volatile bool waitForConfirmationReply;
        private Byte[] packageConfirmation;
        private DateTime actualLastPackageTime;

        /// <summary>
        /// Отправить пакет клиенту (ТСД)
        /// </summary>
        /// <param name="package"></param>
        private void WritePackage(PackageViaWireless package)
            {
            Trace.WriteLine(string.Format("WritePackage package.PackageID = {0};\tQueryNema = {1};\t{2}", package.PackageID, package.QueryName, DateTime.Now));
            WriteStream(package.GetPackage(), package.PackageID);
            }

        private void WriteStream(Byte[] packageData, string packageId)
            {
            var parameters = Aramis.Net.PackageConvertation.GetStrPatametersFromArray(new object[] { packageId });
            var packageOfDeliveryConfirmation = new PackageViaWireless(0, true);
            packageOfDeliveryConfirmation.DefineQueryAndParams(PACKAGE_CONFIRMATION_NAME, parameters);

            lock (this)
                {
                packageConfirmation = packageOfDeliveryConfirmation.GetPackage();

                lastPackage = packageData;
                lastPackageId = packageId;
                actualLastPackageTime = DateTime.Now;

                if (!TCPStream.CanWrite)
                    {
                    CloseChannels();
                    return;
                    }

                try
                    {
                    TCPStream.Write(packageData, 0, packageData.Length);
                    }
                catch
                    {
                    CloseChannels();
                    return;
                    }
                }
            waitForConfirmationReply = true;
            }

        private void RepeateLastPackage()
            {
            Trace.WriteLine(string.Format("RepeateLastPackage package.PackageID = {0};\t{1}", lastPackageId, DateTime.Now));

            WriteStream(lastPackage, lastPackageId);
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
                var package = FTVW.CreateAlertAboutFile();
                Byte[] answer = package.GetPackage();
                Console.WriteLine("FileTransmit. Thread: {0}", Thread.CurrentThread.GetHashCode());


                const int BlockSize = FileTransmitViaWireless.BlockSize;
                Byte[] buffer = new Byte[BlockSize];

                // Чтение и передача файла
                int totalRead = 100;
                lock (this)
                    {
                    FileStream FileForTransmit = File.Open(FullUpdatePath, FileMode.Open, FileAccess.Read);
                    // Отправка предупреждения клиенту о том, что будет передаваться файл
                    WriteStream(answer, package.PackageID);
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

        public Guid SessionId { get; private set; }
        }
    }
