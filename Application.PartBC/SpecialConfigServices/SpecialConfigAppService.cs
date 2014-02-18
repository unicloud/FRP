#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigAppService
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
using UniCloud.Application.PartBC.Query.SpecialConfigQueries;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
#endregion

namespace UniCloud.Application.PartBC.SpecialConfigServices
{
    /// <summary>
    /// 实现SpecialConfig的服务接口。
    ///  用于处理SpecialConfig相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class SpecialConfigAppService : ISpecialConfigAppService
    {
        private readonly ISpecialConfigQuery _specialConfigQuery;

        public SpecialConfigAppService(ISpecialConfigQuery specialConfigQuery)
        {
            _specialConfigQuery = specialConfigQuery;
        }

        #region SpecialConfigDTO

        /// <summary>
        /// 获取所有SpecialConfig。
        /// </summary>
        public IQueryable<SpecialConfigDTO> GetSpecialConfigs()
        {
            var queryBuilder =
               new QueryBuilder<SpecialConfig>();
            return _specialConfigQuery.SpecialConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Insert(typeof(SpecialConfigDTO))]
        public void InsertSpecialConfig(SpecialConfigDTO dto)
        {
        }

        /// <summary>
        ///  更新SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Update(typeof(SpecialConfigDTO))]
        public void ModifySpecialConfig(SpecialConfigDTO dto)
        {
        }

        /// <summary>
        ///  删除SpecialConfig。
        /// </summary>
        /// <param name="dto">SpecialConfigDTO。</param>
        [Delete(typeof(SpecialConfigDTO))]
        public void DeleteSpecialConfig(SpecialConfigDTO dto)
        {
        }

        #endregion

    }
}
