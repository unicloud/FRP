#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/26 9:46:05
// 文件名：SearchResultsDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/26 9:46:05
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.Purchase.DocumentExtension;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    public class SearchResultsDoubleClickHelper : RadListBoxDoubleClickHelper
    {
        protected override void ListBoxDoubleClick(RadListBoxItem listBoxItem)
        {
            var boxItem = listBoxItem.Content as ListBoxDocumentItem;
            if (boxItem != null)
                if (boxItem.IsLeaf)
                {
                    var viewModel = ServiceLocator.Current.GetInstance<QueryContractVM>();
                    viewModel.OpenDocument(boxItem.DocumentGuid);
                }
                else
                {
                    var viewModel = ServiceLocator.Current.GetInstance<QueryContractVM>();
                    viewModel.OpenFolderInSearchResults(boxItem.DocumentPathId);
                }
        }

        protected override bool CanDoubleClick(RadListBoxItem listBoxItem)
        {
            return true;
        }
    }
}
