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
            InitialLeaseContractAircraft();
            InitialCurrency();
            InitialLinkman();
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

        private static QueryableDataServiceCollectionView<LeaseContractAircraftDTO> _leaseContractAircraftQuery;
        public static List<LeaseContractAircraftDTO> LeaseContractAircrafts { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialLeaseContractAircraft()
        {
            _leaseContractAircraftQuery = new QueryableDataServiceCollectionView<LeaseContractAircraftDTO>(Context,
                Context.LeaseContractAircrafts);
            _leaseContractAircraftQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<LeaseContractAircraftDTO>;
                LeaseContractAircrafts = result.ToList();
            };
        }


        public static void LoadLeaseContractAircrafts()
        {
            if (LeaseContractAircrafts == null)
            {
                Context.LeaseContractAircrafts.BeginExecute(
                    p => { LeaseContractAircrafts = Context.LeaseContractAircrafts.EndExecute(p).ToList(); },
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

        #region Currency

        private static QueryableDataServiceCollectionView<CurrencyDTO> _currencyQuery;
        public static List<CurrencyDTO> Currencies { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialCurrency()
        {
            _currencyQuery = new QueryableDataServiceCollectionView<CurrencyDTO>(Context, Context.Currencies);
            _currencyQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<CurrencyDTO>;
                Currencies = result.ToList();
            };
        }

        /// <summary>
        ///     加载数据
        /// </summary>
        public static void LoadCurrency()
        {
            if (Currencies == null)
            {
                _currencyQuery.AutoLoad = true;
            }
        }

        #endregion

        #region Linkman

        private static QueryableDataServiceCollectionView<LinkmanDTO> _linkmanQuery;
        public static List<LinkmanDTO> Linkmen { get; set; }

        /// <summary>
        ///     初始化
        /// </summary>
        private static void InitialLinkman()
        {
            _linkmanQuery = new QueryableDataServiceCollectionView<LinkmanDTO>(Context, Context.Linkmans);
            _linkmanQuery.LoadedData += (o, e) =>
            {
                var result = o as QueryableDataServiceCollectionView<LinkmanDTO>;
                Linkmen = result.ToList();
            };
        }

        /// <summary>
        ///     加载数据
        /// </summary>
        public static void LoadLinkman()
        {
            if (Currencies == null)
            {
                _linkmanQuery.AutoLoad = true;
            }
        }

        #endregion
    }
}