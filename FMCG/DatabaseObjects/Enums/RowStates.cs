using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aramis.Attributes;

namespace FMCG.DatabaseObjects.Enums
    {
    public enum RowsStates
        {
        [DataField(Description = "Запланован прийом")]
        PlannedAcceptance,

        [DataField(Description = "Запланован відбір")]
        PlannedPicking,

        [DataField(Description = "Завершено")]
        Completed,

        [DataField(Description = "Скасовано")]
        Canceled,

        [DataField(Description = "Виконується робота")]
        Processing
        }
    }
