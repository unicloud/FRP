#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/13，14:03
// 文件名：ProgrammingFileAppService.cs
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
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingFileQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ProgrammingFileServices
{
    /// <summary>
    ///     实现规划文档服务接口。
    ///     用于处理规划文档相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ProgrammingFileAppService : ContextBoundObject, IProgrammingFileAppService
    {
        private readonly IProgrammingFileQuery _programmingFileQuery;
        private readonly IIssuedUnitRepository _issuedUnitRepository;
        private readonly IProgrammingRepository _programmingRepository;
        private readonly IProgrammingFileRepository _programmingFileRepository;
        public ProgrammingFileAppService(IProgrammingFileQuery programmingFileQuery,
            IIssuedUnitRepository issuedUnitRepository, IProgrammingRepository programmingRepository,
            IProgrammingFileRepository programmingFileRepository)
        {
            _programmingFileQuery = programmingFileQuery;
            _issuedUnitRepository = issuedUnitRepository;
            _programmingRepository = programmingRepository;
            _programmingFileRepository = programmingFileRepository;
        }

        #region ProgrammingFileDTO

        /// <summary>
        ///     获取所有规划文档
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProgrammingFileDTO> GetProgrammingFiles()
        {
            var queryBuilder =
                new QueryBuilder<ProgrammingFile>();
            return _programmingFileQuery.ProgrammingFileDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增规划文档。
        /// </summary>
        /// <param name="dto">规划文档DTO。</param>
        [Insert(typeof(ProgrammingFileDTO))]
        public void InsertProgrammingFile(ProgrammingFileDTO dto)
        {
            //获取发文单位
            var issuedUnit = _issuedUnitRepository.Get(dto.IssuedUnitId);
            var programming = _programmingRepository.Get(dto.ProgrammingId);

            //创建规划文档
            var newProgrammingFile = ProgrammingFileFactory.CreateProgrammingFile(issuedUnit,dto.IssuedDate,dto.DocNumber,
                dto.DocumentId,dto.DocName,programming,dto.Type);

            _programmingFileRepository.Add(newProgrammingFile);
        }

        /// <summary>
        ///     更新规划文档。
        /// </summary>
        /// <param name="dto">规划文档DTO。</param>
        [Update(typeof(ProgrammingFileDTO))]
        public void ModifyProgrammingFile(ProgrammingFileDTO dto)
        {
            //获取供应商
            var issuedUnit = _issuedUnitRepository.Get(dto.IssuedUnitId);
            var programming = _programmingRepository.Get(dto.ProgrammingId);

            //获取需要更新的对象
            var updateProgrammingFile = _programmingFileRepository.Get(dto.Id);

            if (updateProgrammingFile != null)
            {
                //更新主表：
                updateProgrammingFile.SetDocNumber(dto.DocNumber);
                updateProgrammingFile.SetDocument(dto.DocumentId,dto.DocName);
                updateProgrammingFile.SetIssuedDate(dto.IssuedDate);
                updateProgrammingFile.SetIssuedUnit(issuedUnit);
                updateProgrammingFile.SetProgramming(programming);
            }
            _programmingFileRepository.Modify(updateProgrammingFile);
        }

        /// <summary>
        ///     删除规划文档。
        /// </summary>
        /// <param name="dto">规划文档DTO。</param>
        [Delete(typeof(ProgrammingFileDTO))]
        public void DeleteProgrammingFile(ProgrammingFileDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delProgrammingFile = _programmingFileRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delProgrammingFile != null)
            {
                _programmingFileRepository.Remove(delProgrammingFile); //删除规划文档。
            }
        }
        #endregion
    }
}
