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
using System.Xml;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.MaterialServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class MaterialAppService : ContextBoundObject, IMaterialAppService
    {
        private readonly IMaterialQuery _materialQuery;
        private readonly IMaterialRepository _materialRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        public MaterialAppService(IMaterialQuery materialQuery, IMaterialRepository materialRepository,
            IAircraftTypeRepository aircraftTypeRepository)
        {
            _materialQuery = materialQuery;
            _materialRepository = materialRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
        }

        #region 飞机

        public IQueryable<AircraftMaterialDTO> GetAircraftMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.AircraftMaterialsQuery(queryBuilder);
        }


        /// <summary>
        ///     新增AircraftMaterial。
        /// </summary>
        /// <param name="dto">AircraftMaterialDTO。</param>
        [Insert(typeof(AircraftMaterialDTO))]
        public void InsertAircraftMaterial(AircraftMaterialDTO dto)
        {
            AircraftMaterial aircraftMaterial = MaterialFactory.CreateAircraftMaterial(dto.Name, dto.Description, dto.AircraftTypeId);
            aircraftMaterial.ManufacturerID = dto.ManufacturerId;
            _materialRepository.Add(aircraftMaterial);
        }

        /// <summary>
        ///     更新AircraftMaterial。
        /// </summary>
        /// <param name="dto">AircraftMaterialDTO。</param>
        [Update(typeof(AircraftMaterialDTO))]
        public void ModifyAircraftMaterial(AircraftMaterialDTO dto)
        {
            var updateAircraftMaterial = _materialRepository.Get(dto.AcMaterialId) as AircraftMaterial;  //获取需要更新的对象。

            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);

            //更新。
            if (updateAircraftMaterial != null)
            {
                updateAircraftMaterial.SetAircraftType(aircraftType);
                updateAircraftMaterial.Name = dto.Name;
                updateAircraftMaterial.Description = dto.Description;
                updateAircraftMaterial.ManufacturerID = dto.ManufacturerId;
                _materialRepository.Modify(updateAircraftMaterial);
            }
        }

        /// <summary>
        ///     删除AircraftMaterial。
        /// </summary>
        /// <param name="dto">AircraftMaterialDTO。</param>
        [Delete(typeof(AircraftMaterialDTO))]
        public void DeleteAircraftMaterial(AircraftMaterialDTO dto)
        {
            var delAircraftMaterial = _materialRepository.Get(dto.AcMaterialId); //获取需要删除的对象。
            _materialRepository.Remove(delAircraftMaterial); //删除AircraftMaterial。
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
        ///     新增BFEMaterial。
        /// </summary>
        /// <param name="dto">BFEMaterialDTO。</param>
        [Insert(typeof(BFEMaterialDTO))]
        public void InsertBFEMaterial(BFEMaterialDTO dto)
        {
            BFEMaterial bFEMaterial = MaterialFactory.CreateBFEMaterial(dto.Name, dto.Description, dto.Pn);
            bFEMaterial.ManufacturerID = dto.ManufacturerId;
            _materialRepository.Add(bFEMaterial);
        }

        /// <summary>
        ///     更新BFEMaterial。
        /// </summary>
        /// <param name="dto">BFEMaterialDTO。</param>
        [Update(typeof(BFEMaterialDTO))]
        public void ModifyBFEMaterial(BFEMaterialDTO dto)
        {
            var updateBFEMaterial = _materialRepository.Get(dto.BFEMaterialId) as BFEMaterial;  //获取需要更新的对象。

            //更新。
            if (updateBFEMaterial != null)
            {
                updateBFEMaterial.SetPart(dto.Pn);
                updateBFEMaterial.Name = dto.Name;
                updateBFEMaterial.Description = dto.Description;
                updateBFEMaterial.ManufacturerID = dto.ManufacturerId;
                _materialRepository.Modify(updateBFEMaterial);
            }
        }

        /// <summary>
        ///     删除BFEMaterial。
        /// </summary>
        /// <param name="dto">BFEMaterialDTO。</param>
        [Delete(typeof(BFEMaterialDTO))]
        public void DeleteBFEMaterial(BFEMaterialDTO dto)
        {
            var delBFEMaterial = _materialRepository.Get(dto.BFEMaterialId); //获取需要删除的对象。
            _materialRepository.Remove(delBFEMaterial); //删除BFEMaterial。
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
        ///     新增EngineMaterial。
        /// </summary>
        /// <param name="dto">EngineMaterialDTO。</param>
        [Insert(typeof(EngineMaterialDTO))]
        public void InsertEngineMaterial(EngineMaterialDTO dto)
        {
            EngineMaterial engineMaterial = MaterialFactory.CreateEngineMaterial(dto.Name, dto.Description, dto.Pn);
            engineMaterial.ManufacturerID = dto.ManufacturerId;
            _materialRepository.Add(engineMaterial);
        }

        /// <summary>
        ///     更新EngineMaterial。
        /// </summary>
        /// <param name="dto">EngineMaterialDTO。</param>
        [Update(typeof(EngineMaterialDTO))]
        public void ModifyEngineMaterial(EngineMaterialDTO dto)
        {
            var updateEngineMaterial = _materialRepository.Get(dto.EngineMaterialId) as EngineMaterial;  //获取需要更新的对象。

            //更新。
            if (updateEngineMaterial != null)
            {
                updateEngineMaterial.SetPart(dto.Pn);
                updateEngineMaterial.Name = dto.Name;
                updateEngineMaterial.Description = dto.Description;
                updateEngineMaterial.ManufacturerID = dto.ManufacturerId;
                _materialRepository.Modify(updateEngineMaterial);
            }
        }

        /// <summary>
        ///     删除EngineMaterial。
        /// </summary>
        /// <param name="dto">EngineMaterialDTO。</param>
        [Delete(typeof(EngineMaterialDTO))]
        public void DeleteEngineMaterial(EngineMaterialDTO dto)
        {
            var delEngineMaterial = _materialRepository.Get(dto.EngineMaterialId); //获取需要删除的对象。
            _materialRepository.Remove(delEngineMaterial); //删除EngineMaterial。
        }
        #endregion
    }
}