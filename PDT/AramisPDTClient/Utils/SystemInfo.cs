using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WMS_client.WinProcessesManagement;

namespace AramisPDTClient
    {
    public class SystemInfo
        {

        public static string STARTUP_PATH;

        private static bool releaseModeChecked;
        private static bool releaseMode;

        public static bool ReleaseMode
            {
            get
                {
                if (!releaseModeChecked)
                    {
                    checkIsReleaseMode();
                    releaseModeChecked = true;
                    }
                return releaseMode;
                }
            }

        private static void checkIsReleaseMode()
            {
            releaseMode = false;
#if !DEBUG
            releaseMode = true;
#endif
            }

        public void SetReleaseMode(bool isRelease)
            {
            releaseMode = isRelease;
            releaseModeChecked = true;
            }

        public bool IsExistedSameProcess()
            {
            if (string.IsNullOrEmpty(STARTUP_PATH))
                {
                init(Assembly.GetCallingAssembly());
                }

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

        public void Init()
            {
            var exeAssembly = System.Reflection.Assembly.GetCallingAssembly();
            init(exeAssembly);
            }

        private void init(Assembly assembly)
            {
            STARTUP_PATH = System.IO.Path.GetFullPath(assembly.GetName().CodeBase);
            }
        }
    }
