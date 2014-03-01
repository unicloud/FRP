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
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    /// SnReg工厂。
    /// </summary>
    public static class SnRegFactory
    {
        /// <summary>
        /// 创建SnReg。
        /// </summary>
        ///  <returns>SnReg</returns>
        public static SnReg CreateSnReg()
        {
            var snReg = new SnReg
            {
            };
            snReg.GenerateNewIdentity();
            return snReg;
        }

        /// <summary>
        /// 创建序号件
        /// </summary>
        /// <param name="aircraft">运营飞机</param>
        /// <param name="installDate">初始安装日期</param>
        /// <param name="isStop">是否停用</param>
        /// <param name="pnReg">附件</param>
        /// <param name="sn">序号</param>
        /// <returns></returns>
        public static SnReg CreateSnReg(Aircraft aircraft,DateTime installDate,bool isStop,
            PnReg pnReg,string sn)
        {
            var snReg = new SnReg
            {
            };
            snReg.GenerateNewIdentity();
            snReg.SetAircraft(aircraft);
            snReg.SetInstallDate(installDate);
            snReg.SetIsStop(isStop);
            snReg.SetPnReg(pnReg);
            snReg.SetSn(sn);
            return snReg;
        }
    }
}
