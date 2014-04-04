#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:11
// 方案：FRP
// 项目：Domain
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace UniCloud.Domain.Events
{
    internal class WeakDelegatesManager
    {
        private readonly List<DelegateReference> _listeners = new List<DelegateReference>();

        public void AddListener(Delegate listener)
        {
            _listeners.Add(new DelegateReference(listener, false));
        }

        public void RemoveListener(Delegate listener)
        {
            _listeners.RemoveAll(reference =>
            {
                var local0 = reference.Target;
                if (!listener.Equals(local0))
                    return local0 == null;
                return true;
            });
        }

        public void Raise(params object[] args)
        {
            _listeners.RemoveAll(listener => listener.Target == null);
            foreach (
                var @delegate in
                    _listeners.ToList().Select(listener => listener.Target).Where(listener => listener != null))
                @delegate.DynamicInvoke(args);
        }
    }
}