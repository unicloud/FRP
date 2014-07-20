#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：22:39
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Model;

#endregion

namespace UniCloud.Presentation.Document
{
    public static class SpreadsheetAttachedProperties
    {
        public static readonly DependencyProperty ExcelWorkbookProperty =
            DependencyProperty.RegisterAttached("ExcelWorkbook", typeof (Workbook),
                typeof (SpreadsheetAttachedProperties), new PropertyMetadata(null, OnExcelWorkbookChanged));

        public static Workbook GetExcelWorkbook(DependencyObject obj)
        {
            return (Workbook) obj.GetValue(ExcelWorkbookProperty);
        }

        public static void SetExcelWorkbook(DependencyObject obj, Workbook value)
        {
            obj.SetValue(ExcelWorkbookProperty, value);
        }

        private static void OnExcelWorkbookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spreadsheet = d as RadSpreadsheet;
            if (spreadsheet != null)
            {
                spreadsheet.Workbook = (Workbook) e.NewValue;
            }
        }
    }
}