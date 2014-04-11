#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/10 18:43:09
// 文件名：XmlSettingFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/10 18:43:09
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.XmlSettingAgg
{
    public class XmlSettingFactory
    {
        public static void SetXmlSetting(XmlSetting xmlSetting, string settingContent)
        {
            xmlSetting.SettingContent = settingContent;
        }
    }
}
