using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WMS_client;
using WMS_client.WinProcessesManagement;

namespace AramisPDTClient
    {
    public class SystemInfo
        {

        public static int Version { get; private set; }

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
            Version = readSystemVersion();
            }

        private int readSystemVersion()
            {
            var idsFileName = Path.GetDirectoryName(STARTUP_PATH) + '\\' + SoftUpdater.FILES_IDS_FILE_NAME;
            if (!File.Exists(idsFileName)) return 0;

            using (var idsFile = File.OpenText(idsFileName))
                {
                string row;
                while ((row = idsFile.ReadLine()) != null)
                    {
                    row = row.Trim();
                    var values = row.Split(';');
                    if (values.Length < 4) continue;
                    try
                        {
                        if (Convert.ToBoolean(values[2].Trim()))
                            {
                            return Convert.ToInt32(values[3].Trim());
                            }
                        }
                    catch { }
                    }
                idsFile.Close();
                }

            return 0;
            }
        }
    }
