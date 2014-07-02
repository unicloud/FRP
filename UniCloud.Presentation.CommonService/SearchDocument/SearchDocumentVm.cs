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
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export(typeof (SearchDocumentVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchDocumentVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;

        [ImportingConstructor]
        public SearchDocumentVm(ICommonService service)
            : base(service)
        {
            _context = service.Context;
        }

        #endregion

        #region 属性

        private IEnumerable<DocumentTypeDTO> _documentTypes;
        private List<DocumentDTO> _documents;
        private string _keyword;

        /// <summary>
        ///     关键字
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        /// <summary>
        ///     搜索到的文档集合
        /// </summary>
        public List<DocumentDTO> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged(() => Documents);
            }
        }

        /// <summary>
        ///     文档类型
        /// </summary>
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

        #region 操作

        public void RadWatermarkTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RadButtonClick(sender, e);
            }
        }

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
            string[] documentType = {string.Empty};
            DocumentTypes.ToList().ForEach(p =>
            {
                if (p.IsChecked)
                {
                    documentType[0] += p.DocumentTypeId + ",";
                }
            });
            documentType[0] = documentType[0].TrimEnd(',');
            var keyword = SearchDocuments(Keyword, documentType[0]);
            IsBusy = true;
            _context.BeginExecute<DocumentDTO>(keyword,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as CommonServiceData;
                    try
                    {
                        if (context != null)
                        {
                            context.MergeOption = MergeOption.OverwriteChanges;
                            Documents = context.EndExecute<DocumentDTO>(result).ToList();
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        var response = ex.Response;
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

        #endregion
    }
}