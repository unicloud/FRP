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

#endregion





namespace UniCloud.Domain.UberModel.Aggregates.AdSbAgg
{
    /// <summary>
    /// AdSb工厂。
    /// </summary>
    public static class AdSbFactory
    {
        /// <summary>
        /// 创建AdSb。
        /// </summary>
        ///  <returns>AdSb</returns>
        public static AdSb CreateAdSb()
        {
            var adSb = new AdSb();
            adSb.GenerateNewIdentity();
            return adSb;
        }

    }
}
