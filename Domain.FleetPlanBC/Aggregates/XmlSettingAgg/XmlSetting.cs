#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:17:05
// 文件名：XmlSetting
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Xml.Linq;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.XmlSettingAgg
{
    /// <summary>
    ///     配置相关的xml
    /// </summary>
    public class XmlSetting : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal XmlSetting()
        {
        }

        #endregion

        #region 属性

        public string SettingType { get; protected set; }
        public string SettingContent { get; protected set; }

        public XElement XmlContent
        {
            get { return XElement.Parse(SettingContent); }
            set { SettingContent = value.ToString(); }
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
