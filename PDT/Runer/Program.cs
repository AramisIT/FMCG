using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
                updater.Update();
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
