using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>��� ��������������</summary>
    public enum TypesOfInventory
        {
        /// <summary>��������� ��� ������</summary>
        [DataField(Description = "��������� ��� ������")]
        TypeOfCells,
        /// <summary>��������� ��� ��������</summary>
        [DataField(Description = "��������� ��� ��������")]
        ItemType,
        /// <summary>��������� ������ �������� ������ �� ������� �����</summary>
        [DataField(Description = "��������� ������ �������� ������ �� ������� �����")]
        LatestCellsForPeriod
        }
    }