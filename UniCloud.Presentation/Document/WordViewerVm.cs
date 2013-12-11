#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/25 18:10:13
// 文件名：WordViewerVm
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
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Document;
using UniCloud.Presentation.Service.DocumentService;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof(WordViewerVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WordViewerVm : ViewModelBase
    {
        #region 声明、初始化
        [Import]
        public WordViewer CurrentWordView;
        private Document _currentDoc;
        private bool _onlyView;
        private readonly QueryableDataServiceCollectionView<DocumentDTO> _documents;
        private EventHandler<DataServiceSubmittedChangesEventArgs> _submitChanges;
        private readonly FilterDescriptor _filter;
        #endregion

        public WordViewerVm()
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
                        CurrentWordView.editor.Document = new DocxFormatProvider().Import(result.FileStorage);
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert(ex.Message);
                }
                IsBusy = false;
            };
        }

        #region 操作
        #region 初始化文档信息
        public void InitData(bool onlyView, Document doc, EventHandler<WindowClosedEventArgs> closed)
        {
            IsBusy = true;
            _currentDoc = doc;
            _onlyView = onlyView;
            if (_onlyView)
            {
                CurrentWordView.Header = "查看Word文档";
            }
            CurrentWordView.Closed -= closed;
            CurrentWordView.Closed += closed;
            CurrentWordView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (doc != null && !doc.Id.Equals(Guid.Empty) && doc.Name.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                LoadDocumentByDocId(doc.Id);
            }
            else
            {
                CurrentWordView.editor.Document = new RadDocument();
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
                var openFileDialog = new OpenFileDialog { Filter = "Word Documents(*.docx)|*.docx" };
                if (openFileDialog.ShowDialog() == true)
                {
                    _currentDoc.Name = openFileDialog.File.Name;
                    var input = (Stream)openFileDialog.File.OpenRead();
                    using (input)
                    {
                        CurrentWordView.editor.Document = new DocxFormatProvider().Import(input);
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
            if (_currentDoc.Id.Equals(Guid.Empty))
            {
                isNew = true;
                _currentDoc.Id = Guid.NewGuid();
            }
            var document = new DocumentDTO
            {
                DocumentId = _currentDoc.Id,
                Name = _currentDoc.Name,
                FileStorage = new DocxFormatProvider().Export(CurrentWordView.editor.Document)
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
                        CurrentWordView.Tag = _currentDoc;
                        CurrentWordView.Close();
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

        protected override IService CreateService()
        {
            return null;
        }

        public override void LoadData()
        {
        }
        #endregion
    }

    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
