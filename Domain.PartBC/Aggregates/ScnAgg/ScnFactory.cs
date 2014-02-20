#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ScnAgg
{
    /// <summary>
    /// Scn工厂。
    /// </summary>
    public static class ScnFactory
    {
        /// <summary>
        /// 创建Scn。
        /// </summary>
        ///  <returns>Scn</returns>
        public static Scn CreateScn()
        {
            var scn = new Scn
            {
            };
            scn.GenerateNewIdentity();
            return scn;
        }
    }
}
