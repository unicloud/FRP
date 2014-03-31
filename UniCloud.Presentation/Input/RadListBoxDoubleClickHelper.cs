#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：wuql 时间：2013/11/12 14:44:38
// 文件名：RadListBoxDoubleClickHelper
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Input;

#endregion

namespace UniCloud.Presentation.Input
{
    public abstract class RadListBoxDoubleClickHelper 
    {
        internal void HookEvents(RadListBox listBox)
        {
            listBox.AddHandler(ListControl.MouseDoubleClickEvent,
                new EventHandler<MouseButtonEventArgs>(OnListBoxDoubleClick), true);
        }

        private void OnListBoxDoubleClick(object sender, MouseButtonEventArgs args)
        {

            var listBoxItem = args.Source as RadListBoxItem;
            if (listBoxItem != null && CanDoubleClick(listBoxItem))
            {
                ListBoxDoubleClick(listBoxItem);
            }
        }

        protected abstract void ListBoxDoubleClick(RadListBoxItem listBoxItem);

        protected abstract bool CanDoubleClick(RadListBoxItem listBoxItem);
    }
}
