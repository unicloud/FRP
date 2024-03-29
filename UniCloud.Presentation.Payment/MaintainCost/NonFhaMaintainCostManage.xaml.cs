﻿#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export]
    public partial class NonFhaMaintainCostManage
    {
        public NonFhaMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public NonFhaMaintainCostManageVm ViewModel
        {
            get { return DataContext as NonFhaMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}