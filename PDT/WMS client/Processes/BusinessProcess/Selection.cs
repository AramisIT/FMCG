using System.Linq;
using WMS_client.HelperClasses;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Selection : BusinessProcess
        {
       public Selection()
            : base(1)
            {
           
            }


       public override void DrawControls()
           {
          
           }

       public override void OnBarcode(string barcode)
           {

           }

       public override void OnHotKey(KeyAction key)
           {

           }
        }
    }