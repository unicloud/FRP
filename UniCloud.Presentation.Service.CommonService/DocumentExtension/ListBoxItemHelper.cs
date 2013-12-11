﻿#region 版本信息

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
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Service.CommonService.DocumentExtension
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
                    SmallIconPath =
                        ImagePathHelper.GetSmallImageSource(folderDocument.Extension),
                    BigIconPath =
                        ImagePathHelper.GetBigImageSource(folderDocument.Extension),
                };
            currentListBox.SubDocumentPaths.Clear();
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
                    SmallIconPath =
                        ImagePathHelper.GetSmallImageSource(p.Extension),
                    BigIconPath =
                        ImagePathHelper.GetBigImageSource(p.Extension),
                };
               currentListBox.SubDocumentPaths.Add(newListBoxItem);
               if (!newListBoxItem.IsLeaf)
                {
                    currentListBox.SubDocumentPaths.Add(newListBoxItem);
                }
            });
            return currentListBox;
         
        }

        public static ListBoxDocumentItem TransformToSubListBoxItem(ListBoxDocumentItem currentListBox,
                                                                    IEnumerable<DocumentPathDTO> subDocumentPaths)
        {
            currentListBox.SubDocumentPaths.Clear();
            currentListBox.SubFolderPaths.Clear();
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
                    DocumentGuid = p.DocumentGuid,
                    SmallIconPath =
                        ImagePathHelper.GetSmallImageSource(p.Extension),
                    BigIconPath =
                        ImagePathHelper.GetBigImageSource(p.Extension),
                };
            });
        
            //currentListBox.SubDocumentPaths = new ObservableCollection<ListBoxDocumentItem>(subListBoxItems);
            //currentListBox.SubFolderPaths = new ObservableCollection<ListBoxDocumentItem>(subListBoxItems.Where(p => !p.IsLeaf));
            return currentListBox;
        }
    }
}