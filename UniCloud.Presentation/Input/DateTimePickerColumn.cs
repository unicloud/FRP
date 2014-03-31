#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/9/9 10:26:39
// 文件名：DateTimePickerColumn
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Input.Touch;

#endregion

namespace UniCloud.Presentation.Input
{
    public class DateTimePickerColumn : GridViewBoundColumnBase
    {
        public TimeSpan TimeInterval
        {
            get
            {
                return (TimeSpan)GetValue(TimeIntervalProperty);
            }
            set
            {
                SetValue(TimeIntervalProperty, value);
            }
        }

        public static readonly DependencyProperty TimeIntervalProperty =
            DependencyProperty.Register("TimeInterval", typeof(TimeSpan), typeof(DateTimePickerColumn), new PropertyMetadata(TimeSpan.FromHours(1d)));



        public override FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            BindingTarget = RadDateTimePicker.SelectedValueProperty;

            RadDateTimePicker picker = new RadDateTimePicker();
            TouchManager.SetIsTouchHitTestVisible(picker, false);
            picker.IsTooltipEnabled = false;
            picker.InputMode = InputMode.DatePicker;
            picker.TimeInterval = TimeInterval;

            picker.SetBinding(BindingTarget, CreateValueBinding());

            return picker;
        }

        public override object GetNewValueFromEditor(object editor)
        {
            RadDateTimePicker picker = editor as RadDateTimePicker;
            if (picker != null)
            {
                picker.DateTimeText = picker.CurrentDateTimeText;
            }

            return base.GetNewValueFromEditor(editor);
        }

        private Binding CreateValueBinding()
        {
            Binding valueBinding = new Binding();
            valueBinding.Mode = BindingMode.TwoWay;
            valueBinding.NotifyOnValidationError = true;
            valueBinding.ValidatesOnExceptions = true;
            valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            valueBinding.Path = new PropertyPath(DataMemberBinding.Path.Path);

            return valueBinding;
        }
    }
}
