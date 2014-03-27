#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 14:57:40
// 文件名：AOPAttribute
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 14:57:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

#endregion

namespace UniCloud.Application.AOP.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class AOPAttribute : ContextAttribute
    {
        public AOPAttribute()
            : base("AOP")
        {
        }

        /// <summary>
        /// 向新的Context添加属性集合
        /// </summary>
        /// <param name="ctorMsg"></param>
        public sealed override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
        {
            ctorMsg.ContextProperties.Add(GetAOPProperty());
        }

        protected abstract AOPProperty GetAOPProperty();
    }
}
