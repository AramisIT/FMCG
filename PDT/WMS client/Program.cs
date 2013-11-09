using System;
using System.Diagnostics;
using System.Windows.Forms;
using WMS_client.WinProcessesManagement;

namespace WMS_client
    {
    static class Program
        {
        private static readonly string STARTUP_PATH = System.IO.Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        [MTAThread]
        static void Main()
            {
            if (isExistedSameProcess())
                {
                return;
                }

            Application.Run(new MainForm());
            }

        private static bool isExistedSameProcess()
            {
            int currentPId = Process.GetCurrentProcess().Id;
            ProcessInfo[] processes = ProcessCE.GetProcesses();
            foreach (ProcessInfo pInfo in processes)
                {
                if (pInfo.Pid.ToInt32() == currentPId)
                    {
                    continue;
                    }
                if (pInfo.FullPath.Equals(STARTUP_PATH))
                    {
                    return true;
                    }
                }
            return false;
            }
        }
    }