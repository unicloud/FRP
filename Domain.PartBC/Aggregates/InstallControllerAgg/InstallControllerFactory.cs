#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 20:17:20
// 文件名：InstallControllerFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg
{
    /// <summary>
    ///     InstallController工厂。
    /// </summary>
    public static class InstallControllerFactory
    {
        /// <summary>
        ///     创建装机控制。
        /// </summary>
        /// <returns>InstallController</returns>
        public static InstallController CreateInstallController()
        {
            var installController = new InstallController();
            installController.GenerateNewIdentity();
            return installController;
        }

        /// <summary>
        /// 创建装机控制
        /// </summary>
        /// <param name="startDate">启用日期</param>
        /// <param name="endDate">失效日期</param>
        /// <param name="item">附件项</param>
        /// <param name="pnReg">可互换附件</param>
        /// <param name="aircraftType">机型</param>
        /// <returns></returns>
        public static InstallController CreateInstallController(DateTime startDate, DateTime? endDate, Item item, PnReg pnReg, AircraftType aircraftType)
        {
            var installController = new InstallController();
            installController.GenerateNewIdentity();
            installController.SetAircraftType(aircraftType);
            installController.SetItem(item);
            installController.SetPnReg(pnReg);
            installController.SetStartDate(startDate);
            installController.SetEndDate(endDate);
            return installController;
        }
    }
}
