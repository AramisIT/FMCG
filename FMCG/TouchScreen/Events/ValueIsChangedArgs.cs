using System;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>Значення змінено</summary>
    public class ValueIsChangedArgs<T> : EventArgs
        {
        /// <summary>Нове значення</summary>
        public T Value { get; private set; }

        /// <summary>Значення змінено</summary>
        /// <param name="v">Нове значення</param>
        public ValueIsChangedArgs(T v)
            {
            Value = v;
            }
        }
    }