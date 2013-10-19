using System.Collections.Generic;
using System.Data;
using AtosFMCG.DatabaseObjects.Documents;
using Documents;

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

    /// <summary>��������� ����������� ��������� "���� �������"</summary>
    /// <param name="isSaved">�� ��� �������� ����������</param>
    /// <param name="Document">��������</param>
    public delegate void FinishEditAcceptancePlanDelegate(bool isSaved, AcceptancePlan Document);

    /// <summary>��������� ����������� ���������� ����</summary>
    /// <param name="newValue">���� ��������</param>
    public delegate void FinishFieldEditiong(string newValue);
    }