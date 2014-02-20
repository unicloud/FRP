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
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.TechnicalSolutionQueries;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
#endregion

namespace UniCloud.Application.PartBC.TechnicalSolutionServices
{
    /// <summary>
    /// 实现TechnicalSolution的服务接口。
    ///  用于处理TechnicalSolution相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class TechnicalSolutionAppService : ITechnicalSolutionAppService
    {
        private readonly ITechnicalSolutionQuery _technicalSolutionQuery;

        public TechnicalSolutionAppService(ITechnicalSolutionQuery technicalSolutionQuery)
        {
            _technicalSolutionQuery = technicalSolutionQuery;
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
        }

        /// <summary>
        ///  更新TechnicalSolution。
        /// </summary>
        /// <param name="dto">TechnicalSolutionDTO。</param>
        [Update(typeof(TechnicalSolutionDTO))]
        public void ModifyTechnicalSolution(TechnicalSolutionDTO dto)
        {
        }

        /// <summary>
        ///  删除TechnicalSolution。
        /// </summary>
        /// <param name="dto">TechnicalSolutionDTO。</param>
        [Delete(typeof(TechnicalSolutionDTO))]
        public void DeleteTechnicalSolution(TechnicalSolutionDTO dto)
        {
        }

        #endregion

    }
}
