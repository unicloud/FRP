#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 14:21:47
// 文件名：IAircraftConfigService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 14:21:47
// 修改说明：
// ========================================================================*/
#endregion
using System;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

namespace UniCloud.Presentation.Service.AircraftConfig
{
    public interface IAircraftConfigService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        AircraftConfigData Context { get; }

        #region 获取静态数据

        /// <summary>
        ///     所有制造商
        /// </summary>
        QueryableDataServiceCollectionView<ManufacturerDTO> GetManufacturers(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     所有座级
        /// </summary>
        QueryableDataServiceCollectionView<AircraftCategoryDTO> GetAircraftCategories(Action loaded, bool forceLoad = false);
        #endregion


    }
}
