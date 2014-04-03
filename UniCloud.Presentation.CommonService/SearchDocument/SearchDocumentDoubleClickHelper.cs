#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/23 9:24:32
// 文件名：DocumentDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/23 9:24:32
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    public class SearchDocumentDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var view = ServiceLocator.Current.GetInstance<SearchDocument>();
            var docViewer = ServiceLocator.Current.GetInstance<DocViewer>();
            var docViewerVM = ServiceLocator.Current.GetInstance<DocViewerVM>();
            if (view.DocumentList.SelectedItem != null)
            {
                docViewer.ShowDialog();
                docViewerVM.InitDocument(((DocumentDTO)view.DocumentList.SelectedItem).DocumentId);
            }
        }
        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            return true;
        }
    }
}
