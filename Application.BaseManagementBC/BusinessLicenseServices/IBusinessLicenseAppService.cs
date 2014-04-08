#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 12:03:37
// 文件名：IBusinessLicenseAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 12:03:37
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;

#endregion

namespace UniCloud.Application.BaseManagementBC.BusinessLicenseServices
{
    /// <summary>
    /// BusinessLicense的服务接口。
    /// </summary>
    public interface IBusinessLicenseAppService
    {
        /// <summary>
        /// 获取所有BusinessLicense。
        /// </summary>
        IQueryable<BusinessLicenseDTO> GetBusinessLicenses();
    }
}
