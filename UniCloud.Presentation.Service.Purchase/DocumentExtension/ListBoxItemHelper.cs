#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/11，10:12
// 文件名：ListBoxItemHelper.cs
// 程序集：UniCloud.Presentation.Service.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase.DocumentExtension
{
    /// <summary>
    ///     文件夹文档转化ListBoxItem
    /// </summary>
    public static class ListBoxItemHelper
    {
        /// <summary>
        ///     把文档路径转为ListBoxItem
        /// </summary>
        /// <param name="folderDocument">需要转化的DocumentPathDTO</param>
        /// <returns></returns>
        public static ListBoxDocumentItem TransformToRootListBoxItem(DocumentPathDTO folderDocument)
        {
            if (folderDocument == null)
                throw new Exception("文件夹文档不能为空");
            var currentListBox = new ListBoxDocumentItem
                {
                    DocumentPathId = folderDocument.DocumentPathId,
                    Extension = folderDocument.Extension,
                    Name = folderDocument.Name,
                    IsLeaf = folderDocument.IsLeaf,
                    ParentId = folderDocument.ParentId,
                    DocumentGuid = folderDocument.DocumentGuid,
                    FullPath = folderDocument.Name,
                    Path = folderDocument.Path,
                    SmallIconPath =
                        ImagePathHelper.GetSmallImageSource(folderDocument.Extension),
                    BigIconPath =
                        ImagePathHelper.GetBigImageSource(folderDocument.Extension),
                };
            currentListBox.SubDocumentPaths.Clear();
            currentListBox.SubFolders.Clear();
            //子项路劲遍历
            folderDocument.SubDocumentPaths.ToList().ForEach(p =>
                {
                    var newListBoxItem = new ListBoxDocumentItem
                        {
                            DocumentPathId = p.SubDocumentPathId,
                            Extension = p.Extension,
                            Name = p.Name,
                            IsLeaf = p.IsLeaf,
                            ParentId = p.ParentId,
                            DocumentGuid = p.DocumentGuid,
                            Path = p.Path,
                            SmallIconPath =
                                ImagePathHelper.GetSmallImageSource(p.Extension),
                            BigIconPath =
                                ImagePathHelper.GetBigImageSource(p.Extension),
                            FullPath = currentListBox.FullPath + @"\" + p.Name
                        };
                    if (!p.IsLeaf)
                    {
                        currentListBox.SubFolders.Add(newListBoxItem);
                    }
                    currentListBox.SubDocumentPaths.Add(newListBoxItem);
                });
            return currentListBox;
        }

        public static ListBoxDocumentItem TransformToSubListBoxItem(ListBoxDocumentItem currentListBox,
                                                                    IEnumerable<DocumentPathDTO> subDocumentPaths)
        {
            currentListBox.SubDocumentPaths.Clear();
            currentListBox.SubFolders.Clear();
            //子项路劲遍历
            subDocumentPaths.ToList().ForEach(p =>
                {
                    var newListBoxItem = new ListBoxDocumentItem
                        {
                            DocumentPathId = p.DocumentPathId,
                            Extension = p.Extension,
                            Name = p.Name,
                            IsLeaf = p.IsLeaf,
                            ParentId = p.ParentId,
                            Path = p.Path,
                            DocumentGuid = p.DocumentGuid,
                            SmallIconPath =
                                ImagePathHelper.GetSmallImageSource(p.Extension),
                            BigIconPath =
                                ImagePathHelper.GetBigImageSource(p.Extension),
                            FullPath = currentListBox.FullPath + @"\" + p.Name,
                        };

                    if (!p.IsLeaf)
                    {
                        currentListBox.SubFolders.Add(newListBoxItem);
                    }
                    currentListBox.SubDocumentPaths.Add(newListBoxItem);
                });
            return currentListBox;
        }

        public static ObservableCollection<ListBoxDocumentItem> TransformToListBoxItems(List<DocumentPathDTO> documentPaths)
        {
            var results = new ObservableCollection<ListBoxDocumentItem>();
            documentPaths.ForEach(p => results.Add( new ListBoxDocumentItem
                                                    {
                                                        DocumentPathId = p.DocumentPathId,
                                                        Extension = p.Extension,
                                                        Name = p.Name,
                                                        IsLeaf = p.IsLeaf,
                                                        ParentId = p.ParentId,
                                                        DocumentGuid = p.DocumentGuid,
                                                        FullPath = p.Name,
                                                        Path = p.Path,
                                                        SmallIconPath =
                                                            ImagePathHelper.GetSmallImageSource(p.Extension),
                                                        BigIconPath =
                                                            ImagePathHelper.GetBigImageSource(p.Extension),
                                                    }));
            return results;
        }
    }
}