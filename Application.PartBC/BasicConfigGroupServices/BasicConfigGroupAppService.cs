#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupAppService
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
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
#endregion

namespace UniCloud.Application.PartBC.BasicConfigGroupServices
{
    /// <summary>
    /// 实现BasicConfigGroup的服务接口。
    ///  用于处理BasicConfigGroup相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class BasicConfigGroupAppService : IBasicConfigGroupAppService
    {
        private readonly IBasicConfigGroupQuery _basicConfigGroupQuery;

        public BasicConfigGroupAppService(IBasicConfigGroupQuery basicConfigGroupQuery)
        {
            _basicConfigGroupQuery = basicConfigGroupQuery;
        }

        #region BasicConfigGroupDTO

        /// <summary>
        /// 获取所有BasicConfigGroup。
        /// </summary>
        public IQueryable<BasicConfigGroupDTO> GetBasicConfigGroups()
        {
            var queryBuilder =
               new QueryBuilder<BasicConfigGroup>();
            return _basicConfigGroupQuery.BasicConfigGroupDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Insert(typeof(BasicConfigGroupDTO))]
        public void InsertBasicConfigGroup(BasicConfigGroupDTO dto)
        {
        }

        /// <summary>
        ///  更新BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Update(typeof(BasicConfigGroupDTO))]
        public void ModifyBasicConfigGroup(BasicConfigGroupDTO dto)
        {
        }

        /// <summary>
        ///  删除BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Delete(typeof(BasicConfigGroupDTO))]
        public void DeleteBasicConfigGroup(BasicConfigGroupDTO dto)
        {
        }

        #endregion

    }
}
