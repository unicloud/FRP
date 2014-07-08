#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/10，14:01
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (DocViewer))]
    public partial class DocViewer
    {
        public DocViewer()
        {
            InitializeComponent();
        }

        [Import(typeof (DocViewerVM))]
        public DocViewerVM ViewModel
        {
            get { return DataContext as DocViewerVM; }
            set { DataContext = value; }
        }
    }
}