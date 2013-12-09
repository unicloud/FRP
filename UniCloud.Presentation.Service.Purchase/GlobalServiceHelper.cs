#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/21 11:35:52
// 文件名：PurchaseService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase
{
    public static class GlobalServiceHelper
    {
        private static readonly PurchaseData Context;

        static GlobalServiceHelper()
        {
            if (Context == null)
            {
                Context = new PurchaseData(AgentHelper.PurchaseUri);
            }
            InitialSupplier();
            InitialAircraftType();
            InitialContractAircraft();
        }

        #region Supplier

        private static QueryableDataServiceCollectionView<SupplierDTO> _supplierQuery;
        public static List<SupplierDTO> Suppliers { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialSupplier()
        {
            _supplierQuery = new QueryableDataServiceCollectionView<SupplierDTO>(Context, Context.Suppliers);
            _supplierQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<SupplierDTO>;
                Suppliers = result.ToList();
            };
        }

        /// <summary>
        ///     加载数据
        /// </summary>
        public static void LoadSupplier()
        {
            if (Suppliers == null)
            {
                _supplierQuery.AutoLoad = true;
            }
        }

        #endregion

        #region  ContractAircraft
        private static QueryableDataServiceCollectionView<ContractAircraftDTO> _contractAircraftQuery;
        public static List<ContractAircraftDTO> ContractAircrafts { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialContractAircraft()
        {
            _contractAircraftQuery = new QueryableDataServiceCollectionView<ContractAircraftDTO>(Context, Context.ContractAircrafts);
            _contractAircraftQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<ContractAircraftDTO>;
                ContractAircrafts = result.ToList();
            };
        }


        public static void LoadContractAircrafts()
        {
            if (ContractAircrafts == null)
            {
                Context.ContractAircrafts.BeginExecute(p => { ContractAircrafts = Context.ContractAircrafts.EndExecute(p).ToList(); },
                    null);
            }
        }
        #endregion


        #region AircraftType

        private static QueryableDataServiceCollectionView<AircraftTypeDTO> _aircraftTypeQuery;
        public static List<AircraftTypeDTO> AircraftTypes { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialAircraftType()
        {
            _aircraftTypeQuery = new QueryableDataServiceCollectionView<AircraftTypeDTO>(Context, Context.AircraftTypes);
            _aircraftTypeQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<AircraftTypeDTO>;
                AircraftTypes = result.ToList();
            };
        }

        /// <summary>
        ///     加载数据
        /// </summary>
        public static void LoadAircraftType()
        {
            if (AircraftTypes == null)
            {
                _aircraftTypeQuery.AutoLoad = true;
            }
        }

        #endregion
    }
}