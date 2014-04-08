#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:06:54
// 文件名：IEngineRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Engine}" />
    /// </summary>
    public interface IEngineRepository : IRepository<Engine>
    {
        /// <summary>
        /// 删除发动机
        /// </summary>
        /// <param name="engine"></param>
        void DeleteEngine(Engine engine);

        /// <summary>
        /// 移除商业数据历史
        /// </summary>
        /// <param name="ebh"></param>
        void RemoveEngineBusinessHistory(EngineBusinessHistory ebh);

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="eoh"></param>
        void RemoveEngineOwnershipHistory(EngineOwnershipHistory eoh);
    }
}
