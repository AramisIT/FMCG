using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using Aramis.Printing;
using Catalogs;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;
using TableViewInterfaces;

namespace FMCG.Utils.Printing
    {
    abstract class BaseStickerHelper : AramisThermalTransferStickerHelper
        {
        protected BaseStickerHelper()
            : base(ThermoPrinters.GetCurrentPrinterName(), 390, 300)
            {
            PrintLandscape = true;
            ShowPreview = false;
            }
        }
    }