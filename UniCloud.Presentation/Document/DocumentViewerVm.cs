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
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

namespace UniCloud.Presentation.Document
{
    [Export(typeof(DocumentViewerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DocumentViewerVm : ViewModelBase
    {
        #region 声明、初始化

        [Import]
        public DocumentViewer CurrentDocumentView;
        private DocumentDTO _currentDoc;
        private bool _onlyView;
        private byte[] _byteContent;
        private readonly QueryableDataServiceCollectionView<DocumentDTO> _documents;
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;
        private readonly FilterDescriptor _filter;
        public DocumentViewerVm()
        {
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            OpenDocumentCommand = new DelegateCommand<object>(OpenDocument);
            var commonServiceData = new CommonServiceData(AgentHelper.CommonServiceUri);
            _documents = new QueryableDataServiceCollectionView<DocumentDTO>(commonServiceData, commonServiceData.Documents);
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
                            CurrentDocumentView.WordPane.IsHidden = true;
                            Stream currentContent = new MemoryStream(result.FileStorage);
                            CurrentDocumentView.PdfReader.Document = new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
                            CurrentDocumentView.WordReader.Document = new RadDocument();
                        }
                        else if (result.Name.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                        {
                            CurrentDocumentView.PdfPane.IsHidden = true;
                            CurrentDocumentView.WordReader.Document = new DocxFormatProvider().Import(result.FileStorage);
                            CurrentDocumentView.PdfReader.Document = null;
                        }
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
        public void InitData(bool onlyView, Guid docId, EventHandler<WindowClosedEventArgs> closed)
        {
            CurrentDocumentView.WordPane.IsHidden = false;
            CurrentDocumentView.PdfPane.IsHidden = false;
            IsBusy = true;
            _currentDoc = new DocumentDTO {DocumentId = docId};
            _onlyView = onlyView;
            if (_onlyView)
            {
                CurrentDocumentView.Header = "查看文档";
            }
            CurrentDocumentView.Closed -= closed;
            CurrentDocumentView.Closed += closed;
            CurrentDocumentView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (!docId.Equals(Guid.Empty))
            {
                LoadDocumentByDocId(docId);
            }
            else
            {
                CurrentDocumentView.WordReader.Document = new RadDocument();
                CurrentDocumentView.PdfReader.Document = null ;
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
                if (CurrentDocumentView.PaneGroups.SelectedPane.Name.Equals("WordPane", StringComparison.OrdinalIgnoreCase))
                {
                    var openFileDialog = new OpenFileDialog { Filter = "Word Documents(*.docx)|*.docx" };
                    if (openFileDialog.ShowDialog() == true)
                    {
                        _currentDoc.Name = openFileDialog.File.Name;
                        var input = (Stream)openFileDialog.File.OpenRead();
                        using (input)
                        {
                            CurrentDocumentView.WordReader.Document = new DocxFormatProvider().Import(input);
                        }
                    }
                }
                else if (CurrentDocumentView.PaneGroups.SelectedPane.Name.Equals("PdfPane", StringComparison.OrdinalIgnoreCase))
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
                            CurrentDocumentView.PdfReader.Document =
                                new PdfFormatProvider(currentContent, FormatProviderSettings.ReadOnDemand).Import();
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
                           };
            if (CurrentDocumentView.PaneGroups.SelectedPane.Name.Equals("WordPane", StringComparison.OrdinalIgnoreCase))
            {
                document.FileStorage = new DocxFormatProvider().Export(CurrentDocumentView.WordReader.Document);
            }
            else if (CurrentDocumentView.PaneGroups.SelectedPane.Name.Equals("PdfPane", StringComparison.OrdinalIgnoreCase))
            {
                document.FileStorage = _byteContent;
            }
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
                        CurrentDocumentView.Tag = _currentDoc;
                        _byteContent = null;
                        CurrentDocumentView.Close();
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
