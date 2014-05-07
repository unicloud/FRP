#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/7 17:32:26
// 文件名：XmlSettingFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/7 17:32:26
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.XmlSettingAgg
{
    /// <summary>
    ///     XmlSetting工厂
    /// </summary>
    public class XmlSettingFactory
    {
        public static XmlSetting CreateXmlSetting(Guid id, string settingType, string settingContent)
        {
            var xmlSetting = new XmlSetting
            {
                SettingType = settingType,
                SettingContent = settingContent
            };
            xmlSetting.ChangeCurrentIdentity(id);
            return xmlSetting;
        }
    }
}
