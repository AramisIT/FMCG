namespace AtosFMCG
    {
    /// <summary>�������������� ���</summary>
    public class InformationName<T>
        {
        /// <summary>���</summary>
        public T Name { get; set; }
        /// <summary>������������</summary>
        public T Description { get; set; }

        /// <summary>�������������� ���</summary>
        /// <param name="name">���</param>
        public InformationName(T name)
            {
            Name = name;
            }

        /// <summary>�������������� ���</summary>
        /// <param name="name">���</param>
        /// <param name="description">������������</param>
        public InformationName(T name, T description)
            {
            Name = name;
            Description = description;
            }

        public override string ToString()
            {
            var defaultValue = default(T);

            return Equals(Name, defaultValue) 
                       ? string.Empty
                       : Equals(Description, defaultValue) ? Name.ToString() : string.Format("{0} [{1}]", Name, Description);
            }
        }
    }