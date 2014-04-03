#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：IMaintainWorkQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.MaintainWorkQueries
{
    /// <summary>
    /// MaintainWork查询接口
    /// </summary>
    public interface IMaintainWorkQuery
    {
        /// <summary>
        /// MaintainWork查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>MaintainWorkDTO集合</returns>
        IQueryable<MaintainWorkDTO> MaintainWorkDTOQuery(QueryBuilder<MaintainWork> query);
    }
}
