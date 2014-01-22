#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/21 14:12:17
// 文件名：SearchDocumentVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/21 14:12:17
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export(typeof(SearchDocumentVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SearchDocumentVm : ViewModelBase
    {

        private ICommonService _service;
        private CommonServiceData _context;

        [ImportingConstructor]
        public SearchDocumentVm(ICommonService service)
            : base(service)
        {
            _service = service;
            _context = service.Context;
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
        public void RadButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //_context.se
        }
        public override void LoadData()
        {

        }
    }
}
