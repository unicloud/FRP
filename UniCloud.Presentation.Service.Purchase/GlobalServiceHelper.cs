﻿#region Version Info

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

using System;
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
            SupplierQuery = new QueryableDataServiceCollectionView<SupplierDTO>(Context, Context.Suppliers);
            SupplierQuery.LoadedData += SupplierQueryLoadedData;
            //InitialAcType();
            LoadContractAircrafts();
        }

        //供应商数据加载完毕，分类处理
        static void SupplierQueryLoadedData(object sender, Telerik.Windows.Controls.DataServices.LoadedDataEventArgs e)
        {
            MaintainSupplier = new List<SupplierDTO>();
            var result = sender as QueryableDataServiceCollectionView<SupplierDTO>;
            result.AsEnumerable().ToList().ForEach(MaintainSupplier.Add);
        }

        //初始化数据

        public static List<SupplierDTO> MaintainSupplier { get; set; }
        public static List<ContractAircraftDTO> ContractAircrafts { get; set; }
        private static readonly QueryableDataServiceCollectionView<SupplierDTO> SupplierQuery;
        
        public static void InitData()
        {
            if (MaintainSupplier == null)
            {
                SupplierQuery.AutoLoad = true;
            }
            //LoadSupplier(null);
        }

        //加载供应商数据
        private static void LoadSupplier(Type type)
        {
            if (MaintainSupplier == null)
            {
                Context.Suppliers.BeginExecute(p => { MaintainSupplier = Context.Suppliers.EndExecute(p).ToList(); },
                    null);
            }
        }


        //加载合同飞机
        private static void LoadContractAircrafts()
        {
            if (ContractAircrafts == null)
            {
                Context.ContractAircrafts.BeginExecute(p => { ContractAircrafts = Context.ContractAircrafts.EndExecute(p).ToList(); },
                    null);
            }
        }

        //#region AcType

        //public static IEnumerable<AcTypeDTO> AcTypes { get; set; }
        //private static QueryableDataServiceCollectionView<AcTypeDTO> _acTypeView;

        //public static void  LoadAcType()
        //{
        //    if (AcTypes == null)
        //    {
        //        _acTypeView.AutoLoad = true;
        //        return;
        //    }
        //    _acTypeView.AutoLoad = false;
        //}

        ///// <summary>
        ///// 初始化机型
        ///// </summary>
        //public static void InitialAcType()
        //{
        //    _acTypeView = new QueryableDataServiceCollectionView<AcTypeDTO>(Context, Context.AcTypes);
        //    _acTypeView.LoadedData += (sender, e) =>
        //        {
        //            if (e.Error!=null)
        //            {
        //                e.MarkErrorAsHandled();
        //            }
        //            AcTypes = e.Entities.Cast<AcTypeDTO>();
        //        };
        //}
        //#endregion
    }
}