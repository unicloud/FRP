#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：AcPaymentSchedule.xaml.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof (AcPaymentSchedule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AcPaymentSchedule
    {
        public AcPaymentSchedule()
        {
            InitializeComponent();
        }

        [Import]
        public AcPaymentScheduleVM ViewModel
        {
            get { return DataContext as AcPaymentScheduleVM; }
            set { DataContext = value; }
        }
    }
}