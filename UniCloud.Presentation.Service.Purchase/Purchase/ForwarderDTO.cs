#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/05，09:12
// 文件名：ForwarderDTO.cs
// 程序集：UniCloud.Presentation.Service.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Text.RegularExpressions;

#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class ForwarderDTO
    {
        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("名称不能为空");
            }
        }

        partial void OnEmailChanging(string value)
        {
            //RFC2822 http://en.wikipedia.org/wiki/Email_address 
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^((?'name'.+?)\s*<)?(?'email'(?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|""(?'user'(?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*""\x20*)*(?'angle'<))?(?'user'(?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|""((?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*"")@(?'domain'((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?'angle')(?(name)>)$"))
            {
                throw new Exception("邮箱格式有误");
            }
        }
    }
}
