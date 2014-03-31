#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ModFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ModAgg
{
    /// <summary>
    /// Mod工厂。
    /// </summary>
    public static class ModFactory
    {
        /// <summary>
        /// 创建Mod。
        /// </summary>
        ///  <returns>Mod</returns>
        public static Mod CreateMod()
        {
            var mod = new Mod
            {
            };
            mod.GenerateNewIdentity();
            return mod;
        }

        /// <summary>
        /// 创建Mod号
        /// </summary>
        /// <param name="modNumber">Mod号</param>
        /// <returns></returns>
        public static Mod CreateMod(string modNumber)
        {
            var mod = new Mod
            {
            };
            mod.GenerateNewIdentity();
            mod.SetModNumber(modNumber);
            return mod;
        }
    }
}
