#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/11，12:11
// 文件名：MenuItemsCollection.cs
// 程序集：UniCloud.Presentation.Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.ObjectModel;

#endregion

namespace UniCloud.Presentation.Shell
{
    public class MenuItemsCollection : ObservableCollection<MenuItem>
    {
        public MenuItemsCollection()
            : this(null)
        {
        }

        public MenuItemsCollection(MenuItem parent)
        {
            Parent = parent;
        }

        public MenuItem Parent { get; set; }

        protected override void InsertItem(int index, MenuItem item)
        {
            item.Parent = Parent;
            base.InsertItem(index, item);
        }
    }
}