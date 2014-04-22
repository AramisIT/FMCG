using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AramisPDTClient;
using pdtExternalStorage;
using WMS_client.Processes;
using WMS_client.WinProcessesManagement;

namespace WMS_client
    {
    static class Program
        {
        public static IRemoteCommunications AramisSystem { get; private set; }

        [MTAThread]
        static void Main(string[] parameters)
            {
            AramisSystem = new RemoteInteractionProviderCreator<IRemoteCommunications>().CreateProvider();

            if (new SystemInfo().IsExistedSameProcess()) return;

            if (parameters.Length > 0)
                {
                try
                    {
                    int startParameter = Convert.ToInt32(parameters[0]);
                    if (startParameter == 1)
                        {
                        MessageBox.Show("Обновление не завершилось!");
                        }
                    }
                catch { }
                }

            var releaseMode = false;
#if !DEBUG
            releaseMode = true;
#endif
            releaseMode = false;

            new SystemInfo().SetReleaseMode(releaseMode);

            Consts = new BusinessProcessesParameters();
            Application.Run(new MainForm(typeof(RegistrationProcess)));
            }

        public static BusinessProcessesParameters Consts { get; set; }
        }
    }