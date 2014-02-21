#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// BasicConfigGroup仓储实现
    /// </summary>
    public class BasicConfigGroupRepository : Repository<BasicConfigGroup>, IBasicConfigGroupRepository
    {
        public BasicConfigGroupRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion

        /// <summary>
        /// 删除基本构型组
        /// </summary>
        /// <param name="basicConfigGroup"></param>
        public void DeleteBasicConfigGroup(BasicConfigGroup basicConfigGroup)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var dbBasicConfigs = currentUnitOfWork.CreateSet<BasicConfig>();
            var dbBasicConfigGroups = currentUnitOfWork.CreateSet<BasicConfigGroup>();
            dbBasicConfigs.RemoveRange(basicConfigGroup.BasicConfigs);
            dbBasicConfigGroups.Remove(basicConfigGroup);
        }


        /// <summary>
        ///     移除基本构型
        /// </summary>
        /// <param name="basicConfig">基本构型</param>
        public void RemoveBasicConfig(BasicConfig basicConfig)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            currentUnitOfWork.CreateSet<BasicConfig>().Remove(basicConfig);
        }
    }
}
