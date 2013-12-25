#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，16:12
// 文件名：GuaranteeAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO.GuaranteeDTO;
using UniCloud.Application.PaymentBC.Query.GuaranteeQueries;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;

#endregion

namespace UniCloud.Application.PaymentBC.GuaranteeServices
{
    /// <summary>
    ///     查询保函服务实现
    /// </summary>
    public class GuaranteeAppService : IGuaranteeAppService
    {
        private readonly IGuaranteeQuery _guaranteeQuery;
        private readonly IGuaranteeRepository _guaranteeRepository;

        public GuaranteeAppService(IGuaranteeQuery guaranteeQuery, IGuaranteeRepository guaranteeRepository)
        {
            _guaranteeQuery = guaranteeQuery;
            _guaranteeRepository = guaranteeRepository;
        }

        #region 租赁保证金

        /// <summary>
        ///     获取租赁保证金保函
        /// </summary>
        /// <returns></returns>
        public IQueryable<LeaseGuaranteeDTO> GetLeaseGuarantees()
        {
            return _guaranteeQuery.LeaseGuaranteeQuery(new QueryBuilder<Guarantee>());
        }

        /// <summary>
        ///     新增租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Insert(typeof (LeaseGuaranteeDTO))]
        public void InsertLeaseGuarantee(LeaseGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var newGuarantee = GuaranteeFactory.CreateLeaseGuarantee(guarantee.StartDate, guarantee.EndDate,
                guarantee.Amount, guarantee.SupplierName, guarantee.OperatorName,
                guarantee.SupplierId, guarantee.CurrencyId, guarantee.OrderId);
            AddGuarantee(newGuarantee);
        }

        /// <summary>
        ///     修改租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Update(typeof (LeaseGuaranteeDTO))]
        public void ModifyLeaseGuarantee(LeaseGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var persistGuarantee =
                _guaranteeRepository.Get(guarantee.GuaranteeId) as LeaseGuarantee;
            if (persistGuarantee == null)
            {
                throw new Exception("找不到需要更新的保证金");
            }
            if (!(persistGuarantee.StartDate == guarantee.StartDate))
            {
                persistGuarantee.StartDate = guarantee.StartDate;
            }
            if (!(persistGuarantee.EndDate == guarantee.EndDate))
            {
                persistGuarantee.EndDate = guarantee.EndDate;
            }
            if (!(persistGuarantee.SupplierId.Equals(guarantee.SupplierId)))
            {
                persistGuarantee.SetSupplier(guarantee.SupplierId, guarantee.SupplierName);
            }
            if (!(persistGuarantee.Amount.Equals(guarantee.Amount)))
            {
                persistGuarantee.Amount = guarantee.Amount;
            }
            if (persistGuarantee.Reviewer != (guarantee.Reviewer))
            {
                persistGuarantee.Review(guarantee.Reviewer);
            }
            if (persistGuarantee.CurrencyId != guarantee.CurrencyId)
            {
                persistGuarantee.SetCurrency(guarantee.CurrencyId);
            }
            if (persistGuarantee.OrderId != guarantee.OrderId)
            {
                persistGuarantee.SetOrderId(guarantee.OrderId);
            }
            if (persistGuarantee.OperatorName != guarantee.OperatorName)
            {
                persistGuarantee.SetOperator(guarantee.OperatorName);
            }
            UpdateGuarantee(persistGuarantee);
        }

        /// <summary>
        ///     删除租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Delete(typeof (LeaseGuaranteeDTO))]
        public void DeleteLeaseGuarantee(LeaseGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var persistGuarantee =
                _guaranteeRepository.Get(guarantee.GuaranteeId);
            if (persistGuarantee == null)
            {
                throw new Exception("找不到需要删除的保证金");
            }
            DeleteGuarantee(persistGuarantee);
        }

        #endregion

        #region 大修保证金

        /// <summary>
        ///     获取大修保证金DTO
        /// </summary>
        /// <returns></returns>
        public IQueryable<MaintainGuaranteeDTO> GetMaintainGuarantee()
        {
            return _guaranteeQuery.MaintainGuaranteeQuery(new QueryBuilder<Guarantee>());
        }

        /// <summary>
        ///     新增大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Insert(typeof (MaintainGuaranteeDTO))]
        public void InsertMaintainGuarantee(MaintainGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var newGuarantee = GuaranteeFactory.CreateMaintainGuarantee(guarantee.CreateDate, guarantee.EndDate,
                guarantee.Amount, guarantee.SupplierName, guarantee.OperatorName,
                guarantee.SupplierId, guarantee.CurrencyId, guarantee.MaintainContractId);
            AddGuarantee(newGuarantee);
        }

        /// <summary>
        ///     修改大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Update(typeof (MaintainGuaranteeDTO))]
        public void ModifyMaintainGuarantee(MaintainGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var persistGuarantee =
                _guaranteeRepository.Get(guarantee.GuaranteeId) as MaintainGuarantee;
            if (persistGuarantee == null)
            {
                throw new Exception("找不到需要更新的保证金");
            }
            if (!(persistGuarantee.StartDate == guarantee.StartDate))
            {
                persistGuarantee.StartDate = guarantee.StartDate;
            }
            if (!(persistGuarantee.EndDate == guarantee.EndDate))
            {
                persistGuarantee.EndDate = guarantee.EndDate;
            }
            if (!(persistGuarantee.SupplierId.Equals(guarantee.SupplierId)))
            {
                persistGuarantee.SetSupplier(guarantee.SupplierId, guarantee.SupplierName);
            }
            if (!(persistGuarantee.Amount.Equals(guarantee.Amount)))
            {
                persistGuarantee.Amount = guarantee.Amount;
            }
            if (persistGuarantee.Reviewer != (guarantee.Reviewer))
            {
                persistGuarantee.Review(guarantee.Reviewer);
            }
            if (persistGuarantee.CurrencyId != guarantee.CurrencyId)
            {
                persistGuarantee.SetCurrency(guarantee.CurrencyId);
            }
            if (persistGuarantee.MaintainContractId != guarantee.MaintainContractId)
            {
                persistGuarantee.SetMaintainContractId(guarantee.MaintainContractId);
            }
            if (persistGuarantee.OperatorName != guarantee.OperatorName)
            {
                persistGuarantee.SetOperator(guarantee.OperatorName);
            }
            UpdateGuarantee(persistGuarantee);
        }

        /// <summary>
        ///     删除大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        [Delete(typeof (MaintainGuaranteeDTO))]
        public void DeleteMaintainGuarantee(MaintainGuaranteeDTO guarantee)
        {
            if (guarantee == null)
            {
                throw new Exception("保证金不能为空");
            }
            var persistGuarantee =
                _guaranteeRepository.Get(guarantee.GuaranteeId);
            if (persistGuarantee == null)
            {
                throw new Exception("找不到需要删除的保证金");
            }
            DeleteGuarantee(persistGuarantee);
        }

        #endregion

        /// <summary>
        ///     增加保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        private void AddGuarantee(Guarantee guarantee)
        {
            _guaranteeRepository.Add(guarantee);
        }

        /// <summary>
        ///     修改保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        private void UpdateGuarantee(Guarantee guarantee)
        {
            _guaranteeRepository.Modify(guarantee);
        }

        /// <summary>
        ///     删除保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        private void DeleteGuarantee(Guarantee guarantee)
        {
            _guaranteeRepository.Remove(guarantee);
        }
    }
}