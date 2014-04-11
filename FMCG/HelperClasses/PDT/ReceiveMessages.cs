using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Aramis;
using Aramis.Common_classes;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Network.PdtTCP;
using Aramis.Platform;
using AramisWpfComponents.Excel;
using AtosFMCG.TouchScreen.Controls;
using pdtExternalStorage;

namespace AtosFMCG.HelperClasses.PDT
    {
    public static class ReceiveMessages
        {
        internal static PDTCommunication Ñommunication
            {
            get
                {
                return communication;
                }
            }

        public static List<object> LastParameters
            {
            get
                {
                lock (lastParametersLocker)
                    {
                    return lastParameters;
                    }
                }
            set
                {
                lock (lastParametersLocker)
                    {
                    lastParameters = value;
                    }
                }
            }

        private static readonly PDTCommunication communication;
        private static Dictionary<string, RemoteExecutionMethodCoverBuilder<PDTCommunication>.HandlePdtQueryDelegate> methodCovers;

        static ReceiveMessages()
            {
            communication = new PDTCommunication();
            methodCovers = new RemoteExecutionMethodCoverBuilder<PDTCommunication>().BuildMethodCovers();
            }

        private static List<object> lastParameters;
        private static object lastParametersLocker = new object();

        public static object[] ReceiveMessage(string procedure, object[] parameters, int userId)
            {
            try
                {
                communication.SetUserId(userId);

                LastParameters = parameters.ToList();
                LastParameters.Insert(0, procedure);

                switch (procedure)
                    {
                    case "GetStickerData":
                        return GetStickerData(parameters);
                    case "GetAcceptanceId":
                        return GetAcceptanceId(parameters);
                    case "WriteStickerFact":
                        return WriteStickerFact(parameters);
                    case "ComplateAcceptance":
                        return ComplateAcceptance(parameters);
                    case "ComplateInventory":
                        return ComplateInventory(parameters);
                    case "GetNewInventoryId":
                        return GetNewInventoryId(parameters);
                    case "GetNewMovementId":
                        return GetNewMovementId(parameters);
                    case "ComplateMovement":
                        return ComplateMovement(parameters);
                    case "WriteMovementResult":
                        return new object[]
                                {
                                communication.WriteMovementResult(Convert.ToInt64(parameters[0]),
                                    parameters[1] as DataTable)
                                };

                    case "GetPickingDocuments":
                        return new object[] { communication.GetPickingDocuments() };

                    case "GetPDTFiles":
                        return new object[] { PlatformMethods.GetPDTFiles().ToTable() };

                    case "GetPDTFileBlock":
                        return new object[]
                                {
                                PlatformMethods.GetPDTFileBlock(new Guid(parameters[0].ToString()),
                                    Convert.ToInt32(parameters[1]), Convert.ToInt32(parameters[2]))
                                };

                    case "PrintStickers":
                        return new object[] { communication.PrintStickers(parameters[0] as DataTable) };

                    case "CreatePickingDocuments":
                        return new object[] { communication.CreatePickingDocuments() };

                    case "ReadConsts":
                        DataTable constsTable;
                        communication.ReadConsts(out constsTable);
                        return new object[] { true, constsTable };

                    case "GetWares":
                        return new object[]
                                {
                                true,
                                communication.GetWares(parameters[0] as string, (SelectionFilters) (int) parameters[1])
                                };

                    case "SetBarcode":
                        bool recordWasAdded;
                        var setBarcodeResult = communication.SetBarcode(parameters[0] as string,
                            Convert.ToInt64(parameters[1]), out recordWasAdded);
                        return new object[] { setBarcodeResult, recordWasAdded };

                    case "SetPalletStatus":
                        communication.SetPalletStatus(Convert.ToInt64(parameters[0]), (bool)parameters[1]);
                        break;

                    case "GetParties":
                            {
                            var table = communication.GetParties(Convert.ToInt64(parameters[0]),
                                (SelectionFilters)Convert.ToInt32(parameters[1]));
                            return new object[] { true, table };
                            }
                        break;

                    case "GetWaresInKegs":
                            {
                            var table = communication.GetWaresInKegs((SelectionFilters)Convert.ToInt32(parameters[0]));
                            return new object[] { true, table };
                            }

                    case "CreateNewAcceptance":
                        return new object[] { true, communication.CreateNewAcceptance() };

                    default: 
                        RemoteExecutionMethodCoverBuilder<PDTCommunication>.HandlePdtQueryDelegate dynamicMethod;
                        if (methodCovers.TryGetValue(procedure, out dynamicMethod))
                            {
                            var results = dynamicMethod(communication, parameters);
                            return results;
                            }
                        break;
                    }

                return new object[0];
                }
            catch (Exception exp)
                {
                exp.Message.Error(ErrorLevels.Low);
                return new object[0];
                }
            }

        private static object[] ComplateMovement(object[] parameters)
            {
            string message;
            bool result = communication.ComplateMovement(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        private static object[] GetNewMovementId(object[] parameters)
            {
            long documentId;

            if (!communication.GetNewMovementId(out documentId))
                {
                return new object[] { 0 };
                }

            return new object[] { documentId };
            }

        private static object[] ComplateInventory(object[] parameters)
            {
            string message;
            bool result = communication.ComplateInventory(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        private static object[] GetNewInventoryId(object[] parameters)
            {
            long documentId;

            if (!communication.GetNewInventoryId(out documentId))
                {
                return new object[] { 0 };
                }

            return new object[] { documentId };
            }

        private static object[] WriteStickerFact(object[] parameters)
            {
            if (!communication.WriteStickerFact(
                Convert.ToInt64(parameters[0]),
                Convert.ToInt64(parameters[1]),
                Convert.ToBoolean(parameters[2]),
                Convert.ToInt64(parameters[3]), // cell id
                Convert.ToInt64(parameters[4]), // previous sticker id
                Convert.ToInt64(parameters[5]),
                Convert.ToInt64(parameters[6]),
                Convert.ToInt32(parameters[7]),
                Convert.ToInt32(parameters[8]),
                Convert.ToInt32(parameters[9])))
                {
                return new object[] { };
                }

            return new object[] { true };
            }

        private static object[] GetAcceptanceId(object[] parameters)
            {
            long acceptanceId;

            if (!communication.GetAcceptanceId(Convert.ToInt64(parameters[0]), out acceptanceId))
                {
                return new object[] { };
                }

            return new object[] { acceptanceId };
            }
        
        private static object[] GetStickerData(object[] parameters)
            {
            long nomenclatureId;
            string nomenclatureDescription;
            long trayId;
            int totalUnitsQuantity;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            bool currentAcceptance;
            int wareBarcodesCount;
            if (!communication.GetStickerData(Convert.ToInt64(parameters[0]), Convert.ToInt64(parameters[1]),
                    out nomenclatureId, out nomenclatureDescription, out trayId, out totalUnitsQuantity, out unitsPerBox, out cellId, out cellDescription, out currentAcceptance,
                    out wareBarcodesCount))
                {
                return new object[] { };
                }

            return new object[] { nomenclatureId, nomenclatureDescription, trayId, totalUnitsQuantity, unitsPerBox, cellId, cellDescription, currentAcceptance, wareBarcodesCount };
            }

        private static object[] ComplateAcceptance(object[] parameters)
            {
            string message;
            bool result = communication.ComplateAcceptance(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        }
    }