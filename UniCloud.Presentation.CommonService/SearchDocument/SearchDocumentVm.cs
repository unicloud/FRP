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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
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
        private readonly CommonServiceData _context;

        [ImportingConstructor]
        public SearchDocumentVm(ICommonService service)
            : base(service)
        {
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

        private List<DocumentDTO> _documents;
        public List<DocumentDTO> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged(()=>Documents);
            }
        }
        public void RadButtonClick(object sender, RoutedEventArgs e)
        {
            var keyword = SearchDocuments(Keyword);
            IsBusy = true;
            _context.BeginExecute<DocumentDTO>(keyword,
               result => Deployment.Current.Dispatcher.BeginInvoke(() =>
               {
                   var context = result.AsyncState as CommonServiceData;
                   try
                   {
                       if (context != null)
                       {
                           Documents = context.EndExecute<DocumentDTO>(result).ToList();
                       } 
                   }
                   catch (DataServiceQueryException ex)
                   {
                       QueryOperationResponse response = ex.Response;
                       MessageAlert(response.Error.Message);
                   }
                   IsBusy = false;
               }), _context);
        }

        /// <summary>
        ///     搜索文档
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private Uri SearchDocuments(string keyword)
        {
            return new Uri(string.Format("SearchDocument?keyword='{0}'", keyword),
                UriKind.Relative);
        }
        public override void LoadData()
        {

        }
    }
}
