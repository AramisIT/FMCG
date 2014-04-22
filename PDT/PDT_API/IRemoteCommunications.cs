using System.Data;
using System;

namespace pdtExternalStorage
    {
    public interface IRemoteCommunications
        {
        bool FinishCellInventory(long documentId, long cellId, DataTable currentCellPallets);

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        bool GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
            out string selectionDocCount,
            out string movementDocCount);

        bool GetTareTable(out DataTable tareTable);

        bool GetStickerData(long acceptanceId, long stickerId, out long nomenclatureId,
            out string nomenclatureDescription, out long trayId, out int totalUnitsQuantity, out int unitsPerBox,
            out long cellId, out string cellDescription, out bool currentAcceptance, out int wareBarcodesCount);

        bool GetAcceptanceId(long stickerId, out long acceptanceId);

        bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long previousStickerId,
            long trayId, long linerId, int linersQuantity, int packsCount, int unitsCount);

        bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage);

        bool GetPalletBalance(long stickerId,
            out long nomenclatureId,
            out string nomenclatureDescription,
            out long trayId,
            out long linerId, out int linersAmount,
            out int unitsPerBox,
            out long cellId, out string cellDescription,
            out long previousPalletCode,
            out DateTime productionDate, out long partyId,
            out int totalUnitsQuantity,
            out int traysCount);

        bool GetNewInventoryId(out long documentId);

        bool WriteInventoryResult(long documentId, DataTable resultTable);

        bool ComplateInventory(long documentId, bool forceCompletion, out string errorDescription);

        bool GetNewMovementId(out long documentId);

        bool WriteMovementResult(long documentId, DataTable resultTable);

        bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId,
            out int sameWareNextTaskLineNumber);

        bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage);

        bool GetPickingTask(long documentId, long palletId, int predefinedTaskLineNumber,
            int currentLineNumber,
            out long stickerId,
            out long wareId, out string wareDescription,
            out long cellId, out string cellDescription,
            out long partyId, out DateTime productionDate,
            out int unitsPerBox, out int unitsToPick,
            out int lineNumber);

        bool PrintStickers(DataTable result);

        bool ReadConsts(out DataTable constsTable);

        bool CreatePickingDocuments();

        bool SetBarcode(string barcode, long stickerId, out bool recordWasAdded);

        bool SetPalletStatus(long stickerId, bool fullPallet);

        bool GetParties(long wareId, SelectionFilters selectionFilter, out DataTable parties);

        bool GetWaresInKegs(SelectionFilters selectionFilter, out DataTable waresInKegs);

        bool CreateNewSticker(long wareId, DateTime expirationDate, int unitsQuantity, int boxesCount, long linerId, int linersCount, out long newStickerId);

        bool CreateNewAcceptance(out long newAcceptanceId);

        bool GetUserName(int userId, out string name);

        bool GetWares(string barcode, SelectionFilters selectionFilter, out DataTable wares);

        bool GetPickingDocuments(out DataTable pickingDocuments);
        }
    }