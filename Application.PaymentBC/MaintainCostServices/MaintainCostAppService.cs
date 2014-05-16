#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:52:10
// 文件名：MaintainCostAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:52:10
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.MaintainCostQueries;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainCostServices
{
    public class MaintainCostAppService : IMaintainCostAppService
    {
        private readonly IMaintainCostQuery _maintainCostQuery;
        private readonly IMaintainCostRepository _maintainCostRepository;

        public MaintainCostAppService(IMaintainCostQuery maintainCostQuery, IMaintainCostRepository maintainCostRepository)
        {
            _maintainCostQuery = maintainCostQuery;
            _maintainCostRepository = maintainCostRepository;
        }

        #region RegularCheckMaintainCostDTO
        /// <summary>
        ///     获取所有定检维修成。
        /// </summary>
        /// <returns>所有定检维修成。</returns>
        public IQueryable<RegularCheckMaintainCostDTO> GetRegularCheckMaintainCosts()
        {
            var queryBuilder = new QueryBuilder<RegularCheckMaintainCost>();
            return _maintainCostQuery.RegularCheckMaintainCostDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增定检维修成。
        /// </summary>
        /// <param name="regularCheckMaintainCost">定检维修成DTO。</param>
        [Insert(typeof(RegularCheckMaintainCostDTO))]
        public void InsertRegularCheckMaintainCost(RegularCheckMaintainCostDTO regularCheckMaintainCost)
        {
            var newRegularCheckMaintainCost = MaintainCostFactory.CreateRegularCheckMaintainCost();
            MaintainCostFactory.SetRegularCheckMaintainCost(newRegularCheckMaintainCost, regularCheckMaintainCost.AircraftId, regularCheckMaintainCost.ActionCategoryId, regularCheckMaintainCost.AircraftTypeId,
                regularCheckMaintainCost.RegularCheckType, regularCheckMaintainCost.RegularCheckLevel, regularCheckMaintainCost.InMaintainTime, regularCheckMaintainCost.OutMaintainTime, regularCheckMaintainCost.TotalDays,
                regularCheckMaintainCost.DepartmentDeclareAmount, regularCheckMaintainCost.FinancialApprovalAmount, regularCheckMaintainCost.FinancialApprovalAmountNonTax, regularCheckMaintainCost.MaintainInvoiceId, regularCheckMaintainCost.AnnualId);
            _maintainCostRepository.Add(newRegularCheckMaintainCost);
        }


        /// <summary>
        ///     更新定检维修成。
        /// </summary>
        /// <param name="regularCheckMaintainCost">定检维修成DTO。</param>
        [Update(typeof(RegularCheckMaintainCostDTO))]
        public void ModifyRegularCheckMaintainCost(RegularCheckMaintainCostDTO regularCheckMaintainCost)
        {
            var updateRegularCheckMaintainCost = _maintainCostRepository.Get(regularCheckMaintainCost.Id) as RegularCheckMaintainCost; //获取需要更新的对象。
            MaintainCostFactory.SetRegularCheckMaintainCost(updateRegularCheckMaintainCost, regularCheckMaintainCost.AircraftId, regularCheckMaintainCost.ActionCategoryId, regularCheckMaintainCost.AircraftTypeId,
                regularCheckMaintainCost.RegularCheckType, regularCheckMaintainCost.RegularCheckLevel, regularCheckMaintainCost.InMaintainTime, regularCheckMaintainCost.OutMaintainTime, regularCheckMaintainCost.TotalDays,
                regularCheckMaintainCost.DepartmentDeclareAmount, regularCheckMaintainCost.FinancialApprovalAmount, regularCheckMaintainCost.FinancialApprovalAmountNonTax, regularCheckMaintainCost.MaintainInvoiceId, regularCheckMaintainCost.AnnualId);
            _maintainCostRepository.Modify(updateRegularCheckMaintainCost);
        }

        /// <summary>
        ///     删除定检维修成。
        /// </summary>
        /// <param name="regularCheckMaintainCost">定检维修成DTO。</param>
        [Delete(typeof(RegularCheckMaintainCostDTO))]
        public void DeleteRegularCheckMaintainCost(RegularCheckMaintainCostDTO regularCheckMaintainCost)
        {
            var deleteRegularCheckMaintainCost = _maintainCostRepository.Get(regularCheckMaintainCost.Id);//获取需要删除的对象。
            _maintainCostRepository.Remove(deleteRegularCheckMaintainCost); //删除定检维修成。
        }
        #endregion

        #region UndercartMaintainCostDTO
        /// <summary>
        ///     获取所有起落架维修成。
        /// </summary>
        /// <returns>所有起落架维修成。</returns>
        public IQueryable<UndercartMaintainCostDTO> GetUndercartMaintainCosts()
        {
            var queryBuilder = new QueryBuilder<UndercartMaintainCost>();
            return _maintainCostQuery.UndercartMaintainCostDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增起落架维修成。
        /// </summary>
        /// <param name="undercartMaintainCost">起落架维修成DTO。</param>
        [Insert(typeof(UndercartMaintainCostDTO))]
        public void InsertUndercartMaintainCost(UndercartMaintainCostDTO undercartMaintainCost)
        {
            var newUndercartMaintainCost = MaintainCostFactory.CreateUndercartMaintainCost();
            MaintainCostFactory.SetUndercartMaintainCost(newUndercartMaintainCost, undercartMaintainCost.AircraftId, undercartMaintainCost.ActionCategoryId, undercartMaintainCost.AircraftTypeId,
                undercartMaintainCost.Type, undercartMaintainCost.Part, undercartMaintainCost.InMaintainTime, undercartMaintainCost.OutMaintainTime, undercartMaintainCost.TotalDays,
                undercartMaintainCost.DepartmentDeclareAmount, undercartMaintainCost.FinancialApprovalAmount, undercartMaintainCost.FinancialApprovalAmountNonTax, undercartMaintainCost.MaintainInvoiceId,
                undercartMaintainCost.MaintainFeeEur, undercartMaintainCost.Rate, undercartMaintainCost.MaintainFeeRmb, undercartMaintainCost.FreightFee, undercartMaintainCost.ReplaceFee, undercartMaintainCost.CustomRate,
                undercartMaintainCost.Custom, undercartMaintainCost.AddedValueRate, undercartMaintainCost.AddedValue, undercartMaintainCost.AnnualId);
            _maintainCostRepository.Add(newUndercartMaintainCost);
        }


        /// <summary>
        ///     更新起落架维修成。
        /// </summary>
        /// <param name="undercartMaintainCost">起落架维修成DTO。</param>
        [Update(typeof(UndercartMaintainCostDTO))]
        public void ModifyUndercartMaintainCost(UndercartMaintainCostDTO undercartMaintainCost)
        {
            var updateUndercartMaintainCost = _maintainCostRepository.Get(undercartMaintainCost.Id) as UndercartMaintainCost; //获取需要更新的对象。
            MaintainCostFactory.SetUndercartMaintainCost(updateUndercartMaintainCost, undercartMaintainCost.AircraftId, undercartMaintainCost.ActionCategoryId, undercartMaintainCost.AircraftTypeId,
                undercartMaintainCost.Type, undercartMaintainCost.Part, undercartMaintainCost.InMaintainTime, undercartMaintainCost.OutMaintainTime, undercartMaintainCost.TotalDays,
                undercartMaintainCost.DepartmentDeclareAmount, undercartMaintainCost.FinancialApprovalAmount, undercartMaintainCost.FinancialApprovalAmountNonTax, undercartMaintainCost.MaintainInvoiceId,
                undercartMaintainCost.MaintainFeeEur, undercartMaintainCost.Rate, undercartMaintainCost.MaintainFeeRmb, undercartMaintainCost.FreightFee, undercartMaintainCost.ReplaceFee, undercartMaintainCost.CustomRate,
                undercartMaintainCost.Custom, undercartMaintainCost.AddedValueRate, undercartMaintainCost.AddedValue, undercartMaintainCost.AnnualId);
            _maintainCostRepository.Modify(updateUndercartMaintainCost);
        }

        /// <summary>
        ///     删除起落架维修成。
        /// </summary>
        /// <param name="undercartMaintainCost">起落架维修成DTO。</param>
        [Delete(typeof(UndercartMaintainCostDTO))]
        public void DeleteUndercartMaintainCost(UndercartMaintainCostDTO undercartMaintainCost)
        {
            var deleteUndercartMaintainCost = _maintainCostRepository.Get(undercartMaintainCost.Id);//获取需要删除的对象。
            _maintainCostRepository.Remove(deleteUndercartMaintainCost); //删除起落架维修成。
        }
        #endregion
    }
}
