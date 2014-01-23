#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:52:43
// 文件名：AirlinesFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
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

namespace UniCloud.Domain.UberModel.Aggregates.AirlinesAgg
{
    /// <summary>
    ///     航空公司 工厂
    /// </summary>
    public static class AirlinesFactory
    {
        /// <summary>
        ///     创建航空公司
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cnName">全称</param>
        /// <param name="cnShortName">简称</param>
        /// <param name="iataCode">二字码</param>
        /// <param name="icaoCode">三字码</param>
        /// <param name="isCurrent">是否当前航空公司</param>
        /// <returns></returns>
        public static Airlines CreateAirlines(Guid id,string cnName,string cnShortName,string iataCode,string icaoCode,bool isCurrent)
        {
            var airlines = new Airlines
            {
                CnName = cnName,
                CnShortName = cnShortName,
                IATACode = iataCode,
                ICAOCode = icaoCode,
                IsCurrent = isCurrent,
            };
            airlines.ChangeCurrentIdentity(id);

            return airlines;
        }
    }
}
