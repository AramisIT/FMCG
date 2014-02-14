using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Приймання товару</summary>
    public class Acceptance : ParusProcess
        {
        private class AcceptancePalletControls : HideableControlsCollection
            {
            public MobileTextBox packsCountTextBox;
            public MobileTextBox unitsCountTextBox;
            public MobileTextBox linersQuantityTextBox;

            public MobileLabel nomenclatureLabel;
            public MobileLabel cellLabel;
            public MobileLabel stickerIdInfoLabel;

            public MobileButton trayButton;
            public MobileButton linerButton;
            public MobileLabel packsLabel;
            public MobileLabel unitsLabel;
            public MobileLabel linersLabel;
            public MobileLabel cellCaptionLabel;
            }

        private class ScanPalletControls : HideableControlsCollection
            {
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

        private CatalogItem trayItem;
        private CatalogItem linerItem;
        private BarcodeData currentBarcodeData;
        private long lastStickerId;
        private long acceptanceId;

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        /// <summary>Приймання товару</summary>
        public Acceptance()
            : base()
            {
            ToDoCommand = "Приймання товару";
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

        protected override void OnBarcode(string barcode)
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

        protected override void OnHotKey(KeyAction TypeOfAction)
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
                        if (acceptanceId == 0 || "Скасувати операцію?".Ask())
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

            if (acceptanceId == 0 && !initAcceptance(barcodeData.StickerId))
                {
                return;
                }

            bool currentAcceptance;
            readStickerInfo(acceptanceId, barcodeData, out currentAcceptance);
            if (barcodeData.StickerId == 0)
                {
                string.Format("Палети {0} нема у документі.", barcodeData.StickerId).Warning();
                return;
                }

            if (!currentAcceptance)
                {
                "Відсканований піддон не входить до документу!".Warning();
                return;
                }

            currentBarcodeData = barcodeData;

            ShowControls(palletEditControls);

            updateStickerData();
            }

        private void editPalletOnBarcode(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();

                var samePallet = currentBarcodeData != null && currentBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                bool currentAcceptance;
                readStickerInfo(acceptanceId, barcodeData, out currentAcceptance);

                bool cellFounded = barcodeData.Cell.Id != 0;
                if (!cellFounded)
                    {
                    "Відсканованої палети нема на залишках".Warning();
                    return;
                    }

                trySetCell(barcodeData.Cell, barcodeData.StickerId);
                }
            else if (barcode.IsCell())
                {
                trySetCell(barcode.ToCell(), 0);
                }
            }

        private void startScanNextPallet()
            {
            currentBarcodeData = null;
            ShowControls(scanNextPalletControls);
            }

        private void createScanPalletLabelControls()
            {
            scanNextPalletControls = new ScanPalletControls();

            int top = 140;

            scanNextPalletControls.WillLabel = MainProcess.CreateLabel("Відскануйте палету", 10, top, 230,
                           MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            }

        private void createPalletControls()
            {
            palletEditControls = new AcceptancePalletControls();
            int top = 42;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.nomenclatureLabel = MainProcess.CreateLabel("<номенклатура>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;

            palletEditControls.packsLabel = MainProcess.CreateLabel("упаковок:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.packsCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            palletEditControls.unitsLabel = MainProcess.CreateLabel("+ шт.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.unitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.stickerIdInfoLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.trayButton = MainProcess.CreateButton("<піддон>", 5, top, 230, 35, "modelButton", trayButton_Click);

            top += 2 * VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.linersLabel = MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            linerItem = new CatalogItem();
            palletEditControls.linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click);
            updateLinerButton();

            top += 2 * VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.cellCaptionLabel = MainProcess.CreateLabel("Комірка:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void complateProcess()
            {
            if (!"Завершить операцию?".Ask())
                {
                return;
                }

            string errorDescription;
            if (!new ServerInteraction().ComplateAcceptance(acceptanceId, false, out errorDescription))
                {
                Warning_CantComplateOperation();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            }

        private void updateLinerButton()
            {
            palletEditControls.linerButton.Text = linerItem.Empty ? "<тип прокладки>" : linerItem.Description;
            }

        private void trayButton_Click()
            {
            chooseTray(tray =>
                {
                    trayItem = tray;
                    palletEditControls.trayButton.Text = trayItem.Description;
                });
            }

        private void linerButton_Click()
            {
            chooseLiner(liner =>
                {
                    linerItem = liner;
                    updateLinerButton();
                });
            }

        private void trySetCell(CatalogItem cell, long previousStickerId)
            {
            bool cellApproved = currentBarcodeData.Cell.Id == cell.Id || currentBarcodeData.Cell.Id == 0;
            if (cellApproved || string.Format(@"Розмістити у комірці ""{0}""?", cell.Description).Ask())
                {
                if (saveFact(cell, previousStickerId))
                    {
                    startScanNextPallet();
                    }
                else
                    {
                    "Необхідно знаходитись у зоні Wi-Fi".Warning();
                    return;
                    }
                }
            }

        private bool saveFact(CatalogItem cell, long previousStickerId)
            {
            if (currentBarcodeData == null) return true;

            var palletChanged = linerItem.Id > 0;
            palletChanged |= currentBarcodeData.Tray.Id != trayItem.Id;
            palletChanged |= currentBarcodeData.FullPacksCount != packsCount;
            palletChanged |= currentBarcodeData.UnitsRemainder != unitsCount;

            if (!new ServerInteraction().WriteStickerFact(acceptanceId, currentBarcodeData.StickerId, palletChanged,
                (cell ?? new CatalogItem()).Id, previousStickerId, trayItem.Id, linerItem.Id, linersCount, packsCount, unitsCount + packsCount * currentBarcodeData.UnitsPerBox))
                {
                return false;
                }

            return true;
            }

        public int packsCount
            {
            get { return palletEditControls.packsCountTextBox.GetNumber(); }
            set { palletEditControls.packsCountTextBox.SetNumber(value); }
            }

        public int unitsCount
            {
            get { return palletEditControls.unitsCountTextBox.GetNumber(); }
            set { palletEditControls.unitsCountTextBox.SetNumber(value); }
            }

        public int linersCount
            {
            get { return palletEditControls.linersQuantityTextBox.GetNumber(); }
            set { palletEditControls.linersQuantityTextBox.SetNumber(value); }
            }

        private bool initAcceptance(long stickerId)
            {
            var result = new ServerInteraction().GetAcceptanceId(stickerId,
                out acceptanceId);
            if (!result)
                {
                string.Format("Для палети {0} не знайдено документу!", stickerId).Warning();
                }

            return result;
            }

        private void updateStickerData()
            {
            palletEditControls.cellLabel.Text = (currentBarcodeData.Cell != null && currentBarcodeData.Cell.Id > 0) ? currentBarcodeData.Cell.Description : "<?>";
            palletEditControls.nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            palletEditControls.trayButton.Text = currentBarcodeData.Tray.Description;
            trayItem = currentBarcodeData.Tray;

            palletEditControls.stickerIdInfoLabel.Text = string.Format("Код палети: {0}", currentBarcodeData.StickerId);

            if (currentBarcodeData.UnitsPerBox > 0)
                {
                packsCount = currentBarcodeData.FullPacksCount;
                unitsCount = currentBarcodeData.UnitsRemainder;
                }
            else
                {
                packsCount = 0;
                unitsCount = 0;
                }

            updateLinerButton();
            }

        private void readStickerInfo(long acceptanceId, BarcodeData barcodeData, out bool currentAcceptance)
            {
            string nomenclatureDescription;
            long trayId;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            long nomenclatureId;
            int totalUnitsQuantity;

            if (
                !new ServerInteraction().GetStickerData(acceptanceId, barcodeData.StickerId,
                    out nomenclatureId, out nomenclatureDescription, out trayId,
                    out totalUnitsQuantity, out unitsPerBox, out cellId, out cellDescription, out currentAcceptance))
                {
                barcodeData.StickerId = 0;
                return;
                }

            barcodeData.TotalUnitsQuantity = totalUnitsQuantity;
            barcodeData.Nomenclature.Id = nomenclatureId;
            barcodeData.Nomenclature.Description = nomenclatureDescription;

            barcodeData.Tray.Id = trayId;
            barcodeData.Tray.Description = new Repository().GetTrayDescription(trayId);

            barcodeData.UnitsPerBox = Convert.ToInt32(unitsPerBox);
            barcodeData.Cell.Description = cellDescription;
            barcodeData.Cell.Id = cellId;
            }




        }
    }