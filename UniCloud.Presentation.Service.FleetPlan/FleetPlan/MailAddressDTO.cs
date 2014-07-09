#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/13 11:12:49
// 文件名：MailAddressDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/13 11:12:49
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Text.RegularExpressions;

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class MailAddressDTO
    {
        partial void OnSendSSLChanged()
        {
            SendPort = SendSSL ? 465 : 25;
        }

        partial void OnAddressChanged()
        {
            if (DisplayName == null) DisplayName = Address;
            if (LoginUser == null) LoginUser = Address;
        }

        partial void OnAddressChanging(string value)
        {
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^((?'name'.+?)\s*<)?(?'email'(?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|""(?'user'(?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*""\x20*)*(?'angle'<))?(?'user'(?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|""((?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*"")@(?'domain'((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?'angle')(?(name)>)$"))
            {
                throw new Exception("邮箱格式有误");
            }
        }
    }
}
