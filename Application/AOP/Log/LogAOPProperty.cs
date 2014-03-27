#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 15:06:51
// 文件名：LogAOPProperty
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 15:06:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Runtime.Remoting.Messaging;
using UniCloud.Application.AOP.Base;

#endregion

namespace UniCloud.Application.AOP.Log
{
    public class LogAOPProperty : AOPProperty
    {
        protected override IMessageSink CreateSink(IMessageSink nextSink)
        {
            return new LogAopSink(nextSink);
        }

        protected override string GetName()
        {
            return "LogAOP";
        }
    }
}
