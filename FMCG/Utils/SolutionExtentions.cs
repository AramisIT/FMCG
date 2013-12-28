using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Aramis.Core.WritingUtils;
using Aramis.DatabaseConnector;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using AtosFMCG.Enums;
using FMCG.DatabaseObjects.Enums;
using FMCG.UI;

namespace FMCG.Utils
    {
    public static class SolutionExtentions
        {
        internal static bool LockForCurrentPdtThread(this DatabaseObjectLocker databaseObjectLocker)
            {
            var pdtIpIdStr = System.Threading.Thread.CurrentThread.Name;
            var mainth = ThreadCover.IsMainThread();
            var sessionId = mainth ? Guid.NewGuid() : new Guid(pdtIpIdStr);
            databaseObjectLocker.SessionId = sessionId;
            return databaseObjectLocker.LockForExclusiveAccess();
            }

        internal static void SetRowState(this long databaseObjectId, Type databaseObjectType, long lineNumber, RowsStates newRowState, string fieldName = "RowState", long employee = 0)
            {
            var subtableName = string.Empty;
            var configuration = SystemConfiguration.DBConfigurationTree[databaseObjectType.Name];

            foreach (var tableInfo in configuration.InfoSubTables.Values)
                {
                if (tableInfo.SubtableFields.ContainsKey(fieldName))
                    {
                    subtableName = configuration.GetDatabaseTableName(tableInfo);
                    break;
                    }
                }

            var q = DB.NewQuery(string.Format(@"
            Update top(1) [{0}] 
                Set [{1}] = @RowState, Employee = @employee
                where IdDoc = @DatabaseObjectId and LineNumber = @LineNumber", subtableName, fieldName));
            q.AddInputParameter("DatabaseObjectId", databaseObjectId);
            q.AddInputParameter("LineNumber", lineNumber);
            q.AddInputParameter("RowState", (int)newRowState);
            q.AddInputParameter("employee", employee);
            q.Execute();
            }

        internal static Color GetRowColor(this RowsStates rowState)
            {
            switch (rowState)
                {
                case RowsStates.PlannedAcceptance:
                    return StatesColors.Planed;

                case RowsStates.Processing:
                    return StatesColors.Processing;

                case RowsStates.Canceled:
                    return StatesColors.Canceled;

                case RowsStates.Completed:
                    return StatesColors.Completed;
                }

            return Color.White;
            }


        internal static Color GetDocumentColor(this DataRow row)
            {
            StatesOfDocument rowDocState = (StatesOfDocument)(int)row["State"];

            switch (rowDocState)
                {
                case StatesOfDocument.Processing:
                    return StatesColors.Processing;

                case StatesOfDocument.Performed:
                    return StatesColors.Performed;

                case StatesOfDocument.Canceled:
                    return StatesColors.Canceled;

                case StatesOfDocument.Completed:
                    return StatesColors.Completed;

                case StatesOfDocument.Planned:
                    return StatesColors.Planed;
                }

            return Color.White;
            }

        internal static Color GetRowColor(this DataRow row)
            {
            if (row == null) return Color.White;

            RowsStates rowState = (RowsStates)(int)row["RowState"];
            return rowState.GetRowColor();
            }
        }
    }
