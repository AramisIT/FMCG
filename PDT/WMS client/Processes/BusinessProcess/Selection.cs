using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Picking : BusinessProcess
        {
        private class PickingTaskControls : HideableControlsCollection
            {
            public MobileLabel planPickingCell;
            public MobileLabel pickingWare;
            public MobileLabel productionDate;
            public MobileLabel planedQuantity;
            }

        private class ScanPallet : HideableControlsCollection
            {
            public MobileLabel order;
            }

        private class QuantityEditControls : HideableControlsCollection
            {
            public MobileLabel boxesLabel;
            public MobileLabel unitsLabel;
            public MobileTextBox unitsCountTextBox;
            public MobileTextBox packsCountTextBox;
            public MobileButton proceedButton;
            }

        public int packsCount
            {
            get
                {
                if (string.IsNullOrEmpty(quantityEditControls.packsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(quantityEditControls.packsCountTextBox.Text);
                }
            set
                {
                quantityEditControls.packsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        public int unitsCount
            {
            get
                {
                if (string.IsNullOrEmpty(quantityEditControls.unitsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(quantityEditControls.unitsCountTextBox.Text);
                }
            set
                {
                quantityEditControls.unitsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        private long documentId;
        private PickingTaskControls pickingTask;
        private ScanPallet scanPallet;
        private QuantityEditControls quantityEditControls;
        private BarcodeData pickingTaskData;
        private int currentLineNumber;
        private BarcodeData factPickingData;

        public Picking(long documentId)
            : base(1)
            {
            ToDoCommand = "Відбір товару";
            this.documentId = documentId;
            startPalletChoosing();
            }

        private void startPalletChoosing()
            {
            quantityEditControls.Hide();
            scanPallet.Show();

            if (!readNextPickingTask())
                {
                pickingTaskData = new BarcodeData();
                "Нема зв'язку з сервером. Знайдіть WiFi та натисніть Далі (F4)".ShowMessage();
                return;
                }

            if (pickingTaskData.Empty)
                {
                ComplateOperation();
                return;
                }

            pickingTask.planPickingCell.Text = string.Format("Комірка {0}", pickingTaskData.Cell.Description);
            pickingTask.pickingWare.Text = pickingTaskData.Nomenclature.Description;
            pickingTask.productionDate.Text = string.Format("Дата вироб-ва {0}", pickingTaskData.Party.Description);

            var unitsQuantityStr = pickingTaskData.UnitsRemainder == 0
                ? string.Empty
                : " + " + pickingTaskData.UnitsRemainder.ToString();
            pickingTask.planedQuantity.Text = string.Format("{0} по {1}{2}", pickingTaskData.FullPacksCount,
                pickingTaskData.UnitsPerBox, unitsQuantityStr);
            }

        private void ComplateOperation()
            {
            string errorDescription;
            if (!new ServerInteraction().ComplateMovement(documentId, false, out errorDescription))
                {
                CANT_COMPLATE_OPERATION.Warning();
                }

            MainProcess.ClearControls();
            MainProcess.Process = new Movement();
            }

        private bool readNextPickingTask()
            {
            long stickerId;
            long wareId;
            string wareDescription;
            long cellId;
            string cellDescription;
            long partyId;
            DateTime productionDate;
            int unitsPerBox;
            int unitsToPick;

            if (!new ServerInteraction().GetPickingTask(documentId,
                out stickerId,
                out wareId, out wareDescription,
                out cellId, out cellDescription,
                out partyId, out productionDate,
                out unitsPerBox, out unitsToPick,
                out currentLineNumber)) return false;

            pickingTaskData = new BarcodeData();
            pickingTaskData.Nomenclature = new CatalogItem() { Id = wareId, Description = wareDescription };
            pickingTaskData.Cell = new CatalogItem() { Id = cellId, Description = cellDescription };
            pickingTaskData.UnitsPerBox = unitsPerBox;
            pickingTaskData.StickerId = stickerId;
            pickingTaskData.TotalUnitsQuantity = unitsToPick;
            pickingTaskData.Party = new CatalogItem() { Description = productionDate.ToStandartString(), Id = partyId };
            return true;
            }

        public override void DrawControls()
            {
            pickingTask = new PickingTaskControls();

            int top = 42;
            const int delta = 27;

            top += delta;
            pickingTask.planPickingCell = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            pickingTask.pickingWare = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            pickingTask.productionDate = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            pickingTask.planedQuantity = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            scanPallet = new ScanPallet();

            scanPallet.order = MainProcess.CreateLabel("Відскануйте палету", 10, 240, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            quantityEditControls = new QuantityEditControls();

            top = 190;
            quantityEditControls.boxesLabel = MainProcess.CreateLabel("Відібрано упак.", 10, top, 150,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            quantityEditControls.packsCountTextBox = MainProcess.CreateTextBox(170, top, 55, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            quantityEditControls.unitsLabel = MainProcess.CreateLabel("Відібрано один.", 10, top, 150,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            quantityEditControls.unitsCountTextBox = MainProcess.CreateTextBox(170, top, 55, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += 60;
            quantityEditControls.proceedButton = MainProcess.CreateButton("Продовжити              ( F4 )", 10, top, 220, 35, "modelButton", proceed);
            }

        private void proceed()
            {
            factPickingData.TotalUnitsQuantity = unitsCount + packsCount * factPickingData.UnitsPerBox;
            var resultWriter = new TableMovementWriter(pickingTaskData, factPickingData);
            var success = new ServerInteraction().WritePickingResult(documentId, currentLineNumber, resultWriter.Table, factPickingData.Party.Id);
            if (success)
                {
                startPalletChoosing();
                }
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (!scanPallet.Visible) return;
            if (!barcode.IsSticker()) return;

            var barcodeData = barcode.ToBarcodeData();
            if (barcodeData.StickerId == pickingTaskData.StickerId)
                {
                factPickingData = pickingTaskData.GetCopy();
                }
            else
                {
                barcodeData.ReadStickerInfo();
                if (pickingTaskData.SameWare(barcodeData, false)
                    &&
                    (pickingTaskData.StickerId == 0
                    || string.Format("Выполнить отбор с паллеты {0}", barcodeData.StickerId).Ask())
                    )
                    {
                    factPickingData = barcodeData;
                    factPickingData.Tray = new CatalogItem();
                    factPickingData.Liner = new CatalogItem();
                    }
                else
                    {
                    "Необхідно відсканувати вказаний товар".Warning();
                    return;
                    }
                }

            packsCount = pickingTaskData.FullPacksCount;
            unitsCount = pickingTaskData.UnitsRemainder;
            quantityEditControls.packsCountTextBox.Focus();


            quantityEditControls.Show();
            scanPallet.Hide();
            }

        public override void OnHotKey(KeyAction key)
            {
            switch (key)
                {
                case KeyAction.Complate:
                    ComplateOperation();
                    break;

                case KeyAction.Proceed:
                    if (this.pickingTaskData.StickerId > 0)
                        {
                        proceed();
                        }
                    startPalletChoosing();
                    break;
                }
            }
        }
    }