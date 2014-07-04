#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:52:52
// 文件名：AnnualFactory
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

namespace UniCloud.Domain.UberModel.Aggregates.AnnualAgg
{
    /// <summary>
    ///     年度工厂
    /// </summary>
    public static class AnnualFactory
    {
        /// <summary>
        ///     创建年度
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="programmingId">规划期间</param>
        /// <param name="year">年份</param>
        /// <param name="isOpen">是否打开</param>
        /// <returns></returns>
        public static Annual CreateAnnual(Guid id, Guid programmingId, int year,bool isOpen)
        {
            var annual = new Annual
            {
                ProgrammingId = programmingId,
                Year = year,
            };
            annual.SetIsOpen(isOpen);
            annual.ChangeCurrentIdentity(id);

            return annual;
        }
    }
}
