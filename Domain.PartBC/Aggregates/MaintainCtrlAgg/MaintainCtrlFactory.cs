#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：MaintainCtrlFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl工厂。
    /// </summary>
    public static class MaintainCtrlFactory
    {
        /// <summary>
        ///     创建项维修控制组
        /// </summary>
        /// <param name="item">附件项</param>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="description">维修控制描述</param>
        /// <param name="ctrlDetail">维修控制明细</param>
        /// <param name="maintainWork">维修工作</param>
        /// <returns>项维修控制组</returns>
        public static ItemMaintainCtrl CreateItemMaintainCtrl(Item item, ControlStrategy ctrlStrategy,
            string description, string ctrlDetail, MaintainWork maintainWork)
        {
            var itemMaintainCtrl = new ItemMaintainCtrl();
            itemMaintainCtrl.GenerateNewIdentity();
            itemMaintainCtrl.SetItem(item);
            itemMaintainCtrl.SetCtrlStrategy(ctrlStrategy);
            itemMaintainCtrl.SetDescription(description);
            itemMaintainCtrl.SetMaintainWork(maintainWork);
            itemMaintainCtrl.SetCtrlDetail(ctrlDetail);
            return itemMaintainCtrl;
        }

        /// <summary>
        ///     创建附件维修控制组
        /// </summary>
        /// <param name="pnReg">附件</param>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="description">维修控制描述</param>
        /// <param name="ctrlDetail">维修控制明细</param>
        /// <param name="maintainWork">维修工作</param>
        /// <returns>附件维修控制组</returns>
        public static PnMaintainCtrl CreatePnMaintainCtrl(PnReg pnReg, ControlStrategy ctrlStrategy,
                        string description, string ctrlDetail, MaintainWork maintainWork)
        {
            var pnMaintainCtrl = new PnMaintainCtrl();
            pnMaintainCtrl.GenerateNewIdentity();
            pnMaintainCtrl.SetCtrlStrategy(ctrlStrategy);
            pnMaintainCtrl.SetPnReg(pnReg);
            pnMaintainCtrl.SetCtrlDetail(ctrlDetail);
            pnMaintainCtrl.SetDescription(description);
            pnMaintainCtrl.SetMaintainWork(maintainWork);
            return pnMaintainCtrl;
        }

        /// <summary>
        ///     创建序号件维修控制组
        /// </summary>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="snScope">序号范围</param>
        /// <param name="description">维修控制描述</param>
        /// <param name="ctrlDetail">维修控制明细</param>
        /// <param name="maintainWork">维修工作</param>
        /// <returns>序号件维修控制组</returns>
        public static SnMaintainCtrl CreateSnMaintainCtrl(string snScope, ControlStrategy ctrlStrategy,
                                    string description, string ctrlDetail, MaintainWork maintainWork)
        {
            var snMaintainCtrl = new SnMaintainCtrl();
            snMaintainCtrl.GenerateNewIdentity();
            snMaintainCtrl.SetCtrlStrategy(ctrlStrategy);
            snMaintainCtrl.SetSnScope(snScope);
            snMaintainCtrl.SetCtrlDetail(ctrlDetail);
            snMaintainCtrl.SetDescription(description);
            snMaintainCtrl.SetMaintainWork(maintainWork);
            return snMaintainCtrl;
        }
    }
}