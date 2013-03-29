using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>Типи приходу</summary>
    public enum TypesOfArrival
        {
        /// <summary>не обрано</summary>
        [DataField(Description = "<не обрано>")] 
        None,
        /// <summary>ед.работы</summary>
        [DataField(Description = "З заводу")] 
        FromFactory,
        /// <summary>ед.работы</summary>
        [DataField(Description = "Від дистриб'ютора")] 
        FromDistributor
        }

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
        Completed
        }
    }