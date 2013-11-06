using Aramis.Attributes;

namespace AtosFMCG.Enums
    {
    /// <summary>����� ���������</summary>
    public enum StatesOfDocument
        {
        /// <summary>�����������</summary>
        [DataField(Description = "�����������")]
        Planned,
        /// <summary>������������</summary>
        [DataField(Description = "������������")]
        Processing,
        /// <summary>��������</summary>
        [DataField(Description = "��������")]
        Performed,
        /// <summary>���������</summary>
        [DataField(Description = "���������")]
        Canceled,
        /// <summary>���������</summary>
        [DataField(Description = "���������")]
        Completed,
        /// <summary>�� ������</summary>
        [DataField(Description = "<�� ������>")]
        Empty
        }
    }