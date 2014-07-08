#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/5 9:25:16
// 文件名：PlanAircraftInput
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Purchase.Reception;
using UniCloud.Presentation.Service.Purchase.Purchase;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

#endregion

namespace UniCloud.Presentation.Purchase.Input
{

    #region 计划飞机

    /// <summary>
    ///     拖放行为
    /// </summary>
    public class PlanAircraftDragDrop : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            return true;
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            return true;
        }

        public override void Drop(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<MatchingPlanAircraftManagerVM>();
            var items = (from object item in state.DraggedItems select item).ToList();
            var planAircraft = items[0] as PlanAircraftDTO;
            if (planAircraft == null)
                MessageBox.Show("请选中计划飞机！");
            else if (viewModel.SelContractAircraft == null)
                MessageBox.Show("请选中的合同飞机！");
            else if (viewModel.SelContractAircraft != null && viewModel.SelContractAircraft.PlanAircraftID == null)
            {
                viewModel.SelContractAircraft.PlanAircraftID = planAircraft.Id;
                //viewModel.SelContractAircraft.PlanAircraft = planAircraft.RegNumber;
            }
        }

        public override void DragDropCompleted(GridViewDragDropState state)
        {
        }
    }

    /// <summary>
    ///     拖放展示
    /// </summary>
    public class PlanAircraftDragVisual : IDragVisualProvider
    {
        public FrameworkElement CreateDragVisual(DragVisualProviderState state)
        {
            var visual = new DragVisual
            {
                Content = state.DraggedItems.OfType<object>().FirstOrDefault(),
                ContentTemplate = state.Host.Resources["PlanAircraftDraggedItemTemplate"] as DataTemplate
            };
            return visual;
        }

        public Point GetDragVisualOffset(DragVisualProviderState state)
        {
            return state.RelativeStartPoint;
        }

        public bool UseDefaultCursors { get; set; }
    }

    /// <summary>
    ///     鼠标双击逻辑
    /// </summary>
    public class ContractAircraftClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<MatchingPlanAircraftManagerVM>();
            var contractAircraft = cell.DataContext as ContractAircraftDTO;
            if (viewModel.SelContractAircraft == null)
                RadWindow.Alert(SetAlter("提醒", "确认", "请选中未匹配的合同飞机！", 13, 250, () => { }));
            else if (viewModel.SelContractAircraft != null && viewModel.SelContractAircraft.PlanAircraftID == null)
            {
                viewModel.PlanAircraftChildView.ShowDialog();
                //RadWindow.Confirm(this.SetConfirm("匹配计划飞机", "确认", "取消", "为选中的合同飞机匹配计划！", 13, 250, (o, e) =>
                //        {
                //            if (e.DialogResult == true)
                //            {
                //                viewModel.PlanAircraftChildView.ShowDialog();
                //            }
                //        }));
            }
            else if (viewModel.SelContractAircraft != null &&
                     viewModel.SelContractAircraft.PlanAircraftID != null)
            {
                RadWindow.Confirm(SetConfirm("重新匹配", "确认", "取消", "此合同飞机已匹配计划飞机，是否重新匹配！", 13, 250, (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        viewModel.PlanAircraftChildView.ShowDialog();
                    }
                }));
            }
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            return true;
        }

        /// <summary>
        ///     设置提醒对话框
        /// </summary>
        /// <param name="header">对话框标题</param>
        /// <param name="okContent">Ok按钮显示内容</param>
        /// <param name="content">显示内容</param>
        /// <param name="fontSize">字号</param>
        /// <param name="width">对话框宽度</param>
        /// <param name="callBack">关闭对话框后执行的操作</param>
        /// <returns>提醒对话框</returns>
        protected DialogParameters SetAlter(
            string header,
            string okContent,
            string content,
            int fontSize,
            int width,
            Action callBack)
        {
            var alter = new DialogParameters
            {
                Header = header,
                OkButtonContent = okContent,
                Content = new TextBlock
                {
                    Text = content,
                    FontFamily = new FontFamily("Microsoft YaHei UI"),
                    FontSize = fontSize,
                    TextWrapping = TextWrapping.Wrap,
                    Width = width,
                },
                Closed = (o, e) => callBack(),
            };
            return alter;
        }

        /// <summary>
        ///     设置确认对话框
        /// </summary>
        /// <param name="header">对话框标题</param>
        /// <param name="okContent">Ok按钮显示内容</param>
        /// <param name="cancelContent">Cancel按钮显示内容</param>
        /// <param name="content">显示内容</param>
        /// <param name="fontSize">字号</param>
        /// <param name="width">对话框宽度</param>
        /// <param name="closed">关闭对话框后执行的操作</param>
        /// <returns>确认对话框</returns>
        protected DialogParameters SetConfirm(
            string header,
            string okContent,
            string cancelContent,
            string content,
            int fontSize,
            int width,
            EventHandler<WindowClosedEventArgs> closed)
        {
            var confirm = new DialogParameters
            {
                Header = header,
                OkButtonContent = okContent,
                CancelButtonContent = cancelContent,
                Content = new TextBlock
                {
                    Text = content,
                    FontFamily = new FontFamily("Microsoft YaHei UI"),
                    FontSize = fontSize,
                    TextWrapping = TextWrapping.Wrap,
                    Width = width,
                },
                Closed = closed,
            };
            return confirm;
        }
    }

    #endregion
}