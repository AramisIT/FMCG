using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WMS_client.Base.Visual
    {
    public partial class RemoteCallDialog : Form
        {
        private RemoteCallDialog()
            {
            InitializeComponent();
            }

        private void cancelQuery_Click(object sender, EventArgs e)
            {
            cancelQuery.Enabled = false;
            WMSClient.Current.ConnectionAgent.StopConnection();
            NotifyResponseRecieved();
            }

        internal static void WaitForServerResponse()
            {
            currentForm = new RemoteCallDialog();
            currentForm.ShowDialog();
            }

        internal static void NotifyResponseRecieved()
            {
            lock (formLocker)
                {
                if (_currentForm == null)
                    {
                    return;
                    }

                if (_currentForm.InvokeRequired)
                    {
                    _currentForm.Invoke(new Action(NotifyResponseRecieved));
                    return;
                    }

                _currentForm.Close();
                _currentForm = null;
                }
            }

        private static readonly object formLocker = new object();
        private static RemoteCallDialog _currentForm;
        private static RemoteCallDialog currentForm
            {
            get
                {
                lock (formLocker)
                    {
                    return _currentForm;
                    }
                }

            set
                {
                lock (formLocker)
                    {
                    _currentForm = value;
                    }
                }
            }

        private void timer_Tick(object sender, EventArgs e)
            {
            if (WMSClient.Current.ConnectionAgent.RequestReady
                | !WMSClient.Current.ConnectionAgent.OnLine)
                {
                timer.Enabled = false;
                NotifyResponseRecieved();
                }
            }
        }
    }