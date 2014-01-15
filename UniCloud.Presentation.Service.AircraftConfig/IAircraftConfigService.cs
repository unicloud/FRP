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
        ///     所有机型
        /// </summary>
        QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(Action loaded, bool forceLoad = false);

        
        #endregion

       

    }
}
