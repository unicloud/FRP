#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：wuql 时间：2013/11/12 14:46:43
// 文件名：BaseListBoxDoubleClick
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Windows;

#endregion

namespace UniCloud.Presentation.Input
{
    public class BaseListBoxDoubleClick<THelper>
        where THelper : ListBoxDoubleClickHelper
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
        public static readonly DependencyProperty HelperProperty = DependencyProperty.RegisterAttached("Helper", typeof(THelper), typeof(BaseListBoxDoubleClick<THelper>), new Telerik.Windows.PropertyMetadata(OnHelperChanged));

        static BaseListBoxDoubleClick()
        {
        }

        public static void OnHelperChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
            var listBox = dependencyObject as System.Windows.Controls.ListBox;
            var helper = e.NewValue as THelper;
            if (helper == null)
                return;
            helper.HookEvents(listBox);
        }
    }
}
