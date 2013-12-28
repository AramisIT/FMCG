using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AramisUtils.HootKeys;

namespace StorekeeperManagementServer
    {
    public partial class InfoForm : Form
        {
        private readonly KeyboardHook hotKeys = new KeyboardHook();
        private readonly StorekeeperManagementServer server;

        public StorekeeperManagementServer Server
            {
            get { return server; }
            }

        public bool IsRun;

        private const string AMOUNT_OF_CONNECTIONS_TEXT = "Количество подключений: ";

        public InfoForm(ReceiveMessage receiveMessage, ArrayList allowedIP, string serverIP, string updFolder)
            {
            InitializeComponent();
            //try
            //    {
            //    hotKeys.RegisterHotKey(KeyModifier.Control | KeyModifier.Alt, Keys.S);
            //    hotKeys.KeyPressed += hotKeys_KeyPressed;
            //    }
            //catch { }

            setInfo();

            new PackageViaWireless();
            try
                {
                server = new StorekeeperManagementServer(ShowInfo, InfoLabel, receiveMessage, allowedIP, serverIP, updFolder, out IsRun);
                }
            catch { }
            }

        public void EmulatePressKey(int key)
            {
            server.PressKeyOnTDC(key);
            }

        public void EmulateReadBarcode(string barcode)
            {
            server.PressKeyOnTDC(barcode);
            }

        private void hotKeys_KeyPressed(object sender, KeyPressedEventArgs e)
            {
            if (e.Key == Keys.S && e.Modifier == (KeyModifier.Control | KeyModifier.Alt))
                {
                sendBarcodeFromBuffer();
                }
            }

        private void sendBarcodeFromBuffer()
            {
            textBoxForPaste.Text = "";
            textBoxForPaste.Paste();
            server.PressKeyOnTDC(textBoxForPaste.Text);
            }

        private void setInfo()
            {
            label2.Text = AMOUNT_OF_CONNECTIONS_TEXT + Convert.ToString(0);
            }

        private void ShowInfo(List<DataTerminalSession> StorekeepersList)
            {
            StringBuilder sb =
                new StringBuilder(AMOUNT_OF_CONNECTIONS_TEXT + Convert.ToString(StorekeepersList.Count) + "\r\n");
            foreach (DataTerminalSession t in StorekeepersList)
                {
                sb.AppendLine(t.GetClientIP());
                }

            SetIPInformation(sb.ToString());
            }

        private delegate void SetTextCallback(string text);

        private void SetIPInformation(string text)
            {
            if (label2.InvokeRequired)
                {
                SetTextCallback d = SetIPInformation;
                try
                    {
                    Invoke(d, new object[] { text });
                    }
                catch { }
                }
            else
                {
                // here we just clear place 
                label2.Text =
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n" +
                    "                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n                                                                                 \r\n";
                label2.Text = text;
                }
            }

        private void InfoForm_FormClosing(object sender, FormClosingEventArgs e)
            {
            if (server != null)
                {
                server.WriteToFileAboutUpdate(null);
                }
            }

        private void button1_Click(object sender, EventArgs e)
            {
            if (((Button)sender).Text == "Set")
                {
                bool res = RegisterHotKey(Handle, 101, 0, 124);
                //(uint)((System.Text.Encoding.ASCII.GetBytes(new char[] { 'A' })[0])));
                if (res == false)
                    MessageBox.Show("Can't set vk %(");

                ((Button)sender).Text = "Dispose";
                }
            else
                {
                UnregisterHotKey(Handle, 100);
                ((Button)sender).Text = "Set";
                }

            #region Commented code
            //try
            //{
            //    Process ieProc = Process.Start(@"w:\line.exe");
            //    ieProc.Kill();
            //    //AppDomain secondDomain = AppDomain.CreateDomain("Sec");
            //    //object test = secondDomain.CreateInstanceAndUnwrap("ReshifaLibrary", "ReshifaLibrary.Test1");
            //    //Assembly TestType = Assembly.Load("ReshifaLibrary");
            //    //Type test1Type = TestType.GetType("ReshifaLibrary.Test1");
            //    //object obj = Activator.CreateInstance(test1Type);
            //    //MethodInfo mi = test1Type.GetMethod("Start");
            //    //object res = mi.Invoke(obj, null);
            //    //this.Text = res.ToString();

            //    //GC.Collect();
            //    //GC.WaitForPendingFinalizers();
            //}
            //catch (Exception ex)
            //{
            //    this.Text = String.Format("Error: {0}",ex.Message);
            //}
            ////int result = AppDomain.CurrentDomain.ExecuteAssembly(@"X:\My work\Проекты\Ибоя\Программный продукт\Доп. компоненты и утилиты\С#\Storekeeper Management Server\Storekeeper Management Server\SMSCommonStaticMethods.exe");
            ////InfoForm testDialog = new InfoForm();

            ////// Show testDialog as a modal dialog and determine if DialogResult = OK.
            ////if (testDialog.ShowDialog(this) == DialogResult.OK)
            ////{
            ////    // Read the contents of testDialog's TextBox.
            ////    this.Text = "fsdf";
            ////}
            ////else
            ////{
            ////    this.Text = "Cancelled";
            ////}
            ////testDialog.Dispose(); 
            #endregion
            }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, // handle to window    
                                                 int id, // hot key identifier    
                                                 uint fsModifiers, // key-modifier options    
                                                 uint vk // virtual-key code    
            );

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
            {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
                {
                case WM_HOTKEY:
                    MessageBox.Show("OK !");

                    break;
                }
            base.WndProc(ref m);
            }


        private void button1_KeyUp(object sender, KeyEventArgs e)
            {
            //MessageBox.Show(e.KeyCode.ToString());
            }

        private void button2_Click(object sender, EventArgs e) { }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
            server.PingingUpdate(IsClientPing.Checked);
            }

        private void button3_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(116);
            }

        private void button4_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(120);
            }

        private void button5_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(117);
            }

        private void button8_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(121);
            }

        private void button9_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(122);
            }

        private void button10_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(123);
            }

        private void button7_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(119);
            }

        private void button6_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(118);
            }

        private void button11_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(115);
            }

        private void button12_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(114);
            }

        private void button13_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(113);
            }

        private void button14_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(112);
            }

        private void button15_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode1.Text);
            }

        private void InfoForm_DoubleClick(object sender, EventArgs e)
            {
            var E = e as MouseEventArgs;
            if (E != null && (0 <= E.X && E.X <= 100 && 150 <= E.Y && E.Y <= 220))
                {
                groupBox1.Visible = !groupBox1.Visible;
                }
            }

        private void button16_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode2.Text);
            }

        private void button17_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode3.Text);
            }

        private void button18_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode4.Text);
            }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void button19_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode5.Text);
            }

        private void button20_Click(object sender, EventArgs e)
            {
            server.PressKeyOnTDC(Barcode6.Text);
            }

        private void button21_Click(object sender, EventArgs e)
            {
            sendBarcodeFromBuffer();
            }






        //protected override void WndProc(ref Message msg)
        //{
        //    const int WM_HOTKEY = 0x0312;

        //    switch (msg.Msg)  
        //    {
        //        case WM_HOTKEY:

        //            MessageBox.Show("Hotkey pressed");

        //            //ProcessHotkey();

        //            break;
        //    } 	

        //    //MessageBox.Show("OK");
        //    //if (msg.LParam == 25 || msg.Msg == 25 || msg.Result == 25 || msg.WParam == 25 ) 
        //    unsafe
        //    {
        //        Console.WriteLine("\t\t\tevent ...");
        //        Console.WriteLine("Msg = {0}\tLParam = {1,12}\tResult = {2}\tWParam = {3}", msg.Msg, msg.LParam.ToString(), msg.Result.ToString(), msg.WParam.ToString());
        //        Console.WriteLine();                
        //    }
        //    //if (msg.Msg == 25 ) 
        //    //{ 

        //    //}
        //    base.WndProc(ref msg);
        //}
        }
    }