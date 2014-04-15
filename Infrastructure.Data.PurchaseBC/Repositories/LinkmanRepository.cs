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
using System.Linq.Expressions;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Repositories
{
    /// <summary>
    ///     联系人仓储实现
    /// </summary>
    public class LinkmanRepository : Repository<Linkman>, ILinkmanRepository
    {
        public LinkmanRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion

        public Linkman GetLinkman(Expression<Func<Linkman, bool>> condition)
        {
            var currentUnitOfWork = UnitOfWork as PurchaseBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<Linkman>();
            return set.FirstOrDefault(condition);
        }
    }
}