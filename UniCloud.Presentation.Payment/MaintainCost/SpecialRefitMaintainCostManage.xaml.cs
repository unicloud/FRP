﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(SpecialRefitMaintainCostManage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SpecialRefitMaintainCostManage 
    {
        public SpecialRefitMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public SpecialRefitMaintainCostManageVm ViewModel
        {
            get { return DataContext as SpecialRefitMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}
