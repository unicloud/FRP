#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：MaintainWorkAppService
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
using UniCloud.Application.PartBC.Query.MaintainWorkQueries;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
#endregion

namespace UniCloud.Application.PartBC.MaintainWorkServices
{
    /// <summary>
    /// 实现MaintainWork的服务接口。
    ///  用于处理MaintainWork相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MaintainWorkAppService : IMaintainWorkAppService
    {
        private readonly IMaintainWorkQuery _maintainWorkQuery;

        public MaintainWorkAppService(IMaintainWorkQuery maintainWorkQuery)
        {
            _maintainWorkQuery = maintainWorkQuery;
        }

        #region MaintainWorkDTO

        /// <summary>
        /// 获取所有MaintainWork。
        /// </summary>
        public IQueryable<MaintainWorkDTO> GetMaintainWorks()
        {
            var queryBuilder =
               new QueryBuilder<MaintainWork>();
            return _maintainWorkQuery.MaintainWorkDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Insert(typeof(MaintainWorkDTO))]
        public void InsertMaintainWork(MaintainWorkDTO dto)
        {
        }

        /// <summary>
        ///  更新MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Update(typeof(MaintainWorkDTO))]
        public void ModifyMaintainWork(MaintainWorkDTO dto)
        {
        }

        /// <summary>
        ///  删除MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Delete(typeof(MaintainWorkDTO))]
        public void DeleteMaintainWork(MaintainWorkDTO dto)
        {
        }

        #endregion

    }
}