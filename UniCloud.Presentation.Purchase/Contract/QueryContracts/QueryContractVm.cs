#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/2 14:51:39
// 文件名：QueryContractVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/2 14:51:39
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
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.QueryContracts
{
    [Export(typeof (QueryContractVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QueryContractVm : ViewModelBase
    {
        private readonly PurchaseData _context;

        [ImportingConstructor]
        public QueryContractVm(IPurchaseService service)
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

        public void RadWatermarkTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RadButtonClick(sender, e);
            }
        }

        #endregion

        #region 搜索到的文档集合

        private List<ContractDocumentDTO> _documents;

        public List<ContractDocumentDTO> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged(() => Documents);
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

            var keyword = SearchContractDocuments(Keyword);
            IsBusy = true;
            _context.BeginExecute<ContractDocumentDTO>(keyword,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PurchaseData;
                    try
                    {
                        if (context != null)
                        {
                            context.MergeOption = MergeOption.OverwriteChanges;
                            Documents = context.EndExecute<ContractDocumentDTO>(result).ToList();
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
        /// <returns></returns>
        private Uri SearchContractDocuments(string keyword)
        {
            return new Uri(string.Format("SearchContractDocument?keyword='{0}'", keyword), UriKind.Relative);
        }

        public override void LoadData()
        {
            var main = ServiceLocator.Current.GetInstance<QueryContractMainVm>();
            Keyword = main.Keyword;
            main.Keyword = string.Empty;
            RadButtonClick(null, null);
        }
    }
}