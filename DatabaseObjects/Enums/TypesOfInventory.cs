using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
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