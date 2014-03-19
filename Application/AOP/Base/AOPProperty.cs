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
        /// <summary>
        /// 该方法用来定位被创建的Context的最后位置。
        /// </summary>
        /// <param name="newContext"></param>
        public void Freeze(Context newContext)
        {
            FreezeImpl(newContext);
        }

        /// <summary>
        /// 默认返回true。一个对象可能存在多个Context,使用这个方法来检查新的Context中属性是否存在冲突。
        /// </summary>
        /// <param name="newCtx"></param>
        /// <returns></returns>
        public bool IsNewContextOK(Context newCtx)
        {
            return CheckNewContext(newCtx);
        }

        /// <summary>
        /// 只读属性。返回ContextAttribute的名称
        /// </summary>
        public string Name
        {
            get { return GetName(); }
        }
        #endregion
    }
}
