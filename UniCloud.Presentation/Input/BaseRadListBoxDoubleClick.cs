#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/31 11:06:38
// 文件名：BaseRadListBoxDoubleClick
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/31 11:06:38
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Input
{
    public class BaseRadListBoxDoubleClick<THelper>
        where THelper : RadListBoxDoubleClickHelper
    {
        public static THelper GetHelper(DependencyObject obj)
        {
            return (THelper)obj.GetValue(HelperProperty);
        }

        public static void SetHelper(DependencyObject obj, THelper value)
        {
            obj.SetValue(HelperProperty, value);
        }

		// 依赖项属性
        public static readonly DependencyProperty HelperProperty = DependencyProperty.RegisterAttached("Helper", typeof(THelper), typeof(BaseRadListBoxDoubleClick<THelper>), new Telerik.Windows.PropertyMetadata(OnHelperChanged));

        static BaseRadListBoxDoubleClick()
        {
        }

        public static void OnHelperChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
            var listBox = dependencyObject as RadListBox;
            var helper = e.NewValue as THelper;
            if (helper == null)
                return;
            helper.HookEvents(listBox);
        }
    }
}
