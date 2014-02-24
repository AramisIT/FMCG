using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemObjects;
using Aramis.DatabaseConnector;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;
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

        private void simpleButton1_Click(object sender, EventArgs e)
            {
            Item.PrintCells();
            }

        private void simpleButton2_Click(object sender, EventArgs e)
            {
            Item.CreateNewCells();
            }
        }
    }