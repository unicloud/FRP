#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:18:11
// 文件名：IXmlConfigQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:18:11
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.XmlConfigQueries
{
   public interface IXmlConfigQuery
    {
        /// <summary>
        ///     分析数据相关xml查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>分析数据相关xmlDTO集合</returns>
       IQueryable<XmlConfigDTO> XmlConfigDTOQuery(
            QueryBuilder<XmlConfig> query);
    }
}
