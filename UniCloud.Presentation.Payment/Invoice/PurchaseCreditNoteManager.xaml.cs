﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PurchaseCreditNoteManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PurchaseCreditNoteManager
    {
        public PurchaseCreditNoteManager()
        {
            InitializeComponent();
        }
        [Import]
        public PurchaseCreditNoteManagerVm ViewModel
        {
            get { return DataContext as PurchaseCreditNoteManagerVm; }
            set { DataContext = value; }
        }
    }
}
