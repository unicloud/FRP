#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/20 13:59:51
// 文件名：ControlStrategy
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

namespace UniCloud.Domain.Common.Enums
{
    /// <summary>
    ///     附件控制策略
    /// </summary>
    public enum ControlStrategy
    {
        区间交集 = 0,
        区间并集 = 1,
    }
}
