using System;

namespace AtosFMCG.TouchScreen.Events
    {
    public class DateIsChangedArgs : EventArgs
        {
        public DateTime Value { get; private set; }

        public DateIsChangedArgs(DateTime v)
            {
            Value = v;
            }
        }
    }