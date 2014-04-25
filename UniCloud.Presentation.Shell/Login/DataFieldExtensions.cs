﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 14:19:00
// 文件名：DataFieldExtensions
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 14:19:00
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.Shell.Login
{
    /// <summary>
    ///     Provides extension methods for performing operations on a <see cref="DataField"/>.
    /// </summary>
    public static class DataFieldExtensions
    {
        /// <summary>
        /// Replaces a <see cref="DataField" />'s <see cref="TextBox" /> control with another control and updates the bindings.
        /// </summary>
        /// <param name="field">The <see cref="DataField"/> whose <see cref="TextBox"/> will be replaced.</param>
        /// <param name="newControl">The new control you're going to set as <see cref="DataField.Content" />.</param>
        /// <param name="dataBindingProperty">The control's property that will be used for data binding.</param>        
        public static void ReplaceTextBox(this DataField field, FrameworkElement newControl, DependencyProperty dataBindingProperty)
        {
            field.ReplaceTextBox(newControl, dataBindingProperty, null);
        }

        /// <summary>
        /// Replaces a <see cref="DataField" />'s <see cref="TextBox" /> control with another control and updates the bindings.
        /// </summary>
        /// <param name="field">The <see cref="DataField"/> whose <see cref="TextBox"/> will be replaced.</param>
        /// <param name="newControl">The new control you're going to set as <see cref="DataField.Content" />.</param>
        /// <param name="dataBindingProperty">The control's property that will be used for data binding.</param>        
        /// <param name="bindingSetupFunction">
        ///  An optional <see cref="Action"/> you can use to change parameters on the newly generated binding before it is applied to <paramref name="newControl"/>
        /// </param>
        public static void ReplaceTextBox(this DataField field, FrameworkElement newControl, DependencyProperty dataBindingProperty, Action<Binding> bindingSetupFunction)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            if (newControl == null)
            {
                throw new ArgumentNullException("newControl");
            }

            // Construct new binding by copying existing one, and sending it to bindingSetupFunction for any changes the caller wants to perform.
            Binding newBinding = field.Content.GetBindingExpression(TextBox.TextProperty).ParentBinding.CreateCopy();

            if (bindingSetupFunction != null)
            {
                bindingSetupFunction(newBinding);
            }

            // Replace field
            newControl.SetBinding(dataBindingProperty, newBinding);
            field.Content = newControl;
        }

        /// <summary>
        /// Creates a new <see cref="Binding"/> object by copying all properties from another <see cref="Binding"/> object.
        /// </summary>
        /// <param name="binding"><see cref="Binding"/> from which property values will be copied</param>
        /// <returns>A new <see cref="Binding"/> object.</returns>
        private static Binding CreateCopy(this Binding binding)
        {
            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }

            var newBinding = new Binding
                             {
                                 BindsDirectlyToSource = binding.BindsDirectlyToSource,
                                 Converter = binding.Converter,
                                 ConverterParameter = binding.ConverterParameter,
                                 ConverterCulture = binding.ConverterCulture,
                                 Mode = binding.Mode,
                                 NotifyOnValidationError = binding.NotifyOnValidationError,
                                 Path = binding.Path,
                                 UpdateSourceTrigger = binding.UpdateSourceTrigger,
                                 ValidatesOnExceptions = binding.ValidatesOnExceptions
                             };

            if (binding.ElementName != null)
            {
                newBinding.ElementName = binding.ElementName;
            }
            else if (binding.RelativeSource != null)
            {
                newBinding.RelativeSource = binding.RelativeSource;
            }
            else
            {
                newBinding.Source = binding.Source;
            }

            return newBinding;
        }
    }
}