#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/21 14:50:07
// 文件名：RightAlignedLabelStrategy
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/21 14:50:07
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class RightAlignedLabelStrategy : ChartSeriesLabelStrategy
    {
        public double Offset { get; set; }

        public override LabelStrategyOptions Options
        {
            get { return LabelStrategyOptions.Arrange; }
        }

        public override RadRect GetLabelLayoutSlot(DataPoint point, FrameworkElement visual, int labelIndex)
        {

            var size = new Size(visual.ActualWidth + visual.Margin.Left + visual.Margin.Right, visual.ActualHeight + visual.Margin.Top + visual.Margin.Bottom);

            var series = (ChartSeries)point.Presenter;
            double top = point.LayoutSlot.Center.Y - (size.Height / 2);
            double left = series.Chart.PlotAreaClip.Right - size.Width + Offset;

            return new RadRect(left, top, size.Width, size.Height);
        }
    }
}
