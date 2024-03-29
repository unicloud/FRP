﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/4 14:50:25
// 文件名：XmlConfigFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/4 14:50:25
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.UberModel.Aggregates.XmlConfigAgg
{
    /// <summary>
    ///     XmlConfig工厂
    /// </summary>
    public static class XmlConfigFactory
    {
        public static XmlConfig CreateXmlConfig(Guid id, string configType, int versionNumber, string configContent)
        {
            var xmlConfig = new XmlConfig
            {
                ConfigType = configType,
                VersionNumber = versionNumber,
                ConfigContent = configContent
            };
            xmlConfig.ChangeCurrentIdentity(id);
            return xmlConfig;
        }
    }
}
