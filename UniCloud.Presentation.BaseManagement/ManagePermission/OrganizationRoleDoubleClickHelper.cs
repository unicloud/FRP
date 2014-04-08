#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 10:49:40
// 文件名：OrganizationRoleDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 10:49:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    public class OrganizationRoleDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageOrganizationInRoleVm>();
            viewModel.AddOrganizationRole();
        }
        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            return true;
        }
    }
}
