using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;

namespace FMCG.Utils.Printing
    {
    class StickersPrintingHelper : BaseStickerHelper
        {

        public StickersPrintingHelper(List<StickerInfo> stickersTasks, string printerName = "")
            {
            this.stickersTasks = stickersTasks;
            this.PrinterName = printerName;
            }

        public StickersPrintingHelper(List<Stickers> stickersTasks, string printerName = "")
            {
            this.stickersTasks = createStickersTasks(stickersTasks);
            this.PrinterName = printerName;
            }

        private List<StickerInfo> createStickersTasks(List<Stickers> stickers)
            {
            var result = new List<StickerInfo>();
            stickers.ForEach(sticker =>
            {
                var stickerInfo = new StickerInfo()
                {
                    AcceptionDate = sticker.AcceptionDate,
                    Barcode = sticker.Barcode,
                    Driver = sticker.Driver.Description,
                    ReleaseDate = sticker.ReleaseDate,
                    ExpiryDate = sticker.ExpiryDate,
                    HalpExpiryDate = sticker.HalpExpiryDate,
                    Nomenclature = sticker.Nomenclature.Description,
                    PacksCount = sticker.Quantity,
                    UnitsQuantity = sticker.UnitsQuantity,
                    Id = sticker.Id
                };
                result.Add(stickerInfo);
            });

            return result;
            }
        private List<StickerInfo> stickersTasks;

        protected override string getShortFileNameOfAdapter()
            {
            return "StickerAdapter.xml";
            }

        protected override DataTable getDataSource()
            {
            var dataSource = new DataTable();
            dataSource.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("Nomenclature", typeof(string)), 
                new DataColumn("BarCode", typeof(string)),
                new DataColumn("PacksCount", typeof(int)), 
                new DataColumn("UnitsQuantity", typeof(int)), 
                new DataColumn("ReleaseDate", typeof(DateTime)),
                new DataColumn("HalpExpiryDate", typeof(DateTime)),
                new DataColumn("ExpiryDate", typeof(DateTime)),
                new DataColumn("AcceptionDate", typeof(DateTime)),
                new DataColumn("Driver", typeof(string)),
                new DataColumn("Id", typeof(long)),
                new DataColumn("barcode", typeof(string))
                });

            foreach (var stickersTask in stickersTasks)
                {
                dataSource.Rows.Add(stickersTask.Nomenclature, stickersTask.Barcode, stickersTask.PacksCount, stickersTask.UnitsQuantity,
                    stickersTask.ReleaseDate, stickersTask.HalpExpiryDate, stickersTask.ExpiryDate, stickersTask.AcceptionDate,
                    stickersTask.Driver, stickersTask.Id, string.Format("S{0}", stickersTask.Id));
                }

            return dataSource;
            }
        }
    }
