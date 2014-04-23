#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 18:04:39
// 文件名：IssuedUnitFactory
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

namespace UniCloud.Domain.UberModel.Aggregates.IssuedUnitAgg
{
    /// <summary>
    ///     发文单位工厂
    /// </summary>
    public static class IssuedUnitFactory
    {
        /// <summary>
        ///     创建发文单位
        /// </summary>
        /// <param name="cnShortName">名称</param>
        /// <param name="isInner">是否内部单位\部门</param>
        /// <returns></returns>
        public static IssuedUnit CreateIssuedUnit(string cnShortName,bool isInner)
        {
            var issuedUnit = new IssuedUnit
            {
               CnName = cnShortName,
               CnShortName = cnShortName,
               IsInner = isInner,
            };
            issuedUnit.GenerateNewIdentity();

            return issuedUnit;
        }
    }
}
