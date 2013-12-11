#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:34
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.PaymentBC.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Repositories
{
    /// <summary>
    ///     维修合同仓储实现
    /// </summary>
    public class MaintainContractRepository : Repository<MaintainContract>, IMaintainContractRepository
    {
        public MaintainContractRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}