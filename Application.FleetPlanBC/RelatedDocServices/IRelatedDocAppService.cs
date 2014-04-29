#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 22:03:33
// 文件名：IRelatedDocAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.RelatedDocServices
{
    /// <summary>
    ///     关联文档服务接口
    /// </summary>
    public interface IRelatedDocAppService
    {
        /// <summary>
        ///     获取所有RelatedDocDTO
        /// </summary>
        /// <returns></returns>
        IQueryable<RelatedDocDTO> GetRelatedDocs();
    }
}