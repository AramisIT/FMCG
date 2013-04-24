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
        /// <summary>�������� ������ ����� ��� "��������� ������"</summary>
        /// <returns>������� (Id, Description)</returns>
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

        /// <summary>�������� ���� ��������� � ���������</summary>
        /// <param name="barcode">��������</param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>�� ���� �������� ������</returns>
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

        /// <summary>��������� ��������� �� ��������� ������ �������</summary>
        /// <returns>��������� �� ��������� ������ �������</returns>
        public bool GetPermitInstallPalletManually()
            {
            PerformQuery("GetPermitInstallPalletManually");
            return IsAnswerIsTrue;
            }

        /// <summary>�������� ��� ��� �������, �� �����������</summary>
        /// <param name="palletId">�� �������</param>
        /// <param name="goods">ID �������</param>
        /// <param name="date">���� �������</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="bottleCount">�-��� �������</param>
        /// <returns>����, ����, �-��� �����, �-��� �������</returns>
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

        /// <summary>��� ��� ��������������</summary>
        /// <param name="count">�-���</param>
        /// <param name="nomenclature">����� �����/������������</param>
        /// <param name="date">���� �������</param>
        /// <param name="palletId">�� �������</param>
        /// <param name="measure">��.�����</param>
        /// <param name="docId">����� ���������</param>
        /// <param name="lineNumber">����� ������</param>
        /// <param name="cellId">�� ������</param>
        /// <param name="cell">������������ ������</param>
        /// <returns>�� ���� �������� ������</returns>
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

        /// <summary>�-��� ���������, �� ������� �������</summary>
        /// <param name="acceptanceDocCount">�-��� ��������� "��������"</param>
        /// <param name="inventoryDocCount">�-��� ��������� "�������������"</param>
        /// <param name="selectionDocCount">�-��� ��������� "³���"</param>
        /// <param name="movementDocCount">�-��� ��������� "����������"</param>
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

        /// <summary>���������� ��� ����� ������� (��� ������) ��� ������</summary>
        /// <param name="contractor">����������</param>
        /// <param name="id">ID ���������</param>
        /// <param name="palletId">�� �������</param>
        /// <param name="goods">������������ ������</param>
        /// <param name="date">���� �������</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="unitCount">�-��� ������� (�� �����)</param>
        /// <param name="baseCount">�-��� ������� � ������ �����</param>
        /// <param name="cell">������������ ������</param>
        /// <returns>�� ���� �������� ������</returns>
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

        /// <summary>�������� ��������� ���������� �� ��������� ������</summary>
        /// <param name="count">�-���</param>
        /// <param name="goods">Id ������</param>
        /// <param name="car">Id ��������</param>
        /// <param name="party">Id ����</param>
        /// <param name="incomeDoc">Id ��������� ���������</param>
        /// <param name="date">���� �������</param>
        /// <param name="cellId">Id ������</param>
        /// <param name="cell">������������ ������</param>
        /// <param name="palett">Id �������</param>
        /// <returns>�� ���� �������� ������</returns>
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

        /// <summary>���������� ����� ��������������</summary>
        /// <param name="docId">Id ���������</param>
        /// <param name="lineNumber">����� ������</param>
        /// <param name="count">�-���</param>
        public void SetInventory(long docId, long lineNumber, long count)
            {
            PerformQuery("SetInventory", docId, lineNumber, count);
            }

        /// <summary>���������� ����� �� ���������� �������</summary>
        /// <param name="palletId">Id �������</param>
        /// <param name="newPos">���� ��������� �������</param>
        /// <param name="isCell">�� ��������� ���� ������� �������</param>
        public void SetMoving(long palletId, long newPos, bool isCell)
            {
            PerformQuery("SetMoving", palletId, newPos, isCell);
            }

        /// <summary>�������� ��� �� ��������� ������</summary>
        /// <param name="nomenclature">������������</param>
        /// <param name="party">�����</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="bottleCount">�-��� �������</param>
        /// <param name="planId">�� ���������</param>
        /// <param name="previousPallet">�� ���������� �������</param>
        /// <param name="cellId">�� ������</param>
        /// <param name="isCell">�� ��������� ���� ���� �������</param>
        public void SetAcceptanceData(long nomenclature, long party, double boxCount, double bottleCount, long planId,
                                      long previousPallet, long cellId, bool isCell)
            {
            PerformQuery("SetAcceptanceData", nomenclature, party, boxCount, bottleCount,
                         planId, previousPallet, cellId, isCell);
            }

        /// <summary>�������� ��� ��� ����������</summary>
        /// <param name="docId">Id ���������</param>
        /// <param name="palletId">Id �������</param>
        /// <param name="cellId">Id ������</param>
        public void SetSelectionData(long docId, long palletId, long cellId)
            {
            PerformQuery("SetSelectionData", docId, palletId, cellId);
            }

        /// <summary>��������� �� ���� ���������� � ����� �����-�����</summary>
        /// <param name="barcode">��������</param>
        public bool CheckBarcodeForExistUser(string barcode)
            {
            PerformQuery("CheckBarcodeForExistUser", barcode);
            return IsAnswerIsTrue;
            }

        /// <summary>��������� �� ���� ����� � ����� �����-�����</summary>
        /// <param name="barcode">��������</param>
        /// <param name="id">Id ������</param>
        /// <param name="description">������������ ������</param>
        /// <returns>�� ���� �����</returns>
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

        /// <summary>�������� �����-���� ������� (������) ��� ����������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="cellIsAccepted"></param>
        /// <param name="palletId">Id �������</param>
        /// <param name="isCell">�� ��������� ���� ���� �������</param>
        /// <returns>�� ������� �������� ���� ���������</returns>
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

        /// <summary>��������� ������ ��������������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="cellId">ID ������</param>
        /// <param name="result">�� ���������� �������</param>
        /// <returns>�� ������� ��������</returns>
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

        /// <summary>�������� ������ ��� ������������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="id">�� ������</param>
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