#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/05，17:12
// 文件名：QueryContractDbClickHelper.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Input;

namespace UniCloud.Presentation.Purchase.Contract.ManageContracts
{
    public class QueryContractDbClickHelper : ListBoxDoubleClickHelper
    {
        protected override void ListBoxDoubleClick(System.Windows.Controls.ListBoxItem listBoxItem)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageContractVm>();
            viewModel.ListBoxDoubleClick();
        }

        protected override bool CanDoubleClick(System.Windows.Controls.ListBoxItem listBoxItem)
        {
            return true; 
        }
    }
}
