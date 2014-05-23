#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13，09:12
// 文件名：UnderCartMaintain.xaml.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof(UndercartMaintain))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class UndercartMaintain 
    {
        public UndercartMaintain()
        {
            InitializeComponent();
        }

        [Import]
        public UndercartMaintainVm ViewModel
        {
            get { return DataContext as UndercartMaintainVm; }
            set { DataContext = value; }
        }
    }
}
