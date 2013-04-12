using System;

namespace AtosFMCG.TouchScreen.Events
    {
    public class FieldValueIsChangedArgs : EventArgs
        {
        public string Value { get; private set; }

        public FieldValueIsChangedArgs(string v)
            {
            Value = v;
            }
        }
    }