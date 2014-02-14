using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.Processes
    {
    public abstract class HideableControlsCollection
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

        protected virtual MobileTextBox GetDefaultTextBox()
            {
            return null;
            }

        public void Show()
            {
            checkControlsList();

            controls.ForEach(control => control.Show());

            Visible = true;

            var textBoxForFocusing = GetDefaultTextBox();
            if (textBoxForFocusing != null)
                {
                textBoxForFocusing.Focus();
                }
            }


        public void Hide()
            {
            checkControlsList();

            controls.ForEach(control => control.Hide());

            Visible = false;
            }
        }
    }
