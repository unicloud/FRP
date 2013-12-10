#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

#endregion

namespace UniCloud.Presentation.Service.CommonService.DocumentExtension
{
    /// <summary>
    ///     Document 转化为ExplorerItem
    /// </summary>
    public class ListBoxDocumentItem
    {
        public ListBoxDocumentItem()
        {
            SubListBoxDocumentItems=new ObservableCollection<ListBoxDocumentItem>();
        }

        /// <summary>
        ///     主键
        /// </summary>
        public Guid ListBoxItemId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父亲节点
        /// </summary>
        public Guid? ParentFolderId { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     是否是文件夹
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        ///     是否是自定可以增加的按钮
        /// </summary>
        public bool IsAddButton { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 图片缩略图路径
        /// </summary>
        public ImageSource SmallIconPath { get; set; }

        //图片大图途径
        public ImageSource BigIconPath { get; set; }

        /// <summary>
        /// 子项文档的文件夹
        /// </summary>
        public ObservableCollection<ListBoxDocumentItem> SubPathItems { get; set; }

        /// <summary>
        /// 子项文件夹文档
        /// </summary>
        public ObservableCollection<ListBoxDocumentItem> SubListBoxDocumentItems { get; set; }
    }
}