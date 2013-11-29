#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：MaterialAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
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
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.MaterialServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MaterialAppService : IMaterialAppService
    {
        private readonly IMaterialQuery _materialQuery;
        private readonly IMaterialRepository _materialRepository;

        public MaterialAppService(IMaterialRepository materialRepository, IMaterialQuery materialQuery)
        {
            _materialRepository = materialRepository;
            _materialQuery = materialQuery;
        }

        #region 飞机

        public IQueryable<AircraftMaterialDTO> GetAircraftMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.AircraftMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     新增飞机物料
        /// </summary>
        /// <param name="aircraftMaterial">飞机物料</param>
        [Insert(typeof (AircraftMaterialDTO))]
        public void InsertAircraftMaterial(AircraftMaterialDTO aircraftMaterial)
        {
            var newMaterial = MaterialFactory.CreateAircraftMaterial(aircraftMaterial.Name,
                                                                     aircraftMaterial.Description,
                                                                     aircraftMaterial.AircraftTypeId);
            AddMeterial(newMaterial);
        }

        /// <summary>
        ///     更新飞机物料。
        /// </summary>
        /// <param name="aircraftMaterial">飞机物料</param>
        [Update(typeof (AircraftMaterialDTO))]
        public void ModifyAircraftMaterial(AircraftMaterialDTO aircraftMaterial)
        {
            var updateMaterial = _materialRepository.Get(aircraftMaterial.AcMaterialId); //获取需要更新的对象。

            //更新。
            updateMaterial.Name = aircraftMaterial.Name;
            updateMaterial.Description = aircraftMaterial.Description;
            var material = updateMaterial as AircraftMaterial;
            if (material != null)
                material.SetAircraftTypeId(aircraftMaterial.AircraftTypeId);
            UpdateMeterial(updateMaterial);
        }

        /// <summary>
        ///     删除飞机物料。
        /// </summary>
        /// <param name="aircraftMaterial">飞机物料。</param>
        [Delete(typeof (AircraftMaterialDTO))]
        public void DeleteAircraftMaterial(AircraftMaterialDTO aircraftMaterial)
        {
            var delMaterial = _materialRepository.Get(aircraftMaterial.AcMaterialId); //获取需要删除的对象。
            DelMeterial(delMaterial);
        }

        #endregion

        #region BFE

        public IQueryable<BFEMaterialDTO> GetBFEMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.BFEMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     新增BFE物料
        /// </summary>
        /// <param name="bfeMaterial">BFE物料</param>
        [Insert(typeof (BFEMaterialDTO))]
        public void InsertBFEMaterial(BFEMaterialDTO bfeMaterial)
        {
            var newMaterial = MaterialFactory.CreateBFEMaterial(bfeMaterial.Name,
                                                                bfeMaterial.Description,
                                                                bfeMaterial.PartId);
            AddMeterial(newMaterial);
        }

        /// <summary>
        ///     更新BFE物料。
        /// </summary>
        /// <param name="bfeMaterial">BFE物料</param>
        [Update(typeof (BFEMaterialDTO))]
        public void ModifyBFEMaterial(BFEMaterialDTO bfeMaterial)
        {
            var updateMaterial = _materialRepository.Get(bfeMaterial.BFEMaterialId); //获取需要更新的对象。

            //更新。
            updateMaterial.Name = bfeMaterial.Name;
            updateMaterial.Description = bfeMaterial.Description;
            var material = updateMaterial as BFEMaterial;
            if (material != null)
                material.PartID = bfeMaterial.PartId;
            UpdateMeterial(updateMaterial);
        }

        /// <summary>
        ///     删除BFE物料。
        /// </summary>
        /// <param name="bfeMaterial">BFE物料。</param>
        [Delete(typeof (BFEMaterialDTO))]
        public void DeleteBFEMaterial(BFEMaterialDTO bfeMaterial)
        {
            var delMaterial = _materialRepository.Get(bfeMaterial.BFEMaterialId); //获取需要删除的对象。
            DelMeterial(delMaterial);
        }

        #endregion

        #region 发动机

        public IQueryable<EngineMaterialDTO> GetEngineMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.EngineMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     新增发动机物料
        /// </summary>
        /// <param name="engineMaterial">发动机物料</param>
        [Insert(typeof (EngineMaterialDTO))]
        public void InsertEngineMaterial(EngineMaterialDTO engineMaterial)
        {
            var newMaterial = MaterialFactory.CreateEngineMaterial(engineMaterial.Name,
                                                                   engineMaterial.Description,
                                                                   engineMaterial.PartId);
            AddMeterial(newMaterial);
        }

        /// <summary>
        ///     更新发动机物料。
        /// </summary>
        /// <param name="engineMaterial">发动机物料</param>
        [Update(typeof (EngineMaterialDTO))]
        public void ModifyEngineMaterial(EngineMaterialDTO engineMaterial)
        {
            var updateMaterial = _materialRepository.Get(engineMaterial.EngineMaterialId); //获取需要更新的对象。

            //更新。
            updateMaterial.Name = engineMaterial.Name;
            updateMaterial.Description = engineMaterial.Description;
            var material = updateMaterial as EngineMaterial;
            if (material != null)
                material.PartID = engineMaterial.PartId;
            UpdateMeterial(updateMaterial);
        }

        /// <summary>
        ///     删除发动机物料。
        /// </summary>
        /// <param name="engineMaterial">发动机物料。</param>
        [Delete(typeof (EngineMaterialDTO))]
        public void DeleteEngineMaterial(EngineMaterialDTO engineMaterial)
        {
            var delMaterial = _materialRepository.Get(engineMaterial.EngineMaterialId); //获取需要删除的对象。
            DelMeterial(delMaterial);
        }

        #endregion

        /// <summary>
        ///     增加物料
        /// </summary>
        /// <param name="material">物料</param>
        private void AddMeterial(Material material)
        {
            if (material == null)
                throw new Exception("物料不能为空");
            _materialRepository.Add(material);
        }

        /// <summary>
        ///     更新物料
        /// </summary>
        /// <param name="material">物料</param>
        private void UpdateMeterial(Material material)
        {
            if (material == null)
                throw new Exception("物料不能为空");
            _materialRepository.Modify(material);
        }

        /// <summary>
        ///     删除物料
        /// </summary>
        /// <param name="material">物料</param>
        private void DelMeterial(Material material)
        {
            if (material == null)
                throw new Exception("物料不能为空");
            _materialRepository.Remove(material);
        }
    }
}