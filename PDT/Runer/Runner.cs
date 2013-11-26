using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Runer
    {
    class Runner
        {
        private string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        private const string FILES_IDS_FILE_NAME = "Ids.txt";
        private const string UPDATE_FOLDER_NAME = "update";
        private Dictionary<string, bool> updateTasks = new Dictionary<string, bool>();


        private string updateFolderName
            {
            get
                {
                return currentDirectory + '\\' + UPDATE_FOLDER_NAME;
                }
            }

        public bool NewUpdateExists
            {
            get
                {
                return updateTasks.Count > 0;
                }
            }

        public Runner()
            {
            initUpdateTask();
            }

        private void initUpdateTask()
            {
            if (!Directory.Exists(updateFolderName)) return;

            var updateInfoDict = new DirectoryInfo(updateFolderName);

            foreach (var fileInfo in updateInfoDict.GetFiles())
                {
                var needToReplace = File.Exists(currentDirectory + '\\' + fileInfo.Name);
                    {
                    updateTasks.Add(fileInfo.Name, needToReplace);
                    }
                }

            }

        internal void Update()
            {
            foreach (var kvp in updateTasks)
                {
                var fileName = currentDirectory + '\\' + kvp.Key;
                var needToReplace = kvp.Value;
                if (needToReplace)
                    {
                    File.Delete(fileName);
                    }
                var oldFileName = updateFolderName + '\\' + kvp.Key;
                File.Move(oldFileName, fileName);
                }
            }

        internal void Run()
            {
            var processName = getProcessName();
            if (string.IsNullOrEmpty(processName))
                {
                MessageBox.Show("Не удалось определить имя запускаемого приложения!");
                return;
                }
            System.Diagnostics.Process.Start(currentDirectory + '\\' + processName, string.Empty);
            }

        private string getProcessName()
            {
            var idsFileName = currentDirectory + '\\' + FILES_IDS_FILE_NAME;
            if (!File.Exists(idsFileName)) return string.Empty;
            using (var idsFile = File.OpenText(idsFileName))
                {
                string row;
                while ((row = idsFile.ReadLine()) != null)
                    {
                    row = row.Trim();
                    var values = row.Split(';');
                    if (values.Length < 3) continue;

                    var isStartFileName = Convert.ToBoolean(values[2].Trim());
                    if (isStartFileName) return values[1].Trim();
                    }
                idsFile.Close();
                }

            return string.Empty;
            }
        }
    }
