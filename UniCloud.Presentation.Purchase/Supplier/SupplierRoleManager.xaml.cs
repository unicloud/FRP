#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，13:11
// 文件名：SupplierRoleManager.xaml.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (SupplierRoleManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SupplierRoleManager
    {
        public SupplierRoleManager()
        {
            InitializeComponent();
        }

        [Import]
        public SupplierRoleManagerVM ViewModel
        {
            get { return DataContext as SupplierRoleManagerVM; }
            set { DataContext = value; }
        }
    }
}