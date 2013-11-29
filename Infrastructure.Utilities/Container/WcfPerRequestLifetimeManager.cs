#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/28，10:11
// 方案：FRP
// 项目：Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;
using Microsoft.Practices.Unity;

#endregion

namespace UniCloud.Infrastructure.Utilities.Container
{
    internal class ContainerExtension : IExtension<OperationContext>
    {
        #region 成员

        public object Value { get; set; }

        #endregion

        #region IExtension<OperationContext> 成员

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }

        #endregion
    }

    public class WcfPerRequestLifetimeManager : LifetimeManager
    {
        #region 私有字段

        private readonly Guid _key = Guid.NewGuid();

        #endregion

        public WcfPerRequestLifetimeManager() : this(Guid.NewGuid())
        {
        }

        private WcfPerRequestLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
                throw new ArgumentException("Key is empty.");

            _key = key;
        }

        #region Public Methods

        /// <summary>
        ///     Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>The object desired, or null if no such object is currently stored.</returns>
        public override object GetValue()
        {
            object result = null;

            //Get object depending on  execution environment ( WCF without HttpContext,HttpContext or CallContext)

            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    result = containerExtension.Value;
                }
            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] != null)
                    result = HttpContext.Current.Items[_key.ToString()];
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                result = CallContext.GetData(_key.ToString());
            }

            return result;
        }

        /// <summary>
        ///     Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    var obj = containerExtension.Value;
                    if (obj is IDisposable)
                    {
                        (obj as IDisposable).Dispose();
                    }
                    OperationContext.Current.Extensions.Remove(containerExtension);
                }
            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] != null)
                {
                    if (HttpContext.Current.Items[_key.ToString()] is IDisposable)
                    {
                        (HttpContext.Current.Items[_key.ToString()] as IDisposable).Dispose();
                    }
                    HttpContext.Current.Items[_key.ToString()] = null;
                }
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                CallContext.FreeNamedDataSlot(_key.ToString());
            }
        }

        /// <summary>
        ///     Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                var containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension == null)
                {
                    containerExtension = new ContainerExtension
                    {
                        Value = newValue
                    };

                    OperationContext.Current.Extensions.Add(containerExtension);
                }
            }
            else if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] == null)
                    HttpContext.Current.Items[_key.ToString()] = newValue;
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                CallContext.SetData(_key.ToString(), newValue);
            }
        }

        #endregion
    }
}