﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Guarantees
{
    [Export]
    public partial class LeaseGuarantee
    {
        public LeaseGuarantee()
        {
            InitializeComponent();
        }

        [Import]
        public LeaseGuaranteeVM ViewModel
        {
            get { return DataContext as LeaseGuaranteeVM; }
            set { DataContext = value; }
        }
    }
}