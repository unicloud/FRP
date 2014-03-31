#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：IMaintainCtrlQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.MaintainCtrlQueries
{
    /// <summary>
    /// MaintainCtrl查询接口
    /// </summary>
    public interface IMaintainCtrlQuery
    {
        /// <summary>
        /// ItemMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ItemMaintainCtrlDTO集合</returns>
        IQueryable<ItemMaintainCtrlDTO> ItemMaintainCtrlDTOQuery(QueryBuilder<ItemMaintainCtrl> query);

        /// <summary>
        /// PnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>PnMaintainCtrlDTO集合</returns>
        IQueryable<PnMaintainCtrlDTO> PnMaintainCtrlDTOQuery(QueryBuilder<PnMaintainCtrl> query);

        /// <summary>
        /// SnMaintainCtrl查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>SnMaintainCtrlDTO集合</returns>
        IQueryable<SnMaintainCtrlDTO> SnMaintainCtrlDTOQuery(QueryBuilder<SnMaintainCtrl> query);

    }
}
