using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
            update();
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

        private void update()
            {
            var updateFolder = Path.GetDirectoryName(SystemInfo.STARTUP_PATH) + '\\' + UPDATE_TEMP_FOLDER_NAME;
            if (!checkUpdateDirectory(updateFolder)) return;
           
            var files = getPdtFilesInfo();
            foreach (var pdtFileInfo in files)
                {
                totalBytes += pdtFileInfo.Size;
                }
            currentDownloadedBytes = 0;
            
            foreach (var pdtFileInfo in files)
                {
                downloadFile(pdtFileInfo, updateFolder);
                }
            }

        private bool checkUpdateDirectory(string updateFolder)
            {
            if (Directory.Exists(updateFolder)) return true;

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
