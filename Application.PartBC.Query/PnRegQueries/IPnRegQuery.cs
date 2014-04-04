#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IPnRegQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.PnRegQueries
{
    /// <summary>
    /// PnReg查询接口
    /// </summary>
    public interface IPnRegQuery
    {
        /// <summary>
        /// PnReg查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>PnRegDTO集合</returns>
        IQueryable<PnRegDTO> PnRegDTOQuery(QueryBuilder<PnReg> query);
    }
}
