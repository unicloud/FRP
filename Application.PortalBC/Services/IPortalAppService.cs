//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.PortalBC.DTO;

#endregion

namespace UniCloud.Application.PortalBC.Services
{
    /// <summary>
    ///     应用层就采购的操作契约。
    ///     职责是编排操作、检查安全性，缓存，适配实体到DTO等。
    /// </summary>
    public interface IPortalAppService
    {
        /// <summary>
        ///     获取所有飞机系列
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftSeriesDTO> GetAircraftSeries();
    }
}