#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：CtrlUnitFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using System.Security.Cryptography;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg
{
    /// <summary>
    /// CtrlUnit工厂。
    /// </summary>
    public static class CtrlUnitFactory
    {
        /// <summary>
        /// 创建CtrlUnit。
        /// </summary>
        ///  <returns>CtrlUnit</returns>
        public static CtrlUnit CreateCtrlUnit()
        {
            var ctrlUnit = new CtrlUnit
            {
            };
            ctrlUnit.GenerateNewIdentity();
            return ctrlUnit;
        }
    }
}
