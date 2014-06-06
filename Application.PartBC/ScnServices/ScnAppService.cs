#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnAppService
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
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.ScnQueries;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;

#endregion

namespace UniCloud.Application.PartBC.ScnServices
{
    /// <summary>
    ///     实现Scn的服务接口。
    ///     用于处理Scn相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ScnAppService : ContextBoundObject, IScnAppService
    {
        private readonly IAirBusScnRepository _airBusScnRepository;
        private readonly IScnQuery _scnQuery;
        private readonly IScnRepository _scnRepository;

        public ScnAppService(IScnQuery scnQuery, IScnRepository scnRepository, IAirBusScnRepository airBusScnRepository)
        {
            _scnQuery = scnQuery;
            _scnRepository = scnRepository;
            _airBusScnRepository = airBusScnRepository;
        }

        #region ScnDTO

        /// <summary>
        ///     获取所有Scn。
        /// </summary>
        public IQueryable<ScnDTO> GetScns()
        {
            var queryBuilder =
                new QueryBuilder<Scn>();
            return _scnQuery.ScnDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增Scn。
        /// </summary>
        /// <param name="dto">ScnDTO。</param>
        [Insert(typeof (ScnDTO))]
        public void InsertScn(ScnDTO dto)
        {
            //创建SCN
            Scn newScn = ScnFactory.CreateScn();
            ScnFactory.SetScn(newScn, dto.Title, dto.Type, dto.CheckDate, dto.CSCNumber, dto.ModNumber, dto.RfcNumber,
                dto.ValidDate, dto.Cost,
                dto.ScnNumber, dto.ScnType, dto.ScnStatus, dto.Description, dto.ScnDocName, dto.ScnDocumentId,
                dto.ReceiveDate,
                dto.AuditOrganization, dto.Auditor, dto.AuditTime, dto.AuditNotes);

            //添加使用飞机
            dto.ApplicableAircrafts.ToList().ForEach(appliAc => InsertApplicableAircraft(newScn, appliAc));

            _scnRepository.Add(newScn);
        }

        /// <summary>
        ///     更新Scn。
        /// </summary>
        /// <param name="dto">ScnDTO。</param>
        [Update(typeof (ScnDTO))]
        public void ModifyScn(ScnDTO dto)
        {
            //获取需要更新的对象
            Scn updateScn = _scnRepository.Get(dto.Id);

            if (updateScn != null)
            {
                //更新主表：
                ScnFactory.SetScn(updateScn, dto.Title, dto.Type, dto.CheckDate, dto.CSCNumber, dto.ModNumber,
                    dto.RfcNumber, dto.ValidDate, dto.Cost,
                    dto.ScnNumber, dto.ScnType, dto.ScnStatus, dto.Description, dto.ScnDocName, dto.ScnDocumentId,
                    dto.ReceiveDate,
                    dto.AuditOrganization, dto.Auditor, dto.AuditTime, dto.AuditNotes);
                UpdateApplicableAircrafts(dto.ApplicableAircrafts, updateScn);
                ////更新Scn适用飞机集合：
                //var dtoApplicableAircrafts = dto.ApplicableAircrafts;
                //var applicableAircrafts = updateScn.ApplicableAircrafts;
                //DataHelper.DetailHandle(dtoApplicableAircrafts.ToArray(),
                //    applicableAircrafts.ToArray(),
                //    c => c.Id, p => p.Id,
                //    i => InsertApplicableAircraft(updateScn, i),
                //    UpdateApplicableAircraft,
                //    d => _scnRepository.RemoveApplicableAircraft(d));
            }
            _scnRepository.Modify(updateScn);
        }

        /// <summary>
        ///     删除Scn。
        /// </summary>
        /// <param name="dto">ScnDTO。</param>
        [Delete(typeof (ScnDTO))]
        public void DeleteScn(ScnDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            Scn delScn = _scnRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delScn != null)
            {
                _scnRepository.DeleteScn(delScn); //删除Scn。
            }
        }

        #region 处理适用飞机

        /// <summary>
        ///     插入适用飞机
        /// </summary>
        /// <param name="scn">SCN</param>
        /// <param name="applicableAircraftDto">适用飞机DTO</param>
        private void InsertApplicableAircraft(Scn scn, ApplicableAircraftDTO applicableAircraftDto)
        {
            // 添加基本构型
            ApplicableAircraft newApplicableAircraft = scn.AddNewApplicableAircraft();
            ScnFactory.SetApplicableAircraft(newApplicableAircraft, applicableAircraftDto.CompleteDate,
                applicableAircraftDto.Cost, applicableAircraftDto.ContractAircraftId);
        }

        /// <summary>
        ///     更新适用飞机
        /// </summary>
        /// <param name="applicableAircraftDto">适用飞机DTO</param>
        /// <param name="applicableAircraft">适用飞机</param>
        private void UpdateApplicableAircraft(ApplicableAircraftDTO applicableAircraftDto,
            ApplicableAircraft applicableAircraft)
        {
            ScnFactory.SetApplicableAircraft(applicableAircraft, applicableAircraftDto.CompleteDate,
                applicableAircraftDto.Cost, applicableAircraftDto.ContractAircraftId);
        }

        #endregion

        #region 更新适用飞机集合

        /// <summary>
        ///     更新适用飞机集合
        /// </summary>
        /// <param name="sourceApplicableAircrafts">客户端集合</param>
        /// <param name="dstScn">数据库集合</param>
        private void UpdateApplicableAircrafts(IEnumerable<ApplicableAircraftDTO> sourceApplicableAircrafts, Scn dstScn)
        {
            var applicableAircrafts = new List<ApplicableAircraft>();
            foreach (ApplicableAircraftDTO sourceApplicableAircraft in sourceApplicableAircrafts)
            {
                ApplicableAircraft result =
                    dstScn.ApplicableAircrafts.FirstOrDefault(p => p.Id == sourceApplicableAircraft.Id);
                if (result == null)
                {
                    result = ScnFactory.CreateApplicableAircraft();
                    result.ChangeCurrentIdentity(sourceApplicableAircraft.Id);
                }
                ScnFactory.SetApplicableAircraft(result, sourceApplicableAircraft.CompleteDate,
                    sourceApplicableAircraft.Cost, sourceApplicableAircraft.ContractAircraftId);
                applicableAircrafts.Add(result);
            }
            dstScn.ApplicableAircrafts.ToList().ForEach(p =>
            {
                if (applicableAircrafts.FirstOrDefault(t => t.Id == p.Id) == null)
                {
                    _scnRepository.RemoveApplicableAircraft(p);
                }
            });
            dstScn.ApplicableAircrafts = applicableAircrafts;
        }

        #endregion

        #endregion

        #region AirBusScnDTO

        /// <summary>
        ///     获取所有AirBusScn。
        /// </summary>
        public IQueryable<AirBusScnDTO> GetAirBusScns()
        {
            var queryBuilder =
                new QueryBuilder<AirBusScn>();
            return _scnQuery.AirBusScnDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增AirBusScn。
        /// </summary>
        /// <param name="dto">AirBusScnDTO。</param>
        [Insert(typeof (AirBusScnDTO))]
        public void InsertAirBusScn(AirBusScnDTO dto)
        {
            //创建SCN
            AirBusScn newScn = _airBusScnRepository.Get(dto.CSCNumber, dto.ScnNumber);
            if (newScn == null)
            {
                newScn = AirBusScnFactory.CreateAirBusScn();
                AirBusScnFactory.SetAirBusScn(newScn, dto.Title, dto.CSCNumber, dto.ModNumber, dto.ScnNumber,
                    dto.ScnStatus, dto.Description);
                _airBusScnRepository.Add(newScn);
            }
            else
            {
                AirBusScnFactory.SetAirBusScn(newScn, dto.Title, dto.CSCNumber, dto.ModNumber, dto.ScnNumber,
                    dto.ScnStatus, dto.Description);
                _airBusScnRepository.Modify(newScn);
            }
        }

        /// <summary>
        ///     更新AirBusScn。
        /// </summary>
        /// <param name="dto">AirBusScnDTO。</param>
        [Update(typeof (AirBusScnDTO))]
        public void ModifyScn(AirBusScnDTO dto)
        {
            //获取需要更新的对象
            AirBusScn updateScn = _airBusScnRepository.Get(dto.Id);

            if (updateScn != null)
            {
                //更新主表：
                AirBusScnFactory.SetAirBusScn(updateScn, dto.Title, dto.CSCNumber, dto.ModNumber,
                    dto.ScnNumber, dto.ScnStatus, dto.Description);
            }
            _airBusScnRepository.Modify(updateScn);
        }

        /// <summary>
        ///     删除AirBusScn。
        /// </summary>
        /// <param name="dto">AirBusScnDTO。</param>
        [Delete(typeof (AirBusScnDTO))]
        public void DeleteAirBusScn(ScnDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            AirBusScn delScn = _airBusScnRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delScn != null)
            {
                _airBusScnRepository.Remove(delScn); //删除Scn。
            }
        }

        #endregion
    }
}