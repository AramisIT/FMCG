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
using WMS_client;

namespace AramisPDTClient.UpdatingSoft
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
            }

        public SoftUpdater()
            : base(1)
            {
            tryToUpdate();
            }

        private void tryToUpdate()
            {
            var connectionWasEstablished = MainProcess.ConnectionAgent.WifiEnabled;
            if (connectionWasEstablished)
                {
                StopNetworkConnection();
                System.Threading.Thread.Sleep(1000);
                }

            StartNetworkConnection();
            System.Threading.Thread.Sleep(500);

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

        public override void OnBarcode(string barcode)
            {

            }

        public override void OnHotKey(KeyAction key)
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

                result.Add(_PDTFilesInfo);
                }

            return result;
            }

        private void tryUpdate()
            {
            if (!downloadNewUpdate()) return;

            if (!newUpdateExists()) return;

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
            var updateDirInfo = new DirectoryInfo(updateFolderName);
            return updateDirInfo.GetFiles().Length > 0;
            }

        private string updateFolderName
            {
            get
                {
                return Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + UPDATE_TEMP_FOLDER_NAME;
                }
            }

        private bool downloadNewUpdate()
            {
            var updateFolder = updateFolderName;
            if (!checkUpdateDirectory(updateFolder)) return false;

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

        private bool createIdsFile(List<PDTFileInfo> files)
            {
            var currentFilesIdsFileName = Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + UPDATE_TEMP_FOLDER_NAME + '\\' + FILES_IDS_FILE_NAME;

            try
                {
                using (var pdtIdsInfo = File.CreateText(currentFilesIdsFileName))
                    {
                    foreach (var pdtFileInfo in files)
                        {
                        pdtIdsInfo.WriteLine(string.Format("{0};{1}", pdtFileInfo.Id, pdtFileInfo.Name));
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
                    var separatorPosition = row.IndexOf(';');
                    if (separatorPosition < 0) continue;

                    var guidPart = row.Substring(0, separatorPosition);
                    var fileNamePart = row.Substring(separatorPosition + 1, row.Length - 1 - guidPart.Length).Trim();
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

        private bool checkUpdateDirectory(string updateFolder)
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

        private const string FILES_IDS_FILE_NAME = "Ids.txt";
        private const string UPDATE_TEMP_FOLDER_NAME = "temp_update";
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
