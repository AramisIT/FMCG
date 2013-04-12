using System.Collections.Generic;
using System.Data;
using AtosFMCG.DatabaseObjects.Documents;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>��������� ������</summary>
    /// <param name="enterValue">������� �������� ��� ������ ���������</param>
    /// <returns>������� �����</returns>
    public delegate DataTable UpdateSelectionTableDelegate(string enterValue);

    /// <summary>�������� ������ (������� ���)</summary>
    /// <param name="value">������ �������� � ������</param>
    public delegate void SelectValueFromListDelegate(KeyValuePair<long, string> value);

    /// <summary>����������� �����</summary>
    public delegate void GoDelegate();

    /// <summary></summary>
    public delegate void FinishEditPlannedArrivalDelegate(PlannedArrival Document);

    public delegate void FinishFieldEditiong(string newValue);
    }