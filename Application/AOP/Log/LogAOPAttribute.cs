#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 15:16:26
// 文件名：LogAOPAttribute
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 15:16:26
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Application.AOP.Base;

#endregion

namespace UniCloud.Application.AOP.Log
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogAOPAttribute : AOPAttribute
    {
        protected override AOPProperty GetAOPProperty()
        {
            return new LogAOPProperty();
        }
    }
}
