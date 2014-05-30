#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 21:54:48
// 文件名：ActionType
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
    /// 拆换类型
    /// </summary>
    public enum ActionType
    {
        装上 = 0,
        拆下 = 1,
        拆换 = 2,
        不拆换 = 3,
    }
}
