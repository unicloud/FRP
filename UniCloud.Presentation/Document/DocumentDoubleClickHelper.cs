#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/21 14:22:33
// 文件名：DocumentDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/21 14:22:33
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Document
{
    public class DocumentDoubleClickHelper : GridViewDoubleClickHelper
    {
        public static Action<DocumentDTO> WindowClosed;
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var view = ServiceLocator.Current.GetInstance<ListDocuments>();
            var document = view.DocumentInfos.CurrentItem as DocumentDTO;
            view.Close();
            var docView = ServiceLocator.Current.GetInstance<DocViewer>();
            var docVm = ServiceLocator.Current.GetInstance<DocViewerVM>();
            docView.ShowDialog();
            docVm.AddDocument(document, true, WindowClosed);
            view.Close();
        }
        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            return true;
        }
    }
}
