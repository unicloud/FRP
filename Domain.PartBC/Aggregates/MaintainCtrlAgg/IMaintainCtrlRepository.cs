#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:58

// 文件名：IMaintainCtrlRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl仓储接口。
    /// </summary>
    public interface IMaintainCtrlRepository : IRepository<MaintainCtrl>
    {
        /// <summary>
        ///     删除项控制组
        /// </summary>
        /// <param name="itemMaintainCtrl">项控制组</param>
        void DeleteItemMaintainCtrl(ItemMaintainCtrl itemMaintainCtrl);

        /// <summary>
        ///     删除附件控制组
        /// </summary>
        /// <param name="pnMaintainCtrl">附件控制组</param>
        void DeletePnMaintainCtrl(PnMaintainCtrl pnMaintainCtrl);

        /// <summary>
        ///     删除序号控制组
        /// </summary>
        /// <param name="snMaintainCtrl">序号控制组</param>
        void DeleteSnMaintainCtrl(SnMaintainCtrl snMaintainCtrl);


        /// <summary>
        ///     移除维修控制明细
        /// </summary>
        /// <param name="maintainCtrlLine">维修控制明细</param>
        void RemoveMaintainCtrlLine(MaintainCtrlLine maintainCtrlLine);
    }
}