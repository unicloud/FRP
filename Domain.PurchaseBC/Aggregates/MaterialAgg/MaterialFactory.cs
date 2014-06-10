#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，21:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;

namespace UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg
{
    /// <summary>
    ///     采购物料工厂
    /// </summary>
    public static class MaterialFactory
    {
        /// <summary>
        ///     创建飞机物料
        /// </summary>
        /// <returns>飞机物料</returns>
        public static AircraftMaterial CreateAircraftMaterial(string name, string description, Guid aircraftTypeId)
        {
            var aircraftMaterial = new AircraftMaterial
                {
                    Description = description,
                    Name = name,
                };
            aircraftMaterial.SetAircraftTypeId(aircraftTypeId);

            return aircraftMaterial;
        }

        /// <summary>
        ///     创建BFE物料
        /// </summary>
        /// <returns>BFE物料</returns>
        public static BFEMaterial CreateBFEMaterial(string name, string description, string pn)
        {
            var bfeMaterial = new BFEMaterial
            {
                Description = description,
                Name = name,
                Pn = pn
            };
            bfeMaterial.GenerateNewIdentity();
            return bfeMaterial;
        }

        /// <summary>
        ///     创建发动机物料
        /// </summary>
        /// <returns>发动机物料</returns>
        public static EngineMaterial CreateEngineMaterial(string name, string description, string pn)
        {
            var engineMaterial = new EngineMaterial
            {
                Description = description,
                Name = name,
                Pn = pn
            };
            engineMaterial.GenerateNewIdentity();
            return engineMaterial;
        }


    }
}