#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AirProgrammingAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
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
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AirProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AirProgrammingServices
{
    /// <summary>
    ///     实现航空公司五年规划服务接口。
    ///     用于处理航空公司五年规划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AirProgrammingAppService : IAirProgrammingAppService
    {
        private readonly IAirProgrammingQuery _airProgrammingQuery;
        private readonly IAircraftCategoryRepository _aircraftCategoryRepository;
        private readonly IAirProgrammingRepository _airProgrammingRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IProgrammingRepository _programmingRepository;
        public AirProgrammingAppService(IAirProgrammingQuery airProgrammingQuery
            ,IAircraftCategoryRepository aircraftCategoryRepository,IAirProgrammingRepository airProgrammingRepository,
            IManagerRepository managerRepository,IProgrammingRepository programmingRepository)
        {
            _airProgrammingQuery = airProgrammingQuery;
            _aircraftCategoryRepository = aircraftCategoryRepository;
            _airProgrammingRepository = airProgrammingRepository;
            _managerRepository = managerRepository;
            _programmingRepository = programmingRepository;
        }

        #region AirProgrammingDTO

        /// <summary>
        ///     获取所有航空公司五年规划
        /// </summary>
        /// <returns></returns>
        public IQueryable<AirProgrammingDTO> GetAirProgrammings()
        {
            var queryBuilder =
                new QueryBuilder<AirProgramming>();
            return _airProgrammingQuery.AirProgrammingDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增航空公司五年规划。
        /// </summary>
        /// <param name="dto">航空公司五年规划DTO。</param>
        [Insert(typeof (AirProgrammingDTO))]
        public void InsertAirProgramming(AirProgrammingDTO dto)
        {
            var issuedUnit = _managerRepository.Get(dto.IssuedUnitId);//获取发文单位
            var programming = _programmingRepository.Get(dto.ProgrammingId);//获取规划期间

            //创建航空公司五年规划
            var newAirProgramming = AirProgrammingFactory.CreateAirProgramming();
            newAirProgramming.SetDocument(dto.DocumentId,dto.DocName);
            newAirProgramming.SetIssuedDate(dto.IssuedDate);
            newAirProgramming.SetIssuedUnit(issuedUnit);
            newAirProgramming.SetName(dto.Name);
            newAirProgramming.SetNote(dto.Note);
            newAirProgramming.SetProgramming(programming);

            //添加规划行
            dto.AirProgrammingLines.ToList().ForEach(line => InsertAirProgrammingLine(newAirProgramming, line));

            _airProgrammingRepository.Add(newAirProgramming);
        }

        /// <summary>
        ///     更新航空公司五年规划。
        /// </summary>
        /// <param name="dto">航空公司五年规划DTO。</param>
        [Update(typeof (AirProgrammingDTO))]
        public void ModifyAirProgramming(AirProgrammingDTO dto)
        {
            var issuedUnit = _managerRepository.Get(dto.IssuedUnitId);//获取发文单位
            var programming = _programmingRepository.Get(dto.ProgrammingId);//获取规划期间

            //获取需要更新的对象
            var updateAirProgramming = _airProgrammingRepository.Get(dto.Id);

            if (updateAirProgramming != null)
            {
                //更新主表：
                updateAirProgramming.SetDocument(dto.DocumentId, dto.DocName);
                updateAirProgramming.SetIssuedDate(dto.IssuedDate);
                updateAirProgramming.SetIssuedUnit(issuedUnit);
                updateAirProgramming.SetName(dto.Name);
                updateAirProgramming.SetNote(dto.Note);
                updateAirProgramming.SetProgramming(programming);

                //更新规划行：
                var dtoAirProgrammingLines = dto.AirProgrammingLines;
                var airProgrammingLines = updateAirProgramming.AirProgrammingLines;
                DataHelper.DetailHandle(dtoAirProgrammingLines.ToArray(),
                    airProgrammingLines.ToArray(),
                    c => c.AirProgrammingId, p => p.Id,
                    i => InsertAirProgrammingLine(updateAirProgramming, i),
                    UpdateAirProgrammingLine,
                    d => _airProgrammingRepository.RemoveAirProgrammingLine(d));
            }
            _airProgrammingRepository.Modify(updateAirProgramming);
        }

        /// <summary>
        ///     删除航空公司五年规划。
        /// </summary>
        /// <param name="dto">航空公司五年规划DTO。</param>
        [Delete(typeof (AirProgrammingDTO))]
        public void DeleteAirProgramming(AirProgrammingDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delAirProgramming = _airProgrammingRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delAirProgramming != null)
            {
                _airProgrammingRepository.DeleteAirProgramming(delAirProgramming); //删除航空公司五年规划。
            }
        }


        #region 处理公司五年规划行

        /// <summary>
        ///     插入规划行
        /// </summary>
        /// <param name="airProgramming">航空公司五年规划</param>
        /// <param name="line">规划行DTO</param>
        private void InsertAirProgrammingLine(AirProgramming airProgramming, AirProgrammingLineDTO line)
        {
            //获取
            var aircraftCategory = _aircraftCategoryRepository.Get(line.AircraftCategoryId);
           
            // 添加接机行
            var newAirProgrammingLine =
                airProgramming.AddNewAirProgrammingLine();
            newAirProgrammingLine.SetAcType(line.AcTypeId);
            newAirProgrammingLine.SetAirProgramming(line.Year,line.BuyNum,line.LeaseNum,line.ExportNum);
            newAirProgrammingLine.SetAircraftCategory(aircraftCategory);

        }

        /// <summary>
        ///     更新规划行
        /// </summary>
        /// <param name="line">规划行DTO</param>
        /// <param name="airProgrammingLine">规划行</param>
        private void UpdateAirProgrammingLine(AirProgrammingLineDTO line, AirProgrammingLine airProgrammingLine)
        {
            //获取
            var aircraftCategory = _aircraftCategoryRepository.Get(line.AircraftCategoryId);

            // 更新订单行
            airProgrammingLine.SetAcType(line.AcTypeId);
            airProgrammingLine.SetAirProgramming(line.Year, line.BuyNum, line.LeaseNum, line.ExportNum);
            airProgrammingLine.SetAircraftCategory(aircraftCategory);
            
        }

        #endregion
        #endregion
    }
}
