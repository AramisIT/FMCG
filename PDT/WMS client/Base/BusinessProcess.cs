using System;
using System.Data;
using pdtExternalStorage;

namespace WMS_client
    {
    public abstract class BusinessProcess : BaseProcess, IRemoteCommunications
        {
        #region Constructors
        protected BusinessProcess(WMSClient MainProcess, int FormNumber)
            : base(MainProcess, FormNumber) {}

        protected BusinessProcess(WMSClient MainProcess, string CellName, string CellBarcode, int FormNumber)
            : base(MainProcess, CellName, CellBarcode, FormNumber) {}
        #endregion

        #region Implemention of IRemoteCommunications
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <returns>Таблиця (Id, Description)</returns>
        public bool GetCarsForAcceptance(out DataTable table)
            {
            PerformQuery("GetCarsForAcceptance");

            if (IsExistParameters)
                {
                table = (DataTable) Parameters[1];
                return true;
                }

            table = null;
            return false;
            }

        /// <summary>Отримати місце розміщення зі штрихкоду</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetPlaceDataFromCode(string barcode, out string type, out long id)
            {
            PerformQuery("GetPlaceDataFromCode", barcode);

            if (IsAnswerIsTrue)
                {
                type = Parameters[1].ToString();
                id = Convert.ToInt32(Parameters[2]);

                return true;
                }

            type = null;
            id = 0;
            return false;
            }

        /// <summary>Проверить разрешена ли установка паллет вручную</summary>
        /// <returns>Разрешена ли установка паллет вручную</returns>
        public bool GetPermitInstallPalletManually()
            {
            PerformQuery("GetPermitInstallPalletManually");
            return IsAnswerIsTrue;
            }

        /// <summary>Необхідні дані про паллету, що переміщується</summary>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="goods">ID вантажу</param>
        /// <param name="date">Дата вантажу</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <returns>Груз, Дата, К-сть ящиків, К-сть бутилок</returns>
        public bool GetDataAboutMovingPallet(int palletId, out string goods, out string date, out double boxCount,
                                             out double bottleCount)
            {
            PerformQuery("GetDataAboutMovingPallet", palletId);

            if (IsAnswerIsTrue)
                {
                goods = Parameters[1].ToString();
                date = Parameters[2].ToString();
                boxCount = Convert.ToDouble(Parameters[3]);
                bottleCount = Convert.ToDouble(Parameters[4]);

                return true;
                }

            goods = string.Empty;
            date = string.Empty;
            boxCount = 0;
            bottleCount = 0;
            return false;
            }

        /// <summary>Дані для інвентаризації</summary>
        /// <param name="count">К-сть</param>
        /// <param name="nomenclature">Назва грузу/номенклатури</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="measure">Од.виміру</param>
        /// <param name="docId">Номер документу</param>
        /// <param name="lineNumber">Номер строки</param>
        /// <param name="cellId">ІД комірки</param>
        /// <param name="cell">Найменування комірки</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetDataForInventory(out double count, out string nomenclature, out string date, out long palletId,
                                        out string measure, out long docId, out long lineNumber, out long cellId,
                                        out string cell)
            {
            PerformQuery("GetDataForInventory");

            if (IsAnswerIsTrue)
                {
                count = Convert.ToDouble(Parameters[1]);
                nomenclature = Parameters[2].ToString();
                date = Parameters[3].ToString();
                palletId = Convert.ToInt64(Parameters[4]);
                measure = Parameters[5].ToString();
                docId = Convert.ToInt64(Parameters[6]);
                lineNumber = Convert.ToInt64(Parameters[7]);
                cellId = Convert.ToInt64(Parameters[8]);
                cell = Parameters[9].ToString();

                return true;
                }

            count = 0;
            nomenclature = string.Empty;
            date = string.Empty;
            palletId = 0;
            measure = string.Empty;
            docId = 0;
            lineNumber = 0;
            cellId = 0;
            cell = string.Empty;

            return false;
            }

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        public void GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                        out string selectionDocCount, out string movementDocCount)
            {
            PerformQuery("GetCountOfDocuments");

            if (IsExistParameters)
                {
                acceptanceDocCount = Parameters[0].ToString();
                inventoryDocCount = Parameters[1].ToString();
                selectionDocCount = Parameters[2].ToString();
                movementDocCount = Parameters[3].ToString();
                return;
                }

            acceptanceDocCount = 0.ToString();
            inventoryDocCount = 0.ToString();
            selectionDocCount = 0.ToString();
            movementDocCount = 0.ToString();
        }

        /// <summary>Інформація про ПЕРШУ паллету (тут строка) для відбору</summary>
        /// <param name="contractor">Контрагент</param>
        /// <param name="id">ID документа</param>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="goods">Найменування товару</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="unitCount">К-сть одиниць (не ящики)</param>
        /// <param name="baseCount">К-сть одиниць в одному ящику</param>
        /// <param name="cell">Найменування комірки</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetSelectionRowInfo(long contractor, out long id, out long palletId, out string goods,
                                        out string date, out double boxCount, out double unitCount, out int baseCount,
                                        out string cell)
            {
            PerformQuery("GetSelectionRowInfo", contractor);

            if (IsAnswerIsTrue)
                {
                id = Convert.ToInt64(Parameters[1]);
                palletId = Convert.ToInt64(Parameters[2]);
                goods = Parameters[3].ToString();
                date = Parameters[4].ToString();
                boxCount = Convert.ToInt64(Parameters[5]);
                unitCount = Convert.ToInt64(Parameters[6]);
                baseCount = Convert.ToInt32(Parameters[7]);
                cell = Parameters[8].ToString();

                return true;
                }

            id = 0;
            palletId = 0;
            goods = string.Empty;
            date = string.Empty;
            boxCount = 0;
            unitCount = 0;
            baseCount = 0;
            cell = string.Empty;

            return false;
            }

        /// <summary>Отримати додаткову інформацію по прийманню товару</summary>
        /// <param name="count">К-сть</param>
        /// <param name="goods">Id товару</param>
        /// <param name="car">Id машинини</param>
        /// <param name="party">Id партії</param>
        /// <param name="incomeDoc">Id документу приймання</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="cellId">Id комірки</param>
        /// <param name="cell">Найменування комірки</param>
        /// <param name="palett">Id паллети</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetAdditionalInfoAboutAccepnedGoods(double count, long goods, long car, long party,
                                                        out long incomeDoc, out string date, out long cellId,
                                                        out string cell, out long palett)
            {
            PerformQuery("GetAdditionalInfoAboutAccepnedGoods", count, goods, car, party);

            if (IsAnswerIsTrue)
                {
                incomeDoc = Convert.ToInt64(Parameters[1]);
                date = Parameters[2].ToString();
                cellId = Convert.ToInt64(Parameters[3]);
                cell = Parameters[4].ToString();
                palett = Convert.ToInt64(Parameters[5]);

                return true;
                }

            incomeDoc = 0;
            date = string.Empty;
            cellId = 0;
            cell = string.Empty;
            palett = 0;

            return false;
            }

        /// <summary>Збереження даних інвентаризації</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="lineNumber">Номер строки</param>
        /// <param name="count">К-сть</param>
        public void SetInventory(long docId, long lineNumber, long count)
            {
            PerformQuery("SetInventory", docId, lineNumber, count);
            }

        /// <summary>Збереження даних по переміщенню паллети</summary>
        /// <param name="palletId">Id паллети</param>
        /// <param name="newPos">Нове розміщення паллети</param>
        /// <param name="isCell">Чи являється нова позиція коміркою</param>
        public void SetMoving(long palletId, long newPos, bool isCell)
            {
            PerformQuery("SetMoving", palletId, newPos, isCell);
            }

        /// <summary>Зберегти дані по прийманню товару</summary>
        /// <param name="nomenclature">Номенклатура</param>
        /// <param name="party">Партія</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <param name="planId">ІД документу</param>
        /// <param name="previousPallet">ІД попередньої паллети</param>
        /// <param name="cellId">ІД комірки</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        public void SetAcceptanceData(long nomenclature, long party, double boxCount, double bottleCount, long planId,
                                      long previousPallet, long cellId, bool isCell)
            {
            PerformQuery("SetAcceptanceData", nomenclature, party, boxCount, bottleCount,
                         planId, previousPallet, cellId, isCell);
            }

        /// <summary>Зберегти дані про переміщення</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="cellId">Id комірки</param>
        public void SetSelectionData(long docId, long palletId, long cellId)
            {
            PerformQuery("SetSelectionData", docId, palletId, cellId);
            }

        /// <summary>Перевірити чи існує користувач з таким штрих-кодом</summary>
        /// <param name="barcode">Штрихкод</param>
        public bool CheckBarcodeForExistUser(string barcode)
            {
            PerformQuery("CheckBarcodeForExistUser", barcode);
            return IsAnswerIsTrue;
            }

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="id">Id товару</param>
        /// <param name="description">Найменування товару</param>
        /// <returns>Чи існує товар</returns>
        public bool CheckBarcodeForExistGoods(string barcode, out long id, out string description)
            {
            PerformQuery("CheckBarcodeForExistGoods", barcode);

            if (IsAnswerIsTrue)
                {
                id = Convert.ToInt64(Parameters[1]);
                description = Parameters[2].ToString();
                return true;
                }

            id = 0;
            description = string.Empty;
            return false;
            }

        /// <summary>Перевірка штрих-коду паллети (комірки) для переміщення</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellIsAccepted"></param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        /// <returns>Чи пройшло перевірку нове розміщення</returns>
        public bool CheckPalletBarcodeForMoving(string barcode, bool cellIsAccepted, out long palletId, out bool isCell)
            {
            PerformQuery("CheckPalletBarcodeForMoving", barcode, cellIsAccepted);

            if (IsAnswerIsTrue)
                {
                palletId = Convert.ToInt64(Parameters[1]);
                isCell = Convert.ToBoolean(Parameters[2]);
                return true;
                }

            palletId = 0;
            isCell = false;
            return false;
            }

        /// <summary>Перевірити комірку інвентаризації</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellId">ID комірки</param>
        /// <param name="result">Чи співпадають паллети</param>
        /// <returns>Чи пройшла перевірка</returns>
        public bool CheckInventoryPallet(string barcode, long cellId, out bool result)
            {
            PerformQuery("CheckInventoryPallet", barcode, cellId);

            if (IsAnswerIsTrue)
                {
                result = Convert.ToBoolean(Parameters[1]);
                return true;
                }

            result = false;
            return false;
            }

        /// <summary>Перевірка комірки для відвантаження</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ІД комірки</param>
        public bool CheckCellFormShipment(string barcode, out long id)
            {
            PerformQuery("CheckCellFormShipment", barcode);

            if (IsAnswerIsTrue)
                {
                id = Convert.ToInt64(Parameters[1]);
                return true;
                }

            id = 0;
            return false;
            }
        #endregion
        }
    }