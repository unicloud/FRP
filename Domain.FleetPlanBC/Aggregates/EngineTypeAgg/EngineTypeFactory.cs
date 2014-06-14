#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/13 22:10:19
// 文件名：EngineTypeFactory
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/13 22:10:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg
{
    /// <summary>
    ///     发动机型号工厂
    /// </summary>
    public static class EngineTypeFactory
    {
        /// <summary>
        ///     创建发动机型号
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="manufacturerId">制造商</param>
        /// <returns>备发计划</returns>
        public static EngineType CreateEngineType(string name,Guid manufacturerId)
        {
            var engineType = new EngineType
            {
                ManufacturerId=manufacturerId,
                Name=name,
            };

            engineType.GenerateNewIdentity();
            return engineType;
        }
    }
}
