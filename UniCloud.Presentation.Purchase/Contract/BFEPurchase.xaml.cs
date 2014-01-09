#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/07，13:01
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
    public partial class BFEPurchase
    {
        public BFEPurchase()
        {
            InitializeComponent();
        }

        [Import(typeof (BFEPurchaseVM))]
        public BFEPurchaseVM ViewModel
        {
            get { return DataContext as BFEPurchaseVM; }
            set { DataContext = value; }
        }
    }
}