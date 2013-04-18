using System.Collections.Generic;

namespace TouchScreen.Models.Data
    {
    /// <summary></summary>
    public class ObjectValue
        {
        /// <summary>����</summary>
        public string Description { get; set; }
        /// <summary>�������������</summary>
        public long Id { get; set; }

        /// <summary></summary>
        /// <param name="description">����</param>
        /// <param name="id">�������������</param>
        public ObjectValue(string description, long id)
            {
            Description = description;
            Id = id;
            }

        /// <summary></summary>
        /// <param name="obj">����; �������������</param>
        public ObjectValue(KeyValuePair<long, string> obj)
            {
            Description = obj.Value;
            Id = obj.Key;
            }

        public override string ToString()
            {
            return Description;
            }
        }
    }