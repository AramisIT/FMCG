using System.Collections.Generic;
using Aramis.DatabaseConnector;
using Aramis.SystemConfigurations;
using AramisInfostructure.Core.InfoContainers;
using AramisInfostructure.Queries;

namespace AtosFMCG
    {
    public static class AlterDeletedColumn
        {
        public const string COLUMN_NAME = "Deleted";
        private const string CHECK_SCRIPT = "SELECT TOP 1 {0} FROM {1}";
        private const string ADD_SCRIPT =
            "ALTER TABLE [dbo].[{0}] ADD {1} bit NOT NULL CONSTRAINT DF_{0}_{1} DEFAULT 0";

        public static Dictionary<string, bool> Run()
            {
            SortedDictionary<string, IDatabaseObjectInfo>.ValueCollection list =
                SystemConfiguration.DBConfigurationTree.Values;

            var resultDic = new Dictionary<string, bool>();

            foreach (IDatabaseObjectInfo info in list)
                {
                bool result = alterColumnToTable(info);
                resultDic.Add(string.Format("{0} ({1})", info.Type.Name, info.Description), result);
                }

            return resultDic;
            }

        private static bool alterColumnToTable(IDatabaseObjectInfo info)
            {
            if (!checkColumnForExist(info.Type.Name))
                {
                return addColumn(info.Type.Name);
                }

            return true;
            }

        private static bool checkColumnForExist(string tableName)
            {
            string command = string.Format(CHECK_SCRIPT, COLUMN_NAME, tableName);
            IQuery query = DB.NewQuery(command);
            query.Execute();

            return query.SuccessfulExecution;
            }

        private static bool addColumn(string tableName)
            {
            string command = string.Format(ADD_SCRIPT, tableName, COLUMN_NAME);
            IQuery query = DB.NewQuery(command);
            query.Execute();

            return query.SuccessfulExecution;
            }
        }
    }