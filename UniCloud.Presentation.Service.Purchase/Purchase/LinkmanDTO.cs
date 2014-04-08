#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/02，18:12
// 文件名：LinkmanDTO.cs
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
    public partial class LinkmanDTO
    {
        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("名称不为空");
            }
        }

        partial void OnEmailChanging(string value)
        {
            if (!string.IsNullOrEmpty(value) &&
                !Regex.IsMatch(value, @"^\s*([A-Za-z0-9_-]+(\.\w+)*@([\w-]+\.)+\w{2,10})\s*$"))
            {
                throw new Exception("邮箱格式有误");
            }
        }
    }
}