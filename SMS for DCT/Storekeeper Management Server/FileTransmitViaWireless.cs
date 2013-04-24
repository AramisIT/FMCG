using System;
using System.IO;

namespace StorekeeperManagementServer
{
    public class FileTransmitViaWireless
    {
        private readonly string FileName;
        private readonly string FullFileName;
        public const int BlockSize = 8192; //524288 * 64; //8192
        private readonly long FileSize;
        private FileStream FileForTransmit;

        #region Constructor

        public FileTransmitViaWireless(string FullFileName)
        {
            if (!File.Exists(FullFileName))
            {
                throw new FileNotFoundException();
            }

            FileInfo fInfo = new FileInfo(FullFileName);
            FileSize = fInfo.Length;
            FileName = fInfo.Name;
            this.FullFileName = FullFileName;
        }

        #endregion

        public PackageViaWireless CreateAlertAboutFile()
        {
            PackageViaWireless alertPackage = new PackageViaWireless();
            string parameters = String.Format("3{0}\r2{1}", FileName, FileSize.ToString());
            alertPackage.DefineQueryAndParams("FileTransmit", parameters);
            return alertPackage;
        }

        public static string GetFileName(string parameters)
        {
            int separatorIndex = parameters.IndexOf('\t');
            if (separatorIndex == -1) return "";
            return parameters.Substring(0, separatorIndex);
        }

        public static ulong? GetFileSize(string parameters)
        {
            int separatorIndex = parameters.IndexOf('\t') + 1;
            if (separatorIndex == 0) return null;
            return Convert.ToUInt64(parameters.Substring(separatorIndex, parameters.Length - separatorIndex));
        }

        public byte[] GetNextBlock(out int TotalRead)
        {
            bool fileNotAccessible = true;
            if (FileForTransmit == null)
            {
                while (fileNotAccessible)
                {
                    //try
                    //{
                    FileForTransmit = File.Open(FullFileName, FileMode.Open, FileAccess.Read);
                    fileNotAccessible = false;
                    //}
                    //catch
                    //{
                    //    System.Threading.Thread.Sleep(100);
                    //}
                }
            }
            byte[] data = new byte[BlockSize];

            TotalRead = FileForTransmit.Read(data, 0, BlockSize);
            if (TotalRead > 0)
            {
                return data;
            }

            FileForTransmit.Close();
            return null;
        }
    }
}