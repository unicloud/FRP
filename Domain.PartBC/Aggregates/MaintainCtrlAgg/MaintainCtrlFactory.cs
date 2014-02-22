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
    }
}
