#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 11:18:59
// 文件名：RoleCommonMethod
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 11:18:59
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    public class RoleCommonMethod
    {
        #region 筛选角色功能
        public static void SelectFunctionItems(RoleDTO role, List<FunctionItemDTO> functionItems)
        {
            for (int i = functionItems.Count - 1; i >= 0; i--)
            {
                var temp = functionItems[i];
                if (role.RoleFunctions.All(p => p.FunctionItemId != temp.Id))
                {
                    functionItems.Remove(temp);
                    continue;
                }
                SelectFunctionItems(role, temp.SubFunctionItems);
            }
        }

        private static void SelectFunctionItems(RoleDTO role, DataServiceCollection<FunctionItemDTO> functionItems)
        {
            for (int i = functionItems.Count - 1; i >= 0; i--)
            {
                var temp = functionItems[i];
                if (role.RoleFunctions.All(p => p.FunctionItemId != temp.Id))
                {
                    functionItems.Remove(temp);
                    continue;
                }
                SelectFunctionItems(role, temp.SubFunctionItems);
            }
        }
        #endregion
    }
}
