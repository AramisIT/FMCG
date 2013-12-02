using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Runer
    {
    class Program
        {
        static void Main(string[] args)
            {
            if (args.Length > 0)
                {
                tryKillProcess(args[0]);
                }

            var updater = new Runner();
            if (updater.NewUpdateExists)
                {
                if (!updater.Update())
                    {
                    MessageBox.Show("Приложение не обновлено!", "Aramis PDT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            updater.Run();
            }

        private static void tryKillProcess(string processIdStr)
            {
            var processId = 0;
            try
                {
                processId = Convert.ToInt32(processIdStr);
                }
            catch
                {
                return;
                }
            try
                {
                var process = Process.GetProcessById(processId);
                process.Kill();
                System.Threading.Thread.Sleep(1000);
                }
            catch { }
            }
        }
    }
