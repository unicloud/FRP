#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/3 15:14:24
// 文件名：IndexAircraftInput
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    #region PlanHistory

    /// <summary>
    /// 计划明细双击处理
    /// </summary>
    public class PlanDetailDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
        }

        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            return true;
        }
    }
    #endregion

    #region ApprovalHistory

    /// <summary>
    /// 申请明细双击处理
    /// </summary>
    public class ApprovalDetailDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
        }

        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            return true;
        }
    }
    #endregion
}
