using System;

namespace AtosFMCG.DatabaseObjects.Interfaces
    {
    /// <summary>Синхронізація з 1С</summary>
    public interface ISyncWith1C
        {
        /// <summary>Посилання 1С</summary>
        Guid Ref1C { get; set; }
        }
    }
