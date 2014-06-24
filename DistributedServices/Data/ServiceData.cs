#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：13:49
// 方案：FRP
// 项目：DistributedServices
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Reflection;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain;
using UniCloud.Infrastructure.FastReflection;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.Data
{
    /// <summary>
    ///     服务数据的抽象基类
    /// </summary>
    public abstract class ServiceData : IUpdatable
    {
        private readonly List<object> _deleteCollection;
        private readonly string _dtoAssemblies;
        private readonly List<object> _insertCollection;
        private readonly List<MethodInfo> _methodInfos;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<object> _updateCollection;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="dtoAssemblies">DTO程序集</param>
        /// <param name="unitOfWork">UnitOfWork实例</param>
        protected ServiceData(string dtoAssemblies, IUnitOfWork unitOfWork)
        {
            _dtoAssemblies = dtoAssemblies;
            _methodInfos = new List<MethodInfo>();
            _insertCollection = new List<object>();
            _updateCollection = new List<object>();
            _deleteCollection = new List<object>();
            _unitOfWork = unitOfWork;
            GetMemberInfos();
        }

        #region 私有方法

        /// <summary>
        ///     获取容器中所有方法的集合。
        /// </summary>
        private void GetMemberInfos()
        {
            UniContainer.Create()
                .Registrations.ToList()
                .ForEach(r =>
                {
                    if (r.Name == null)
                    {
                        var instance = UniContainer.Resolve(r.RegisteredType);
                        _methodInfos.AddRange(instance.GetType().GetMethods());
                    }
                    else
                    {
                        var instance = UniContainer.Resolve(r.RegisteredType, r.Name);
                        _methodInfos.AddRange(instance.GetType().GetMethods());
                    }
                });
        }


        /// <summary>
        ///     插入对象
        /// </summary>
        /// <param name="obj">待插入的对象</param>
        private void InsertObject(object obj)
        {
            var method = _methodInfos.FirstOrDefault(m =>
                m.GetCustomAttribute(typeof (InsertAttribute)) != null &&
                ((InsertAttribute) m.GetCustomAttribute(typeof (InsertAttribute))).Type == obj.GetType());
            if (method == null) throw new Exception("未定义插入方法！");
            var instance = UniContainer.Resolve(method.ReflectedType);
            method.FastInvoke(instance, new[] {obj});
        }

        /// <summary>
        ///     更新对象
        /// </summary>
        /// <param name="obj">待更新的对象</param>
        private void UpdateObject(object obj)
        {
            var method = _methodInfos.FirstOrDefault(m =>
                m.GetCustomAttribute(typeof (UpdateAttribute)) != null &&
                ((UpdateAttribute) m.GetCustomAttribute(typeof (UpdateAttribute))).Type == obj.GetType());
            if (method == null) throw new Exception("未定义更新方法！");
            var instance = UniContainer.Resolve(method.ReflectedType);
            method.FastInvoke(instance, new[] {obj});
        }

        /// <summary>
        ///     删除对象
        /// </summary>
        /// <param name="obj">待删除的对象</param>
        private void DeleteObject(object obj)
        {
            var method = _methodInfos.FirstOrDefault(m =>
                m.GetCustomAttribute(typeof (DeleteAttribute)) != null &&
                ((DeleteAttribute) m.GetCustomAttribute(typeof (DeleteAttribute))).Type == obj.GetType());
            if (method == null) throw new Exception("未定义删除方法！");
            var instance = UniContainer.Resolve(method.ReflectedType);
            method.FastInvoke(instance, new[] {obj});
        }

        #endregion

        #region IUpdatable 成员

        /// <summary>
        ///     将指定值添加到集合。
        /// </summary>
        /// <param name="targetResource">用于定义属性的目标对象。</param>
        /// <param name="propertyName">应向其中添加资源的集合属性的名称。</param>
        /// <param name="resourceToBeAdded">表示要添加的资源的不透明对象。</param>
        public void AddReferenceToCollection(object targetResource, string propertyName, object resourceToBeAdded)
        {
            var propertyInfo = targetResource.GetType().GetProperty(propertyName);
            var collection = (IList) propertyInfo.FastGetValue(targetResource);
            collection.Add(resourceToBeAdded);
        }

        /// <summary>
        ///     取消对数据的更改。
        /// </summary>
        public void ClearChanges()
        {
            _unitOfWork.RollbackChanges();
        }

        /// <summary>
        ///     创建具有指定类型并属于指定容器的资源。
        /// </summary>
        /// <param name="containerName">资源所属实体集的名称。</param>
        /// <param name="fullTypeName">资源的完全限定命名空间类型名称。</param>
        /// <returns>表示指定类型的资源并属于指定容器的对象。</returns>
        public object CreateResource(string containerName, string fullTypeName)
        {
            if (string.IsNullOrWhiteSpace(fullTypeName)) throw new ArgumentNullException("fullTypeName");
            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.FullName.StartsWith(_dtoAssemblies));
            if (assemblies == null) throw new Exception("程序集为空");
            var t = assemblies.GetType(fullTypeName, true);
            var resource = Activator.CreateInstance(t);
            if (!string.IsNullOrWhiteSpace(containerName))
                _insertCollection.Add(resource);
            return resource;
        }

        /// <summary>
        ///     删除指定的资源。
        /// </summary>
        /// <param name="targetResource">要删除的资源。</param>
        public void DeleteResource(object targetResource)
        {
            if (targetResource == null) throw new ArgumentNullException("targetResource");
            _deleteCollection.Add(targetResource);
        }

        /// <summary>
        ///     获取查询和类型名称所标识的指定类型的资源。
        /// </summary>
        /// <remarks>
        ///     更新或删除都会先通过此方法获取对象。
        ///     如果fullTypeName非空，表示当前记录为更新。
        /// </remarks>
        /// <param name="query">指向特定资源的语言集成查询 (LINQ)。</param>
        /// <param name="fullTypeName">资源的完全限定类型名称。</param>
        /// <returns>表示指定类型的资源的不透明对象（由指定查询引用）。</returns>
        public object GetResource(IQueryable query, string fullTypeName)
        {
            var resource = (query as IQueryable<object>).ToList().SingleOrDefault();

            if (fullTypeName != null)
            {
                if (resource.GetType().FullName != fullTypeName)
                    throw new ApplicationException("不是期望的资源类型！");
                _updateCollection.Add(resource);
            }

            return resource;
        }

        /// <summary>
        ///     获取目标对象的指定属性的值。
        /// </summary>
        /// <remarks>将为标量属性或复杂属性调用此方法。 对于标量属性，返回的对象应为实际值。</remarks>
        /// <param name="targetResource">一个表示资源的不透明对象。</param>
        /// <param name="propertyName">需要检索其值的属性的名称。</param>
        /// <returns>对象的值。</returns>
        public object GetValue(object targetResource, string propertyName)
        {
            var propertyInfo = targetResource.GetType().GetProperty(propertyName);
            return propertyInfo.FastGetValue(targetResource);
        }

        /// <summary>
        ///     从集合中移除指定值。
        /// </summary>
        /// <remarks>
        ///     从目标对象上的导航属性所标识的集合中移除指定资源。 此操作将删除通过关系绑定的两个资源对象之间的关系。
        /// </remarks>
        /// <param name="targetResource">用于定义属性的目标对象。</param>
        /// <param name="propertyName">需要更新其值的属性的名称。</param>
        /// <param name="resourceToBeRemoved">需要移除的属性值。</param>
        public void RemoveReferenceFromCollection(object targetResource, string propertyName, object resourceToBeRemoved)
        {
            var propertyInfo = targetResource.GetType().GetProperty(propertyName);
            var collection = (IList) propertyInfo.FastGetValue(targetResource);
            collection.Remove(resourceToBeRemoved);
        }

        /// <summary>
        ///     将由参数 resource 标识的资源重置为其默认值。
        /// </summary>
        /// <param name="resource">要更新的资源。</param>
        /// <returns>其值已重置为默认值的资源。</returns>
        public object ResetResource(object resource)
        {
            return resource;
        }

        /// <summary>
        ///     返回由指定资源对象表示的资源的实例。
        /// </summary>
        /// <remarks>
        ///     将调用此方法，以便将 CreateResource 或 GetResource API 返回的不透明对象解析为 CLR 实例。
        ///     通常在 SaveChanges 之后调用此方法，以便为 POST 方法序列化资源。
        ///     如果有使用公共语言运行时 (CLR) 资源实例调用的更新拦截器，将也会调用此方法。
        ///     如果提供程序支持开放式并发，并且资源类型具有由基于 CLR 的提供程序中的 ETagAttribute 定义的并发标记，
        ///     则可以使用此方法。
        /// </remarks>
        /// <param name="resource">表示需要检索其实例的资源的对象。</param>
        /// <returns>返回由指定资源对象表示的资源的实例。</returns>
        public object ResolveResource(object resource)
        {
            return resource;
        }

        /// <summary>
        ///     保存所有使用 IUpdatable API 进行的更改。
        /// </summary>
        /// <remarks>
        ///     IUpdatable  实现必须跟踪所有更改直至调用 SaveChanges，然后在调用 SaveChanges 后保存所有更改。
        ///     IUpdatable  实现应同时保存所有更改或同时拒绝所有更改
        /// </remarks>
        public void SaveChanges()
        {
            _insertCollection.ForEach(InsertObject);
            _updateCollection.ForEach(UpdateObject);
            _deleteCollection.ForEach(DeleteObject);
            _unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        ///     设置目标对象的指定引用属性的值。
        /// </summary>
        /// <param name="targetResource">用于定义属性的目标对象。</param>
        /// <param name="propertyName">需要更新其值的属性的名称。</param>
        /// <param name="propertyValue">要更新的属性值。</param>
        public void SetReference(object targetResource, string propertyName, object propertyValue)
        {
            ((IUpdatable) this).SetValue(targetResource, propertyName, propertyValue);
        }

        /// <summary>
        ///     将目标资源上具有指定名称的属性的值设置为指定属性值。
        /// </summary>
        /// <param name="targetResource">用于定义属性的目标对象。</param>
        /// <param name="propertyName">需要更新其值的属性的名称。</param>
        /// <param name="propertyValue">要更新的属性值。</param>
        public void SetValue(object targetResource, string propertyName, object propertyValue)
        {
            var property = targetResource.GetType().GetProperties().Single(p => p.Name == propertyName);
            if (property.PropertyType.GetInterface("IEnumerable", true) != null && property.PropertyType.IsGenericType)
            {
                var listType = typeof (List<>);
                Type targetType = null;
                var listObject = new List<object>();
                foreach (var value in (IEnumerable) propertyValue)
                {
                    if (targetType == null)
                        targetType = listType.MakeGenericType(value.GetType());
                    listObject.Add(value);
                }
                if (targetType != null)
                {
                    var addMethod = targetType.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
                    var constructor = targetType.GetConstructor(new Type[] {});
                    if (constructor != null)
                    {
                        var list = constructor.FastInvoke(new object[] {});
                        foreach (var value in listObject)
                        {
                            addMethod.FastInvoke(list, new[] {value});
                        }
                        var propertyInfo = targetResource.GetType().GetProperty(propertyName);
                        propertyInfo.FastSetValue(targetResource, propertyValue);
                    }
                }
            }
            else
            {
                var propertyInfo = targetResource.GetType().GetProperty(propertyName);
                propertyInfo.FastSetValue(targetResource, propertyValue);
            }
        }

        #endregion
    }
}