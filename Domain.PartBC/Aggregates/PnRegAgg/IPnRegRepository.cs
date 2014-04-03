#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IPnRegRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.PnRegAgg
{
    /// <summary>
    ///     PnReg仓储接口。
    /// </summary>
    public interface IPnRegRepository : IRepository<PnReg>
    {
        /// <summary>
        ///     删除附件
        /// </summary>
        /// <param name="pnReg"></param>
        void DeletePnReg(PnReg pnReg);

        /// <summary>
        ///     移除依赖项
        /// </summary>
        /// <param name="dependency">依赖项</param>
        void RemoveDependency(Dependency dependency);
    }
}