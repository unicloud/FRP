#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/2 14:50:19
// 文件名：QueryContractMainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/2 14:50:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.QueryContracts
{
    [Export(typeof(QueryContractMainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryContractMainVm : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        [ImportingConstructor]
        public QueryContractMainVm(ICommonService service, IRegionManager regionManager)
            : base(service)
        {
            _regionManager = regionManager;
        }

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
        public void RadWatermarkTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RadButtonClick(sender, e);
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

            if (!string.IsNullOrEmpty(Keyword))
            {
                _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("UniCloud.Presentation.Purchase.Contract.QueryContracts.QueryContract", UriKind.Relative));
            }
        }

        public override void LoadData()
        {
        }
    }
}
