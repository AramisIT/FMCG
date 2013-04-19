using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>Тип інвентаризації</summary>
    public enum TypesOfInventory
        {
        /// <summary>Перевірити тип комірок</summary>
        [DataField(Description = "Перевірити тип комірок")]
        TypeOfCells,
        /// <summary>Перевірити тип артикулів</summary>
        [DataField(Description = "Перевірити тип артикулів")]
        ItemType,
        /// <summary>Перевірити останні зачіплені комірки за обраний період</summary>
        [DataField(Description = "Перевірити останні зачіплені комірки за обраний період")]
        LatestCellsForPeriod
        }
    }