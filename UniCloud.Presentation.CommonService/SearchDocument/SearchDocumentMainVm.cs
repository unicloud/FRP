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
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;

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
        }

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

        public void RadButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Keyword))
            {
                _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("UniCloud.Presentation.CommonService.SearchDocument.SearchDocument", UriKind.Relative));
              
            }
        }

        public override void LoadData()
        {
            //throw new System.NotImplementedException();
        }
    }
}
