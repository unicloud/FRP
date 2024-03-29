﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export]
    public partial class PurchasePrepayInvoiceManager
    {
        public PurchasePrepayInvoiceManager()
        {
            InitializeComponent();
        }

        [Import]
        public PurchasePrepayInvoiceManagerVm ViewModel
        {
            get { return DataContext as PurchasePrepayInvoiceManagerVm; }
            set { DataContext = value; }
        }
    }
}