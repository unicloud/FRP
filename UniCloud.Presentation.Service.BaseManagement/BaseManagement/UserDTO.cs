#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/17 18:01:33
// 文件名：UserDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/17 18:01:33
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Text.RegularExpressions;

#endregion

namespace UniCloud.Presentation.Service.BaseManagement.BaseManagement
{
    public partial class UserDTO
    {
        #region 操作

        partial void OnUserNameChanging(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("登录名必填");
        }

        partial void OnOrganizationNoChanging(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new Exception("密码必填");
        }

        partial void OnEmailChanging(string value)
        {
            //RFC2822 http://en.wikipedia.org/wiki/Email_address 
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value,
                    @"^((?'name'.+?)\s*<)?(?'email'(?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|""(?'user'(?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*""\x20*)*(?'angle'<))?(?'user'(?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|""((?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*"")@(?'domain'((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?'angle')(?(name)>)$"))
            {
                throw new Exception("邮箱格式有误");
            }
        }

        #endregion
    }
}