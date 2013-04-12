using System.Collections.Generic;
using System.Data;
using AtosFMCG.DatabaseObjects.Documents;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>Оновлення списку</summary>
    /// <param name="enterValue">Введене значення для пошуку співпадань</param>
    /// <returns>Таблиця даних</returns>
    public delegate DataTable UpdateSelectionTableDelegate(string enterValue);

    /// <summary>Значення обрано (перейти далі)</summary>
    /// <param name="value">Обране значення зі списку</param>
    public delegate void SelectValueFromListDelegate(KeyValuePair<long, string> value);

    /// <summary>Повернутись назад</summary>
    public delegate void GoDelegate();

    /// <summary></summary>
    public delegate void FinishEditPlannedArrivalDelegate(PlannedArrival Document);

    public delegate void FinishFieldEditiong(string newValue);
    }