#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013-11-29，13:11
// 方案：FRP
// 项目：Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Forwarder
{
    [Export]
    public partial class ForwarderManager
    {
        public ForwarderManager()
        {
            InitializeComponent();
        }

        [Import]
        public ForwarderManagerVM ViewModel
        {
            get { return DataContext as ForwarderManagerVM; }
            set { DataContext = value; }
        }
    }
}