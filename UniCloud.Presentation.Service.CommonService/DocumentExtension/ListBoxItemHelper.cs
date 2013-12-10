using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using UniCloud.Presentation.Service.CommonService.Common;

namespace UniCloud.Presentation.Service.CommonService.DocumentExtension
{
    /// <summary>
    /// 文件夹文档转化ListBoxItem
    /// </summary>
    public static class ListBoxItemHelper
    { 

        private static readonly ImageSourceConverter Isc;
        static ListBoxItemHelper()
        {
             Isc = new ImageSourceConverter();
        }

        /// <summary>
        /// 把folderDocument转为ListBoxItem
        /// </summary>
        /// <param name="parentListBoxItem">父亲节点ListBox</param>
        /// <param name="folderDocument">需要转化的FolderDocumentDTO</param>
        /// <returns></returns>
        public static ListBoxDocumentItem TransformToListBoxItem(ListBoxDocumentItem parentListBoxItem,
                                                                 FolderDocumentDTO folderDocument)
        {
            if (folderDocument == null)
                throw new Exception("文件夹文档不能为空");
            ListBoxDocumentItem currentListBox;
            if (parentListBoxItem==null)
            {
                currentListBox = new ListBoxDocumentItem
                    {
                        Extension = null,
                        IsAddButton = false,
                        IsFolder = true,
                        ListBoxItemId = folderDocument.FolderId,
                        Name = folderDocument.Name,
                        SmallIconPath =
                            (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/folder.png"),
                        BigIconPath =
                            (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/bigFolder.png"),
                    };
            }
            else
            {
                currentListBox = parentListBoxItem;
            }
            var subListBoxItems = new ObservableCollection<ListBoxDocumentItem>();//子项文件夹与文档
                var subFolderListBoxItems = new ObservableCollection<ListBoxDocumentItem>();//子项文件夹
                //文件夹类型遍历
                folderDocument.SubFolders.ToList().ForEach(p =>
                {
                    var newListBoxItem = new ListBoxDocumentItem
                    {
                        Extension = null,
                        IsAddButton = false,
                        IsFolder = true,
                        ListBoxItemId = p.FolderId,
                        Name = p.Name,
                        UpdateDateTime = p.UpdteDateTime,
                        ParentFolderId = p.ParentFolderId,
                        SmallIconPath = (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/folder.png"),
                        BigIconPath = (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/bigFolder.png"),
                    };
                    subListBoxItems.Add(newListBoxItem);
                    subFolderListBoxItems.Add(newListBoxItem);
                });
                //文档类型遍历
                folderDocument.Documents.ToList().ForEach(p =>
                {
                    var newListBoxItem = new ListBoxDocumentItem
                    {
                        Extension = p.ExtendType,
                        IsAddButton = false,
                        IsFolder = false,
                        ListBoxItemId = p.FolderId,
                        Name = p.Name,
                        ParentFolderId = p.FolderId,
                        SmallIconPath = GetImageSource(p.ExtendType),
                        BigIconPath = GetImageSource(p.ExtendType)
                    };
                    subListBoxItems.Add(newListBoxItem);
                });
            currentListBox.SubListBoxDocumentItems = subListBoxItems;
            currentListBox.SubPathItems = subFolderListBoxItems;
                return currentListBox;
        }

        /// <summary>
        /// 获取图标路径
        /// </summary>
        /// <param name="extendType"></param>
        /// <returns></returns>
        private static ImageSource GetImageSource(string extendType)
        {
           
            switch (extendType)
            {
                case "pdf":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/pdf.png");
                case "doc":
                case "docx":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/doc.png");
                case "zip":
                case "rar":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/rar.png");
                case "jpg":
                case "png":
                case "bmp":
                case "jpeg":
                case "gif":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/jpg.png");
                case "xlsx":
                case "xls":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/xls.png");
                default:
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/folder.png");
            }
        }
    }
}