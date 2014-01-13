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
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Fixed.FormatProviders;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (PDFViewerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PDFViewerVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly QueryableDataServiceCollectionView<DocumentDTO> _documents;
        private readonly FilterDescriptor _filter;
        private byte[] _byteContent;
        private DocumentDTO _currentDoc;
        private bool _onlyView;
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;
        [Import] public PDFViewer currentPdfView;

        public PDFViewerVm(ICommonService service) : base(service)
        {
            var context = service.Context;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            OpenDocumentCommand = new DelegateCommand<object>(OpenDocument);
            _documents = service.CreateCollection(context.Documents);
            service.RegisterCollectionView(_documents);
            _filter = new FilterDescriptor("DocumentId", FilterOperator.IsEqualTo, Guid.Empty);
            _documents.FilterDescriptors.Add(_filter);
            _documents.LoadedData += (o, e) =>
            {
                try
                {
                    var result = (o as QueryableDataServiceCollectionView<DocumentDTO>).FirstOrDefault();
                    if (result != null)
                    {
                        Stream currentContent = new MemoryStream(result.FileStorage);
                        currentPdfView.pdfViewer.Document =
                            new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert(ex.Message);
                }
                IsBusy = false;
            };
        }

        #endregion

        #region 数据

        #endregion

        #region 操作

        #region 初始化文档信息

        public void InitData(bool onlyView, DocumentDTO doc, EventHandler<WindowClosedEventArgs> closed)
        {
            IsBusy = true;
            _currentDoc = doc;
            _onlyView = onlyView;
            if (_onlyView)
            {
                currentPdfView.Header = "查看PDF文档";
            }
            currentPdfView.Closed -= closed;
            currentPdfView.Closed += closed;
            currentPdfView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (doc != null && !doc.DocumentId.Equals(Guid.Empty) &&
                doc.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                LoadDocumentByDocId(doc.DocumentId);
            }
            else
            {
                currentPdfView.pdfViewer.DocumentSource = null;
                IsBusy = false;
            }
        }

        #endregion

        #region 加载文档

        private void LoadDocumentByDocId(Guid docId)
        {
            _filter.Value = docId;
            _documents.AutoLoad = true;
        }

        #endregion

        #region 打开文档

        public DelegateCommand<object> OpenDocumentCommand { get; set; }

        private void OpenDocument(object sender)
        {
            try
            {
                var openFileDialog = new OpenFileDialog {Filter = "PDF Files(*.pdf)|*.pdf"};
                if (openFileDialog.ShowDialog() == true)
                {
                    _currentDoc.Name = openFileDialog.File.Name;
                    _byteContent = new byte[openFileDialog.File.Length];
                    openFileDialog.File.OpenRead().Read(_byteContent, 0, _byteContent.Length);
                    Stream currentContent = new MemoryStream(_byteContent);
                    //using (input)
                    {
                        currentPdfView.pdfViewer.Document =
                            new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
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
            if (_currentDoc.DocumentId.Equals(Guid.Empty))
            {
                isNew = true;
                _currentDoc.DocumentId = Guid.NewGuid();
            }
            var document = new DocumentDTO
            {
                DocumentId = _currentDoc.DocumentId,
                Name = _currentDoc.Name,
                FileStorage = _byteContent
            };
            if (isNew)
            {
                _documents.AddNew(document);
            }
            else
            {
                _documents.EditItem(document);
            }
            _documents.SubmitChanges();
            if (_submitChanges == null)
            {
                _submitChanges += (o, e) =>
                {
                    try
                    {
                        if (e.Error != null)
                        {
                            MessageAlert("保存失败: " + e.Error.Message);
                            return;
                        }
                        currentPdfView.Tag = _currentDoc;
                        _byteContent = null;
                        currentPdfView.Close();
                        MessageAlert("保存成功！");
                    }
                    catch (Exception ex)
                    {
                        MessageAlert("保存失败: " + ex.Message);
                    }
                };
                _documents.SubmittedChanges += _submitChanges;
            }
        }

        #endregion

        public override void LoadData()
        {
        }

        #endregion
    }
}