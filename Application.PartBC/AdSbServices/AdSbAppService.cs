#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AdSbAppService
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
using UniCloud.Application.PartBC.Query.AdSbQueries;
using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;
#endregion

namespace UniCloud.Application.PartBC.AdSbServices
{
    /// <summary>
    /// 实现AdSb的服务接口。
    ///  用于处理AdSb相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AdSbAppService : IAdSbAppService
    {
        private readonly IAdSbQuery _adSbQuery;

        public AdSbAppService(IAdSbQuery adSbQuery)
        {
            _adSbQuery = adSbQuery;
        }

        #region AdSbDTO

        /// <summary>
        /// 获取所有AdSb。
        /// </summary>
        public IQueryable<AdSbDTO> GetAdSbs()
        {
            var queryBuilder =
               new QueryBuilder<AdSb>();
            return _adSbQuery.AdSbDTOQuery(queryBuilder);
        }

        #endregion

    }
}
