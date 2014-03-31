#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/23 10:36:09
// 文件名：SearchDocumentMainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/23 10:36:09
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export(typeof(SearchDocumentMainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SearchDocumentMainVm : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        [ImportingConstructor]
        public SearchDocumentMainVm(ICommonService service, IRegionManager regionManager)
            : base(service)
        {
            _regionManager = regionManager;
            DocumentTypes = new QueryableDataServiceCollectionView<DocumentTypeDTO>(service.Context, service.Context.DocumentTypes);
        }

        /// <summary>
        /// 文档类型
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentTypeDTO> DocumentTypes { get; set; }
        #region 关键字
        private string _keyword;
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }
        #endregion
        public void RadButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                MessageAlert("请输入搜索关键字！");
                return;
            }
            if (DocumentTypes.All(p => p.IsChecked == false))
            {
                MessageAlert("请选择搜索范围！");
                return;
            }

            if (!string.IsNullOrEmpty(Keyword))
            {
                _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("UniCloud.Presentation.CommonService.SearchDocument.SearchDocument", UriKind.Relative));
            }
        }

        public override void LoadData()
        {
            DocumentTypes.Load();
            //throw new System.NotImplementedException();
        }
    }
}
