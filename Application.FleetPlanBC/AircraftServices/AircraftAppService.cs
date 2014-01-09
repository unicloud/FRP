#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 12:06:16
// 文件名：AircraftAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 12:06:16
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftServices
{
    /// <summary>
    ///     实现实际飞机接口。
    ///     用于处于实际飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftAppService : IAircraftAppService
    {
        private readonly IAircraftQuery _aircraftQuery;
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly ISupplierRepository _supplierRepository;

        public AircraftAppService(IAircraftQuery aircraftQuery, IActionCategoryRepository actionCategoryRepository,
            IAircraftRepository aircraftRepository, IAircraftTypeRepository aircraftTypeRepository,
            IAirlinesRepository airlinesRepository, ISupplierRepository supplierRepository)
        {
            _aircraftQuery = aircraftQuery;
            _actionCategoryRepository = actionCategoryRepository;
            _aircraftRepository = aircraftRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _airlinesRepository = airlinesRepository;
            _supplierRepository = supplierRepository;
        }

        #region AircraftDTO
        /// <summary>
        ///     获取所有实际飞机。
        /// </summary>
        /// <returns>所有实际飞机。</returns>
        public IQueryable<AircraftDTO> GetAircrafts()
        {
            var queryBuilder = new QueryBuilder<Aircraft>();
            return _aircraftQuery.AircraftDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增实际飞机。
        /// </summary>
        /// <param name="dto">实际飞机DTO。</param>
        [Insert(typeof(AircraftDTO))]
        public void InsertAircraft(AircraftDTO dto)
        {
            //获取相关数据
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var importCategory = _actionCategoryRepository.Get(dto.ImportCategoryId);
            var supplier = _supplierRepository.Get(dto.SupplierId);

            //创建新的实际飞机
            var newAircraft = AircraftFactory.CreateAircraft();
            newAircraft.SetAircraftType(aircraftType);
            newAircraft.SetAirlines(airlines);
            newAircraft.SetCarryingCapacity(dto.CarryingCapacity);
            newAircraft.SetExportDate(dto.ExportDate);
            newAircraft.SetFactoryDate(dto.FactoryDate);
            newAircraft.SetImportCategory(importCategory);
            newAircraft.SetImportDate(dto.ImportDate);
            newAircraft.SetOperation();
            newAircraft.SetRegNumber(dto.RegNumber);
            newAircraft.SetSeatingCapacity(dto.SeatingCapacity);
            newAircraft.SetSerialNumber(dto.SerialNumber);
            newAircraft.SetSupplier(supplier);

            //添加商业数据历史
            dto.AircraftBusinesses.ToList().ForEach(line => InsertAircraftBusiness(newAircraft, line));

            //添加运营权历史
            dto.OperationHistories.ToList().ForEach(line => InsertOperationHistory(newAircraft, line));

            //添加所有权历史
            dto.OwnershipHistories.ToList().ForEach(line => InsertOwnershipHistory(newAircraft, line));

            _aircraftRepository.Add(newAircraft);
        }


        /// <summary>
        ///     更新实际飞机。
        /// </summary>
        /// <param name="dto">实际飞机DTO。</param>
        [Update(typeof(AircraftDTO))]
        public void ModifyAircraft(AircraftDTO dto)
        {
            //获取相关数据
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var importCategory = _actionCategoryRepository.Get(dto.ImportCategoryId);
            var supplier = _supplierRepository.Get(dto.SupplierId);

            //获取
            var updateAircraft = _aircraftRepository.Get(dto.AircraftId);
            if (updateAircraft != null)
            {
                //更新实际飞机
                updateAircraft.SetAircraftType(aircraftType);
                updateAircraft.SetAirlines(airlines);
                updateAircraft.SetCarryingCapacity(dto.CarryingCapacity);
                updateAircraft.SetExportDate(dto.ExportDate);
                updateAircraft.SetFactoryDate(dto.FactoryDate);
                updateAircraft.SetImportCategory(importCategory);
                updateAircraft.SetImportDate(dto.ImportDate);
                updateAircraft.SetOperation();
                updateAircraft.SetRegNumber(dto.RegNumber);
                updateAircraft.SetSeatingCapacity(dto.SeatingCapacity);
                updateAircraft.SetSerialNumber(dto.SerialNumber);
                updateAircraft.SetSupplier(supplier);

                //更新商业数据历史：
                var dtoAircraftBusinesses = dto.AircraftBusinesses;
                var aircraftBusinesses = updateAircraft.AircraftBusinesses;
                DataHelper.DetailHandle(dtoAircraftBusinesses.ToArray(),
                    aircraftBusinesses.ToArray(),
                    c => c.AircraftId, p => p.Id,
                    i => InsertAircraftBusiness(updateAircraft, i),
                    UpdateAircraftBusiness,
                    d => _aircraftRepository.RemoveAircraftBusiness(d));

                //更新运营权历史：
                var dtoOperationHistories = dto.OperationHistories;
                var operationHistories = updateAircraft.OperationHistories;
                DataHelper.DetailHandle(dtoOperationHistories.ToArray(),
                    operationHistories.ToArray(),
                    c => c.AircraftId, p => p.Id,
                    i => InsertOperationHistory(updateAircraft, i),
                    UpdateOperationHistory,
                    d => _aircraftRepository.RemoveOperationHistory(d));

                //更新所有权历史：
                var dtoOwnershipHistories = dto.OwnershipHistories;
                var ownershipHistories = updateAircraft.OwnershipHistories;
                DataHelper.DetailHandle(dtoOwnershipHistories.ToArray(),
                    ownershipHistories.ToArray(),
                    c => c.AircraftId, p => p.Id,
                    i => InsertOwnershipHistory(updateAircraft, i),
                    UpdateOwnershipHistory,
                    d => _aircraftRepository.RemoveOwnershipHistory(d));
            }
            _aircraftRepository.Modify(updateAircraft);
        }

        /// <summary>
        ///     删除实际飞机。
        /// </summary>
        /// <param name="dto">实际飞机DTO。</param>
        [Delete(typeof(AircraftDTO))]
        public void DeleteAircraft(AircraftDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delAircraft = _aircraftRepository.Get(dto.AircraftId);
            //获取需要删除的对象。
            if (delAircraft != null)
            {
                _aircraftRepository.DeleteAircraft(delAircraft); //删除实际飞机。
            }
        }

        #region 处理实际飞机的相关数据

        /// <summary>
        ///     插入商业数据历史
        /// </summary>
        /// <param name="aircraft">实际飞机</param>
        /// <param name="aircraftBusinessDto">商业数据历史DTO</param>
        private void InsertAircraftBusiness(Aircraft aircraft, AircraftBusinessDTO aircraftBusinessDto)
        {
            //获取相关数据
            var aircraftType = _aircraftTypeRepository.Get(aircraftBusinessDto.AircraftTypeId);
            var importCategory = _actionCategoryRepository.Get(aircraftBusinessDto.ImportCategoryId);

            //添加商业数据历史
            var newAb = aircraft.AddNewAircraftBusiness();
            newAb.SetAircraftType(aircraftType);
            newAb.SetCarryingCapacity(aircraftBusinessDto.CarryingCapacity);
            newAb.SetEndDate(aircraftBusinessDto.EndDate);
            newAb.SetImportCategory(importCategory);
            newAb.SetOperationStatus(OperationStatus.草稿);
            newAb.SetSeatingCapacity(aircraftBusinessDto.SeatingCapacity);
            newAb.SetStartDate(aircraftBusinessDto.StartDate);

        }

        /// <summary>
        ///     插入运营权历史
        /// </summary>
        /// <param name="aircraft">实际飞机</param>
        /// <param name="operationHistoryDto">运营权历史DTO</param>
        private void InsertOperationHistory(Aircraft aircraft, OperationHistoryDTO operationHistoryDto)
        {
            //获取相关数据
            var airlines = _airlinesRepository.Get(operationHistoryDto.AirlinesId);
            var exportCategory = _actionCategoryRepository.Get(operationHistoryDto.ExportCategoryId);
            var importCategory = _actionCategoryRepository.Get(operationHistoryDto.ImportCategoryId);

            //添加运营权历史
            var newOh = aircraft.AddNewOperationHistory();
            newOh.SetAirlines(airlines);
            newOh.SetEndDate(operationHistoryDto.EndDate);
            newOh.SetExportCategoryID(exportCategory);
            newOh.SetImportCategory(importCategory);
            newOh.SetNote(operationHistoryDto.Note);
            newOh.SetOnHireDate(operationHistoryDto.OnHireDate);
            newOh.SetOperationStatus(OperationStatus.草稿);
            newOh.SetReceiptDate(operationHistoryDto.ReceiptDate);
            newOh.SetRegNumber(operationHistoryDto.RegNumber);
            newOh.SetStartDate(operationHistoryDto.StartDate);
            newOh.SetStopDate(operationHistoryDto.StopDate);
            newOh.SetTechDeliveryDate(operationHistoryDto.TechDeliveryDate);
            newOh.SetTechReceiptDate(operationHistoryDto.TechReceiptDate);
        }

        /// <summary>
        ///     插入所有权历史
        /// </summary>
        /// <param name="aircraft">实际飞机</param>
        /// <param name="ownershipHistoryDto">所有权历史DTO</param>
        private void InsertOwnershipHistory(Aircraft aircraft, OwnershipHistoryDTO ownershipHistoryDto)
        {
            //获取相关数据
            var supplier = _supplierRepository.Get(ownershipHistoryDto.SupplierId);

            //添加所有权历史
            var newOwnh = aircraft.AddNewOwnershipHistory();
            newOwnh.SetEndDate(ownershipHistoryDto.EndDate);
            newOwnh.SetOperationStatus(OperationStatus.草稿);
            newOwnh.SetStartDate(ownershipHistoryDto.StartDate);
            newOwnh.SetSupplier(supplier);
        }

        /// <summary>
        ///     更新商业数据历史
        /// </summary>
        /// <param name="aircraftBusinessDto">商业数据历史DTO</param>
        /// <param name="aircraftBusiness">商业数据历史</param>
        private void UpdateAircraftBusiness(AircraftBusinessDTO aircraftBusinessDto, AircraftBusiness aircraftBusiness)
        {
            //获取相关数据
            var aircraftType = _aircraftTypeRepository.Get(aircraftBusinessDto.AircraftTypeId);
            var importCategory = _actionCategoryRepository.Get(aircraftBusinessDto.ImportCategoryId);

            //更新商业数据历史
            aircraftBusiness.SetAircraftType(aircraftType);
            aircraftBusiness.SetCarryingCapacity(aircraftBusinessDto.CarryingCapacity);
            aircraftBusiness.SetEndDate(aircraftBusinessDto.EndDate);
            aircraftBusiness.SetImportCategory(importCategory);
            aircraftBusiness.SetOperationStatus((OperationStatus)aircraftBusinessDto.Status);
            aircraftBusiness.SetSeatingCapacity(aircraftBusinessDto.SeatingCapacity);
            aircraftBusiness.SetStartDate(aircraftBusinessDto.StartDate);
        }

        /// <summary>
        ///     更新运营权历史
        /// </summary>
        /// <param name="operationHistoryDto">运营权历史DTO</param>
        /// <param name="operationHistory">运营权历史</param>
        private void UpdateOperationHistory(OperationHistoryDTO operationHistoryDto, OperationHistory operationHistory)
        {
            //获取相关数据
            var airlines = _airlinesRepository.Get(operationHistoryDto.AirlinesId);
            var exportCategory = _actionCategoryRepository.Get(operationHistoryDto.ExportCategoryId);
            var importCategory = _actionCategoryRepository.Get(operationHistoryDto.ImportCategoryId);

            //更新运营权历史
            operationHistory.SetAirlines(airlines);
            operationHistory.SetEndDate(operationHistoryDto.EndDate);
            operationHistory.SetExportCategoryID(exportCategory);
            operationHistory.SetImportCategory(importCategory);
            operationHistory.SetNote(operationHistoryDto.Note);
            operationHistory.SetOnHireDate(operationHistoryDto.OnHireDate);
            operationHistory.SetOperationStatus((OperationStatus)operationHistoryDto.Status);
            operationHistory.SetReceiptDate(operationHistoryDto.ReceiptDate);
            operationHistory.SetRegNumber(operationHistoryDto.RegNumber);
            operationHistory.SetStartDate(operationHistoryDto.StartDate);
            operationHistory.SetStopDate(operationHistoryDto.StopDate);
            operationHistory.SetTechDeliveryDate(operationHistoryDto.TechDeliveryDate);
            operationHistory.SetTechReceiptDate(operationHistoryDto.TechReceiptDate);
        }

        /// <summary>
        ///     更新所有权历史
        /// </summary>
        /// <param name="ownershipHistoryDto">所有权历史DTO</param>
        /// <param name="ownershipHistory">所有权历史</param>
        private void UpdateOwnershipHistory(OwnershipHistoryDTO ownershipHistoryDto, OwnershipHistory ownershipHistory)
        {
            //获取相关数据
            var supplier = _supplierRepository.Get(ownershipHistoryDto.SupplierId);

            //更新所有权历史
            ownershipHistory.SetEndDate(ownershipHistoryDto.EndDate);
            ownershipHistory.SetOperationStatus((OperationStatus)ownershipHistoryDto.Status);
            ownershipHistory.SetStartDate(ownershipHistoryDto.StartDate);
            ownershipHistory.SetSupplier(supplier);
        }
        #endregion

        #endregion
    }
}
