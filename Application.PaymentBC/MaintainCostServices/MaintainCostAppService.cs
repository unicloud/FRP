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

        #region SpecialRefitMaintainCostDTO
        /// <summary>
        ///     获取所有特修改装维修成。
        /// </summary>
        /// <returns>所有特修改装维修成。</returns>
        public IQueryable<SpecialRefitMaintainCostDTO> GetSpecialRefitMaintainCosts()
        {
            var queryBuilder = new QueryBuilder<SpecialRefitMaintainCost>();
            return _maintainCostQuery.SpecialRefitMaintainCostDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增特修改装维修成。
        /// </summary>
        /// <param name="specialRefitMaintainCost">特修改装维修成DTO。</param>
        [Insert(typeof(SpecialRefitMaintainCostDTO))]
        public void InsertSpecialRefitMaintainCost(SpecialRefitMaintainCostDTO specialRefitMaintainCost)
        {
            var newSpecialRefitMaintainCost = MaintainCostFactory.CreateSpecialRefitMaintainCost();
            MaintainCostFactory.SetSpecialRefitMaintainCost(newSpecialRefitMaintainCost, specialRefitMaintainCost.Project, specialRefitMaintainCost.Info, specialRefitMaintainCost.DepartmentDeclareAmount,
                specialRefitMaintainCost.Note, specialRefitMaintainCost.FinancialApprovalAmount, specialRefitMaintainCost.FinancialApprovalAmountNonTax, specialRefitMaintainCost.MaintainInvoiceId, specialRefitMaintainCost.AnnualId);
            _maintainCostRepository.Add(newSpecialRefitMaintainCost);
        }


        /// <summary>
        ///     更新特修改装维修成。
        /// </summary>
        /// <param name="specialRefitMaintainCost">特修改装维修成DTO。</param>
        [Update(typeof(SpecialRefitMaintainCostDTO))]
        public void ModifySpecialRefitMaintainCost(SpecialRefitMaintainCostDTO specialRefitMaintainCost)
        {
            var updateSpecialRefitMaintainCost = _maintainCostRepository.Get(specialRefitMaintainCost.Id) as SpecialRefitMaintainCost; //获取需要更新的对象。
            MaintainCostFactory.SetSpecialRefitMaintainCost(updateSpecialRefitMaintainCost, specialRefitMaintainCost.Project, specialRefitMaintainCost.Info, specialRefitMaintainCost.DepartmentDeclareAmount,
                specialRefitMaintainCost.Note, specialRefitMaintainCost.FinancialApprovalAmount, specialRefitMaintainCost.FinancialApprovalAmountNonTax, specialRefitMaintainCost.MaintainInvoiceId, specialRefitMaintainCost.AnnualId);
            _maintainCostRepository.Modify(updateSpecialRefitMaintainCost);
        }

        /// <summary>
        ///     删除特修改装维修成。
        /// </summary>
        /// <param name="specialRefitMaintainCost">特修改装维修成DTO。</param>
        [Delete(typeof(SpecialRefitMaintainCostDTO))]
        public void DeleteSpecialRefitMaintainCost(SpecialRefitMaintainCostDTO specialRefitMaintainCost)
        {
            var deleteSpecialRefitMaintainCost = _maintainCostRepository.Get(specialRefitMaintainCost.Id);//获取需要删除的对象。
            _maintainCostRepository.Remove(deleteSpecialRefitMaintainCost); //删除特修改装维修成。
        }
        #endregion

        #region NonFhaMaintainCostDTO
        /// <summary>
        ///     获取所有非FHA.超包修维修成。
        /// </summary>
        /// <returns>所有非FHA.超包修维修成。</returns>
        public IQueryable<NonFhaMaintainCostDTO> GetNonFhaMaintainCosts()
        {
            var queryBuilder = new QueryBuilder<NonFhaMaintainCost>();
            return _maintainCostQuery.NonFhaMaintainCostDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增非FHA.超包修维修成。
        /// </summary>
        /// <param name="nonFhaMaintainCost">非FHA.超包修维修成DTO。</param>
        [Insert(typeof(NonFhaMaintainCostDTO))]
        public void InsertNonFhaMaintainCost(NonFhaMaintainCostDTO nonFhaMaintainCost)
        {
            var newNonFhaMaintainCost = MaintainCostFactory.CreateNonFhaMaintainCost();
            MaintainCostFactory.SetNonFhaMaintainCost(newNonFhaMaintainCost, nonFhaMaintainCost.EngineNumber, nonFhaMaintainCost.ContractRepairt, nonFhaMaintainCost.Type, nonFhaMaintainCost.AircraftId,
                nonFhaMaintainCost.ActionCategoryId, nonFhaMaintainCost.AircraftTypeId, nonFhaMaintainCost.SupplierId, nonFhaMaintainCost.InMaintainTime, nonFhaMaintainCost.OutMaintainTime, nonFhaMaintainCost.MaintainLevel,
                nonFhaMaintainCost.ChangeLlpNumber, nonFhaMaintainCost.Tsr, nonFhaMaintainCost.Csr, nonFhaMaintainCost.NonFhaFee, nonFhaMaintainCost.PartFee, nonFhaMaintainCost.ChangeLlpFee, nonFhaMaintainCost.FeeLittleSum,
                nonFhaMaintainCost.Rate, nonFhaMaintainCost.FeeTotalSum, nonFhaMaintainCost.CustomRate, nonFhaMaintainCost.Custom, nonFhaMaintainCost.AddedValueRate, nonFhaMaintainCost.AddedValue, nonFhaMaintainCost.CustomsTax,
                nonFhaMaintainCost.FreightFee, nonFhaMaintainCost.DepartmentDeclareAmount, nonFhaMaintainCost.FinancialApprovalAmount, nonFhaMaintainCost.FinancialApprovalAmountNonTax, nonFhaMaintainCost.Note, nonFhaMaintainCost.ActualMaintainLevel,
                nonFhaMaintainCost.ActualChangeLlpNumber, nonFhaMaintainCost.ActualTsr, nonFhaMaintainCost.ActualCsr, nonFhaMaintainCost.MaintainInvoiceId, nonFhaMaintainCost.AnnualId);
            _maintainCostRepository.Add(newNonFhaMaintainCost);
        }


        /// <summary>
        ///     更新非FHA.超包修维修成。
        /// </summary>
        /// <param name="nonFhaMaintainCost">非FHA.超包修维修成DTO。</param>
        [Update(typeof(NonFhaMaintainCostDTO))]
        public void ModifyNonFhaMaintainCost(NonFhaMaintainCostDTO nonFhaMaintainCost)
        {
            var updateNonFhaMaintainCost = _maintainCostRepository.Get(nonFhaMaintainCost.Id) as NonFhaMaintainCost; //获取需要更新的对象。
            MaintainCostFactory.SetNonFhaMaintainCost(updateNonFhaMaintainCost, nonFhaMaintainCost.EngineNumber, nonFhaMaintainCost.ContractRepairt, nonFhaMaintainCost.Type, nonFhaMaintainCost.AircraftId,
                nonFhaMaintainCost.ActionCategoryId, nonFhaMaintainCost.AircraftTypeId, nonFhaMaintainCost.SupplierId, nonFhaMaintainCost.InMaintainTime, nonFhaMaintainCost.OutMaintainTime, nonFhaMaintainCost.MaintainLevel,
                nonFhaMaintainCost.ChangeLlpNumber, nonFhaMaintainCost.Tsr, nonFhaMaintainCost.Csr, nonFhaMaintainCost.NonFhaFee, nonFhaMaintainCost.PartFee, nonFhaMaintainCost.ChangeLlpFee, nonFhaMaintainCost.FeeLittleSum,
                nonFhaMaintainCost.Rate, nonFhaMaintainCost.FeeTotalSum, nonFhaMaintainCost.CustomRate, nonFhaMaintainCost.Custom, nonFhaMaintainCost.AddedValueRate, nonFhaMaintainCost.AddedValue, nonFhaMaintainCost.CustomsTax,
                nonFhaMaintainCost.FreightFee, nonFhaMaintainCost.DepartmentDeclareAmount, nonFhaMaintainCost.FinancialApprovalAmount, nonFhaMaintainCost.FinancialApprovalAmountNonTax, nonFhaMaintainCost.Note, nonFhaMaintainCost.ActualMaintainLevel,
                nonFhaMaintainCost.ActualChangeLlpNumber, nonFhaMaintainCost.ActualTsr, nonFhaMaintainCost.ActualCsr, nonFhaMaintainCost.MaintainInvoiceId, nonFhaMaintainCost.AnnualId);
            _maintainCostRepository.Modify(updateNonFhaMaintainCost);
        }

        /// <summary>
        ///     删除非FHA.超包修维修成。
        /// </summary>
        /// <param name="nonFhaMaintainCost">非FHA.超包修维修成DTO。</param>
        [Delete(typeof(NonFhaMaintainCostDTO))]
        public void DeleteNonFhaMaintainCost(NonFhaMaintainCostDTO nonFhaMaintainCost)
        {
            var deleteNonFhaMaintainCost = _maintainCostRepository.Get(nonFhaMaintainCost.Id);//获取需要删除的对象。
            _maintainCostRepository.Remove(deleteNonFhaMaintainCost); //删除非FHA.超包修维修成。
        }
        #endregion

        #region ApuMaintainCostDTO
        /// <summary>
        ///     获取所有APU维修成。
        /// </summary>
        /// <returns>所有APU维修成。</returns>
        public IQueryable<ApuMaintainCostDTO> GetApuMaintainCosts()
        {
            var queryBuilder = new QueryBuilder<ApuMaintainCost>();
            return _maintainCostQuery.ApuMaintainCostDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增APU维修成。
        /// </summary>
        /// <param name="apuMaintainCost">APU维修成DTO。</param>
        [Insert(typeof(ApuMaintainCostDTO))]
        public void InsertApuMaintainCost(ApuMaintainCostDTO apuMaintainCost)
        {
            var newApuMaintainCost = MaintainCostFactory.CreateApuMaintainCost();
            MaintainCostFactory.SetApuMaintainCost(newApuMaintainCost, apuMaintainCost.NameType, apuMaintainCost.Type, apuMaintainCost.LastYearRate, apuMaintainCost.YearAddedRate,
                apuMaintainCost.YearBudgetRate, apuMaintainCost.Rate, apuMaintainCost.BudgetHour, apuMaintainCost.HourPercent, apuMaintainCost.Hour, apuMaintainCost.ContractRepairFeeUsd,
                apuMaintainCost.ContractRepairFeeRmb, apuMaintainCost.CustomRate, apuMaintainCost.TotalTax, apuMaintainCost.AddedValueRate, apuMaintainCost.AddedValue,
                apuMaintainCost.IncludeAddedValue, apuMaintainCost.MaintainInvoiceId, apuMaintainCost.AnnualId);
            _maintainCostRepository.Add(newApuMaintainCost);
        }


        /// <summary>
        ///     更新APU维修成。
        /// </summary>
        /// <param name="apuMaintainCost">APU维修成DTO。</param>
        [Update(typeof(ApuMaintainCostDTO))]
        public void ModifyApuMaintainCost(ApuMaintainCostDTO apuMaintainCost)
        {
            var updateApuMaintainCost = _maintainCostRepository.Get(apuMaintainCost.Id) as ApuMaintainCost; //获取需要更新的对象。
            MaintainCostFactory.SetApuMaintainCost(updateApuMaintainCost, apuMaintainCost.NameType, apuMaintainCost.Type, apuMaintainCost.LastYearRate, apuMaintainCost.YearAddedRate,
                apuMaintainCost.YearBudgetRate, apuMaintainCost.Rate, apuMaintainCost.BudgetHour, apuMaintainCost.HourPercent, apuMaintainCost.Hour, apuMaintainCost.ContractRepairFeeUsd,
                apuMaintainCost.ContractRepairFeeRmb, apuMaintainCost.CustomRate, apuMaintainCost.TotalTax, apuMaintainCost.AddedValueRate, apuMaintainCost.AddedValue,
                apuMaintainCost.IncludeAddedValue, apuMaintainCost.MaintainInvoiceId, apuMaintainCost.AnnualId);
            _maintainCostRepository.Modify(updateApuMaintainCost);
        }

        /// <summary>
        ///     删除APU维修成。
        /// </summary>
        /// <param name="apuMaintainCost">APU维修成DTO。</param>
        [Delete(typeof(ApuMaintainCostDTO))]
        public void DeleteApuMaintainCost(ApuMaintainCostDTO apuMaintainCost)
        {
            var deleteApuMaintainCost = _maintainCostRepository.Get(apuMaintainCost.Id);//获取需要删除的对象。
            _maintainCostRepository.Remove(deleteApuMaintainCost); //删除APU维修成。
        }
        #endregion
    }
}
