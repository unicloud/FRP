#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：EnginePaymentSchedule.xaml.cs
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

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export]
    public partial class EnginePaymentSchedule
    {
        public EnginePaymentSchedule()
        {
            InitializeComponent();
        }

        [Import]
        public EnginePaymentScheduleVM ViewModel
        {
            get { return DataContext as EnginePaymentScheduleVM; }
            set { DataContext = value; }
        }
    }
}