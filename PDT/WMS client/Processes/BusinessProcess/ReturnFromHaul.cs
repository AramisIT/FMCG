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
    public class ReturnFromHaul : BusinessProcess
        {
        private class WareIdentificationControls : HideableControlsCollection
            {
            public MobileLabel ScanWareLabel;

            public MobileLabel ComplateTipLabel;

            public MobileButton ItIsKegButton;
            }

        private WareIdentificationControls wareIdentificationControls;

        private long acceptanceId;
        private CatalogItem currentWareItem;
        private CatalogItem currentParty;

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        public ReturnFromHaul()
            : base(1)
            {
            ToDoCommand = "Повернення з рейсу";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            createIdentificationStepControls();


            startNewIdentification();
            }

        private void createIdentificationStepControls()
            {
            wareIdentificationControls = new WareIdentificationControls();

            int top = 42;
            const int delta = 27;

            top += delta;
            wareIdentificationControls.ScanWareLabel = MainProcess.CreateLabel("Сканируйте товар", 5, top, 230,
               MobileFontSize.Large, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += 2 * delta;

            wareIdentificationControls.ScanWareLabel = MainProcess.CreateLabel("Завершить F4", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            wareIdentificationControls.ItIsKegButton = MainProcess.CreateButton("Возврат кеги", 5, 270, 230, 40, string.Empty, itIsKeg_ButtonClick);
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

        private void startNewIdentification()
            {
            currentWareItem = currentParty = null;
            ShowControls(wareIdentificationControls);
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (wareIdentificationControls.Visible)
                {
                identificationStepOnBarcode(barcode);
                return;
                }

            //  if (!barcode.IsSticker()) return;


            }

        private void identificationStepOnBarcode(string barcode)
            {
            if (!barcode.IsItEAN13()) return;

            var table = new ServerInteraction().GetWares(barcode, SelectionFilters.RecentlyShipped);

            if (table == null || table.Rows.Count == 0)
                {
                string.Format("Штрих-код не найден!\r\n{0}", barcode).ShowMessage();
                return;
                }

            identifyWare(table);
            }

        private void identifyWare(DataTable waresTable)
            {
            if (!selectingItem(waresTable, out currentWareItem)) return;

            var parties = new ServerInteraction().GetParties(currentWareItem.Id, SelectionFilters.RecentlyShipped);
            if (parties == null || parties.Rows.Count == 0)
                {
                "Не найдено ни одной партии!".ShowMessage();
                return;
                }

            if (!selectingItem(parties, out currentParty)) return;
            }

        private bool selectingItem(DataTable table, out CatalogItem selectedItem)
            {
            var itemsList = table.ToItemsList();
            if (itemsList.Count > 1)
                {
                if (!SelectFromList(itemsList, -1, 40, out selectedItem))
                    {
                    selectedItem = null;
                    return false;
                    }
                }
            else
                {
                selectedItem = itemsList.First();
                }

            return true;
            }

        public override void OnHotKey(KeyAction TypeOfAction)
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