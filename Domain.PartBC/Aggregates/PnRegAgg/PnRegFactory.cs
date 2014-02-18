#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.PnRegAgg
{
    /// <summary>
    /// PnReg工厂。
    /// </summary>
    public static class PnRegFactory
    {
        /// <summary>
        /// 创建PnReg。
        /// </summary>
        ///  <returns>PnReg</returns>
        public static PnReg CreatePnReg()
        {
            var pnReg = new PnReg
            {
            };
            pnReg.GenerateNewIdentity();
            return pnReg;
        }
    }
}
