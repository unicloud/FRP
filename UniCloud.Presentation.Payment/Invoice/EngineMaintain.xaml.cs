#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13，09:12
// 文件名：EngineMaintain.xaml.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(EngineMaintain))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EngineMaintain 
    {
        public EngineMaintain()
        {
            InitializeComponent();
        }

        [Import]
        public EngineMaintainVm ViewModel
        {
            get { return DataContext as EngineMaintainVm; }
            set { DataContext = value; }
        }
    }
}
