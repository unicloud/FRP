#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/11，10:12
// 文件名：ListBoxDocumentItem.cs
// 程序集：UniCloud.Presentation.Service.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using UniCloud.Presentation.Service.Purchase.Annotations;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase.DocumentExtension
{
    /// <summary>
    ///     Document 转化为ExplorerItem
    /// </summary>
    public class ListBoxDocumentItem : INotifyPropertyChanged
    {
        readonly PurchaseData _context = new PurchaseData(AgentHelper.PurchaseUri);
        public ListBoxDocumentItem()
        {
            SubDocumentPaths = new ObservableCollection<ListBoxDocumentItem>();
            SubFolders = new ObservableCollection<ListBoxDocumentItem>();
            SubFolders.CollectionChanged += (o, e) =>
                                            {
                                                if (e.NewItems != null)
                                                    foreach (INotifyPropertyChanged item in e.NewItems)
                                                    {
                                                        var temp = item as ListBoxDocumentItem;
                                                        if (temp != null && temp.ParentId != DocumentPathId)
                                                        {
                                                            ModifyDocumentPath(temp.DocumentPathId, temp.Name,
                                                                DocumentPathId);
                                                        }
                                                    }
                                            };
        }

        #region 属性
        /// <summary>
        ///     主键
        /// </summary>
        public int DocumentPathId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        ///     是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid? DocumentGuid { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     父节点ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        ///     子项文档集合
        /// </summary>
        public ObservableCollection<ListBoxDocumentItem> SubDocumentPaths { get; set; }

        /// <summary>
        /// 子项文件夹
        /// </summary>
        public ObservableCollection<ListBoxDocumentItem> SubFolders { get; set; }

        /// <summary>
        ///     图片缩略图路径
        /// </summary>
        public ImageSource SmallIconPath { get; set; }

        //图片大图途径
        public ImageSource BigIconPath { get; set; }

        /// <summary>
        /// 全局路径
        /// </summary>
        public string FullPath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 操作

        private void ModifyDocumentPath(int documentPathId, string name, int? parentId)
        {
            var modifyDocPath = ModifyDocumentPathUri(documentPathId, name, parentId);

            _context.BeginExecute<string>(modifyDocPath, p => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
            }), null);
        }

        private Uri ModifyDocumentPathUri(int documentPathId, string name, int? parentId)
        {
            return new Uri(string.Format("ModifyDocPath?docPathId={0}&name='{1}'&parentId={2}", documentPathId, name, parentId),
                UriKind.Relative);
        }
        #endregion
    }
}