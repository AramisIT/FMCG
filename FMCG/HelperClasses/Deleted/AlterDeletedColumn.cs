using System.Collections.Generic;
using Aramis.DatabaseConnector;
using Aramis.SystemConfigurations;

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
            SortedDictionary<string, DatabaseObjectInfo>.ValueCollection list =
                SystemConfiguration.DBConfigurationTree.Values;
            Dictionary<string, bool> resultDic = new Dictionary<string, bool>();

            foreach (DatabaseObjectInfo info in list)
                {
                bool result = alterColumnToTable(info);
                resultDic.Add(string.Format("{0} ({1})", info.Type.Name, info.Description), result);
                }

            return resultDic;
            }

        private static bool alterColumnToTable(DatabaseObjectInfo info)
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
            Query query = DB.NewQuery(command);
            query.Execute();

            return query.SuccessfulExecution;
            }

        private static bool addColumn(string tableName)
            {
            string command = string.Format(ADD_SCRIPT, tableName, COLUMN_NAME);
            Query query = DB.NewQuery(command);
            query.Execute();

            return query.SuccessfulExecution;
            }
        }
    }