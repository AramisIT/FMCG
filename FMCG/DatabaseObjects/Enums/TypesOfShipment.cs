using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>Типи відвантаження</summary>
    public enum TypesOfShipment
        {
        /// <summary>не обрано</summary>
        [DataField(Description = "<не обрано>")]
        None,
        /// <summary>Повернення на завод</summary>
        [DataField(Description = "Повернення на завод")] 
        ReturnToPlant,
        /// <summary>Переміщення на дистрибьютора</summary>
        [DataField(Description = "Переміщення на дистрибьютора")] 
        MovementIntoDistributor,
        /// <summary>Переміщення на Викуп</summary>
        [DataField(Description = "Переміщення на Викуп")] 
        MovementIntoPurchase
        }
    }