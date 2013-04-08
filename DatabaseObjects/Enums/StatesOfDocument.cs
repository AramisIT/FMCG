using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>Стани документів</summary>
    public enum StatesOfDocument
        {
        /// <summary>Заплановано</summary>
        [DataField(Description = "Заплановано")]
        Planned,
        /// <summary>Обробляється</summary>
        [DataField(Description = "Обробляється")]
        Processed,
        /// <summary>Виконано</summary>
        [DataField(Description = "Виконано")]
        Achieved,
        /// <summary>Скасовано</summary>
        [DataField(Description = "Скасовано")]
        Canceled,
        /// <summary>Завершено</summary>
        [DataField(Description = "Завершено")]
        Completed,
        /// <summary>Не обрано</summary>
        [DataField(Description = "<не обрано>")]
        Empty
        }
    }