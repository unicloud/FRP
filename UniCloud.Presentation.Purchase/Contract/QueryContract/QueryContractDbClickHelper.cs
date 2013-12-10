#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：wuql 时间：2013/11/12 14:59:04
// 文件名：DocumentsManagerDoubleClickHelper
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using Telerik.Windows.Controls;
using UniCloud.Presentation.Input;

namespace UniCloud.Presentation.Purchase.Contract
{
    public class QueryContractDbClickHelper : ListBoxDoubleClickHelper
    {
        protected override void ListBoxDoubleClick(RadListBoxItem listBoxItem)
        {
            var viewModel = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<QueryContractVM>();
            viewModel.ListBoxDoubleClick();
        }

        protected override bool CanDoubleClick(RadListBoxItem listBoxItem)
        {
            return true; 
        }
    }
}
