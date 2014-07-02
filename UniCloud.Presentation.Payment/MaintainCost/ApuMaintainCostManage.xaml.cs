﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export]
    public partial class ApuMaintainCostManage
    {
        public ApuMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public ApuMaintainCostManageVm ViewModel
        {
            get { return DataContext as ApuMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}