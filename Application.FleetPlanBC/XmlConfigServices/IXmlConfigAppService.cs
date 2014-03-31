#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:27:41
// 文件名：IXmlConfigAppServices
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:27:41
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.XmlConfigServices
{
    /// <summary>
    ///     表示用于分析数据相关xml相关信息服务
    /// </summary>
    public interface IXmlConfigAppService
    {
        /// <summary>
        ///     获取所有分析数据相关xml
        /// </summary>
        /// <returns>所有分析数据相关xml</returns>
        IQueryable<XmlConfigDTO> GetXmlConfigs();
    }
}
