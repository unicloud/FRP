﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.Service;

#endregion

namespace UniCloud.Presentation.Input
{
    public class DataPageSerialColumn : GridViewColumn
    {
        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            var textBlock = cell.Content as TextBlock ?? new TextBlock();

            textBlock.Text = string.Format("{0}",
                DataControl.Items.IndexOf(dataItem) + 1 + DataControl.Items.PageSize * DataControl.Items.PageIndex);
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.TextAlignment = TextAlignment.Center;
            return textBlock;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName.Equals("DataControl", StringComparison.OrdinalIgnoreCase))
            {
                if (DataControl != null && DataControl.Items != null)
                {
                    DataControl.Items.CollectionChanged += (s, e) =>
                    {
                        if (e.Action == NotifyCollectionChangedAction.Remove)
                        {
                            Refresh();
                        }
                    };
                }
            }
        }
    }
}