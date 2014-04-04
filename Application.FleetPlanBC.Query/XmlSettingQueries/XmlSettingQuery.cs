#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:18:41
// 文件名：XmlSettingQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:18:41
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
    public class XmlSettingQuery : IXmlSettingQuery
    {
        private readonly IXmlSettingRepository _xmlSettingRepository;

        public XmlSettingQuery(IXmlSettingRepository xmlSettingRepository)
        {
            _xmlSettingRepository = xmlSettingRepository;
        }

        /// <summary>
        ///     配置相关的xml查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>配置相关的xmlDTO集合。</returns>
        public IQueryable<XmlSettingDTO> XmlSettingDTOQuery(
          QueryBuilder<XmlSetting> query)
        {
            return
                query.ApplyTo(_xmlSettingRepository.GetAll())
                    .Select(p => new XmlSettingDTO
                                 {
                                     XmlSettingId = p.Id,
                                     SettingContent = p.SettingContent,
                                     SettingType = p.SettingType
                                 });
        }
    }
}
