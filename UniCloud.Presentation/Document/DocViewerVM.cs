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
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof(DocViewerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DocViewerVM : ViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;
        private readonly ICommonService _service;
        private DocumentDTO _addedDocument;
        private FilterDescriptor _docDescriptor;
        private DocumentDTO _loadedDocument;
        private Action<DocumentDTO> _windowClosed;

        [ImportingConstructor]
        public DocViewerVM(ICommonService service)
            : base(service)
        {
            _service = service;
            _context = service.Context;

            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);

            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            InitializeViewDocumentDTO();
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
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
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
                if (_wordVisibility != value)
                {
                    _wordVisibility = value;
                    RaisePropertyChanged(() => WordVisibility);
                }
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
                if (_wordContent != value)
                {
                    _wordContent = value;
                    RaisePropertyChanged(() => WordContent);
                }
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
                if (_pdfVisibility != value)
                {
                    _pdfVisibility = value;
                    RaisePropertyChanged(() => PDFVisibility);
                }
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
                if (_pdfContent != value)
                {
                    _pdfContent = value;
                    RaisePropertyChanged(() => PDFContent);
                }
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
        ///     文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentDTO> ViewDocumentDTO { get; set; }

        /// <summary>
        ///     选中的文档
        /// </summary>
        private DocumentDTO _selDocumentDTO;
        public DocumentDTO SelDocumentDTO
        {
            get { return _selDocumentDTO; }
            private set
            {
                if (_selDocumentDTO != value)
                {
                    _selDocumentDTO = value;
                    RaisePropertyChanged(() => SelDocumentDTO);
                }
            }
        }

        /// <summary>
        ///     初始化文档集合
        /// </summary>
        private void InitializeViewDocumentDTO()
        {
            ViewDocumentDTO = _service.CreateCollection(_context.Documents);
            _docDescriptor = new FilterDescriptor("DocumentId", FilterOperator.IsEqualTo, Guid.Empty);
            ViewDocumentDTO.FilterDescriptors.Add(_docDescriptor);
            _service.RegisterCollectionView(ViewDocumentDTO);
            ViewDocumentDTO.LoadedData += (o, e) =>
            {
                _loadedDocument = (o as QueryableDataServiceCollectionView<DocumentDTO>).FirstOrDefault();
                if (_loadedDocument != null)
                    ViewDocument(_loadedDocument);
            };
            DocumentTypes = new QueryableDataServiceCollectionView<DocumentTypeDTO>(_context, _context.DocumentTypes);
        }

        #endregion

        #region 文档类型
        public QueryableDataServiceCollectionView<DocumentTypeDTO> DocumentTypes { get; set; }

        private DocumentTypeDTO _documentType;
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

        /// <summary>
        ///    是否可见
        /// </summary>
        private Visibility _documentTypeVisibility;
        public Visibility DocumentTypeVisibility
        {
            get { return _documentTypeVisibility; }
            set
            {
                if (_documentTypeVisibility != value)
                {
                    _documentTypeVisibility = value;
                    RaisePropertyChanged(() => DocumentTypeVisibility);
                }
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
            if (_addedDocument.DocumentTypeId == 0)
            {
                MessageAlert("请选择文档类型！");
                return;
            }
            IsBusy = true;
            ViewDocumentDTO.AddNew(_addedDocument);
            _service.SubmitChanges(sm =>
            {
                IsBusy = false;
                _windowClosed(_addedDocument);
                MessageAlert("提示", sm.Error == null ? "保存成功。" : "保存失败，请检查！");
            });
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
        internal void InitDocument(FileInfo fi, Action<DocumentDTO> callback)
        {
            DocumentTypeVisibility = Visibility.Visible;
            DocumentTypes.Load();
            _windowClosed = callback;
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
                _docDescriptor.Value = docId;
                ViewDocumentDTO.Load(true);
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
            var length = (int)fi.Length;
            var fs = fi.OpenRead();
            var content = new byte[length];
            using (fs)
            {
                fs.Read(content, 0, length);
            }
            switch (extension.ToLower())
            {
                case ".docx":
                    PDFVisibility = Visibility.Collapsed;
                    WordVisibility = Visibility.Visible;
                    WordContent = content;
                    break;
                case ".pdf":
                    WordVisibility = Visibility.Collapsed;
                    PDFVisibility = Visibility.Visible;
                    PDFContent = new MemoryStream(content);
                    break;
                default:
                    throw new ArgumentException("不是合法的Word文档或者PDF文档！");
            }
            Title = fi.Name;
            _addedDocument = new DocumentDTO
            {
                DocumentId = Guid.NewGuid(),
                Name = fi.Name,
                Extension = extension,
                FileStorage = content
            };
        }

        /// <summary>
        ///     展示从服务端获取的文档
        /// </summary>
        /// <param name="doc">获取的文档</param>
        private void ViewDocument(DocumentDTO doc)
        {
            var extension = doc.Extension;
            var content = doc.FileStorage;
            switch (extension)
            {
                case ".docx":
                    PDFVisibility = Visibility.Collapsed;
                    WordVisibility = Visibility.Visible;
                    WordContent = content;
                    break;
                case ".pdf":
                    WordVisibility = Visibility.Collapsed;
                    PDFVisibility = Visibility.Visible;
                    PDFContent = new MemoryStream(content);
                    break;
                default:
                    throw new ArgumentException("不是合法的Word文档或者PDF文档！");
            }
            Title = doc.Name;
        }

        #endregion

        #endregion
    }
}