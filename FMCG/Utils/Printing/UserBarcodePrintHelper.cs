using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using AtosFMCG.TouchScreen.PalletSticker;
using FMCG.Utils.Printing;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;

namespace FMCG.Utils.Printing
    {
    class UserBarcodePrintHelper : BaseStickerHelper
        {
        private long userId;
        private string userDescription;

        public UserBarcodePrintHelper(long userId, string userDescription)
            {
            this.userId = userId;
            this.userDescription = userDescription;
            }

        protected override void setReportParameters(Dictionary<string, object> reportParameters)
            {
            reportParameters.Add("Barcode", "EM." + userId);
            reportParameters.Add("Name", userDescription);
            }

        protected override DataTable getDataSource()
            {
            return null;
            }

        protected override string getShortFileNameOfAdapter()
            {
            return "UserBarcode.xml";
            }
        }
    }
