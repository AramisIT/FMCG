using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.Processes
    {
    abstract class HideableControlsCollection
        {
        public bool Visible { get; private set; }

        private List<MobileControl> controls;
        private void checkControlsList()
            {
            if (controls != null) return;

            controls = new List<MobileControl>();

            var fields = GetType().GetFields();
            foreach (var fieldInfo in fields)
                {
                var fieldValue = fieldInfo.GetValue(this);
                if (fieldValue is MobileControl)
                    {
                    controls.Add(fieldValue as MobileControl);
                    }
                }
            }

        internal void Show()
            {
            checkControlsList();

            controls.ForEach(control => control.Show());

            Visible = true;
            }


        internal void Hide()
            {
            checkControlsList();

            controls.ForEach(control => control.Hide());

            Visible = false;
            }
        }
    }
