#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 11:51:02
// 文件名：BusinessLicenseRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 11:51:02
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Repositories
{
    /// <summary>
    /// BusinessLicense仓储实现
    /// </summary>
    public class BusinessLicenseRepository : Repository<BusinessLicense>, IBusinessLicenseRepository
    {
        public BusinessLicenseRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
