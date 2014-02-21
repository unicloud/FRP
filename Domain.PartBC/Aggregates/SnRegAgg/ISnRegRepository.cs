#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ISnRegRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    /// SnReg仓储接口。
    /// </summary>
    public interface ISnRegRepository : IRepository<SnReg>
    {
        /// <summary>
        /// 删除序号件
        /// </summary>
        /// <param name="snReg"></param>
        void DeleteSnReg(SnReg snReg);

        /// <summary>
        ///     移除装机历史
        /// </summary>
        /// <param name="snHistory">装机历史</param>
        void RemoveSnHistory(SnHistory snHistory);

        /// <summary>
        ///     移除到寿监控
        /// </summary>
        /// <param name="lifeMonitor">到寿监控</param>
        void RemoveLifeMonitor(LifeMonitor lifeMonitor);
    }
}
