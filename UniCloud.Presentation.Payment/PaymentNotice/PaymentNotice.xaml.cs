﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20，13:12
// 文件名：PaymentNotice.xaml.cs
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
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(PaymentNotice))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PaymentNotice
    {
        public PaymentNotice()
        {
            InitializeComponent();
            this.AddHandler(Selector.SelectionChangedEvent, new Telerik.Windows.Controls.SelectionChangedEventHandler(OnSelectionChanged), true);
        }

        private void OnSelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                ViewModel.SelectedChanged(e.AddedItems[0]);
            }
        }

        [Import]
        public PaymentNoticeVm ViewModel
        {
            get { return DataContext as PaymentNoticeVm; }
            set { DataContext = value; }
        }
    }
}
