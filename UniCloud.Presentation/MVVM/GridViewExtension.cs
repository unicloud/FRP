#region

using System;
using System.Windows.Media;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Export;
using ExportFormat = UniCloud.Presentation.Export.ExportFormat;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public class GridViewBackground
    {
        #region 数据导出背景色

        public GridViewBackground()
        {
            GroupHeaderBackground = Color.FromArgb(255, 216, 216, 216);
            HeaderBackground = Color.FromArgb(255, 127, 127, 127);
            RowBackground = Color.FromArgb(255, 251, 247, 255);
        }

        /// <summary>
        ///     分组栏背景色
        /// </summary>
        public Color GroupHeaderBackground { get; set; }

        /// <summary>
        ///     标题背景色
        /// </summary>
        public Color HeaderBackground { get; set; }

        /// <summary>
        ///     行背景色
        /// </summary>
        public Color RowBackground { get; set; }

        #endregion
    }

    public static class GridViewExtension
    {
        #region 导出Excel

        public static void ExportToExcel(this RadGridView grid)
        {
            if (grid != null)
            {
                var gvBackground = new GridViewBackground();
                grid.ExportToExcel(gvBackground);
            }
        }

        public static void ExportToExcel(this RadGridView grid, GridViewBackground gvBackground)
        {
            if (grid != null)
            {
                grid.Export(new ExportSettings
                    {
                        GroupHeaderBackground = gvBackground.GroupHeaderBackground,
                        HeaderBackground = gvBackground.HeaderBackground,
                        RowBackground = gvBackground.RowBackground,
                        Format = (ExportFormat) Enum.Parse(typeof (ExportFormat), "Excel", false),
                        FileName = (string) grid.Tag
                    });
            }
        }

        #endregion

        #region 导出Word

        public static void ExportToWord(this RadGridView grid)
        {
            if (grid != null)
            {
                var gvBackground = new GridViewBackground();

                grid.ExportToWord(gvBackground);
            }
        }

        public static void ExportToWord(this RadGridView grid, GridViewBackground gvBackground)
        {
            if (grid != null)
            {
                grid.Export(new ExportSettings
                    {
                        GroupHeaderBackground = gvBackground.GroupHeaderBackground,
                        HeaderBackground = gvBackground.HeaderBackground,
                        RowBackground = gvBackground.RowBackground,
                        Format = (ExportFormat) Enum.Parse(typeof (ExportFormat), "Word", false),
                        FileName = (string) grid.Tag
                    });
            }
        }

        #endregion

        #region 导出图表数据到Excel

        /// <summary>
        ///     导出图表数据到Excel
        /// </summary>
        /// <param name="sender"></param>
        public static void ExportChartData(object sender)
        {
        }

        #endregion
    }
}