#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 14:53:15
// 文件名：LogAOPSink
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 14:53:15
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.IO;
using System.Runtime.Remoting.Messaging;
using UniCloud.Application.AOP.Base;

#endregion

namespace UniCloud.Application.AOP.Log
{
    public class LogAopSink : AopSink
    {
        public LogAopSink(IMessageSink nextSink)
            : base(nextSink)
        {

        }

        private void Before_Log(IMethodCallMessage callMsg)
        {
            if (callMsg == null)
            {
                return;
            }
        }

        private void After_Log(IMethodReturnMessage replyMsg)
        {
            if (replyMsg == null)
            {
                return;
            }
        }

        protected override void AddAllBeforeAopHandles()
        {
            AddBeforeAopHandle("GetUsers", Before_Log);
        }

        protected override void AddAllAfterAopHandles()
        {
            AddAfterAopHandle("GetUsers", After_Log);
        }
    }
}
