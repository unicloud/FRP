#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/21，15:11
// 文件名：AcTypeAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.AcTypeQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.AcTypeServices
{
    /// <summary>
    ///     实现机型服务接口。
    ///     用于处理机型相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AcTypeAppService : IAcTypeAppService
    {
        private readonly IAcTypeQuery _acTypeQuery;


        public AcTypeAppService(IAcTypeQuery acTypeQuery)
        {
            _acTypeQuery = acTypeQuery;
        }

        public IQueryable<AcTypeDTO> GetAcTypes()
        {
            var query = new QueryBuilder<AircraftType>();
            return _acTypeQuery.AcTypesQuery(query);
        }
    }
}