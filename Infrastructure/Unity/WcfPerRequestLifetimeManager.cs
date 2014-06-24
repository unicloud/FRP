#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:55
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;
using Microsoft.Practices.Unity;

#endregion

namespace UniCloud.Infrastructure.Unity
{
    internal class ContainerExtension : IExtension<OperationContext>
    {
        public object Value { get; set; }

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }

    /// <summary>
    ///     每个WCF服务请求生命期。
    /// </summary>
    public class WcfPerRequestLifetimeManager : LifetimeManager
    {
        private readonly Guid _key = Guid.NewGuid();

        /// <summary>
        ///     构造函数。
        /// </summary>
        public WcfPerRequestLifetimeManager()
            : this(Guid.NewGuid())
        {
        }

        private WcfPerRequestLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
                throw new ArgumentException("Key is empty.");

            _key = key;
        }

        public override object GetValue()
        {
            object result = null;

            if (OperationContext.Current != null)
            {
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    result = containerExtension.Value;
                }
            }
            else if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[_key.ToString()] != null)
                    result = HttpContext.Current.Items[_key.ToString()];
            }
            else
            {
                result = CallContext.GetData(_key.ToString());
            }

            return result;
        }

        public override void RemoveValue()
        {
            if (OperationContext.Current != null)
            {
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                    OperationContext.Current.Extensions.Remove(containerExtension);
            }
            else if (HttpContext.Current != null && HttpContext.Current.Items[_key.ToString()] != null)
            {
                HttpContext.Current.Items[_key.ToString()] = null;
            }
            else
            {
                CallContext.FreeNamedDataSlot(_key.ToString());
            }
        }

        public override void SetValue(object newValue)
        {
            if (OperationContext.Current != null)
            {
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null) return;
                containerExtension = new ContainerExtension
                {
                    Value = newValue
                };

                OperationContext.Current.Extensions.Add(containerExtension);
            }
            else if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[_key.ToString()] == null)
                    HttpContext.Current.Items[_key.ToString()] = newValue;
            }
            else
            {
                CallContext.SetData(_key.ToString(), newValue);
            }
        }
    }
}