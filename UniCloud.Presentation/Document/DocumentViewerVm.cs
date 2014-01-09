#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/12 11:16:10
// 文件名：DocumentViewerVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/12 11:16:10
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
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (DocumentViewerVm))]
    public class DocumentViewerVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly ICommonService _service;
        private byte[] _byteContent;
        private DocumentDTO _currentDoc;
        private QueryableDataServiceCollectionView<DocumentDTO> _documents;
        private FilterDescriptor _filter;
        private int _navigatePageNumber;
        private bool _onlyView;
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;
        [Import] public DocumentViewer currentDocumentView;

        public DocumentViewerVm(DocumentViewer documentView, ICommonService service) : base(service)
        {
            currentDocumentView = documentView;
            _service = service;
            InitVm();
        }

        public DocumentViewerVm(ICommonService service) : base(service)
        {
            _service = service;
            InitVm();
        }

        private void InitVm()
        {
            _navigatePageNumber = 1;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            OpenDocumentCommand = new DelegateCommand<object>(OpenDocument);
            PdfDocumentChangedCommand = new DelegateCommand<object>(PdfDocumentChanged);
            var commonServiceData = new CommonServiceData(AgentHelper.CommonServiceUri);
            _documents = new QueryableDataServiceCollectionView<DocumentDTO>(commonServiceData,
                commonServiceData.Documents);

            _filter = new FilterDescriptor("DocumentId", FilterOperator.IsEqualTo, Guid.Empty);
            _documents.FilterDescriptors.Add(_filter);
            _documents.LoadedData += (o, e) =>
            {
                try
                {
                    var result = (o as QueryableDataServiceCollectionView<DocumentDTO>).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            currentDocumentView.WordPane.IsHidden = true;
                            Stream currentContent = new MemoryStream(result.FileStorage);
                            currentDocumentView.PdfReader.DocumentSource = new PdfDocumentSource(currentContent,
                                FormatProviderSettings.ReadOnDemand);
                        }
                        else if (result.Name.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                        {
                            currentDocumentView.PdfPane.IsHidden = true;
                            currentDocumentView.WordReader.Document = new DocxFormatProvider().Import(result.FileStorage);
                        }
                    }
                    //else
                    //{
                    //    MessageAlert("找不到该文档！");
                    //}
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

        public void InitData(bool onlyView, Guid docId, EventHandler<WindowClosedEventArgs> closed)
        {
            _currentDoc = new DocumentDTO();
            currentDocumentView.Tag = null;
            currentDocumentView.WordReader.Document = new RadDocument();
            currentDocumentView.PdfReader.Document = null;
            currentDocumentView.WordPane.IsHidden = false;
            currentDocumentView.PdfPane.IsHidden = false;
            IsBusy = true;
            _currentDoc.DocumentId = docId;
            _onlyView = onlyView;
            if (_onlyView)
            {
                currentDocumentView.Header = "查看文档";
                currentDocumentView.Closed -= closed;
            }
            else
            {
                currentDocumentView.Header = "编辑文档";
                currentDocumentView.Closed -= closed;
                currentDocumentView.Closed += closed;
            }
            currentDocumentView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (!docId.Equals(Guid.Empty))
            {
                LoadDocumentByDocId(docId);
            }
            IsBusy = false;
        }

        #endregion

        #region 加载文档

        private void LoadDocumentByDocId(Guid docId)
        {
            _filter.Value = docId;
            _documents.Load(true);
        }

        #endregion

        #region 打开文档

        public DelegateCommand<object> OpenDocumentCommand { get; set; }

        private void OpenDocument(object sender)
        {
            try
            {
                _navigatePageNumber = 1;
                if (currentDocumentView.PaneGroups.SelectedPane.Name.Equals("WordPane",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var openFileDialog = new OpenFileDialog {Filter = "Word Documents(*.docx)|*.docx"};
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _currentDoc.Name = openFileDialog.File.Name;
                        _currentDoc.Extension = openFileDialog.File.Extension;
                        var input = (Stream) openFileDialog.File.OpenRead();
                        using (input)
                        {
                            currentDocumentView.WordReader.Document = new DocxFormatProvider().Import(input);
                        }
                    }
                }
                else if (currentDocumentView.PaneGroups.SelectedPane.Name.Equals("PdfPane",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var openFileDialog = new OpenFileDialog {Filter = "PDF Files(*.pdf)|*.pdf"};
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _currentDoc.Name = openFileDialog.File.Name;
                        _currentDoc.Extension = openFileDialog.File.Extension;
                        _byteContent = new byte[openFileDialog.File.Length];
                        openFileDialog.File.OpenRead().Read(_byteContent, 0, _byteContent.Length);
                        Stream currentContent = new MemoryStream(_byteContent);
                        //using (input)
                        {
                            currentDocumentView.PdfReader.DocumentSource = new PdfDocumentSource(currentContent,
                                FormatProviderSettings.ReadOnDemand);
                        }
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
            IsBusy = true;

            if (currentDocumentView.PaneGroups.SelectedPane.Name.Equals("WordPane", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(_currentDoc.Name))
                {
                    _currentDoc.Name = "新建Word文档" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";
                }
                _currentDoc.FileStorage = new DocxFormatProvider().Export(currentDocumentView.WordReader.Document);
            }
            else if (currentDocumentView.PaneGroups.SelectedPane.Name.Equals("PdfPane",
                StringComparison.OrdinalIgnoreCase))
            {
                _currentDoc.FileStorage = _byteContent;
            }
            if (_currentDoc.DocumentId.Equals(Guid.Empty))
            {
                _currentDoc.DocumentId = Guid.NewGuid();

                _documents.AddNew(_currentDoc);
            }
            else
            {
                var tempDoc = _documents.FirstOrDefault();
                if (tempDoc != null)
                {
                    tempDoc.Name = _currentDoc.Name;
                    tempDoc.Extension = _currentDoc.Extension;
                    tempDoc.FileStorage = _currentDoc.FileStorage;
                }
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
                        currentDocumentView.Tag = _currentDoc;
                        _byteContent = null;
                        currentDocumentView.Close();
                        MessageAlert("保存成功！");
                    }
                    catch (Exception ex)
                    {
                        MessageAlert("保存失败: " + ex.Message);
                    }
                    IsBusy = false;
                };
                _documents.SubmittedChanges += _submitChanges;
            }
        }

        #endregion

        #region  Pdf文档导航特定页

        public void PdfNavigateSpecialPage(int navigatePageNumber)
        {
            _navigatePageNumber = navigatePageNumber;
        }

        #endregion

        #region PdfDocumentChanged

        public DelegateCommand<object> PdfDocumentChangedCommand { get; set; }

        private void PdfDocumentChanged(object sender)
        {
            if (currentDocumentView.PdfReader.Document != null)
            {
                if (_navigatePageNumber < 1)
                {
                    _navigatePageNumber = 1;
                }
                else if (_navigatePageNumber > currentDocumentView.PdfReader.Document.Pages.Count)
                {
                    _navigatePageNumber = currentDocumentView.PdfReader.Document.Pages.Count;
                }
                currentDocumentView.PdfReader.CurrentPageNumber = _navigatePageNumber;
            }
        }

        #endregion

        public override void LoadData()
        {
        }

        #endregion
    }
}