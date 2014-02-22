#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ITechnicalSolutionRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg
{
    /// <summary>
    /// TechnicalSolution仓储接口。
    /// </summary>
    public interface ITechnicalSolutionRepository : IRepository<TechnicalSolution>
    {
        /// <summary>
        /// 删除解决方案
        /// </summary>
        /// <param name="ts"></param>
        void DeleteTechnicalSolution(TechnicalSolution ts);

        /// <summary>
        /// 删除解决方案明细
        /// </summary>
        /// <param name="tsLine"></param>
        void DeleteTsLine(TsLine tsLine);

        /// <summary>
        ///     移除依赖项
        /// </summary>
        /// <param name="dependency">依赖项</param>
        void RemoveDependency(Dependency dependency);
    }
}
