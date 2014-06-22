#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.Portal
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PortalBC.DTO;
using UniCloud.Application.PortalBC.Services;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Portal
{
    /// <summary>
    ///     管理门户模块数据类
    /// </summary>
    public class PortalData : ExposeData.ExposeData
    {
        private readonly IPortalAppService _portalAppService;

        public PortalData()
            : base("UniCloud.Application.PortalBC.DTO")
        {
            _portalAppService = UniContainer.Resolve<IPortalAppService>();
        }

        #region 飞机系列集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<AircraftSeriesDTO> AircraftSeries
        {
            get { return _portalAppService.GetAircraftSeries(); }
        }

        #endregion
    }
}