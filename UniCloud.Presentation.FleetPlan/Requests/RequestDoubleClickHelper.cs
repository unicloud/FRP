#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，10:12
// 文件名：RequestDoubleClickHelper.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.Input;

namespace UniCloud.Presentation.FleetPlan.Requests
{
    /// <summary>
    /// 申请双击
    /// </summary>
    public class RequestDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {

        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            return true;
        }
    }
}
