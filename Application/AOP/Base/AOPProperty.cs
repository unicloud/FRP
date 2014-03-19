#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 14:58:35
// 文件名：AOPProperty
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 14:58:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

#endregion

namespace UniCloud.Application.AOP.Base
{
    public abstract class AOPProperty : IContextProperty, IContributeObjectSink
    {
        protected abstract IMessageSink CreateSink(IMessageSink nextSink);

        protected virtual string GetName()
        {
            return "AOP";
        }

        protected virtual void FreezeImpl(Context newContext)
        {
            return;
        }

        protected virtual bool CheckNewContext(Context newCtx)
        {
            return true;
        }

        #region IContributeObjectSink Members

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return CreateSink(nextSink);
        }

        #endregion

        #region IContextProperty Members
        public void Freeze(Context newContext)
        {
            FreezeImpl(newContext);
        }

        public bool IsNewContextOK(Context newCtx)
        {
            return CheckNewContext(newCtx);
        }

        public string Name
        {
            get { return GetName(); }
        }
        #endregion
    }
}
