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
        /// <param name="parentListBoxItem">父亲节点ListBox</param>
        /// <param name="folderDocument">需要转化的FolderDocumentDTO</param>
        /// <returns></returns>
        public static ListBoxDocumentItem TransformToListBoxItem(ListBoxDocumentItem parentListBoxItem,
                                                                 DocumentPathDTO folderDocument)
        {
            if (folderDocument == null)
                throw new Exception("文件夹文档不能为空");
            ListBoxDocumentItem currentListBox;
            if (parentListBoxItem == null)
            {
                currentListBox = new ListBoxDocumentItem
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
            }
            else
            {
                currentListBox = parentListBoxItem;
            }
            var subListBoxItems = new List<ListBoxDocumentItem>(); //子项文件夹与文档
            //文件夹类型遍历
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
                    subListBoxItems.Add(newListBoxItem);
                });
            currentListBox.SubDocumentPaths = subListBoxItems;
            currentListBox.SubFolderPaths = subListBoxItems.Where(p => !p.IsLeaf).ToList();
            return currentListBox;
        }
    }
}