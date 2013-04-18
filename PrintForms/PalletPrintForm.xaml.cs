using System;
using System.Windows;

namespace AtosFMCG.PrintForms
    {
    /// <summary>
    /// Друкована форма паллети
    /// </summary>
    public partial class PalletPrintForm
        {
        /// <summary>Розміри етикетки</summary>
        public static readonly Size LabelSize = new Size(300,400);

        /// <summary>
        /// Друкована форма паллети
        /// </summary>
        public PalletPrintForm()
            {
            InitializeComponent();

            Width = LabelSize.Width;
            Height = LabelSize.Height;
            MaxWidth = LabelSize.Width;
            MaxHeight = LabelSize.Height;
            }

        /// <summary>
        /// Друкована форма паллети
        /// </summary>
        /// <param name="nomenclature">Найменвання номенклатури</param>
        /// <param name="quantity">Кількість упаковок</param>
        /// <param name="code">Код паллети</param>
        /// <param name="manufacture">Дата виробництва</param>
        /// <param name="half">Половина терміну реалізації</param>
        /// <param name="full">Кінцевий термін реалізації</param>
        /// <param name="employee">Співробітник</param>
        /// <param name="employeeCode">Код співробітника</param>
        /// <param name="dateOf">Дата</param>
        public PalletPrintForm(string nomenclature, int quantity, long code, DateTime manufacture, DateTime half, DateTime full, string employee, string employeeCode, DateTime dateOf):this()
            {
            nomencletureDescription.Content = nomenclature;
            quantityString.Content = string.Concat(quantity, " УП");
            palletCode.Content = code.ToString();
            dateOfManufacture.Content = manufacture.ToShortDateString();
            shelfLife50P.Content = half.ToShortDateString();
            deadlineSuitability.Content = full.ToShortDateString();
            employeeString.Content = string.Concat(employee, " ", employeeCode);
            date.Content = dateOf.ToShortDateString();
            }
        }
    }
