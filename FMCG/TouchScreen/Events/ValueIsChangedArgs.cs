using System;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>�������� ������</summary>
    public class ValueIsChangedArgs<T> : EventArgs
        {
        /// <summary>���� ��������</summary>
        public T Value { get; private set; }

        /// <summary>�������� ������</summary>
        /// <param name="v">���� ��������</param>
        public ValueIsChangedArgs(T v)
            {
            Value = v;
            }
        }
    }