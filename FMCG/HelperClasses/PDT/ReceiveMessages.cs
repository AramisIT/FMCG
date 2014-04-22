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

                RemoteExecutionMethodCoverBuilder<PDTCommunication>.HandlePdtQueryDelegate dynamicMethod;
                if (methodCovers.TryGetValue(procedure, out dynamicMethod))
                    {
                    var results = dynamicMethod(communication, parameters);
                    return results;
                    }

                return new object[] { false };
                }
            catch (Exception exp)
                {
                exp.Message.Error(ErrorLevels.Low);
                return new object[0];
                }
            }
        }
    }