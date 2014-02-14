using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Movement : BusinessProcess
        {
        private class AcceptancePalletControls : HideableControlsCollection
            {
            public MobileLabel nomenclatureLabel;
            public MobileLabel stickerIdInfoLabel;

            public MobileLabel cellCaptionLabel;
            public MobileLabel cellLabel;

            public MobileLabel previousPalletStickerInfoLabel;

            public MobileLabel nextCommandLabel1;
            public MobileLabel nextCommandLabel2;
            }

        private class ScanPalletControls : HideableControlsCollection
            {
            public MobileLabel MovedPalletName;

            public MobileLabel MovedPalletCell;

            public MobileLabel PreviousCodeForMovedPallet;

            public MobileLabel WillLabel;
            }

        /// <summary>
        /// Редактирование паллеты
        /// </summary>
        private AcceptancePalletControls palletEditControls;

        /// <summary>
        /// Приглашение отсканировать следующую паллету
        /// </summary>
        private ScanPalletControls scanNextPalletControls;

        private BarcodeData finalBarcodeData;
        private long lastStickerId;
        private BarcodeData startBarcodeData;
        private long documentId;

        private Dictionary<long, bool> processedPallets = new Dictionary<long, bool>();

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        public Movement()
            : base(1)
            {
            ToDoCommand = "Переміщення палети";
            checkAcceptanceCache();
            }

        private void checkAcceptanceCache()
            {
            var repository = new Repository();
            repository.GetTraysList();
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            createPalletControls();

            createScanPalletLabelControls();

            startScanNextPallet();
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (scanNextPalletControls.Visible)
                {
                scanNextPalletOnBarcode(barcode);
                }
            else if (palletEditControls.Visible)
                {
                editPalletOnBarcode(barcode);
                }
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            if (palletEditControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Esc:
                        startScanNextPallet();
                        return;
                    }
                }
            else if (scanNextPalletControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Complate:
                        complateProcess();
                        return;

                    case KeyAction.Esc:
                        if ("Завершити операцію?".Ask())
                            {
                            MainProcess.ClearControls();
                            MainProcess.Process = new SelectingProcess();
                            }
                        return;
                    }
                }
            }

        #endregion

        private void scanNextPalletOnBarcode(string barcode)
            {
            if (!barcode.IsSticker()) return;

            var barcodeData = barcode.ToBarcodeData();

            if (processedPallets.ContainsKey(barcodeData.StickerId))
                {
                "Ця палета вже була оброблена".ShowMessage();
                return;
                }

            if (!barcodeData.ReadStickerInfo()) return;

            startBarcodeData = barcodeData;
            finalBarcodeData = barcodeData.GetCopy();

            ShowControls(palletEditControls);

            updateStickerData();
            }

        private void editPalletOnBarcode(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();
                processedPallets = new Dictionary<long, bool>();

                var samePallet = finalBarcodeData != null && finalBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                onAnotherStickerScan(barcodeData);
                }
            else if (barcode.IsCell())
                {
                onCellScan(barcode.ToCell());
                }
            }

        private void onAnotherStickerScan(BarcodeData barcodeData)
            {
            if (barcodeData.StickerId == finalBarcodeData.PreviousStickerCode) return;

            barcodeData.ReadStickerInfo();
            bool cellFounded = barcodeData.Cell.Id != 0;
            if (!cellFounded)
                {
                "Відсканованої палети нема на залишках".Warning();
                return;
                }
            this.finalBarcodeData.Cell = barcodeData.Cell;
            this.finalBarcodeData.PreviousStickerCode = barcodeData.StickerId;
            saveMovement();
            }

        private void onCellScan(CatalogItem scannedCell)
            {
            //if (scannedCell.Id == startBarcodeData.Cell.Id)
            //    {
            //    if (startBarcodeData.PreviousStickerCode == 0)
            //        {

            //        }
            //    else if ("Зараз палета перша у комірці?".Ask())
            //        {
            //        finalBarcodeData.PreviousStickerCode = 0;

            //        }
            //    }
            //else if (string.Format(@"Розмістити палету першою у комірці ""{0}""?", scannedCell.Description).Ask())
            //    {
            finalBarcodeData.Cell = scannedCell;
            finalBarcodeData.PreviousStickerCode = 0;
            saveMovement();
            //}
            }

        private void startScanNextPallet()
            {
            setLastPalletNewCellInfo();
            finalBarcodeData = null;
            ShowControls(scanNextPalletControls);
            }

        private void setLastPalletNewCellInfo()
            {
            bool lastPalletNull = finalBarcodeData == null;
            scanNextPalletControls.MovedPalletName.Text = lastPalletNull ? string.Empty : string.Format("Палету {0}", finalBarcodeData.StickerId);
            scanNextPalletControls.MovedPalletCell.Text = lastPalletNull ? string.Empty : string.Format("розміщено у {0}", finalBarcodeData.Cell.Description);

            bool previousPalletForLastPalletNotExists = lastPalletNull || finalBarcodeData.PreviousStickerCode == 0;
            scanNextPalletControls.PreviousCodeForMovedPallet.Text = previousPalletForLastPalletNotExists ? string.Empty
                : string.Format("Код попередньої палети {0}", finalBarcodeData.PreviousStickerCode);
            }

        private void createScanPalletLabelControls()
            {
            scanNextPalletControls = new ScanPalletControls();

            int top = 60;
            

            scanNextPalletControls.MovedPalletName = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            scanNextPalletControls.MovedPalletCell = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            scanNextPalletControls.PreviousCodeForMovedPallet = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            scanNextPalletControls.WillLabel = MainProcess.CreateLabel("Відскануйте палету", 10, 190, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void createPalletControls()
            {
            palletEditControls = new AcceptancePalletControls();
            int top = 42;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.nomenclatureLabel = MainProcess.CreateLabel("<номенклатура>", 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.stickerIdInfoLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.cellCaptionLabel = MainProcess.CreateLabel("Комірка:", 10, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.previousPalletStickerInfoLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            palletEditControls.nextCommandLabel1 = MainProcess.CreateLabel("Відскануйте нову", 10, 190, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            palletEditControls.nextCommandLabel2 = MainProcess.CreateLabel("адресу", 10, 220, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void updateStickerData()
            {
            palletEditControls.nomenclatureLabel.Text = startBarcodeData.Nomenclature.Description;
            palletEditControls.stickerIdInfoLabel.Text = string.Format("Код палети: {0}", startBarcodeData.StickerId);
            palletEditControls.cellLabel.Text = (startBarcodeData.Cell != null && startBarcodeData.Cell.Id > 0) ? startBarcodeData.Cell.Description : "<?>";

            var previousPalletExists = startBarcodeData.PreviousStickerCode > 0;
            palletEditControls.previousPalletStickerInfoLabel.Text = previousPalletExists
                ? string.Format("Код попередньої палети {0}", startBarcodeData.PreviousStickerCode)
                : string.Empty;

            }

        private void complateProcess()
            {
            if (!"Завершить операцию?".Ask())
                {
                return;
                }

            string errorDescription;
            if (!new ServerInteraction().ComplateMovement(documentId, false, out errorDescription))
                {
                Warning_CantComplateOperation();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            }

        private bool saveMovement()
            {
            if (documentId == 0 && !initDocument())
                {
                return false;
                }

            var movementWriter = new TableMovementWriter(startBarcodeData, finalBarcodeData);
            var success = new ServerInteraction().WriteMovementResult(documentId, movementWriter.Table);
            if (success)
                {
                startScanNextPallet();
                }

            return success;
            }

        private bool initDocument()
            {
            return new ServerInteraction().GetNewMovementId(0, out documentId);
            }

        }
    }