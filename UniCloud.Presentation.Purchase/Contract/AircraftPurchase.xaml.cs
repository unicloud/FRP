#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/12，18:11
// 方案：FRP
// 项目：Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class AircraftPurchase
    {
        public AircraftPurchase()
        {
            InitializeComponent();
        }

        [Import(typeof (AircraftPurchaseVM))]
        public AircraftPurchaseVM ViewModel
        {
            get { return DataContext as AircraftPurchaseVM; }
            set { DataContext = value; }
        }
    }
}