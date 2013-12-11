#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/27 16:11:19
// 文件名：PDFViewerVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Fixed.FormatProviders;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Document;
using UniCloud.Presentation.Service.DocumentService;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;
using System.Data.Services.Client;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof(PDFViewerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PDFViewerVm : ViewModelBase
    {
        #region 声明、初始化
        private readonly DocumentClient _documentService;
        //private CommonServiceData _commonServiceData;
        [Import]
        public PDFViewer CurrentPdfView;
        private Document _currentDoc;
        private bool _onlyView;
        private byte[] _byteContent;

        public PDFViewerVm()
        {
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            OpenDocumentCommand = new DelegateCommand<object>(OpenDocument);
            //_commonServiceData = new CommonServiceData(AgentHelper.CommonServiceUri);
            //_documents = new QueryableDataServiceCollectionView<FolderDTO>(_commonServiceData, _commonServiceData.Folders);
        }
        #endregion

        #region 数据

        private QueryableDataServiceCollectionView<FolderDTO> _documents;
        #endregion

        #region 操作
        #region 初始化文档信息
        public void InitData(bool onlyView, Document doc, EventHandler<WindowClosedEventArgs> closed)
        {
            IsBusy = true;
            _currentDoc = doc;
            _onlyView = onlyView;
            if (_onlyView)
            {
                CurrentPdfView.Header = "查看PDF文档";
            }
            CurrentPdfView.Closed -= closed;
            CurrentPdfView.Closed += closed;
            CurrentPdfView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (doc != null && !doc.Id.Equals(Guid.Empty) && doc.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                LoadDocumentByDocId(doc.Id);
            }
            else
            {
                //CurrentPdfView.pdfViewer.DocumentSource = new RadFixedDocument();
                IsBusy = false;
            }
        }
        #endregion

        #region 加载文档
        private void LoadDocumentByDocId(Guid docId)
        {
            //var query = (from doc in _commonServiceData.Folders where doc.FolderId == docId select doc) as DataServiceQuery<FolderDTO>;
            //query.BeginExecute(OnQueryCompleted, query);

            _documentService.GetDocumentFileStream(docId, (s, arg) =>
            {
                try
                {
                    if (arg.Error != null) { MessageAlert(arg.Error.Message); return; }
                    var document = arg.Result;
                    Stream currentContent = new MemoryStream(document);
                    CurrentPdfView.pdfViewer.Document = new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
                }
                catch (Exception e)
                {
                    MessageAlert(e.Message);
                }
                IsBusy = false;
            });
        }

        private void OnQueryCompleted(IAsyncResult result)
        {
            try
            {
                var query = result.AsyncState as DataServiceQuery<FolderDTO>;
                var response = query.EndExecute(result).FirstOrDefault();
            }
            catch (Exception e)
            {
                MessageAlert(e.Message);
            }
            IsBusy = false;
        }
        #endregion

        #region 打开文档
        public DelegateCommand<object> OpenDocumentCommand { get; set; }
        private void OpenDocument(object sender)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "PDF Files(*.pdf)|*.pdf" };
                if (openFileDialog.ShowDialog() == true)
                {
                    _currentDoc.Name = openFileDialog.File.Name;
                    _byteContent = new byte[openFileDialog.File.Length];
                    openFileDialog.File.OpenRead().Read(_byteContent, 0, _byteContent.Length);
                    Stream currentContent = new MemoryStream(_byteContent);
                    //using (input)
                    {
                        CurrentPdfView.pdfViewer.Document = new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
                    }
                }
            }
            catch (Exception e)
            {
                MessageAlert(e.Message);
            }
        }
        #endregion

        #region 保存文档到服务器
        public DelegateCommand<object> SaveCommand { get; set; }
        private bool CanSave(object sender)
        {
            if (_onlyView)
            {
                return false;
            }
            return true;
        }

        private void Save(object sender)
        {
            bool isNew = false;
            var commitDocuments = new ResultDataStandardDocumentDataObject();
            var addDocuments = new ObservableCollection<StandardDocumentDataObject>();
            var modifyDocuments = new ObservableCollection<StandardDocumentDataObject>();
            if (_currentDoc.Id.Equals(Guid.Empty))
            {
                isNew = true;
                _currentDoc.Id = Guid.NewGuid();
                var newDocument = new StandardDocumentDataObject
                                      {
                                          ID = _currentDoc.Id,
                                          FileName = _currentDoc.Name,
                                          DocumentFileStream = _byteContent
                                      };
                addDocuments.Add(newDocument);
            }
            else
            {
                var modifyDocument = new StandardDocumentDataObject
                                         {
                                             ID = _currentDoc.Id,
                                             FileName = _currentDoc.Name,
                                             DocumentFileStream = _byteContent
                                         };
                modifyDocuments.Add(modifyDocument);
            }
            commitDocuments.AddedCollection = addDocuments;
            commitDocuments.ModefiedCollection = modifyDocuments;
            _documentService.CommitDocument(commitDocuments, (s, arg) =>
                                           {
                                               if (arg.Error != null)
                                               {
                                                   MessageAlert("保存失败，请检查！");
                                                   return;
                                               }
                                               MessageAlert("保存成功！");
                                               if (isNew)
                                               {
                                                   _currentDoc.Id = arg.Result.AddedCollection[0].ID;
                                               }
                                               CurrentPdfView.Tag = _currentDoc;
                                               _byteContent = null;
                                               CurrentPdfView.Close();
                                           });
        }
        #endregion

        protected override IService CreateService()
        {
            return null;
        }

        public override void LoadData()
        {
        }
        #endregion
    }
}
