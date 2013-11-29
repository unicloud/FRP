#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ForwarderAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ForwarderQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Domain.PurchaseBC.ValueObjects;

#endregion

namespace UniCloud.Application.PurchaseBC.ForwarderServices
{
    /// <summary>
    ///     实现承运人接口。
    ///     用于处于承运人相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class ForwarderAppService : IForwarderAppService
    {
        private readonly IForwarderQuery _forwarderQuery;
        private readonly IForwarderRepository _forwarderRepository;

        public ForwarderAppService(IForwarderQuery forwarderQuery, IForwarderRepository forwarderRepository)
        {
            _forwarderQuery = forwarderQuery;
            _forwarderRepository = forwarderRepository;
        }

        /// <summary>
        ///     获取所有承运人。
        /// </summary>
        /// <returns>所有承运人。</returns>
        public IQueryable<ForwarderDTO> GetForwarders()
        {
            var queryBuilder =
                new QueryBuilder<Forwarder>();
            return _forwarderQuery.ForwardersQuery(queryBuilder);
        }

        /// <summary>
        ///     新增承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        [Insert(typeof (ForwarderDTO))]
        public void InsertForwarder(ForwarderDTO forwarder)
        {
            var newForwarder = ForwarderFactory.Create(forwarder.Name, forwarder.Tel, forwarder.Fax, forwarder.Attn,
                forwarder.Email,
                forwarder.Addr);

            _forwarderRepository.Add(newForwarder);
        }

        /// <summary>
        ///     更新承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        [Update(typeof (ForwarderDTO))]
        public void ModifyForwarder(ForwarderDTO forwarder)
        {
            var updateForwarder = _forwarderRepository.Get(forwarder.ForwarderId); //获取需要更新的对象。

            //更新。
            updateForwarder.CnName = forwarder.Name;
            updateForwarder.Tel = forwarder.Tel;
            updateForwarder.Fax = forwarder.Fax;
            updateForwarder.Attn = forwarder.Attn;
            updateForwarder.Email = forwarder.Email;
            updateForwarder.Address = new Address(null, null, forwarder.Addr, null);
            _forwarderRepository.Modify(updateForwarder);
        }

        /// <summary>
        ///     删除承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        [Delete(typeof (ForwarderDTO))]
        public void DeleteForwarder(ForwarderDTO forwarder)
        {
            var newForwarder = _forwarderRepository.Get(forwarder.ForwarderId); //获取需要删除的对象。
            _forwarderRepository.Remove(newForwarder); //删除承运人。
        }
    }
}