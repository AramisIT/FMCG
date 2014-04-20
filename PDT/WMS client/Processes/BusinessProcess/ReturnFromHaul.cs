using System.Drawing;
using System.Linq;
using pdtExternalStorage;
using WMS_client.Base.Visual;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class ReturnFromHaul : ParusProcess
        {
        private class WareIdentificationControls : HideableControlsCollection
            {
            public MobileLabel ScanWareLabel;

            public MobileLabel ComplateTipLabel;

            
            }

        private class ReturnWareControls : HideableControlsCollection
            {
            public MobileLabel WareLabel;
            public MobileLabel PartyLabel;

            public MobileLabel QuantityLabel;
            public MobileTextBox UnitsCountTextBox;

            public MobileLabel QuantityBoxesLabel;
            public MobileTextBox PacksCountTextBox;

            public MobileButton LinerButton;
            public MobileLabel LinersQuantityLabel;
            public MobileTextBox LinersCountTextBox;


            public MobileLabel ScanPalletLabel;
            public MobileButton NewPalletButton;

            protected override MobileTextBox GetDefaultTextBox()
                {
                return UnitsCountTextBox;
                }
            }

        private class AddressIdentificationControls : HideableControlsCollection
            {
            public MobileLabel ScannCellLabel1;
            public MobileLabel ScannCellLabel2;
            public MobileLabel ScannCellLabel3;
            public MobileLabel ScannCellLabel4;
            public MobileLabel ScannCellLabel5;
            public MobileLabel ScannCellLabel6;
            }

        private WareIdentificationControls wareIdentificationControls;
        private ReturnWareControls returnWareControls;
        private AddressIdentificationControls addressIdentificationControls;

        private long acceptanceId;
        private CatalogItem currentWare = new CatalogItem();
        private CatalogItem currentParty = new CatalogItem();
        private CatalogItem currentLiner = new CatalogItem();
        private long newStickerId;
        private CatalogItem currentCell = new CatalogItem();
        private long previousStickerId;

        public ReturnFromHaul()
            : base()
            {
            ToDoCommand = "Повернення з рейсу";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            createIdentificationStepControls();

            createReturnWareStepControls();

            createAddressIdentificationStepControls();

            startNewIdentification();
            }

        private void createAddressIdentificationStepControls()
            {
            addressIdentificationControls = new AddressIdentificationControls();
            int top = 80;

            addressIdentificationControls.ScannCellLabel1 = MainProcess.CreateLabel("Сканируйте", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            addressIdentificationControls.ScannCellLabel2 = MainProcess.CreateLabel("предыдущую", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            addressIdentificationControls.ScannCellLabel3 = MainProcess.CreateLabel("паллету или ячейку", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            addressIdentificationControls.ScannCellLabel4 = MainProcess.CreateLabel("для размещения", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            addressIdentificationControls.ScannCellLabel5 = MainProcess.CreateLabel("новой", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            addressIdentificationControls.ScannCellLabel6 = MainProcess.CreateLabel("паллеты", 5, top, 230,
                MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            }

        private void createReturnWareStepControls()
            {
            returnWareControls = new ReturnWareControls();

            int top = TOPMOST_CONTROL_Y_POSITION;


            returnWareControls.WareLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
                MobileFontSize.Little, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.PartyLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
                MobileFontSize.Little, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.QuantityLabel = MainProcess.CreateLabel("Кол-во единиц", 5, top, 160,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            returnWareControls.UnitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty,
                ControlsStyle.LabelNormal, null, false);


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.QuantityBoxesLabel = MainProcess.CreateLabel("Кол-во упак.", 5, top, 160,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            returnWareControls.PacksCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty,
                ControlsStyle.LabelNormal, null, false);


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.LinerButton = MainProcess.CreateButton("<без прокладки>", 5, top, 230, 30, string.Empty,
                chooseLiner_ButtonClick);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS + 10;
            returnWareControls.LinersQuantityLabel = MainProcess.CreateLabel("Кол-во прокл.", 5, top, 160,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            returnWareControls.LinersCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty,
                ControlsStyle.LabelNormal, null, false);

            updateLinerName();

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.ScanPalletLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            returnWareControls.NewPalletButton = MainProcess.CreateButton("Новая паллета", 5, top, 230, 40, string.Empty,
                newPallet_ButtonClick);
            }

        private void updateScanPalletLabelText()
            {
            returnWareControls.ScanPalletLabel.Text = newStickerId > 0 ?
                string.Format("Код новой паллеты: {0}", newStickerId)
                : "Cканируйте паллету";
            }

        private void updateLinerName()
            {
            returnWareControls.LinerButton.Text = currentLiner.Empty ? "<без прокладки>" : currentLiner.Description;
            returnWareControls.LinersCountTextBox.Visible = !currentLiner.Empty;
            returnWareControls.LinersQuantityLabel.Visible = !currentLiner.Empty;
            }

        private void createIdentificationStepControls()
            {
            wareIdentificationControls = new WareIdentificationControls();

            int top = 42;

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            wareIdentificationControls.ScanWareLabel = MainProcess.CreateLabel("Сканируйте товар", 5, top, 230,
               MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += 2 * VERTICAL_DISTANCE_BETWEEN_CONTROLS;

            wareIdentificationControls.ComplateTipLabel = MainProcess.CreateLabel("Завершить F4", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            //wareIdentificationControls.ItIsKegButton = MainProcess.CreateButton("Возврат кеги", 5, 270, 230, 40, string.Empty, itIsKeg_ButtonClick);
            }


        private void itIsKeg_ButtonClick()
            {
            var table = new ServerInteraction().GetWaresInKegs(SelectionFilters.All);

            if (table == null || table.Rows.Count == 0)
                {
                "Нет недавно отгруженных кег!".ShowMessage();
                return;
                }

            identifyWare(table);
            }

        private void chooseLiner_ButtonClick()
            {
            chooseLiner(liner =>
                {
                    currentLiner = liner;
                    updateLinerName();
                });
            }

        private bool checkThatUserHasEnteredAnyData()
            {
            var isUserHasEnteredAnyData = returnWareControls.UnitsCountTextBox.GetNumber() > 0;
            if (!isUserHasEnteredAnyData)
                {
                "Введите количество товара".ShowMessage();
                }

            return isUserHasEnteredAnyData;
            }
        private void newPallet_ButtonClick()
            {
            if (!checkThatUserHasEnteredAnyData()) return;

            newStickerId = new ServerInteraction().CreateNewSticker(currentWare.Id,
                currentParty.Description.ToDateTime(),
                returnWareControls.UnitsCountTextBox.GetNumber(),
                returnWareControls.PacksCountTextBox.GetNumber(),
                currentLiner.Id, returnWareControls.LinersCountTextBox.GetNumber());

            if (newStickerId > 0)
                {
                updateScanPalletLabelText();
                "Сканируйте новую этикетку".ShowMessage();
                }
            }

        private void startNewIdentification()
            {
            currentWare = new CatalogItem();
            currentParty = new CatalogItem();
            currentLiner = new CatalogItem();
            currentCell = new CatalogItem();
            previousStickerId = 0;
            newStickerId = 0;
            returnWareControls.UnitsCountTextBox.SetNumber(0);
            returnWareControls.PacksCountTextBox.SetNumber(0);
            returnWareControls.LinersCountTextBox.SetNumber(0);
            updateScanPalletLabelText();
            ShowControls(wareIdentificationControls);
            }

        protected override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (wareIdentificationControls.Visible)
                {
                identificationStepOnBarcode(barcode);
                }
            else if (returnWareControls.Visible)
                {
                if (newStickerId > 0)
                    {
                    if (!barcode.IsSticker())
                        {
                        "Отсканируйте новую этикетку!".ShowMessage();
                        return;
                        }
                    tryCheckNewSticker(barcode.ToBarcodeData());
                    }
                else
                    {
                    if (!checkThatUserHasEnteredAnyData()) return;

                    if (!barcode.IsSticker())
                        {
                        "Отсканируйте паллету в которую размещается возврат!".ShowMessage();
                        return;
                        }

                    acceptWare(barcode.ToBarcodeData());
                    }
                }
            else if (addressIdentificationControls.Visible)
                {
                if (establishNewPalletLocation(barcode))
                    {
                    acceptWare(new BarcodeData() { StickerId = newStickerId });
                    }
                }
            }

        private bool establishNewPalletLocation(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();
                if (!barcodeData.ReadStickerInfo()) return false;

                if (!barcodeData.LocatedInCell)
                    {
                    showPalletCellNotFountMessage();
                    return false;
                    }

                currentCell = barcodeData.Cell;
                previousStickerId = barcodeData.StickerId;

                return true;
                }
            else if (barcode.IsCell())
                {
                var cell = barcode.ToCell();
                if (!cell.Empty)
                    {
                    currentCell = cell;
                    return true;
                    }
                }

            return false;
            }

        private void acceptWare(BarcodeData barcodeData)
            {
            barcodeData.ReadStickerInfo();
            var itIsNewPallet = barcodeData.StickerId == newStickerId;
            if (!itIsNewPallet)
                {
                if (barcodeData.Nomenclature.Id != currentWare.Id)
                    {
                    string.Format("Продукция в паллете другая: {0}", barcodeData.Nomenclature.Description).ShowMessage();
                    return;
                    }

                if (barcodeData.Party.Id != currentParty.Id)
                    {
                    string.Format("Дата производства в паллете другая: {0}", barcodeData.Party.Description)
                        .ShowMessage();
                    return;
                    }

                if (barcodeData.Liner.Id > 0 && barcodeData.Liner.Id != currentLiner.Id && !currentLiner.Empty)
                    {
                    string.Format("В указанной паллете используются другой вид прокладок: {0}", barcodeData.Liner.Description)
                        .ShowMessage();
                    return;
                    }

                if (!barcodeData.LocatedInCell)
                    {
                    "Паллеты нет на остатках".ShowMessage();
                    return;
                    }
                currentCell = barcodeData.Cell;
                }

            if (acceptanceId == 0)
                {
                acceptanceId = new ServerInteraction().CreateNewAcceptance();
                if (acceptanceId == 0)
                    {
                    return;
                    }
                }

            if (!new ServerInteraction().WriteStickerFact(acceptanceId, barcodeData.StickerId, false,
                            currentCell.Id, previousStickerId, 0, currentLiner.Id, returnWareControls.LinersCountTextBox.GetNumber(),
                            returnWareControls.PacksCountTextBox.GetNumber(), returnWareControls.UnitsCountTextBox.GetNumber()))
                {
                return;
                }

            startNewIdentification();
            }

        private void tryCheckNewSticker(BarcodeData barcodeData)
            {
            if (newStickerId != barcodeData.StickerId)
                {
                if ("Удалить новую этикетку?".Ask())
                    {
                    newStickerId = 0;
                    updateScanPalletLabelText();
                    }
                return;
                }
            startCellIdentification();
            }

        private void startCellIdentification()
            {
            ShowControls(addressIdentificationControls);
            }

        private void identificationStepOnBarcode(string barcode)
            {
            if (!barcode.IsItEAN13()) return;

            var table = new ServerInteraction().GetWares(barcode, SelectionFilters.RecentlyShipped);
            if (table == null) return;

            if (table == null || table.Rows.Count == 0)
                {
                var allBarcodes = new ServerInteraction().GetWares(barcode, SelectionFilters.All);
                if (allBarcodes == null) return;

                var databaseHasn_tBarcode = allBarcodes.Rows.Count == 0;
                if (databaseHasn_tBarcode)
                    {
                    "Штрих-кода нет в базе".ShowMessage();
                    return;
                    }

                string.Format("Не было недавних отгрузок с таким штрих-кодом!\r\n{0}", barcode).ShowMessage();
                return;
                }

            if (!identifyWare(table)) return;

            showWare();
            }

        private void showWare()
            {
            ShowControls(returnWareControls);

            returnWareControls.WareLabel.Text = currentWare.Description;
            returnWareControls.PartyLabel.Text = string.Format("Конечный срок хранения {0}", currentParty.Description);

            updateLinerName();
            }

        private bool identifyWare(DataTable waresTable)
            {
            if (!selectingItem(waresTable, out currentWare)) return false;

            var parties = new ServerInteraction().GetParties(currentWare.Id, SelectionFilters.RecentlyShipped);
            if (parties == null || parties.Rows.Count == 0)
                {
                "Не найдено ни одной партии!".ShowMessage();
                currentWare.Clear();
                return false;
                }

            if (!selectingItem(parties, out currentParty))
                {
                currentWare.Clear();
                return false;
                }

            return true;
            }

        private bool selectingItem(DataTable table, out CatalogItem selectedItem)
            {
            var itemsList = table.ToItemsList();
            if (itemsList.Count > 1)
                {
                if (!SelectFromList(itemsList, -1, 40, out selectedItem))
                    {
                    selectedItem = new CatalogItem();
                    return false;
                    }
                }
            else
                {
                selectedItem = itemsList.First();
                }

            return true;
            }

        protected override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Complate:
                    complateProcess();
                    return;

                case KeyAction.Esc:
                    if (acceptanceId == 0 || "Призупинити операцію?".Ask())
                        {
                        exit();
                        }
                    return;
                }
            }

        #endregion

        private void exit()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new SelectingProcess();
            }

        private void complateProcess()
            {
            if (acceptanceId > 0)
                {
                string errorMessage;
                if (!new ServerInteraction().ComplateAcceptance(acceptanceId, true, out errorMessage))
                    {
                    Warning_CantComplateOperation();
                    return;
                    }
                }

            exit();
            }
        }
    }