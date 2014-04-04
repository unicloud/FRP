//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.ExposeData
{
    /// <summary>
    ///     实现IExoseData接口，作为自定义暴露数据基类。
    /// </summary>
    public abstract class ExposeData : IExposeData
    {
        #region IExposeData 接口实现

        private readonly string _dtoAsseblies;
        private readonly List<object> _localDeletedCollection = new List<object>(); //本地保存删除集合。
        private readonly List<object> _localModifiedCollection = new List<object>(); //本地保存修改集合。
        private readonly List<object> _localNewCollection = new List<object>(); //本地保存新增集合。
        private readonly IQueryableUnitOfWork _unitOfWork; //工作单元。
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     初始化构造函数。
        ///     <param name="dtoAsseblies">dto程序集名称。</param>
        /// </summary>
        protected ExposeData(string dtoAsseblies)
        {
            Initialize();
            _localNewCollection.Clear();
            _localModifiedCollection.Clear();
            _localDeletedCollection.Clear();
            _dtoAsseblies = dtoAsseblies;
            _unitOfWork = DefaultContainer.Resolve<IQueryableUnitOfWork>();
            _unityContainer = DefaultContainer.Current;
        }

        #region Properites

        /// <summary>
        ///     本地保存删除集合。
        /// </summary>
        public List<object> LocalDeletedCollection
        {
            get { return _localDeletedCollection; }
        }

        /// <summary>
        ///     本地保存修改集合。
        /// </summary>
        public List<object> LocalModifiedCollection
        {
            get { return _localModifiedCollection; }
        }

        /// <summary>
        ///     本地保存新增集合。
        /// </summary>
        public List<object> LocalNewCollection
        {
            get { return _localNewCollection; }
        }

        #endregion

        /// <summary>
        ///     增加数据接口
        /// </summary>
        /// <param name="containerName">增加数据的容器。</param>
        /// <param name="fullTypeName">数据类型全称。</param>
        /// <returns>增加对象实例。</returns>
        public virtual object CreateResource(string containerName, string fullTypeName)
        {
            if (fullTypeName.IsNullOrEmpty()) throw new ArgumentNullException("fullTypeName");
            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.FullName.StartsWith(_dtoAsseblies));
            if (assemblies.IsNull()) throw new Exception("程序集为空");
            var t = assemblies.GetType(fullTypeName);
            if (t.IsNull()) throw new Exception("实例化对象失败");
            var resource = FormatterServices.GetUninitializedObject(t);
            if (!containerName.IsNullOrEmpty())
            {
                _localNewCollection.Add(resource); //增加到本地新增容器中。
            }
            return resource;
        }

        /// <summary>
        ///     获取对象。
        ///     更新或删除都先通过此方法获取对象，  //如果fullTypeName为空，表示当前记录为更新。
        /// </summary>
        /// <param name="query">获取对象的表达式。</param>
        /// <param name="fullTypeName">数据类型全称。</param>
        /// <returns>对象。</returns>
        public virtual object GetResource(IQueryable query, string fullTypeName)
        {
            var resource = (query as IQueryable<object>).ToList().SingleOrDefault();

            if (!resource.IsNull() && (!fullTypeName.IsNullOrEmpty() && resource.GetType().FullName != fullTypeName))
                throw new ApplicationException("获取对象失败");

            //如果fullTypeName不为空，表示当前记录为更新。
            if (!fullTypeName.IsNullOrEmpty())
            {
                _localModifiedCollection.Add(resource); //当前记录为更新，增加到本地更新容器中。
            }
            return resource;
        }

        /// <summary>
        ///     将参数 resource 标识的资源重置为其默认值。
        /// </summary>
        /// <param name="resource">需要重置的对象。</param>
        /// <returns>设置为默认值对象</returns>
        public virtual object ResetResource(object resource)
        {
            return resource;
        }

        /// <summary>
        ///     设置对象属性值。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <param name="propertyValue">属性值。</param>
        public virtual void SetValue(object targetResource, string propertyName, object propertyValue)
        {
            //获取集合类型属性
            var property = targetResource.GetType()
                .GetProperties()
                .Single(p => p.Name == propertyName);
            if (property.PropertyType.GetInterface("IEnumerable", true) != null
                && property.PropertyType.IsGenericType)
            {
                var listType = typeof (List<>);
                Type targetType = null; //目标类型
                var listObject = new List<object>(); //存放List对象。
                foreach (var value in (IEnumerable) propertyValue)
                {
                    if (targetType == null)
                    {
                        targetType = listType.MakeGenericType(value.GetType());
                    }
                    listObject.Add(value);
                }
                if (targetType != null)
                {
                    var addMethod = targetType.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
                    var constructor = targetType.GetConstructor(new Type[] {});
                    if (constructor != null)
                    {
                        var list = constructor.Invoke(new object[] {});
                        foreach (var value in listObject)
                        {
                            addMethod.Invoke(list, new[] {value});
                        }
                        var propertyInfo = targetResource
                            .GetType()
                            .GetProperties()
                            .FirstOrDefault(p => p.Name == propertyName);
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(targetResource, list);
                        }
                    }
                }
            }
            else
            {
                var propertyInfo = targetResource
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == propertyName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(targetResource, propertyValue);
                }
            }
        }

        /// <summary>
        ///     获取对象的属性值。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <returns></returns>
        public virtual
            object GetValue
            (object targetResource, string propertyName)
        {
            var value = targetResource
                .GetType()
                .GetProperties()
                .Single(p => p.Name == propertyName)
                .GetValue(targetResource);
            return value;
        }

        /// <summary>
        ///     设置对象引用值。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <param name="propertyValue">属性值。</param>
        public virtual
            void SetReference
            (object targetResource, string propertyName, object propertyValue)
        {
            ((IUpdatable) this).SetValue(targetResource, propertyName, propertyValue);
        }

        /// <summary>
        ///     添加引用到集合中。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <param name="resourceToBeAdded">需要添加的引用对象。</param>
        public virtual
            void AddReferenceToCollection
            (object targetResource, string propertyName,
                object resourceToBeAdded)
        {
            var pi = targetResource.GetType().GetProperty(propertyName);
            if (pi.IsNull())
                throw new Exception("Can't find property");
            var collection = (IList) pi.GetValue(targetResource, null);
            collection.Add(resourceToBeAdded);
        }

        /// <summary>
        ///     移除集合的引用。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <param name="resourceToBeRemoved">需要移除的引用对象。</param>
        public virtual
            void RemoveReferenceFromCollection
            (object targetResource, string propertyName,
                object resourceToBeRemoved)
        {
            var pi = targetResource.GetType().GetProperty(propertyName);
            if (pi.IsNull())
                throw new Exception("Can't find property");
            var collection = (IList) pi.GetValue(targetResource, null);
            collection.Remove(resourceToBeRemoved);
        }

        /// <summary>
        ///     删除对象。
        /// </summary>
        /// <param name="targetResource">对象。</param>
        public virtual
            void DeleteResource
            (object
                targetResource)
        {
            if (targetResource.IsNull()) throw new ArgumentNullException("targetResource");
            _localDeletedCollection.Add(targetResource);
        }

        /// <summary>
        ///     保存。
        /// </summary>
        public virtual
            void SaveChanges
            ()
        {
            _localNewCollection.ForEach(AddObject); //新增对象。
            _localModifiedCollection.ForEach(UpdateObject); //修改对象。
            _localDeletedCollection.ForEach(DeleteObject); //删除对象。
            _unitOfWork.CommitAndRefreshChanges();
        }

        /// <summary>
        ///     获取对象。
        /// </summary>
        /// <param name="resource">对象。</param>
        /// <returns></returns>
        public virtual
            object ResolveResource
            (object
                resource)
        {
            return resource;
        }

        /// <summary>
        ///     撤销。
        /// </summary>
        public virtual
            void ClearChanges
            ()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     删除对象。
        /// </summary>
        /// <param name="obj">DTO对象。</param>
        private
            void DeleteObject
            (object
                obj)
        {
            _unityContainer.Registrations.ForEach(p =>
            {
                var objInstance = _unityContainer.Resolve(p.RegisteredType);
                foreach (var mInfo in objInstance.GetType().GetMethods())
                {
                    var attr = mInfo.GetCustomAttributes(typeof (DeleteAttribute), false).SingleOrDefault();
                    if (!attr.IsNull() && ((DeleteAttribute) (attr)).Type == obj.GetType())
                    {
                        mInfo.Invoke(objInstance, new[] {obj});
                    }
                }
            });
        }

        /// <summary>
        ///     增加对象。
        /// </summary>
        /// <param name="obj">DTO对象。</param>
        private
            void AddObject
            (object
                obj)
        {
            _unityContainer.Registrations.ForEach(p =>
            {
                var objInstance = _unityContainer.Resolve(p.RegisteredType);
                foreach (var mInfo in objInstance.GetType().GetMethods())
                {
                    var attr = mInfo.GetCustomAttributes(typeof (InsertAttribute), false).SingleOrDefault();

                    if (!attr.IsNull() &&
                        ((InsertAttribute) (attr)).Type.ToString().Equals(obj.GetType().ToString()))
                    {
                        mInfo.Invoke(objInstance, new[] {obj});
                    }
                }
            });
        }

        /// <summary>
        ///     更新对象。
        /// </summary>
        /// <param name="obj">DTO对象。</param>
        private
            void UpdateObject
            (object
                obj)
        {
            _unityContainer.Registrations.ForEach(p =>
            {
                var objInstance = _unityContainer.Resolve(p.RegisteredType);
                foreach (var mInfo in objInstance.GetType().GetMethods())
                {
                    var attr = mInfo.GetCustomAttributes(typeof (UpdateAttribute), false).SingleOrDefault();
                    if (!attr.IsNull() && ((UpdateAttribute) (attr)).Type == obj.GetType())
                    {
                        mInfo.Invoke(objInstance, new[] {obj});
                    }
                }
            });
        }

        #endregion

        #region 缓存管理

        private TimeSpan _expiration;
        protected ObjectCache cache;

        /// <summary>
        ///     初始化缓存
        /// </summary>
        /// <param name="cacheExpiration">过期时间间隔</param>
        private void Initialize(TimeSpan? cacheExpiration = null)
        {
            cache = MemoryCache.Default;
            _expiration = cacheExpiration ?? new TimeSpan(2, 0, 0);
        }

        /// <summary>
        ///     获取过期策略
        /// </summary>
        /// <returns>过期策略</returns>
        private CacheItemPolicy GetExpiration()
        {
            var policy = new CacheItemPolicy();

            if (_expiration > TimeSpan.Zero &&
                _expiration < TimeSpan.MaxValue)
            {
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_expiration);
            }

            return policy;
        }

        /// <summary>
        ///     获取静态数据
        /// </summary>
        /// <typeparam name="TDTO">DTO类型</typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="getData">获取数据委托</param>
        /// <returns>数据集合对象</returns>
        protected IQueryable<TDTO> GetStaticData<TDTO>(string key, Func<IQueryable<TDTO>> getData) where TDTO : class
        {
            var result = cache.Get(key) as IQueryable<TDTO>;
            if (result == null)
            {
                result = getData();
                if (result != null)
                {
                    cache.Add(key, result, GetExpiration());
                }
            }
            return result;
        }

        #endregion
    }
}