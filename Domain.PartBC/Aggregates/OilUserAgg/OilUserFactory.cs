#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/28，9:14
// 方案：FRP
// 项目：Domain.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.OilUserAgg
{
    /// <summary>
    ///     滑油用户工厂
    /// </summary>
    public static class OilUserFactory
    {
        /// <summary>
        ///     创建发动机滑油用户
        /// </summary>
        /// <param name="snReg">序号件对象</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>发动机滑油用户</returns>
        public static EngineOil CreateEngineOil(
            SnReg snReg,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var engineOil = new EngineOil
            {
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            engineOil.SetSnReg(snReg);

            return engineOil;
        }

        /// <summary>
        ///     创建APU滑油用户
        /// </summary>
        /// <param name="snReg">序号件对象</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="csn">CSN</param>
        /// <param name="csr">CSR</param>
        /// <returns>发动机滑油用户</returns>
        public static APUOil CreateAPUOil(
            SnReg snReg,
            decimal tsn,
            decimal tsr,
            decimal csn,
            decimal csr)
        {
            var apuOil = new APUOil
            {
                TSN = tsn,
                TSR = tsr,
                CSN = csn,
                CSR = csr
            };
            apuOil.SetSnReg(snReg);

            return apuOil;
        }
    }
}