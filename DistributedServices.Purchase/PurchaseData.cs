﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，15:11
// 方案：FRP
// 项目：DistributedServices.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.ContractServices;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.ForwarderServices;
using UniCloud.Application.PurchaseBC.MaterialServices;
using UniCloud.Application.PurchaseBC.PartServices;
using UniCloud.Application.PurchaseBC.ReceptionServices;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Application.PurchaseBC.TradeServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Purchase
{
    /// <summary>
    ///     采购模块数据类
    /// </summary>
    public class PurchaseData : ExposeData.ExposeData
    {
        private readonly IAircraftLeaseReceptionAppService _aircraftLeaseReceptionAppService;
        private readonly IAircraftPurchaseReceptionAppService _aircraftPurchaseReceptionAppService;
        private readonly IEngineLeaseReceptionAppService _engineLeaseReceptionAppService;
        private readonly IEnginePurchaseReceptionAppService _enginePurchaseReceptionAppService;
        private readonly IForwarderAppService _forwarderAppService;
        private readonly IMaintainContractAppService _maintainContractAppService;
        private readonly IMaterialAppService _materialAppService;
        private readonly IPartAppService _partAppService;
        private readonly ISupplierAppService _supplierAppService;
        private readonly ITradeAppService _tradeAppService;

        public PurchaseData()
            : base("UniCloud.Application.PurchaseBC.DTO")
        {
            _forwarderAppService = DefaultContainer.Resolve<IForwarderAppService>();
            _maintainContractAppService = DefaultContainer.Resolve<IMaintainContractAppService>();
            _supplierAppService = DefaultContainer.Resolve<ISupplierAppService>();
            _tradeAppService = DefaultContainer.Resolve<ITradeAppService>();
            _aircraftLeaseReceptionAppService = DefaultContainer.Resolve<IAircraftLeaseReceptionAppService>();
            _aircraftPurchaseReceptionAppService = DefaultContainer.Resolve<IAircraftPurchaseReceptionAppService>();
            _engineLeaseReceptionAppService = DefaultContainer.Resolve<IEngineLeaseReceptionAppService>();
            _enginePurchaseReceptionAppService = DefaultContainer.Resolve<IEnginePurchaseReceptionAppService>();
            _materialAppService = DefaultContainer.Resolve<IMaterialAppService>();
            _partAppService = DefaultContainer.Resolve<IPartAppService>();
        }

        #region 合作公司相关集合

        /// <summary>
        ///     承运人信息。
        /// </summary>
        public IQueryable<ForwarderDTO> Forwarders
        {
            get { return _forwarderAppService.GetForwarders(); }
        }

        /// <summary>
        ///     供应商信息。
        /// </summary>
        public IQueryable<SupplierDTO> Suppliers
        {
            get { return _supplierAppService.GetSuppliers(); }
        }

        /// <summary>
        ///     供应商公司信息。
        /// </summary>
        public IQueryable<SupplierCompanyDTO> SupplierCompanys
        {
            get { return _supplierAppService.GetSupplierCompanys(); }
        }

        /// <summary>
        ///     联系人信息。
        /// </summary>
        public IQueryable<LinkmanDTO> Linkmans
        {
            get { return _supplierAppService.GetLinkmans(); }
        }

        /// <summary>
        /// 合作公司飞机物料
        /// </summary>
        public IQueryable<SupplierCompanyAcMaterialDTO> SupplierCompanyAcMaterials {
            get { return _supplierAppService.GetSupplierCompanyAcMaterials(); }
        }

        /// <summary>
        /// 合作公司发动机物料
        /// </summary>
        public IQueryable<SupplierCompanyEngineMaterialDTO> SupplierCompanyEngineMaterials
        {
            get { return _supplierAppService.GetSupplierCompanyEngineMaterials(); }
        }

        /// <summary>
        /// 合作公司BFE物料
        /// </summary>
        public IQueryable<SupplierCompanyBFEMaterialDTO> SupplierCompanyBFEMaterials
        {
            get { return _supplierAppService.GetSupplierCompanyBFEMaterials(); }
        }

        #endregion

        #region 物料相关集合

        /// <summary>
        ///     飞机物料
        /// </summary>
        public IQueryable<AircraftMaterialDTO> AircraftMaterias
        {
            get { return _materialAppService.GetAircraftMaterials(); }
        }

        /// <summary>
        ///     BFE物料
        /// </summary>
        public IQueryable<BFEMaterialDTO> BFEMaterials
        {
            get { return _materialAppService.GetBFEMaterials(); }
        }

        /// <summary>
        ///     发动机物料
        /// </summary>
        public IQueryable<EngineMaterialDTO> EngineMaterials
        {
            get { return _materialAppService.GetEngineMaterials(); }
        }

        #endregion

        #region 部件集合

        /// <summary>
        ///     部件集合
        /// </summary>
        public IQueryable<PartDTO> Parts
        {
            get { return _partAppService.GetParts(); }
        }

        #endregion

        /// <summary>
        ///     发动机维修合同信息
        /// </summary>
        public IQueryable<EngineMaintainContractDTO> EngineMaintainContracts
        {
            get { return _maintainContractAppService.GetEngineMaintainContracts(); }
        }

        /// <summary>
        ///     APU维修合同信息
        /// </summary>
        public IQueryable<APUMaintainContractDTO> APUMaintainContracts
        {
            get { return _maintainContractAppService.GetApuMaintainContracts(); }
        }

        /// <summary>
        ///     起落架维修合同信息
        /// </summary>
        public IQueryable<UndercartMaintainContractDTO> UndercartMaintainContracts
        {
            get { return _maintainContractAppService.GetUndercartMaintainContracts(); }
        }

        #region Reception

        /// <summary>
        ///     租赁飞机接收项目集合
        /// </summary>
        public IQueryable<AircraftLeaseReceptionDTO> AircraftLeaseReceptions
        {
            get { return _aircraftLeaseReceptionAppService.GetAircraftLeaseReceptions(); }
        }

        /// <summary>
        ///     采购飞机接收项目集合
        /// </summary>
        public IQueryable<AircraftPurchaseReceptionDTO> AircraftPurchaseReceptions
        {
            get { return _aircraftPurchaseReceptionAppService.GetAircraftPurchaseReceptions(); }
        }

        /// <summary>
        ///     租赁发动机接收项目集合
        /// </summary>
        public IQueryable<EngineLeaseReceptionDTO> EngineLeaseReceptions
        {
            get { return _engineLeaseReceptionAppService.GetEngineLeaseReceptions(); }
        }

        /// <summary>
        ///     采购发动机接收项目集合
        /// </summary>
        public IQueryable<EnginePurchaseReceptionDTO> EnginePurchaseReceptions
        {
            get { return _enginePurchaseReceptionAppService.GetEnginePurchaseReceptions(); }
        }

        #endregion

        #region 交易

        /// <summary>
        ///     交易集合
        /// </summary>
        public IQueryable<TradeDTO> Trades
        {
            get { return _tradeAppService.GetTrades(); }
        }

        /// <summary>
        ///     租赁飞机订单集合
        /// </summary>
        public IQueryable<AircraftLeaseOrderDTO> AircraftLeaseOrders
        {
            get { return _tradeAppService.GetAircraftLeaseOrders(); }
        }

        /// <summary>
        ///     购买飞机订单集合
        /// </summary>
        public IQueryable<AircraftPurchaseOrderDTO> AircraftPurchaseOrders
        {
            get { return _tradeAppService.GetAircraftPurchaseOrders(); }
        }

        /// <summary>
        ///     租赁发动机订单集合
        /// </summary>
        public IQueryable<EngineLeaseOrderDTO> EngineLeaseOrders
        {
            get { return _tradeAppService.GetEngineLeaseOrders(); }
        }

        /// <summary>
        ///     购买发动机订单集合
        /// </summary>
        public IQueryable<EnginePurchaseOrderDTO> EnginePurchaseOrders
        {
            get { return _tradeAppService.GetEnginePurchaseOrders(); }
        }

        /// <summary>
        ///     BFE采购订单集合
        /// </summary>
        public IQueryable<BFEPurchaseOrderDTO> BFEPurchaseOrders
        {
            get { return _tradeAppService.GetBFEPurchaseOrders(); }
        }

        #endregion
    }
}