#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，08:12
// 文件名：Approval.xaml.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export(typeof (Approval))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Approval
    {
        public Approval()
        {
            InitializeComponent();
        }
        [Import]
        public ApprovalVM ViewModel
        {
            get { return DataContext as ApprovalVM; }
            set { DataContext = value; }
        }

    }
}