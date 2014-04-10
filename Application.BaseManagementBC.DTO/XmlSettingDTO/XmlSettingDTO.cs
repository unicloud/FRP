#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:36:04
// 文件名：XmlSettingDTO
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

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    /// 配置相关的xml
    /// </summary>
    [DataServiceKey("XmlSettingId")]
    public class XmlSettingDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid XmlSettingId { get; set; }
        public string SettingType { get; set; }
        public string SettingContent { get; set; }

        public XElement XmlContent
        {
            get { return XElement.Parse(SettingContent); }
            set { SettingContent = value.ToString(); }
        }

        #endregion
    }
}
