#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 14:21:57
// 文件名：AircraftConfigService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 14:21:57
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;

namespace UniCloud.Presentation.Service.AircraftConfig
{
    [Export(typeof(IAircraftConfigService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftConfigService : ServiceBase, IAircraftConfigService
    {
        public AircraftConfigService()
        {
            context = new AircraftConfigData(AgentHelper.AircraftConfigUri);
        }

        #region IAircraftConfigService 成员

        public AircraftConfigData Context
        {
            get { return context as AircraftConfigData; }
        }

        #region 获取静态数据
        /// <summary>
        ///     所有制造商
        /// </summary>
        public QueryableDataServiceCollectionView<ManufacturerDTO> GetManufacturers(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Manufacturers, loaded, forceLoad);
        }

        /// <summary>
        ///     所有座级
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> GetAircraftCategories(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftCategories, loaded, forceLoad);
        }

        /// <summary>
        ///     所有民航机型
        /// </summary>
        public QueryableDataServiceCollectionView<CAACAircraftTypeDTO> GetCAACAircraftTypes(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.CAACAircraftTypes, loaded, forceLoad);
        }
        #endregion

        #endregion
    }
}
