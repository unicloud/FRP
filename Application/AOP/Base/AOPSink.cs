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
    /// <summary>
    /// 方法执行前操作
    /// </summary>
    /// <param name="callMsg">其值为要截取上下文的方法的消息</param>
    public delegate void BeforeAopHandle(IMethodCallMessage callMsg);

    /// <summary>
    /// 方法执行后操作
    /// </summary>
    /// <param name="replyMsg">该方法执行后返回的消息</param>
    public delegate void AfterAopHandle(IMethodReturnMessage replyMsg);

    public abstract class AopSink : IMessageSink
    {
        /// <summary>
        /// 存放方法名与BeforeAOPHandle对象之间的映射
        /// </summary>
        private readonly SortedList _mBeforeHandles;

        /// <summary>
        /// 存放方法名与AfterAOPHandle对象之间的映射
        /// </summary>
        private readonly SortedList _mAfterHandles;

        /// <summary>
        /// 接收器链中的下一个消息接收器
        /// </summary>
        private readonly IMessageSink _mNextSink;

        public AopSink(IMessageSink nextSink)
        {
            _mNextSink = nextSink;
            _mBeforeHandles = new SortedList();
            _mAfterHandles = new SortedList();

            AddAllBeforeAopHandles();
            AddAllAfterAopHandles();
        }

        /// <summary>
        /// 添加所有的映射关系
        /// </summary>
        protected abstract void AddAllBeforeAopHandles();

        /// <summary>
        /// 添加所有的映射关系
        /// </summary>
        protected abstract void AddAllAfterAopHandles();

        /// <summary>
        /// 添加映射关系
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="beforeHandle"></param>
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

        /// <summary>
        /// 添加映射关系
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="afterHandle"></param>
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

        /// <summary>
        /// 根据方法名获得相对应的委托对象
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        protected BeforeAopHandle FindBeforeAopHandle(string methodName)
        {
            BeforeAopHandle beforeHandle;
            lock (_mBeforeHandles)
            {
                beforeHandle = (BeforeAopHandle)_mBeforeHandles[methodName.ToUpper()];
            }
            return beforeHandle;
        }

        /// <summary>
        /// 根据方法名获得相对应的委托对象
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        protected AfterAopHandle FindAfterAopHandle(string methodName)
        {
            AfterAopHandle afterHandle;
            lock (_mAfterHandles)
            {
                afterHandle = (AfterAopHandle)_mAfterHandles[methodName.ToUpper()];
            }
            return afterHandle;
        }

        /// <summary>
        /// 接收器链中的下一个消息接收器
        /// </summary>
        public IMessageSink NextSink
        {
            get { return _mNextSink; }
        }

        /// <summary>
        /// 当消息传递的时候，该方法被调用
        /// </summary>
        /// <param name="msg">定义了被传送的消息的实现</param>
        /// <returns></returns>
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

        /// <summary>
        /// 该方法用于异步处理
        /// </summary>
        /// <param name="msg">定义了被传送的消息的实现</param>
        /// <param name="replySink">定义了消息接收器的接口</param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }
    }
}
