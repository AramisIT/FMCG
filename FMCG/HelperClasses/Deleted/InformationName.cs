namespace AtosFMCG
    {
    /// <summary>Информационное имя</summary>
    public class InformationName<T>
        {
        /// <summary>Имя</summary>
        public T Name { get; set; }
        /// <summary>Наименование</summary>
        public T Description { get; set; }

        /// <summary>Информационное имя</summary>
        /// <param name="name">Имя</param>
        public InformationName(T name)
            {
            Name = name;
            }

        /// <summary>Информационное имя</summary>
        /// <param name="name">Имя</param>
        /// <param name="description">Наименование</param>
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