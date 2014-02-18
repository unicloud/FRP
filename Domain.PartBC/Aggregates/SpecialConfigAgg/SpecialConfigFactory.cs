#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg
{
    /// <summary>
    /// SpecialConfig工厂。
    /// </summary>
    public static class SpecialConfigFactory
    {
        /// <summary>
        /// 创建SpecialConfig。
        /// </summary>
        ///  <returns>SpecialConfig</returns>
        public static SpecialConfig CreateSpecialConfig()
        {
            var specialConfig = new SpecialConfig
            {
            };
            specialConfig.GenerateNewIdentity();
            return specialConfig;
        }
    }
}
