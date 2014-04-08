#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/01，20:12
// 方案：FRP
// 项目：Domain.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ThrustAgg
{
    /// <summary>
    ///     发动机推力工厂
    /// </summary>
    public static class ThrustFactory
    {
        /// <summary>
        ///     创建发动机推力
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static Thrust CreateThrust(string name, string description)
        {
            var thrust = new Thrust
            {
                Name = name,
                Description = description
            };
            thrust.GenerateNewIdentity();

            return thrust;
        }
    }
}