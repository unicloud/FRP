#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，16:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Repositories
{
    /// <summary>
    ///     供应商角色仓储实现
    /// </summary>
    public class SupplierRoleRepository : Repository<SupplierRole>, ISupplierRoleRepository
    {
        public SupplierRoleRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion

        /// <summary>
        ///     根据合作公司Id，获取角色。
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="supplierCmpyId">主键</param>
        /// <returns></returns>
        public SupplierRole GetSupplierRoleBySupplierCmpyId(Type type, int supplierCmpyId)
        {
            SupplierRole role;
            if (type == typeof (AircraftLeaseSupplier))
            {
                role =
                    GetAll().OfType<AircraftLeaseSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else if (type == typeof (AircraftPurchaseSupplier))
            {
                role =
                    GetAll().OfType<AircraftPurchaseSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else if (type == typeof (EngineLeaseSupplier))
            {
                role = GetAll().OfType<EngineLeaseSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else if (type == typeof (EnginePurchaseSupplier))
            {
                role =
                    GetAll().OfType<EnginePurchaseSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else if (type == typeof (BFEPurchaseSupplier))
            {
                role =
                    GetAll().OfType<BFEPurchaseSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else if (type == typeof(MaintainSupplier))
            {
                role = GetAll().OfType<MaintainSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            else
            {
                role = GetAll().OfType<OtherSupplier>().FirstOrDefault(p => p.SupplierCompanyId == supplierCmpyId);
            }
            return role;
        }
    }
}