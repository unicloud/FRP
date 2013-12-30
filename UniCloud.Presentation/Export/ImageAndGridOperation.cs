#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 11:21:52
// 文件名：ImageAndGridOperation
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 11:21:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Export
{
    /// <summary>
    /// 图表导出操作
    /// </summary>
    public static class ImageAndGridOperation
    {
        /// <summary>
        /// 保存成文件流
        /// </summary>
        /// <param name="filter">保存格式</param>
        /// <returns></returns>
        public static Stream DowmLoadDialogStream(string filter)
        {
            //保存对话框
            var dialog = new SaveFileDialog {Filter = filter};
            //保存的格式


            if (!(bool)dialog.ShowDialog())
                return null;
            //获取保存的文件流
            return dialog.OpenFile();
        }

        /// <summary>
        /// 设置导出样式
        /// </summary>
        /// <returns></returns>
        public static GridViewExportOptions SetGridViewExportOptions()
        {
            var exportOptions = new GridViewExportOptions
                                {
                                    Encoding = System.Text.Encoding.UTF8,
                                    ShowColumnFooters = false,
                                    ShowColumnHeaders = true,
                                    ShowGroupFooters = false
                                };
            //exportOptions.Format = ExportFormat.Html;
            return exportOptions;

        }


        /// <summary>
        /// 创建RadGridView
        /// </summary>
        /// <param name="structs">RadGridView列</param>
        /// <param name="itemsSource">数据源</param>
        /// <param name="headerName">RadGridView名称</param>
        /// <returns></returns>
        public static RadGridView CreatDataGridView(Dictionary<string, string> structs, IEnumerable<object> itemsSource, string headerName)
        {
            var rgView = new RadGridView
                         {
                             ShowGroupPanel = false,
                             AutoGenerateColumns = false,
                             IsReadOnly = true,
                             Name = headerName,
                             RowIndicatorVisibility = Visibility.Collapsed
                         };

            foreach (var item in structs.Keys)
            {
                var gvColumn = new GridViewDataColumn
                               {
                                   Header = structs[item],
                                   IsFilterable = false,
                                   IsSortable = false,
                                   DataMemberBinding = new System.Windows.Data.Binding(item)
                               };
                rgView.Columns.Add(gvColumn);

            }
            rgView.ItemsSource = itemsSource;


            return rgView;
        }
    }
}
