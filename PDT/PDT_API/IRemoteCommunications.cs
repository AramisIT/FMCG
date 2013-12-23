using System.Data;
using System;

namespace pdtExternalStorage
    {
    public interface IRemoteCommunications
        {
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <param name="table">Таблиця (Id, Description)</param>
        /// <returns>Чи була операція успішна</returns>
        bool GetCarsForAcceptance(out DataTable table);

        /// <summary>Отримати місце розміщення зі штрихкоду</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>Чи була операція успішна</returns>
        bool GetPlaceDataFromCode(string barcode, out string type, out long id);

        /// <summary>Проверить разрешена ли установка паллет вручную</summary>
        /// <returns>Разрешена ли установка паллет вручную</returns>
        bool GetPermitInstallPalletManually();

        /// <summary>Необхідні дані про паллету, що переміщується</summary>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="goods">ID вантажу</param>
        /// <param name="date">Дата вантажу</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <returns>Груз, Дата, К-сть ящиків, К-сть бутилок</returns>
        bool GetDataAboutMovingPallet(int palletId, out string goods, out string date, out double boxCount,
                                      out double bottleCount);

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
        bool GetDataForInventory(out double count, out string nomenclature, out string date, out long palletId,
                                 out string measure, out long docId, out long lineNumber, out long cellId,
                                 out string cell);

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        bool GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                 out string selectionDocCount,
                                 out string movementDocCount);

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
        bool GetSelectionRowInfo(long contractor, out long id, out long palletId, out string goods, out string date, out double boxCount, out double unitCount, out int baseCount, out string cell);

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
        bool GetAdditionalInfoAboutAccepnedGoods(double count, long goods, long car, long party,
                                                 out long incomeDoc, out string date, out long cellId, out string cell,
                                                 out long palett);

        /// <summary>Збереження даних інвентаризації</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="lineNumber">Номер строки</param>
        /// <param name="count">К-сть</param>
        void SetInventory(long docId, long lineNumber, long count);

        /// <summary>Збереження даних по переміщенню паллети</summary>
        /// <param name="palletId">Id паллети</param>
        /// <param name="newPos">Нове розміщення паллети</param>
        /// <param name="isCell">Чи являється нова позиція коміркою</param>
        void SetMoving(long palletId, long newPos, bool isCell);

        /// <summary>Зберегти дані по прийманню товару</summary>
        /// <param name="nomenclature">Номенклатура</param>
        /// <param name="party">Партія</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <param name="planId">ІД документу</param>
        /// <param name="previousPallet">ІД попередньої паллети</param>
        /// <param name="cellId">ІД комірки</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        void SetAcceptanceData(long nomenclature, long party, double boxCount, double bottleCount,
                               long planId, long previousPallet, long cellId, bool isCell);

        /// <summary>Зберегти дані про переміщення</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="cellId">Id комірки</param>
        void SetSelectionData(long docId, long palletId, long cellId);

        /// <summary>Перевірити чи існує користувач з таким штрих-кодом</summary>
        /// <param name="barcode">Штрихкод</param>
        bool CheckBarcodeForExistUser(string barcode);

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="id">Id товару</param>
        /// <param name="description">Найменування товару</param>
        /// <returns>Чи існує товар</returns>
        bool CheckBarcodeForExistGoods(string barcode, out long id, out string description);

        /// <summary>Перевірка штрих-коду паллети (комірки) для переміщення</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellIsAccepted"></param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        /// <returns>Чи пройшло перевірку нове розміщення</returns>
        bool CheckPalletBarcodeForMoving(string barcode, bool cellIsAccepted, out long palletId, out bool isCell);

        /// <summary>Перевірити комірку інвентаризації</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellId">ID комірки</param>
        /// <param name="result">Чи співпадають паллети</param>
        /// <returns>Чи пройшла перевірка</returns>
        bool CheckInventoryPallet(string barcode, long cellId, out bool result);

        /// <summary>Перевірка комірки для відвантаження</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ІД комірки</param>
        bool CheckCellFormShipment(string barcode, out long id);

        bool GetTareTable(out DataTable tareTable);

        bool GetStickerData(long acceptanceId, long stickerId, out string nomenclatureDescription, out long trayId, out int unitsPerBox, out long cellId, out string cellDescription, out bool currentAcceptance);

        bool GetAcceptanceId(long stickerId, out long acceptanceId);

        bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long trayId, long linerId, int linersQuantity, int packsCount, int unitsCount);

        bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage);

        bool GetPalletBalance(long stickerId,
            out string nomenclatureDescription,
            out long trayId,
            out long linerId, out byte linersAmount,
            out int unitsPerBox,
            out long cellId, out string cellDescription,
            out long previousPalletCode,
            out DateTime productionDate, out long partyId);

        bool GetNewInventoryId(long userId, out long documentId);

        bool WriteInventoryResult(long documentId, DataTable resultTable);

        bool ComplateInventory(long documentId, bool forceCompletion, out string errorDescription);

        bool GetNewMovementId(long userId, out long documentId);

        bool WriteMovementResult(long documentId, DataTable resultTable);

        bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId, out int sameWareNextTaskLineNumber);

        bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage);

        DataTable GetPickingDocuments();

        bool GetPickingTask(long documentId, long palletId, int predefinedTaskLineNumber, int currentLineNumber,
            out long stickerId,
            out long wareId, out string wareDescription,
            out long cellId, out string cellDescription,
            out long partyId, out DateTime productionDate,
            out int unitsPerBox, out int unitsToPick,
            out int lineNumber);

        bool PrintStickers(DataTable result);

        bool ReadConsts(out DataTable constsTable);

        bool CreatePickingDocuments();
        }
    }