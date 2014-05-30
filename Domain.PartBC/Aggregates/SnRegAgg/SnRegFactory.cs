#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegFactory
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
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    ///     SnReg工厂
    /// </summary>
    public static class SnRegFactory
    {
        #region 创建

        /// <summary>
        ///     创建序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <returns>序号件</returns>
        public static SnReg CreateSnReg(
            DateTime installDate,
            PnReg pnReg,
            string sn)
        {
            var snReg = new SnReg
            {
                InstallDate = installDate,
                Sn = sn,
            };
            snReg.GenerateNewIdentity();
            snReg.SetPnReg(pnReg);
            snReg.CreateDate = DateTime.Now;
            snReg.UpdateDate = DateTime.Now;

            return snReg;
        }

        /// <summary>
        ///     创建发动机序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="thrust"></param>
        /// <param name="sn">序号</param>
        /// <returns>发动机序号件</returns>
        public static EngineReg CreateEngineReg(
            DateTime installDate,
            PnReg pnReg,
            Thrust thrust,
            string sn)
        {
            var engineReg = new EngineReg
            {
                InstallDate = installDate,
                Sn = sn,
            };
            engineReg.GenerateNewIdentity();
            engineReg.SetPnReg(pnReg);
            engineReg.SetThrust(thrust);
            engineReg.SetSnStatus(SnStatus.装机);
            engineReg.SetIsLife(false, false, 0);
            engineReg.SetMonitorStatus((OilMonitorStatus.正常));
            engineReg.CreateDate = DateTime.Now;
            engineReg.UpdateDate = DateTime.Now;

            return engineReg;
        }

        /// <summary>
        ///     创建APU序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <returns>APU序号件</returns>
        public static APUReg CreateAPUReg(
            DateTime installDate,
            PnReg pnReg,
            string sn)
        {
            var apuReg = new APUReg
            {
                InstallDate = installDate,
                Sn = sn,
            };
            apuReg.GenerateNewIdentity();
            apuReg.SetPnReg(pnReg);
            apuReg.SetSnStatus(SnStatus.装机);
            apuReg.SetIsLife(false, false, 0);
            apuReg.SetMonitorStatus((OilMonitorStatus.正常));
            apuReg.CreateDate = DateTime.Now;
            apuReg.UpdateDate = DateTime.Now;

            return apuReg;
        }

        #endregion

        #region 更新

        /// <summary>
        ///     更新序号件
        /// </summary>
        /// <param name="updateSnReg">需要更新的序号件</param>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <returns>序号件</returns>
        public static SnReg UpdateSnReg(
            SnReg updateSnReg,
            DateTime installDate,
            PnReg pnReg,
            string sn)
        {
            updateSnReg.InstallDate = installDate;
            updateSnReg.Sn = sn;

            updateSnReg.SetPnReg(pnReg);
            updateSnReg.UpdateDate = DateTime.Now;

            return updateSnReg;
        }

        /// <summary>
        ///     更新发动机序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="thrust"></param>
        /// <param name="sn">序号</param>
        /// <returns>发动机序号件</returns>
        public static EngineReg UpdateEngineReg(
            DateTime installDate,
            PnReg pnReg,
            Thrust thrust,
            string sn)
        {
            var engineReg = new EngineReg
            {
                InstallDate = installDate,
                Sn = sn,
            };
            engineReg.GenerateNewIdentity();
            engineReg.SetPnReg(pnReg);
            engineReg.SetThrust(thrust);
            engineReg.UpdateDate = DateTime.Now;

            return engineReg;
        }

        /// <summary>
        ///     更新APU序号件
        /// </summary>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <returns>APU序号件</returns>
        public static APUReg UpdateAPUReg(
            DateTime installDate,
            PnReg pnReg,
            string sn)
        {
            var apuReg = new APUReg
            {
                InstallDate = installDate,
                Sn = sn,
            };
            apuReg.GenerateNewIdentity();
            apuReg.SetPnReg(pnReg);
            apuReg.UpdateDate = DateTime.Now;

            return apuReg;
        }

        #endregion

    }
}