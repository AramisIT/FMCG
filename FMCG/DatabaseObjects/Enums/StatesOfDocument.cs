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
        Processed,
        /// <summary>��������</summary>
        [DataField(Description = "��������")]
        Achieved,
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