using System;
using System.Threading;
using System.Windows.Forms;
using StorekeeperManagementServer;

namespace AtosFMCG.HelperClasses.PDT
    {
    public partial class SendToTCD : Form
        {
        private readonly InfoForm smServer;

        public SendToTCD(InfoForm server)
            {
            InitializeComponent();
            smServer = server;
            }

        #region Barcode
        private void send_Click(object sender, EventArgs e)
            {
            smServer.EmulateReadBarcode(message.Text);
            }

        private void sendMe_Click(object sender, EventArgs e)
            {
            Button btn = (Button) sender;
            smServer.EmulateReadBarcode(btn.Text);
            }
        #endregion

        #region Keys
        private void f4_Click(object sender, EventArgs e)
            {
            smServer.EmulatePressKey(115);
            }

        private void f5_Click(object sender, EventArgs e)
            {
            smServer.EmulatePressKey(116);
            }

        private void button86_Click(object sender, EventArgs e)
            {
            smServer.EmulatePressKey(11);
            }
        #endregion

        private void sendNbarcodes_Click(object sender, EventArgs e)
            {
            int count;
            int.TryParse(barcodecCount.Text, out count);

            for (int i = 0; i < count; i++)
                {
                smServer.EmulateReadBarcode(string.Concat(barcodePrefix.Text, i));
                Thread.Sleep(1000);
                }
            }
        }
    }