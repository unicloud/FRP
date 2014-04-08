#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/16 16:56:06
// 文件名：UserRoleDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/16 16:56:06
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    public class UserRoleDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageUserInRoleVm>();
            viewModel.AddUserRole();
        }
        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            return true;
        }
    }
}
