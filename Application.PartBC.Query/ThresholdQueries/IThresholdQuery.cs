#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 16:08:31
// 文件名：IThresholdQuery
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 16:08:31
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.ThresholdQueries
{
    /// <summary>
    /// Threshold查询接口
    /// </summary>
    public interface IThresholdQuery
    {
        /// <summary>
        /// Threshold查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ThresholdDTO集合</returns>
        IQueryable<ThresholdDTO> ThresholdDTOQuery(QueryBuilder<Threshold> query);
    }
}
