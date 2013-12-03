#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：IMaterialAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.MaterialServices
{
    /// <summary>
    ///     表示用于处理物料相关信息服务接口。
    /// </summary>
    public interface IMaterialAppService
    {
        #region 飞机

        /// <summary>
        ///     飞机物料查询。
        /// </summary>
        /// <returns>飞机物料DTO集合</returns>
        IQueryable<AircraftMaterialDTO> GetAircraftMaterials(
           );
        #endregion

        #region BFE

        /// <summary>
        ///     BFE物料查询。
        /// </summary>
        /// <returns>BFE物料DTO集合</returns>
        IQueryable<BFEMaterialDTO> GetBFEMaterials(
            );
        #endregion

        #region 发动机

        /// <summary>
        ///     发动机物料查询。
        /// </summary>
        /// <returns>发动机DTO集合</returns>
        IQueryable<EngineMaterialDTO> GetEngineMaterials(
          );
        #endregion
    }
}