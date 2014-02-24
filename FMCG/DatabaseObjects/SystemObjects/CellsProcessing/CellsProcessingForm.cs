using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemObjects;
using DevExpress.XtraBars;

namespace FMCG.DatabaseObjects.SystemObjects
    {
    public partial class CellsProcessingForm : DevExpress.XtraBars.Ribbon.RibbonForm
        {
        public CellsProcessingForm()
            {
            InitializeComponent();
            }

        public CellsProcessing Item { get; set; }
        }
    }