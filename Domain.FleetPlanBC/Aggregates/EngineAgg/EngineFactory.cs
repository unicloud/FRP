#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:28:02
// 文件名：EngineFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机工厂
    /// </summary>
    public static class EngineFactory
    {
        /// <summary>
        ///     创建实际发动机
        /// </summary>
        /// <returns>实际发动机</returns>
        public static Engine CreateEngine()
        {
            var engine = new Engine
            {
                CreateDate = DateTime.Now,
            };

            engine.GenerateNewIdentity();
            return engine;
        }
    }
}
