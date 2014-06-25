#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 16:11:06
// 文件名：IOrganizationAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 16:11:06
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;

#endregion

namespace UniCloud.Application.BaseManagementBC.OrganizationServices
{
    /// <summary>
    ///     Organization的服务接口。
    /// </summary>
    public interface IOrganizationAppService
    {
        /// <summary>
        ///     获取所有Organization。
        /// </summary>
        IQueryable<OrganizationDTO> GetOrganizations();

        /// <summary>
        ///     同步组织数据。
        /// </summary>
        /// <param name="organizations">组织集合</param>
        void SyncOrganizationInfo(List<OrganizationDTO> organizations);
    }
}