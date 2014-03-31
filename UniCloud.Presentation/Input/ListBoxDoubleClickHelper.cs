#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：wuql 时间：2013/11/12 14:31:36
// 文件名：ListBoxDoubleClickHelper
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using Telerik.Windows;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Input;

#endregion

namespace UniCloud.Presentation.Input
{
    public abstract class ListBoxDoubleClickHelper
    {
        internal void HookEvents(System.Windows.Controls.ListBox listBox)
        {
            listBox.AddHandler(ListControl.MouseDoubleClickEvent,
                new EventHandler<MouseButtonEventArgs>(OnListBoxDoubleClick), true);
        }

        private void OnListBoxDoubleClick(object sender, MouseButtonEventArgs args)
        {

            var listBoxItem = args.Source as System.Windows.Controls.ListBoxItem;
            if (listBoxItem != null && CanDoubleClick(listBoxItem))
            {
                ListBoxDoubleClick(listBoxItem);
            }
        }

        protected abstract void ListBoxDoubleClick(System.Windows.Controls.ListBoxItem listBoxItem);

        protected abstract bool CanDoubleClick(System.Windows.Controls.ListBoxItem listBoxItem);
    }
}
