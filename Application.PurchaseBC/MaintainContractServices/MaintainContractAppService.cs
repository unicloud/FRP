#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/15 17:32:01
// 文件名：MaintainContractAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.MaintainContractQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.MaintainContractServices
{
    /// <summary>
    ///     实现发动机维修合同接口。
    ///     用于处于维修合同相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class MaintainContractAppService : ContextBoundObject, IMaintainContractAppService
    {
        private readonly IMaintainContractQuery _maintainContractQuery;
        private readonly IMaintainContractRepository _contractRepository;


        public MaintainContractAppService(IMaintainContractQuery maintainContractQuery,
                                                IMaintainContractRepository contractRepository)
        {
            _maintainContractQuery = maintainContractQuery;
            _contractRepository = contractRepository;
        }

        #region EngineMaintainContractDTO
        /// <summary>
        ///     获取所有发动机维修合同。
        /// </summary>
        /// <returns>所有发动机维修合同。</returns>
        public IQueryable<EngineMaintainContractDTO> GetEngineMaintainContracts()
        {
            var queryBuilder = new QueryBuilder<MaintainContract>();
            return _maintainContractQuery.EngineMaintainContractDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增发动机维修合同。
        /// </summary>
        /// <param name="engineMaintainContract">发动机维修合同DTO。</param>
        [Insert(typeof(EngineMaintainContractDTO))]
        public void InsertEngineMaintainContract(EngineMaintainContractDTO engineMaintainContract)
        {
            var newEngineMaintainContract = MaintainContractFactory.CreateEngineMaintainContract();
            MaintainContractFactory.SetMaintainContract(newEngineMaintainContract, engineMaintainContract.Name, engineMaintainContract.Number, engineMaintainContract.Signatory,
                engineMaintainContract.SignDate, engineMaintainContract.Abstract, engineMaintainContract.SignatoryId, engineMaintainContract.DocumentId, engineMaintainContract.DocumentName);
            _contractRepository.Add(newEngineMaintainContract);
        }


        /// <summary>
        ///     更新发动机维修合同。
        /// </summary>
        /// <param name="engineMaintainContract">发动机维修合同DTO。</param>
        [Update(typeof(EngineMaintainContractDTO))]
        public void ModifyEngineMaintainContract(EngineMaintainContractDTO engineMaintainContract)
        {
            var updateEngineMaintainContract =
                _contractRepository.Get(engineMaintainContract.EngineMaintainContractId); //获取需要更新的对象。
            MaintainContractFactory.SetMaintainContract(updateEngineMaintainContract, engineMaintainContract.Name, engineMaintainContract.Number, engineMaintainContract.Signatory,
                engineMaintainContract.SignDate, engineMaintainContract.Abstract, engineMaintainContract.SignatoryId, engineMaintainContract.DocumentId, engineMaintainContract.DocumentName);
            _contractRepository.Modify(updateEngineMaintainContract);
        }

        /// <summary>
        ///     删除发动机维修合同。
        /// </summary>
        /// <param name="engineMaintainContract">发动机维修合同DTO。</param>
        [Delete(typeof(EngineMaintainContractDTO))]
        public void DeleteEngineMaintainContract(EngineMaintainContractDTO engineMaintainContract)
        {
            var deleteEngineMaintainContract =
                _contractRepository.Get(engineMaintainContract.EngineMaintainContractId); //获取需要删除的对象。
            _contractRepository.Remove(deleteEngineMaintainContract); //删除发动机维修合同。
        }
        #endregion

        #region APUMaintainContractDTO
        /// <summary>
        ///     获取所有APU维修合同。
        /// </summary>
        /// <returns>所有APU维修合同。</returns>
        public IQueryable<APUMaintainContractDTO> GetApuMaintainContracts()
        {
            var queryBuilder = new QueryBuilder<MaintainContract>();
            return _maintainContractQuery.APUMaintainContractDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增APU维修合同。
        /// </summary>
        /// <param name="apuMaintainContract">APU维修合同DTO。</param>
        [Insert(typeof(APUMaintainContractDTO))]
        public void InsertApuMaintainContract(APUMaintainContractDTO apuMaintainContract)
        {
            var newApuMaintainContract = MaintainContractFactory.CreateApuMaintainContract();
            MaintainContractFactory.SetMaintainContract(newApuMaintainContract, apuMaintainContract.Name, apuMaintainContract.Number, apuMaintainContract.Signatory,
               apuMaintainContract.SignDate, apuMaintainContract.Abstract, apuMaintainContract.SignatoryId, apuMaintainContract.DocumentId, apuMaintainContract.DocumentName);
            _contractRepository.Add(newApuMaintainContract);
        }

        /// <summary>
        ///     更新APU维修合同。
        /// </summary>
        /// <param name="apuMaintainContract">APU维修合同DTO。</param>
        [Update(typeof(APUMaintainContractDTO))]
        public void ModifyApuMaintainContract(APUMaintainContractDTO apuMaintainContract)
        {
            var updateApuMaintainContract = _contractRepository.Get(apuMaintainContract.APUMaintainContractId);
            MaintainContractFactory.SetMaintainContract(updateApuMaintainContract, apuMaintainContract.Name, apuMaintainContract.Number, apuMaintainContract.Signatory,
               apuMaintainContract.SignDate, apuMaintainContract.Abstract, apuMaintainContract.SignatoryId, apuMaintainContract.DocumentId, apuMaintainContract.DocumentName);
            //获取需要更新的对象。
            _contractRepository.Modify(updateApuMaintainContract);
        }

        /// <summary>
        ///     删除APU维修合同。
        /// </summary>
        /// <param name="apuMaintainContract">APU维修合同DTO。</param>
        [Delete(typeof(APUMaintainContractDTO))]
        public void DeleteApuMaintainContract(APUMaintainContractDTO apuMaintainContract)
        {
            var deleteApuMaintainContract = _contractRepository.Get(apuMaintainContract.APUMaintainContractId);
            //获取需要删除的对象。
            _contractRepository.Remove(deleteApuMaintainContract); //删除APU维修合同。
        }
        #endregion

        #region UndercartMaintainContractDTO
        /// <summary>
        ///     获取所有起落架维修合同。
        /// </summary>
        /// <returns>所有起落架维修合同。</returns>
        public IQueryable<UndercartMaintainContractDTO> GetUndercartMaintainContracts()
        {
            var queryBuilder = new QueryBuilder<MaintainContract>();
            return _maintainContractQuery.UndercartMaintainContractDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增起落架维修合同。
        /// </summary>
        /// <param name="undercartMaintainContract">起落架维修合同DTO。</param>
        [Insert(typeof(UndercartMaintainContractDTO))]
        public void InsertUndercartMaintainContract(UndercartMaintainContractDTO undercartMaintainContract)
        {
            var newUndercartMaintainContract = MaintainContractFactory.CreateUndercartMaintainContract();
            MaintainContractFactory.SetMaintainContract(newUndercartMaintainContract, undercartMaintainContract.Name, undercartMaintainContract.Number, undercartMaintainContract.Signatory,
               undercartMaintainContract.SignDate, undercartMaintainContract.Abstract, undercartMaintainContract.SignatoryId, undercartMaintainContract.DocumentId, undercartMaintainContract.DocumentName);
            _contractRepository.Add(newUndercartMaintainContract);
        }

        /// <summary>
        ///     更新起落架维修合同。
        /// </summary>
        /// <param name="undercartMaintainContract">起落架维修合同DTO。</param>
        [Update(typeof(UndercartMaintainContractDTO))]
        public void ModifyUndercartMaintainContract(UndercartMaintainContractDTO undercartMaintainContract)
        {
            var updateUndercartMaintainContract =
                _contractRepository.Get(undercartMaintainContract.UndercartMaintainContractId);
            MaintainContractFactory.SetMaintainContract(updateUndercartMaintainContract, undercartMaintainContract.Name, undercartMaintainContract.Number, undercartMaintainContract.Signatory,
               undercartMaintainContract.SignDate, undercartMaintainContract.Abstract, undercartMaintainContract.SignatoryId, undercartMaintainContract.DocumentId, undercartMaintainContract.DocumentName);
            //获取需要更新的对象。
            _contractRepository.Modify(updateUndercartMaintainContract);
        }

        /// <summary>
        ///     删除起落架维修合同。
        /// </summary>
        /// <param name="undercartMaintainContract">起落架维修合同DTO。</param>
        [Delete(typeof(UndercartMaintainContractDTO))]
        public void DeleteUndercartMaintainContract(UndercartMaintainContractDTO undercartMaintainContract)
        {
            var deleteUndercartMaintainContract =
                _contractRepository.Get(undercartMaintainContract.UndercartMaintainContractId);
            //获取需要删除的对象。
            _contractRepository.Remove(deleteUndercartMaintainContract); //删除Undercart维修合同。
        }
        #endregion

        #region AirframeMaintainContractDTO
        /// <summary>
        ///     获取所有机身维修合同。
        /// </summary>
        /// <returns>所有机身维修合同。</returns>
        public IQueryable<AirframeMaintainContractDTO> GetAirframeMaintainContracts()
        {
            var queryBuilder = new QueryBuilder<MaintainContract>();
            return _maintainContractQuery.AirframeMaintainContractDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增机身维修合同。
        /// </summary>
        /// <param name="airframeMaintainContract">机身维修合同DTO。</param>
        [Insert(typeof(AirframeMaintainContractDTO))]
        public void InsertAirframeMaintainContract(AirframeMaintainContractDTO airframeMaintainContract)
        {
            var newAirframeMaintainContract = MaintainContractFactory.CreateAirframeMaintainContract();
            MaintainContractFactory.SetMaintainContract(newAirframeMaintainContract, airframeMaintainContract.Name, airframeMaintainContract.Number, airframeMaintainContract.Signatory,
               airframeMaintainContract.SignDate, airframeMaintainContract.Abstract, airframeMaintainContract.SignatoryId, airframeMaintainContract.DocumentId, airframeMaintainContract.DocumentName);
            _contractRepository.Add(newAirframeMaintainContract);
        }

        /// <summary>
        ///     更新机身维修合同。
        /// </summary>
        /// <param name="airframeMaintainContract">机身维修合同DTO。</param>
        [Update(typeof(AirframeMaintainContractDTO))]
        public void ModifyAirframeMaintainContract(AirframeMaintainContractDTO airframeMaintainContract)
        {
            var updateAirframeMaintainContract =
                _contractRepository.Get(airframeMaintainContract.AirframeMaintainContractId);
            MaintainContractFactory.SetMaintainContract(updateAirframeMaintainContract, airframeMaintainContract.Name, airframeMaintainContract.Number, airframeMaintainContract.Signatory,
               airframeMaintainContract.SignDate, airframeMaintainContract.Abstract, airframeMaintainContract.SignatoryId, airframeMaintainContract.DocumentId, airframeMaintainContract.DocumentName);
            //获取需要更新的对象。
            _contractRepository.Modify(updateAirframeMaintainContract);
        }

        /// <summary>
        ///     删除机身维修合同。
        /// </summary>
        /// <param name="airframeMaintainContract">机身维修合同DTO。</param>
        [Delete(typeof(AirframeMaintainContractDTO))]
        public void DeleteAirframeMaintainContract(AirframeMaintainContractDTO airframeMaintainContract)
        {
            var deleteAirframeMaintainContract =
                _contractRepository.Get(airframeMaintainContract.AirframeMaintainContractId);
            //获取需要删除的对象。
            _contractRepository.Remove(deleteAirframeMaintainContract); 
        }
        #endregion
    }
}
