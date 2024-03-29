﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:16:55
// 文件名：XmlConfig
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Xml.Linq;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg
{
    /// <summary>
    ///     分析数据相关xml
    /// </summary>
    public class XmlConfig : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal XmlConfig()
        {
        }

        #endregion

        #region 属性

        public string ConfigType { get; internal set; }
        public string ConfigContent { get; internal set; }
        public int VersionNumber { get; set; }

        public XElement XmlContent
        {
            get { return XElement.Parse(ConfigContent); }
            set { ConfigContent = value.ToString(); }
        }

        #endregion

        #region 外键属性



        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
