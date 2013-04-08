using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>���� ������������</summary>
    public enum TypesOfShipment
        {
        /// <summary>�� ������</summary>
        [DataField(Description = "<�� ������>")]
        None,
        /// <summary>���������� �� �����</summary>
        [DataField(Description = "���������� �� �����")] 
        ReturnToPlant,
        /// <summary>���������� �� �������������</summary>
        [DataField(Description = "���������� �� �������������")] 
        MovementIntoDistributor,
        /// <summary>���������� �� �����</summary>
        [DataField(Description = "���������� �� �����")] 
        MovementIntoPurchase
        }
    }