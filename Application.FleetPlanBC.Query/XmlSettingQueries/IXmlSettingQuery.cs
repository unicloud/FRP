#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:18:35
// 文件名：IXmlSettingQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:18:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlSettingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.XmlSettingQueries
{
    public interface IXmlSettingQuery
    {
        /// <summary>
        ///     配置相关的xml查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>配置相关的xmlDTO集合</returns>
        IQueryable<XmlSettingDTO> XmlSettingDTOQuery(
            QueryBuilder<XmlSetting> query);
    }
}
