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
    }