#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/10，13:59
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;
using Expression=System.Linq.Expressions.Expression;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (DocViewerVM))]
    public class DocViewerVM : ViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;
        private readonly FilterDescriptor<DocumentTypeDTO> _filter;
        private readonly ICommonService _service;
        private DocumentDTO _addedDocument;
        private byte[] _content;
        private bool _fromServer;
        private DocumentDTO _loadedDocument;

        [ImportingConstructor]
        public DocViewerVM(ICommonService service)
            : base(service)
        {
            _service = service;
            _context = service.Context;

            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            SaveDocumentToLocalCommand = new DelegateCommand<object>(OnSaveDocumentToLocal, CanSaveDocumentToLocal);
            InitializeVM();

            _filter = new FilterDescriptor<DocumentTypeDTO> {FilteringExpression = e => true};
            DocumentTypes.FilterDescriptors.Add(_filter);
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            InitializeViewDocumentDto();
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 窗口标题

        private string _title;

        /// <summary>
        ///     窗口标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        #endregion

        #region Word文档可见

        private Visibility _wordVisibility;

        /// <summary>
        ///     Word文档可见
        /// </summary>
        public Visibility WordVisibility
        {
            get { return _wordVisibility; }
            private set
            {
                if (_wordVisibility == value) return;
                _wordVisibility = value;
                RaisePropertyChanged(() => WordVisibility);
            }
        }

        #endregion

        #region Word文档内容

        private byte[] _wordContent;

        /// <summary>
        ///     Word文档内容
        /// </summary>
        public byte[] WordContent
        {
            get { return _wordContent; }
            private set
            {
                if (_wordContent == value) return;
                _wordContent = value;
                RaisePropertyChanged(() => WordContent);
            }
        }

        #endregion

        #region PDF文档可见

        private Visibility _pdfVisibility;

        /// <summary>
        ///     PDF文档可见
        /// </summary>
        public Visibility PDFVisibility
        {
            get { return _pdfVisibility; }
            private set
            {
                if (_pdfVisibility == value) return;
                _pdfVisibility = value;
                RaisePropertyChanged(() => PDFVisibility);
            }
        }

        #endregion

        #region PDF文档内容

        private MemoryStream _pdfContent;

        /// <summary>
        ///     PDF文档内容
        /// </summary>
        public MemoryStream PDFContent
        {
            get { return _pdfContent; }
            private set
            {
                if (_pdfContent == value) return;
                _pdfContent = value;
                RaisePropertyChanged(() => PDFContent);
            }
        }

        #endregion

        #endregion

        #region 加载数据

        public override void LoadData()
        {
        }

        #region 文档

        /// <summary>
        ///     选中的文档
        /// </summary>
        private DocumentDTO _selDocumentDto;

        /// <summary>
        ///     文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentDTO> ViewDocumentDto { get; set; }

        public DocumentDTO SelDocumentDto
        {
            get { return _selDocumentDto; }
            set
            {
                if (_selDocumentDto == value) return;
                _selDocumentDto = value;
                RaisePropertyChanged(() => SelDocumentDto);
            }
        }

        /// <summary>
        ///     初始化文档集合
        /// </summary>
        private void InitializeViewDocumentDto()
        {
            ViewDocumentDto = _service.CreateCollection(_context.Documents);
            _service.RegisterCollectionView(ViewDocumentDto);
            DocumentTypes = new QueryableDataServiceCollectionView<DocumentTypeDTO>(_context, _context.DocumentTypes);
        }

        #endregion

        #region 文档类型

        private DocumentTypeDTO _documentType;

        /// <summary>
        ///     是否可见
        /// </summary>
        private Visibility _documentTypeVisibility;

        public QueryableDataServiceCollectionView<DocumentTypeDTO> DocumentTypes { get; set; }

        public DocumentTypeDTO DocumentType
        {
            get { return _documentType; }
            set
            {
                _documentType = value;
                if (_documentType != null)
                    _addedDocument.DocumentTypeId = _documentType.DocumentTypeId;
                RaisePropertyChanged(() => DocumentType);
            }
        }

        public Visibility DocumentTypeVisibility
        {
            get { return _documentTypeVisibility; }
            set
            {
                if (_documentTypeVisibility == value) return;
                _documentTypeVisibility = value;
                RaisePropertyChanged(() => DocumentTypeVisibility);
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #region 保存命令

        /// <summary>
        ///     保存命令
        /// </summary>
        public DelegateCommand<object> SaveCommand { get; private set; }

        private void OnSave(object obj)
        {
            if (_fromServer)
            {
                windowClosed(_loadedDocument);
                MessageAlert("提示", "保存成功。");
            }
            else
            {
                if (_addedDocument.DocumentTypeId == 0)
                {
                    MessageAlert("请选择文档类型！");
                    return;
                }
                IsBusy = true;
                ViewDocumentDto.AddNew(_addedDocument);
                _service.SubmitChanges(sm =>
                {
                    IsBusy = false;
                    windowClosed(_addedDocument);
                    MessageAlert("提示", sm.Error == null ? "保存成功。" : "保存失败，请检查！");
                });
            }
        }

        private bool CanSave(object obj)
        {
            return true;
        }

        #endregion

        #region 查询文档相关操作

        /// <summary>
        ///     添加文档
        /// </summary>
        /// <param name="fi">文件</param>
        /// <param name="callback">回调操作</param>
        /// <param name="docTypeIds">文档类型ID</param>
        internal void InitDocument(FileInfo fi, Action<DocumentDTO> callback, int[] docTypeIds)
        {
            _fromServer = false;
            DocumentTypeVisibility = Visibility.Visible;

            DocTypeIds = docTypeIds;
            if (docTypeIds.Length == 0)
                _filter.FilteringExpression = e => true;
            else
            {
                var pe = Expression.Parameter(typeof (DocumentTypeDTO), "doc");
                var docTypeId = Expression.Property(pe, (typeof (DocumentTypeDTO)).GetProperty("DocumentTypeId"));

                Expression expression = null;
                for (var i = 0; i < docTypeIds.Length; i++)
                {
                    var right = Expression.Constant(docTypeIds[i], typeof (int));
                    var subExp = Expression.Equal(docTypeId, right);
                    if (i == 0) expression = subExp;
                    else
                    {
                        if (expression != null)
                            expression = Expression.OrElse(expression, subExp);
                    }
                }

                if (expression == null) throw new Exception("未生成Lambda表达式");

                var lambda = Expression.Lambda<Func<DocumentTypeDTO, bool>>(expression, new[] {pe});
                _filter.FilteringExpression = lambda;
            }

            if (!DocumentTypes.AutoLoad) DocumentTypes.AutoLoad = true;
            else DocumentTypes.Load(true);

            windowClosed = callback;
            ViewDocument(fi);
        }

        /// <summary>
        ///     查看文档
        /// </summary>
        /// <param name="docId">文档ID</param>
        internal void InitDocument(Guid docId)
        {
            DocumentTypeVisibility = Visibility.Collapsed;
            Title = string.Empty;
            PDFVisibility = Visibility.Collapsed;
            WordVisibility = Visibility.Collapsed;
            if (_loadedDocument != null && docId == _loadedDocument.DocumentId)
            {
                ViewDocument(_loadedDocument);
            }
            else
            {
                GetSingleDocument(docId);
            }
        }

        internal void AddDocument(DocumentDTO document, bool fromServer, Action<DocumentDTO> callback)
        {
            DocumentTypeVisibility = Visibility.Collapsed;
            _fromServer = fromServer;
            windowClosed = callback;
            if (_loadedDocument != null && document.DocumentId == _loadedDocument.DocumentId)
            {
                ViewDocument(_loadedDocument);
            }
            else
            {
                GetSingleDocument(document.DocumentId);
            }
        }

        /// <summary>
        ///     展示从本地添加的文档
        ///     <remarks>
        ///         需要将文档相关内容赋给本地的_currentDocument。
        ///     </remarks>
        /// </summary>
        /// <param name="fi">文件</param>
        private void ViewDocument(FileInfo fi)
        {
            var extension = fi.Extension.ToLower();
            var length = (int) fi.Length;
            var fs = fi.OpenRead();
            _content = new byte[length];
            using (fs)
            {
                fs.Read(_content, 0, length);
            }
            switch (extension.ToLower())
            {
                case ".docx":
                    PDFVisibility = Visibility.Collapsed;
                    WordVisibility = Visibility.Visible;
                    WordContent = _content;
                    break;
                case ".pdf":
                    WordVisibility = Visibility.Collapsed;
                    PDFVisibility = Visibility.Visible;
                    PDFContent = new MemoryStream(_content);
                    break;
                default:
                    MessageAlert("不是合法的Word文档或者PDF文档！");
                    break;
            }
            Title = fi.Name;
            _addedDocument = new DocumentDTO
            {
                DocumentId = Guid.NewGuid(),
                Name = fi.Name,
                Extension = extension,
                FileStorage = _content
            };
        }

        /// <summary>
        ///     展示从服务端获取的文档
        /// </summary>
        /// <param name="doc">获取的文档</param>
        private void ViewDocument(DocumentDTO doc)
        {
            var extension = doc.Extension;
            _content = doc.FileStorage;
            switch (extension)
            {
                case ".docx":
                    PDFVisibility = Visibility.Collapsed;
                    WordVisibility = Visibility.Visible;
                    WordContent = _content;
                    break;
                case ".pdf":
                    WordVisibility = Visibility.Collapsed;
                    PDFVisibility = Visibility.Visible;
                    PDFContent = new MemoryStream(_content);
                    break;
                default:
                    MessageAlert("不是合法的Word文档或者PDF文档！");
                    break;
            }
            Title = doc.Name;
        }

        #endregion

        #region 获取单个文档信息

        private void GetSingleDocument(Guid documentId)
        {
            var uri = GetDocumentUri(documentId);
            IsBusy = true;
            _context.BeginExecute<DocumentDTO>(uri,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as CommonServiceData;
                    try
                    {
                        if (context != null)
                        {
                            _loadedDocument = context.EndExecute<DocumentDTO>(result).FirstOrDefault();
                            if (_loadedDocument != null)
                                ViewDocument(_loadedDocument);
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

        private Uri GetDocumentUri(Guid documentId)
        {
            return new Uri(string.Format("GetSingleDocument?documentId='{0}'", documentId),
                UriKind.Relative);
        }

        #endregion

        #region 保存文档到本地

        /// <summary>
        ///     保存命令
        /// </summary>
        public DelegateCommand<object> SaveDocumentToLocalCommand { get; set; }

        private void OnSaveDocumentToLocal(object obj)
        {
            var saveFileDialog = new SaveFileDialog
            {
                DefaultFileName = Title,
                Filter = Title.ToLower().EndsWith(".docx")
                    ? "Word文档(*.docx)|*.docx"
                    : "PDF文档(*.pdf)|*.pdf"
            };

            if (saveFileDialog.ShowDialog() != true) return;
            var fileStream = (FileStream) saveFileDialog.OpenFile();
            fileStream.Write(_content, 0, _content.Length);
            MessageAlert("保存成功。");
            fileStream.Dispose();
        }

        private bool CanSaveDocumentToLocal(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}