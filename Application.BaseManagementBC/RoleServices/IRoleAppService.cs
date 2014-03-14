#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/14 15:14:24
// 文件名：IRoleAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/14 15:14:24
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.BaseManagementBC.DTO;

namespace UniCloud.Application.BaseManagementBC.RoleServices
{
    /// <summary>
    /// Role的服务接口。
    /// </summary>
    public interface IRoleAppService
    {
        /// <summary>
        /// 获取所有Role。
        /// </summary>
        IQueryable<RoleDTO> GetRoles();
    }
}
