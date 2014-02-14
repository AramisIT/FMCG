using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AramisPDTClient;
using WMS_client;

namespace WMS_client
    {
    public class SoftUpdater : BusinessProcess
        {
        private int totalBytes;
        private int currentDownloadedBytes;

        private class PDTFileInfo
            {
            public DateTime Date { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Size { get; set; }
            public int Version { get; set; }
            }

        public SoftUpdater()
            : base(1)
            {
            var currentToDO = ToDoCommand;
            ToDoCommand = "Оновлення";
            
            tryToUpdate();

            ToDoCommand = currentToDO;
            }

        private void tryToUpdate()
            {
            var connectionWasEstablished = MainProcess.ConnectionAgent.WifiEnabled;
            var wasOnLine = MainProcess.ConnectionAgent.OnLine;
            if (connectionWasEstablished && !wasOnLine)
                {
                StopNetworkConnection();
                System.Threading.Thread.Sleep(1000);
                }

            if (!wasOnLine)
                {
                StartNetworkConnection();
                System.Threading.Thread.Sleep(500);
                }

            if (MainProcess.ConnectionAgent.OnLine)
                {
                tryUpdate();
                }

            if (!connectionWasEstablished)
                {
                StopNetworkConnection();
                }
            }

        public override void DrawControls()
            {

            }

        protected override void OnBarcode(string barcode)
            {

            }

        protected override void OnHotKey(KeyAction key)
            {

            }

        private List<PDTFileInfo> getPdtFilesInfo()
            {
            PerformQuery("GetPDTFiles");

            var result = new List<PDTFileInfo>();
            if (!IsExistParameters || !(ResultParameters[0] is DataTable)) return result;

            var table = ResultParameters[0] as DataTable;
            foreach (DataRow row in table.Rows)
                {
                var _PDTFilesInfo = new PDTFileInfo();

                _PDTFilesInfo.Name = row["Name"].ToString();
                _PDTFilesInfo.Size = Convert.ToInt32(row["Size"]);
                _PDTFilesInfo.Date = row["Date"].ToString().ToDateTime();
                _PDTFilesInfo.Id = new Guid(row["Id"].ToString());
                _PDTFilesInfo.Version = Convert.ToInt32(row["Version"]);

                result.Add(_PDTFilesInfo);
                }

            return result;
            }

        private void tryUpdate()
            {
            if (!downloadNewUpdate()) return;

            if (!newUpdateExists()) return;

            Directory.Move(tempUpdateFolderName, updateFolderName);

            StopNetworkConnection();

            runUpdater();
            }

        private const string RUNER_NAME = "Runer.exe";
        private void runUpdater()
            {
            var updaterPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(SystemInfo.STARTUP_PATH), RUNER_NAME);

            try
                {
                Process.Start(updaterPath, Process.GetCurrentProcess().Id.ToString());
                }
            catch (Exception exp)
                {
                string.Format("Ошибка запуска Раннера:{0}", exp.Message).Warning();
                return;
                }
            TerminateApplication();
            }

        private bool newUpdateExists()
            {
            var updateDirInfo = new DirectoryInfo(tempUpdateFolderName);
            return updateDirInfo.GetFiles().Length > 0;
            }

        private string tempUpdateFolderName
            {
            get
                {
                return Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + UPDATE_TEMP_FOLDER_NAME;
                }
            }

        private string updateFolderName
            {
            get
                {
                return Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + UPDATE_FOLDER_NAME;
                }
            }

        private bool downloadNewUpdate()
            {
            var updateFolder = tempUpdateFolderName;
            if (!checkTempUpdateDirectory(updateFolder)) return false;
            if (!deleteUpdateDirectory()) return false;

            var existsFileInfo = getExistsFilesInfo();
            var files = getPdtFilesInfo();
            foreach (var pdtFileInfo in files)
                {
                if (!existsFileInfo.ContainsKey(pdtFileInfo.Id))
                    {
                    totalBytes += pdtFileInfo.Size;
                    }
                }
            currentDownloadedBytes = 0;

            var newFilesAmount = 0;
            foreach (var pdtFileInfo in files)
                {
                if (!existsFileInfo.ContainsKey(pdtFileInfo.Id))
                    {
                    if (!downloadFile(pdtFileInfo, updateFolder)) return false;
                    newFilesAmount++;
                    }
                }

            if (newFilesAmount > 0)
                {
                if (!createIdsFile(files)) return false;
                }

            return true;
            }

        private bool deleteUpdateDirectory()
            {
            if (!Directory.Exists(updateFolderName)) return true;

            try
                {
                foreach (var fileInfo in new DirectoryInfo(updateFolderName).GetFiles())
                    {
                    fileInfo.Delete();
                    }

                Directory.Delete(updateFolderName);
                }
            catch
                {
                return false;
                }

            return !Directory.Exists(updateFolderName);
            }

        private bool createIdsFile(List<PDTFileInfo> files)
            {
            var currentFilesIdsFileName = tempUpdateFolderName + '\\' + FILES_IDS_FILE_NAME;
            var exeFileName = Path.GetFileName(SystemInfo.STARTUP_PATH);
            try
                {
                using (var pdtIdsInfo = File.CreateText(currentFilesIdsFileName))
                    {
                    foreach (var pdtFileInfo in files)
                        {
                        var exeFile = exeFileName.Equals(pdtFileInfo.Name, StringComparison.InvariantCultureIgnoreCase);
                        pdtIdsInfo.WriteLine(string.Format("{0};{1};{2};{3}", pdtFileInfo.Id, pdtFileInfo.Name, exeFile, pdtFileInfo.Version));
                        }
                    pdtIdsInfo.Close();
                    }
                }
            catch (Exception exp)
                {
                Trace.WriteLine(string.Format("Ошибка записи файла с перечнем идентификаторов{0}", exp.Message));
                return false;
                }

            return true;
            }

        private Dictionary<Guid, string> getExistsFilesInfo()
            {
            var currentFilesIdsFileName = Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + FILES_IDS_FILE_NAME;
            var result = new Dictionary<Guid, string>();
            if (!File.Exists(currentFilesIdsFileName)) return result;

            using (var idsFile = File.OpenText(currentFilesIdsFileName))
                {
                string row;
                while ((row = idsFile.ReadLine()) != null)
                    {
                    row = row.Trim();
                    var values = row.Split(';');
                    if (values.Length < 3) continue;

                    var guidPart = values[0].Trim();
                    var fileNamePart = values[1].Trim();
                    try
                        {
                        result.Add(new Guid(guidPart.Trim()), fileNamePart);
                        }
                    catch { }
                    }
                idsFile.Close();
                }

            return result;
            }

        private bool checkTempUpdateDirectory(string updateFolder)
            {
            if (Directory.Exists(updateFolder))
                {
                var dirInfo = new DirectoryInfo(updateFolder);
                var files = dirInfo.GetFiles();
                foreach (var fileInfo in files)
                    {
                    try
                        {
                        fileInfo.Delete();
                        }
                    catch { }
                    }
                return new DirectoryInfo(updateFolder).GetFiles().Length == 0;
                }

            try
                {
                Directory.CreateDirectory(updateFolder);
                }
            catch (Exception exp)
                {
                Trace.WriteLine(string.Format("Error on creating folder: {0}", exp.Message));
                return false;
                }

            return true;
            }

        public const string FILES_IDS_FILE_NAME = "Ids.txt";
        private const string UPDATE_TEMP_FOLDER_NAME = "temp_update";
        private const string UPDATE_FOLDER_NAME = "update";
        private const int FILE_BLOCK_SIZE = 65536;
        private bool downloadFile(PDTFileInfo fileInfo, string path)
            {
            var destinationFilePath = string.Format("{0}\\{1}", path, fileInfo.Name);
            try
                {
                using (var newFile = File.OpenWrite(destinationFilePath))
                    {
                    var bytesToLeft = fileInfo.Size;
                    var currentIndex = 0;
                    while (bytesToLeft > 0)
                        {
                        PerformQuery("GetPDTFileBlock", fileInfo.Id.ToString(), currentIndex, FILE_BLOCK_SIZE);
                        if (!IsExistParameters || !(ResultParameters[0] is string)) return false;
                        var downloadedFileBlock = ResultParameters[0] as string;

                        int blockSize = downloadedFileBlock.Length / 2;
                        var downloadedBytes = new byte[blockSize];

                        for (int byteIndex = 0; byteIndex < blockSize; byteIndex++)
                            {
                            var byteHex = downloadedFileBlock.Substring(byteIndex << 1, 2);
                            byte currentByte = byte.Parse(byteHex, NumberStyles.HexNumber);
                            downloadedBytes[byteIndex] = currentByte;
                            }

                        newFile.Write(downloadedBytes, 0, downloadedBytes.Length);
                        bytesToLeft -= blockSize;
                        currentIndex += blockSize;
                        currentDownloadedBytes += blockSize;

                        updateProgress();
                        }

                    newFile.Close();
                    }
                }
            catch (Exception exp)
                {
                Trace.WriteLine(string.Format("Ошибка записи файла:\r\n{0}", exp.Message));
                return false;
                }

            return true;
            }

        private void updateProgress()
            {
            ShowProgress(currentDownloadedBytes, totalBytes);
            }
        }
    }
