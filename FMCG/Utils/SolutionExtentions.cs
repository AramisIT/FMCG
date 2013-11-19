using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using AtosFMCG.Enums;
using FMCG.DatabaseObjects.Enums;
using FMCG.UI;

namespace FMCG.Utils
    {
    public static class SolutionExtentions
        {
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
