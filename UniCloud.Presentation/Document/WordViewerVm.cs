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
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;
using UniCloud.Presentation.Service;
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
        private readonly DocumentClient _documentService;
        [Import]
        public WordViewer CurrentWordView;
        private Document _currentDoc;
        private bool _onlyView;
        #endregion

        public WordViewerVm()
        {
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            OpenDocumentCommand = new DelegateCommand<object>(OpenDocument);
            _documentService = DocumentClient.Instance;
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
            _documentService.GetDocumentFileStream(docId, (s, arg) =>
            {
                if (arg.Error != null) { MessageAlert(arg.Error.Message); return; }
                var document = arg.Result;
                CurrentWordView.editor.Document = new DocxFormatProvider().Import(document);
                IsBusy = false;
            });
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
                                          DocumentFileStream = new DocxFormatProvider().Export(CurrentWordView.editor.Document)
                                      };
                addDocuments.Add(newDocument);
            }
            else
            {
                var modifyDocument = new StandardDocumentDataObject
                                         {
                                             ID = _currentDoc.Id,
                                             FileName = _currentDoc.Name,
                                             DocumentFileStream = new DocxFormatProvider().Export(CurrentWordView.editor.Document)
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
                                               CurrentWordView.Tag = _currentDoc;
                                               CurrentWordView.Close();
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

    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
