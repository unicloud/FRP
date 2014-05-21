#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/21 17:11:34
// 文件名：XmlConfigFactory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/21 17:11:34
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg
{
    /// <summary>
    ///     XmlConfig工厂
    /// </summary>
    public class XmlConfigFactory
    {
        /// <summary>
        /// 创建XmlConfig
        /// </summary>
        /// <param name="configType"></param>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static XmlConfig CreateXmlConfig(string configType, XElement xmlContent)
        {
            var newXmlConfig = new XmlConfig()
            {
                ConfigType = configType,
                XmlContent = xmlContent,
            };
            newXmlConfig.GenerateNewIdentity();

            return newXmlConfig;
        }
    }
}
