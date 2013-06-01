using System;
using System.Collections.Generic;
using System.Data;
using Aramis.DatabaseConnector;

namespace AtosFMCG.HelperClasses.PDT
    {
    public static class ReceiveMessages
        {
        private static readonly PDTCommunication communication;

        static ReceiveMessages()
            {
            communication = new PDTCommunication();
            }

        public static object[] ReceiveMessage(string procedure, object[] parameters)
            {
            switch (procedure)
                {
                    case "GetCarsForAcceptance":
                        return GetCarsForAcceptance();
                    case "GetPlaceDataFromCode":
                        return GetPlaceDataFromCode(parameters);
                    case "GetPermitInstallPalletManually":
                        return GetPermitInstallPalletManually();
                    case "GetDataAboutMovingPallet":
                        return GetDataAboutMovingPallet(parameters);
                    case "GetAdditionalInfoAboutAccepnedGoods":
                        return GetAdditionalInfoAboutAccepnedGoods(parameters);
                    case "GetDataForInventory":
                        return GetDataForInventory();
                    case "GetCountOfDocuments":
                        return GetCountOfDocuments();
                    case "GetContractorsForSelection":
                        return GetContractorsForSelection();
                    case "GetSelectionRowInfo":
                        return GetSelectionRowInfo();
                    case "SetAcceptanceData":
                        SetAcceptanceData(parameters);
                        break;
                    case "SetMoving":
                        SetMoving(parameters);
                        break;
                    case "SetInventory":
                        SetInventory(parameters);
                        break;
                    case "SetSelectionData":
                        SetSelectionData(parameters);
                        break;
                    case "CheckBarcodeForExistUser":
                        return CheckBarcodeForExistUser(parameters);
                    case "CheckBarcodeForExistGoods":
                        return CheckBarcodeForExistGoods(parameters);
                    case "CheckPalletBarcodeForMoving":
                        return CheckPalletBarcodeForMoving(parameters);
                    case "CheckInventoryPallet":
                        return CheckInventoryPallet(parameters);
                    case "CheckCellFormShipment":
                        return CheckCellFormShipment(parameters);
                }

            return new object[0];
            }

        #region Get
        /// <summary>�������� ������ ����� ��� "��������� ������"</summary>
        /// <returns>������� (Id, Description)</returns>
        private static object[] GetCarsForAcceptance()
            {
            DataTable table;
            return new object[] {communication.GetCarsForAcceptance(out table), table};
            }

        /// <summary>�������� ���� ��������� � ���������</summary>
        /// <param name="parameters">��������</param>
        /// <returns>³����������� ���������</returns>
        private static object[] GetPlaceDataFromCode(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            string type;
            long id;
            return new object[] {communication.GetPlaceDataFromCode(barcode, out type, out id), type, id};
            }

        /// <summary>��������� ��������� �� ��������� ������ �������</summary>
        /// <returns>��������� �� ��������� ������ �������</returns>
        private static object[] GetPermitInstallPalletManually()
            {
            return new object[] {communication.GetPermitInstallPalletManually()};
            }

        /// <summary>�������� ��� ��� �������, �� �����������</summary>
        /// <param name="parameters">�� �������</param>
        /// <returns>����, ����, �-��� �����, �-��� �������</returns>
        private static object[] GetDataAboutMovingPallet(IList<object> parameters)
            {
            int palletId = Convert.ToInt32(parameters[0]);
            string goods;
            DateTime date;
            double boxCount;
            double bottleCount;

            return new object[]
                       {
                           communication.GetDataAboutMovingPallet(
                               palletId, out goods, out date, out boxCount, out bottleCount),
                           goods, date, boxCount, bottleCount
                       };
            }

        /// <summary>
        /// ��� ��� ��������������
        /// </summary>
        private static object[] GetDataForInventory()
            {
            double count;
            string nomenclature;
            string date;
            long palletId;
            string measure;
            long docId;
            long lineNumber;
            long cellId;
            string cell;

            return new object[]
                       {
                           communication.GetDataForInventory(out count, out nomenclature, out date, out palletId,
                                                             out measure, out docId, out lineNumber, out cellId,
                                                             out cell),
                           count, nomenclature, date, palletId, measure, docId, lineNumber, cellId, cell
                       };
            }

        /// <summary>
        /// �-��� ���������, �� ������� �������
        /// </summary>
        /// <returns>��������, �������������, ³���, ����������</returns>
        private static object[] GetCountOfDocuments()
            {
            string acceptanceDocCount;
            string inventoryDocCount;
            string selectionDocCount;
            string movementDocCount;
            communication.GetCountOfDocuments(out acceptanceDocCount, out inventoryDocCount,
                                              out selectionDocCount, out movementDocCount);
            return new object[]
                       {
                           acceptanceDocCount,
                           inventoryDocCount,
                           selectionDocCount,
                           movementDocCount
                       };
            }

        /// <summary>
        /// �������� ������ ���������� ��� ����� ���������
        /// </summary>
        /// <returns>������� ����������� (Description, Id)</returns>
        private static object[] GetContractorsForSelection()
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS Date)
DECLARE @ShipmentPlan_Type uniqueidentifier='029B0572-E5B5-48CD-9805-1211319A5633'

SELECT RTRIM(RTRIM(c.Description)+' �'+CAST(s.Number AS VARCHAR)) Description, c.Id Id
FROM ShipmentPlan s 
JOIN Contractors c ON c.Id=s.Contractor
JOIN Movement m ON m.Source=s.Id AND m.SourceType=@ShipmentPlan_Type
WHERE s.State=0 AND s.MarkForDeleting=0 AND CAST(s.Date AS DATE)=@Today");
            DataTable table = query.SelectToTable();

            if (table != null)
                {
                return new object[] {true, table};
                }

            return new object[] {false};
            }

        /// <summary>���������� ��� ����� ������� (��� ������) ��� ������</summary>
        private static object[] GetSelectionRowInfo()
            {
            long id;
            long palletId;
            string goods;
            string date;
            double boxCount;
            double unitCount;
            int baseCount;
            string cell;

            return new object[]
                       {
                           communication.GetSelectionRowInfo(
                               0, out id, out palletId, out goods, out date, out boxCount, out unitCount, out baseCount,
                               out cell),
                           id, palletId, goods, date, boxCount, unitCount, baseCount, cell
                       };
            }

        /// <summary>�������� ��������� ���������� �� ��������� ������</summary>
        private static object[] GetAdditionalInfoAboutAccepnedGoods(IList<object> parameters)
            {
            double count = Convert.ToDouble(parameters[0]);
            long goods = Convert.ToInt64(parameters[1]);
            long car = Convert.ToInt64(parameters[2]);
            long party = Convert.ToInt64(parameters[3]);

            long incomeDoc;
            string date;
            long cellId;
            string cell;
            long palett;

            return new object[]
                       {
                           communication.GetAdditionalInfoAboutAccepnedGoods(
                               count, goods, car, party, out incomeDoc, out date, out cellId, out cell, out palett),
                           incomeDoc, date, cellId, cell, palett
                       };
            }
        #endregion

        #region Set
        /// <summary>
        /// ���������� ����� ��������������
        /// </summary>
        /// <param name="parameters">�� ���������, ����� �����, �-���</param>
        private static void SetInventory(IList<object> parameters)
            {
            long docId = Convert.ToInt64(parameters[0]);
            long lineNumber = Convert.ToInt64(parameters[1]);
            long count = Convert.ToInt64(parameters[2]);

            communication.SetInventory(docId, lineNumber, count);
            }

        /// <summary>���������� ����� �� ���������� �������</summary>
        /// <param name="parameters">�� �������, ���� �������, �� ��������� ���� ������� ��������� �������</param>
        private static void SetMoving(IList<object> parameters)
            {
            long palletId = Convert.ToInt64(parameters[0]);
            long newPos = Convert.ToInt64(parameters[1]);
            bool isCell = Convert.ToBoolean(parameters[2]);

            communication.SetMoving(palletId, newPos, isCell);
            }

        /// <summary>�������� ��� �� ��������� ������</summary>
        private static void SetAcceptanceData(IList<object> parameters)
            {
            long nomenclature = Convert.ToInt64(parameters[0]);
            long party = Convert.ToInt64(parameters[1]);
            double boxCount = Convert.ToDouble(parameters[2]);
            double bottleCount = Convert.ToDouble(parameters[3]);
            long planId = Convert.ToInt64(parameters[4]);
            long previousPallet = Convert.ToInt64(parameters[5]);
            long cellId = Convert.ToInt64(parameters[6]);
            bool isCell = Convert.ToBoolean(parameters[7]);

            communication.SetAcceptanceData(nomenclature, party, boxCount, bottleCount, planId, previousPallet, cellId,
                                            isCell);
            }

        /// <summary>
        /// �������� ��� ��� ����������
        /// </summary>
        /// <param name="parameters">�� ���������, �� �������, �� ������</param>
        private static void SetSelectionData(IList<object> parameters)
            {
            long docId = Convert.ToInt64(parameters[0]);
            long palletId = Convert.ToInt64(parameters[1]);
            long cellId = Convert.ToInt64(parameters[2]);

            communication.SetSelectionData(docId, palletId, cellId);
            }
        #endregion

        #region Check
        /// <summary>��������� �� ���� ���������� � ����� �����-�����</summary>
        private static object[] CheckBarcodeForExistUser(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            return new object[] {communication.CheckBarcodeForExistUser(barcode)};
            }

        /// <summary>��������� �� ���� ����� � ����� �����-�����</summary>
        private static object[] CheckBarcodeForExistGoods(IList<object> parameters)
            {
            long id;
            string description;
            string barcode = parameters[0].ToString();

            return new object[]
                       {
                           communication.CheckBarcodeForExistGoods(barcode, out id, out description),
                           id,
                           description
                       };
            }

        /// <summary>�������� �����-���� ������� (������) ��� ����������</summary>
        /// <param name="parameters">�����-���</param>
        /// <returns>result, palletId</returns>
        private static object[] CheckPalletBarcodeForMoving(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            bool cellIsAccepted = parameters.Count > 1 && Convert.ToBoolean(parameters[1]);
            long palletId;
            bool isCell;
            return new object[]
                       {
                           communication.CheckPalletBarcodeForMoving(barcode, cellIsAccepted, out palletId, out isCell),
                           palletId,
                           isCell
                       };
            }

        /// <summary>��������� ������ ��������������</summary>
        /// <param name="parameters">��������, �� ������</param>
        /// <returns></returns>
        private static object[] CheckInventoryPallet(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            long cellId = Convert.ToInt64(parameters[1]);
            bool result;

            return new object[] { communication.CheckInventoryPallet(barcode, cellId, out result), result };
            }

        /// <summary>�������� ������ ��� ������������</summary>
        /// <param name="parameters">��������</param>
        private static object[] CheckCellFormShipment(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            long id;

            return new object[] {communication.CheckCellFormShipment(barcode, out id), id};
            }
        #endregion
        }
    }