#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13，09:12
// 文件名：FuselageMaintain.xaml.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Payment.Invoice;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof(AirframeMaintain))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AirframeMaintain 
    {
        public AirframeMaintain()
        {
            InitializeComponent();
            this.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnSelectionChanged), true);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                ViewModel.SelectedChanged(e.AddedItems[0]);
            }
        }

        [Import]
        public AirframeMaintainVm ViewModel
        {
            get { return DataContext as AirframeMaintainVm; }
            set { DataContext = value; }
        }
    }
}
