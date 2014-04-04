#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:35:24
// 文件名：XmlConfigDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;
using System.Xml.Linq;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 分析数据相关的xml
    /// </summary>
    [DataServiceKey("XmlConfigId")]
    public class XmlConfigDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid XmlConfigId { get; set; }

        public string ConfigType { get; set; }
        public string ConfigContent { get; set; }
        public int VersionNumber { get; set; }

        public XElement XmlContent
        {
            get { return XElement.Parse(ConfigContent); }
            set { ConfigContent = value.ToString(); }
        }

        #endregion
    }
}
