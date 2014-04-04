#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 9:53:24
// 文件名：ProgrammingFactory
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

namespace UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg
{
    /// <summary>
    ///     规划期间工厂
    /// </summary>
    public static class ProgrammingFactory
    {
        /// <summary>
        ///     创建规划期间
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static Programming CreateProgramming(Guid id, string name,DateTime startDate,DateTime endDate)
        {
            var programming = new Programming
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
            };
            programming.ChangeCurrentIdentity(id);

            return programming;
        }
    }
}
