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
using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    /// MaintainCtrl工厂。
    /// </summary>
    public static class MaintainCtrlFactory
    {
        /// <summary>
        /// 创建ItemMaintainCtrl。
        /// </summary>
        ///  <returns>ItemMaintainCtrl</returns>
        public static ItemMaintainCtrl CreateItemMaintainCtrl()
        {
            var itemMaintainCtrl = new ItemMaintainCtrl
            {
            };
            itemMaintainCtrl.GenerateNewIdentity();
            return itemMaintainCtrl;
        }

        /// <summary>
        /// 创建项维修控制组
        /// </summary>
        /// <param name="acConfigId">构型外键</param>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="itemNo">项号</param>
        /// <returns></returns>
        public static ItemMaintainCtrl CreateItemMaintainCtrl(int acConfigId,int ctrlStrategy,string itemNo)
        {
            var itemMaintainCtrl = new ItemMaintainCtrl
            {
            };
            itemMaintainCtrl.GenerateNewIdentity();
            itemMaintainCtrl.SetAcConfig(acConfigId);
            itemMaintainCtrl.SetCtrlStrategy((ControlStrategy)ctrlStrategy);
            itemMaintainCtrl.SetItemNo(itemNo);
            return itemMaintainCtrl;
        }

        /// <summary>
        /// 创建PnMaintainCtrl。
        /// </summary>
        ///  <returns>PnMaintainCtrl</returns>
        public static PnMaintainCtrl CreatePnMaintainCtrl()
        {
            var pnMaintainCtrl = new PnMaintainCtrl
            {
            };
            pnMaintainCtrl.GenerateNewIdentity();
            return pnMaintainCtrl;
        }

        /// <summary>
        /// 创建附件维修控制组
        /// </summary>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="pnReg">附件</param>
        /// <returns></returns>
        public static PnMaintainCtrl CreatePnMaintainCtrl(int ctrlStrategy, PnReg pnReg)
        {
            var pnMaintainCtrl = new PnMaintainCtrl
            {
            };
            pnMaintainCtrl.GenerateNewIdentity();
            pnMaintainCtrl.SetCtrlStrategy((ControlStrategy)ctrlStrategy);
            pnMaintainCtrl.SetPnReg(pnReg);
            return pnMaintainCtrl;
        }

        /// <summary>
        /// 创建SnMaintainCtrl。
        /// </summary>
        ///  <returns>SnMaintainCtrl</returns>
        public static SnMaintainCtrl CreateSnMaintainCtrl()
        {
            var snMaintainCtrl = new SnMaintainCtrl
            {
            };
            snMaintainCtrl.GenerateNewIdentity();
            return snMaintainCtrl;
        }

        /// <summary>
        /// 创建序号件维修控制组
        /// </summary>
        /// <param name="ctrlStrategy">控制策略</param>
        /// <param name="snScope">序号范围</param>
        /// <returns></returns>
        public static SnMaintainCtrl CreateSnMaintainCtrl(int ctrlStrategy,string snScope)
        {
            var snMaintainCtrl = new SnMaintainCtrl
            {
            };
            snMaintainCtrl.GenerateNewIdentity();
            snMaintainCtrl.SetCtrlStrategy((ControlStrategy)ctrlStrategy);
            snMaintainCtrl.SetSnScope(snScope);
            return snMaintainCtrl;
        }
    }
}
