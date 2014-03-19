#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 14:42:29
// 文件名：AOPSink
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 14:42:29
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections;
using System.Runtime.Remoting.Messaging;

#endregion

namespace UniCloud.Application.AOP.Base
{
    public delegate void BeforeAopHandle(IMethodCallMessage callMsg);
    public delegate void AfterAopHandle(IMethodReturnMessage replyMsg);

    public abstract class AopSink : IMessageSink
    {
        private readonly SortedList _mBeforeHandles;
        private readonly SortedList _mAfterHandles;
        private readonly IMessageSink _mNextSink;

        public AopSink(IMessageSink nextSink)
        {
            _mNextSink = nextSink;
            _mBeforeHandles = new SortedList();
            _mAfterHandles = new SortedList();

            AddAllBeforeAopHandles();
            AddAllAfterAopHandles();
        }

        protected abstract void AddAllBeforeAopHandles();
        protected abstract void AddAllAfterAopHandles();
        protected virtual void AddBeforeAopHandle(string methodName, BeforeAopHandle beforeHandle)
        {
            lock (_mBeforeHandles)
            {
                if (!_mBeforeHandles.Contains(methodName.ToUpper()))
                {
                    _mBeforeHandles.Add(methodName.ToUpper(), beforeHandle);
                }
            }
        }
        protected virtual void AddAfterAopHandle(string methodName, AfterAopHandle afterHandle)
        {
            lock (_mAfterHandles)
            {
                if (!_mAfterHandles.Contains(methodName.ToUpper()))
                {
                    _mAfterHandles.Add(methodName.ToUpper(), afterHandle);
                }
            }
        }

        protected BeforeAopHandle FindBeforeAopHandle(string methodName)
        {
            BeforeAopHandle beforeHandle;
            lock (_mBeforeHandles)
            {
                beforeHandle = (BeforeAopHandle)_mBeforeHandles[methodName.ToUpper()];
            }
            return beforeHandle;
        }

        protected AfterAopHandle FindAfterAopHandle(string methodName)
        {
            AfterAopHandle afterHandle;
            lock (_mAfterHandles)
            {
                afterHandle = (AfterAopHandle)_mAfterHandles[methodName.ToUpper()];
            }
            return afterHandle;
        }

        public IMessageSink NextSink
        {
            get { return _mNextSink; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            var call = msg as IMethodCallMessage;
            if (call != null)
            {
                BeforeAopHandle beforeHandle = FindBeforeAopHandle(call.MethodName.ToUpper());

                if (beforeHandle != null)
                {
                    beforeHandle(call);
                }

                IMessage retMsg = _mNextSink.SyncProcessMessage(msg);
                var replyMsg = retMsg as IMethodReturnMessage;
                AfterAopHandle afterHandle = FindAfterAopHandle(call.MethodName.ToUpper());

                if (afterHandle != null)
                {
                    afterHandle(replyMsg);
                }
                return retMsg;
            }
            return null;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }
    }
}
