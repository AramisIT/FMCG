using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AramisPDTClient;
using WMS_client.Processes;
using WMS_client.WinProcessesManagement;

namespace WMS_client
    {
    static class Program
        {
        [MTAThread]
        static void Main()
            {
            if (new SystemInfo().IsExistedSameProcess()) return;

            var releaseMode = false;
#if !DEBUG
            releaseMode = true;
#endif
            new SystemInfo().SetReleaseMode(releaseMode);

            Application.Run(new MainForm(typeof(RegistrationProcess)));
            }
        }
    }