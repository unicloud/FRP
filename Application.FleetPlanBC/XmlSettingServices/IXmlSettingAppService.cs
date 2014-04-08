#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:28:03
// 文件名：IXmlSettingAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:28:03
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.XmlSettingServices
{
    /// <summary>
    ///     表示用于配置相关的xml相关信息服务
    /// </summary>
    public interface IXmlSettingAppService
    {
        /// <summary>
        ///     获取所有配置相关的xml
        /// </summary>
        /// <returns>所有配置相关的xml</returns>
        IQueryable<XmlSettingDTO> GetXmlSettings();
    }
}
