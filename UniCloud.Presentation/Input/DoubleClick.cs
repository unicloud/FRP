#region 命名空间

using System.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Input
{
    public class DoubleClick<THelper>
        where THelper : GridViewDoubleClickHelper
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
        public static readonly DependencyProperty HelperProperty = DependencyProperty.RegisterAttached("Helper", typeof(THelper), typeof(DoubleClick<THelper>), new Telerik.Windows.PropertyMetadata(OnHelperChanged));

        static DoubleClick()
        {
        }

        public static void OnHelperChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
            var grid = dependencyObject as RadGridView;
            var helper = e.NewValue as THelper;
            if (helper == null)
                return;
            helper.HookEvents(grid);
        }

    }
}
