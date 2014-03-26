#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TechnicalSolutionAppService
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
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.TechnicalSolutionQueries;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
#endregion

namespace UniCloud.Application.PartBC.TechnicalSolutionServices
{
    /// <summary>
    /// 实现TechnicalSolution的服务接口。
    ///  用于处理TechnicalSolution相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class TechnicalSolutionAppService : ContextBoundObject, ITechnicalSolutionAppService
    {
        private readonly ITechnicalSolutionQuery _technicalSolutionQuery;
        private readonly ITechnicalSolutionRepository _technicalSolutionRepository;
        private readonly IPnRegRepository _pnRegRepository;

        public TechnicalSolutionAppService(ITechnicalSolutionQuery technicalSolutionQuery,
            ITechnicalSolutionRepository technicalSolutionRepository,
            IPnRegRepository pnRegRepository)
        {
            _technicalSolutionQuery = technicalSolutionQuery;
            _technicalSolutionRepository = technicalSolutionRepository;
            _pnRegRepository = pnRegRepository;
        }

        #region TechnicalSolutionDTO

        /// <summary>
        /// 获取所有TechnicalSolution。
        /// </summary>
        public IQueryable<TechnicalSolutionDTO> GetTechnicalSolutions()
        {
            var queryBuilder =
               new QueryBuilder<TechnicalSolution>();
            return _technicalSolutionQuery.TechnicalSolutionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增TechnicalSolution。
        /// </summary>
        /// <param name="dto">TechnicalSolutionDTO。</param>
        [Insert(typeof(TechnicalSolutionDTO))]
        public void InsertTechnicalSolution(TechnicalSolutionDTO dto)
        {
            //创建技术解决方案
            var newTechnicalSolution = TechnicalSolutionFactory.CreateTechnicalSolution();
            newTechnicalSolution.SetFiNumber(dto.FiNumber);
            newTechnicalSolution.SetPosition(dto.Position);
            newTechnicalSolution.SetTsNumber(dto.TsNumber);

            //添加解决方案明细
            dto.TsLines.ToList().ForEach(tsLine => InsertTsLine(newTechnicalSolution, tsLine));

            _technicalSolutionRepository.Add(newTechnicalSolution);
        }

        /// <summary>
        ///  更新TechnicalSolution。
        /// </summary>
        /// <param name="dto">TechnicalSolutionDTO。</param>
        [Update(typeof(TechnicalSolutionDTO))]
        public void ModifyTechnicalSolution(TechnicalSolutionDTO dto)
        {
            //获取需要更新的对象
            var updateTechnicalSolution = _technicalSolutionRepository.Get(dto.Id);

            if (updateTechnicalSolution != null)
            {
                //更新主表：
                updateTechnicalSolution.SetFiNumber(dto.FiNumber);
                updateTechnicalSolution.SetPosition(dto.Position);
                updateTechnicalSolution.SetTsNumber(dto.TsNumber);

                //更新解决方案明细：
                var dtoTsLines = dto.TsLines;
                var tsLines = updateTechnicalSolution.TsLines;
                DataHelper.DetailHandle(dtoTsLines.ToArray(),
                    tsLines.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertTsLine(updateTechnicalSolution, i),
                    UpdateTsLine,
                    d => _technicalSolutionRepository.DeleteTsLine(d));
            }
            _technicalSolutionRepository.Modify(updateTechnicalSolution);

        }

        /// <summary>
        ///  删除TechnicalSolution。
        /// </summary>
        /// <param name="dto">TechnicalSolutionDTO。</param>
        [Delete(typeof(TechnicalSolutionDTO))]
        public void DeleteTechnicalSolution(TechnicalSolutionDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delTechnicalSolution = _technicalSolutionRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delTechnicalSolution != null)
            {
                _technicalSolutionRepository.DeleteTechnicalSolution(delTechnicalSolution); //删除技术解决方案。
            }
        }


        #region 处理技术解决方案明细

        /// <summary>
        ///     插入技术解决方案明细
        /// </summary>
        /// <param name="ts">技术解决方案</param>
        /// <param name="tsLineDto">技术解决方案明细DTO</param>
        private void InsertTsLine(TechnicalSolution ts, TsLineDTO tsLineDto)
        {
            // 添加技术解决方案明细
            var newTsLine = ts.AddNewTsLine();
            newTsLine.SetDescription(tsLineDto.Description);
            newTsLine.SetPn(tsLineDto.Pn);
            newTsLine.SetTsNumber(tsLineDto.TsNumber);

            //添加依赖项
            tsLineDto.Dependencies.ToList().ForEach(dependency => InsertDependency(newTsLine, dependency));
        }

        /// <summary>
        ///     更新技术解决方案明细
        /// </summary>
        /// <param name="tsLineDto">技术解决方案明细DTO</param>
        /// <param name="tsLine">技术解决方案明细</param>
        private void UpdateTsLine(TsLineDTO tsLineDto, TsLine tsLine)
        {
            // 更新技术解决方案明细
            tsLine.SetDescription(tsLineDto.Description);
            tsLine.SetPn(tsLineDto.Pn);
            tsLine.SetTsNumber(tsLineDto.TsNumber);

            //更新依赖项:
            var dtoDependencies = tsLineDto.Dependencies;
            var dependencies = tsLine.Dependencies;
            DataHelper.DetailHandle(dtoDependencies.ToArray(),
                dependencies.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertDependency(tsLine, i),
                UpdateDependency,
                d => _technicalSolutionRepository.RemoveDependency(d));
        }

        #endregion


        #region 处理到寿监控

        /// <summary>
        ///     插入到寿监控
        /// </summary>
        /// <param name="tsLine">序号件</param>
        /// <param name="dependencyDto">到寿监控DTO</param>
        private void InsertDependency(TsLine tsLine, DependencyDTO dependencyDto)
        {
            //获取
            var pnReg = _pnRegRepository.Get(dependencyDto.PnRegId);

            // 添加到寿监控
            var newDependency = tsLine.AddNewDependency();
            newDependency.SetPnReg(pnReg);
        }

        /// <summary>
        ///     更新到寿监控
        /// </summary>
        /// <param name="dependencyDto">到寿监控DTO</param>
        /// <param name="dependency">到寿监控</param>
        private void UpdateDependency(DependencyDTO dependencyDto, Dependency dependency)
        {
            //获取
            var pnReg = _pnRegRepository.Get(dependencyDto.PnRegId);

            // 添加到寿监控
            dependency.SetPnReg(pnReg);
        }

        #endregion

        #endregion

    }
}
