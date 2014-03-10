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
using Microsoft.Practices.ServiceLocation;
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

        #region 搜索到的文档集合
        private List<DocumentDTO> _documents;
        public List<DocumentDTO> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged(() => Documents);
            }
        }
        #endregion

        #region 文档类型
        private IEnumerable<DocumentTypeDTO> _documentTypes;
        public IEnumerable<DocumentTypeDTO> DocumentTypes
        {
            get { return _documentTypes; }
            set
            {
                _documentTypes = value;
                RaisePropertyChanged(() => DocumentTypes);
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
            var documentType = string.Empty;
            DocumentTypes.ToList().ForEach(p =>
                                           {
                                               if (p.IsChecked)
                                               {
                                                   documentType += p.DocumentTypeId + ",";
                                               }
                                           });
            documentType = documentType.TrimEnd(',');
            var keyword = SearchDocuments(Keyword, documentType);
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
        /// <param name="documentType"></param>
        /// <returns></returns>
        private Uri SearchDocuments(string keyword, string documentType)
        {
            return new Uri(string.Format("SearchDocument?keyword='{0}'&documentType='{1}'", keyword, documentType),
                UriKind.Relative);
        }

        public override void LoadData()
        {
            var main = ServiceLocator.Current.GetInstance<SearchDocumentMainVm>();
            Keyword = main.Keyword;
            main.Keyword = string.Empty;
            DocumentTypes = main.DocumentTypes.ToList();
            RadButtonClick(null, null);
        }
    }
}
