#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：CaacProgrammingAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.CaacProgrammingQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.CaacProgrammingServices
{
    /// <summary>
    ///     实现民航局五年规划服务接口。
    ///     用于处理民航局五年规划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class CaacProgrammingAppService : ICaacProgrammingAppService
    {
        private readonly ICaacProgrammingQuery _caacProgrammingQuery;
        private readonly IAircraftCategoryRepository _aircraftCategoryRepository;
        private readonly ICaacProgrammingRepository _caacProgrammingRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IProgrammingRepository _programmingRepository;

        public CaacProgrammingAppService(ICaacProgrammingQuery caacProgrammingQuery,
            IAircraftCategoryRepository aircraftCategoryRepository,
            ICaacProgrammingRepository caacProgrammingRepository,
            IManagerRepository managerRepository, IProgrammingRepository programmingRepository)
        {
            _caacProgrammingQuery = caacProgrammingQuery;
            _aircraftCategoryRepository = aircraftCategoryRepository;
            _caacProgrammingRepository = caacProgrammingRepository;
            _managerRepository = managerRepository;
            _programmingRepository = programmingRepository;
        }

        #region CaacProgrammingDTO

        /// <summary>
        ///     获取所有民航局五年规划
        /// </summary>
        /// <returns></returns>
        public IQueryable<CaacProgrammingDTO> GetCaacProgrammings()
        {
            var queryBuilder =
                new QueryBuilder<CaacProgramming>();
            return _caacProgrammingQuery.CaacProgrammingDTOQuery(queryBuilder);
        }


        /// <summary>
        ///     新增民航局五年规划。
        /// </summary>
        /// <param name="dto">民航局五年规划DTO。</param>
        [Insert(typeof(CaacProgrammingDTO))]
        public void InsertCaacProgramming(CaacProgrammingDTO dto)
        {
            var issuedUnit = _managerRepository.Get(dto.IssuedUnitId);//获取发文单位
            var programming = _programmingRepository.Get(dto.ProgrammingId);//获取规划期间

            //创建民航局五年规划
            var newCaacProgramming = CaacProgrammingFactory.CreateCaacProgramming();
            newCaacProgramming.SetDocument(dto.DocumentId, dto.DocName);
            newCaacProgramming.SetIssuedDate(dto.IssuedDate);
            newCaacProgramming.SetIssuedUnit(issuedUnit);
            newCaacProgramming.SetName(dto.Name);
            newCaacProgramming.SetNote(dto.Note);
            newCaacProgramming.SetProgramming(programming);
            newCaacProgramming.SetDocNumber(dto.DocNumber);

            //添加规划行
            dto.CaacProgrammingLines.ToList().ForEach(line => InsertCaacProgrammingLine(newCaacProgramming, line));

            _caacProgrammingRepository.Add(newCaacProgramming);
        }

        /// <summary>
        ///     更新民航局五年规划。
        /// </summary>
        /// <param name="dto">民航局五年规划DTO。</param>
        [Update(typeof(CaacProgrammingDTO))]
        public void ModifyCaacProgramming(CaacProgrammingDTO dto)
        {
            var issuedUnit = _managerRepository.Get(dto.IssuedUnitId);//获取发文单位
            var programming = _programmingRepository.Get(dto.ProgrammingId);//获取规划期间

            //获取需要更新的对象
            var updateCaacProgramming = _caacProgrammingRepository.Get(dto.Id);

            if (updateCaacProgramming != null)
            {
                //更新主表：
                updateCaacProgramming.SetDocument(dto.DocumentId, dto.DocName);
                updateCaacProgramming.SetIssuedDate(dto.IssuedDate);
                updateCaacProgramming.SetIssuedUnit(issuedUnit);
                updateCaacProgramming.SetName(dto.Name);
                updateCaacProgramming.SetNote(dto.Note);
                updateCaacProgramming.SetProgramming(programming);
                updateCaacProgramming.SetDocNumber(dto.DocNumber);

                //更新规划行：
                var dtoCaacProgrammingLines = dto.CaacProgrammingLines;
                var caacProgrammingLines = updateCaacProgramming.CaacProgrammingLines;
                DataHelper.DetailHandle(dtoCaacProgrammingLines.ToArray(),
                    caacProgrammingLines.ToArray(),
                    c => c.CaacProgrammingId, p => p.Id,
                    i => InsertCaacProgrammingLine(updateCaacProgramming, i),
                    UpdateCaacProgrammingLine,
                    d => _caacProgrammingRepository.RemoveCaacProgrammingLine(d));
            }
            _caacProgrammingRepository.Modify(updateCaacProgramming);
        }

        /// <summary>
        ///     删除民航局五年规划。
        /// </summary>
        /// <param name="dto">民航局五年规划DTO。</param>
        [Delete(typeof(CaacProgrammingDTO))]
        public void DeleteCaacProgramming(CaacProgrammingDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delCaacProgramming = _caacProgrammingRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delCaacProgramming != null)
            {
                _caacProgrammingRepository.DeleteCaacProgramming(delCaacProgramming); //删除民航局五年规划。
            }
        }


        #region 处理公司五年规划行

        /// <summary>
        ///     插入规划行
        /// </summary>
        /// <param name="caacProgramming">民航局五年规划</param>
        /// <param name="line">规划行DTO</param>
        private void InsertCaacProgrammingLine(CaacProgramming caacProgramming, CaacProgrammingLineDTO line)
        {
            //获取
            var aircraftCategory = _aircraftCategoryRepository.Get(line.AircraftCategoryId);

            // 添加接机行
            var newCaacProgrammingLine =
                caacProgramming.AddNewCaacProgrammingLine();
            newCaacProgrammingLine.SetAircraftCategory(aircraftCategory);
            newCaacProgrammingLine.SetCaacProgramming(line.Year, line.Number);

        }

        /// <summary>
        ///     更新规划行
        /// </summary>
        /// <param name="line">规划行DTO</param>
        /// <param name="caacProgrammingLine">规划行</param>
        private void UpdateCaacProgrammingLine(CaacProgrammingLineDTO line, CaacProgrammingLine caacProgrammingLine)
        {
            //获取
            var aircraftCategory = _aircraftCategoryRepository.Get(line.AircraftCategoryId);

            // 更新订单行
            caacProgrammingLine.SetAircraftCategory(aircraftCategory);
            caacProgrammingLine.SetCaacProgramming(line.Year, line.Number);

        }

        #endregion

        #endregion
    }
}
