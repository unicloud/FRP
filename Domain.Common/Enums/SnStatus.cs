#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/3 11:26:02
// 文件名：SnStatus
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
    ///     序号件状态
    /// </summary>
    public enum SnStatus
    {
        NotAgree = 0,
        Stored = 1,
        InTransfer = 3,
        FlightKit = 4,
        Lent = 5,
        InLocalRepair = 11,
        InOutsideRepair = 12,
        InRepairOnOtherSite = 13,
        ToolsWithPersonalInventoryDocument = 20,
        WorkshopOk = 21,
        InStoreUorS = 22,
        WorkshopUorS = 23,
        Unknown = 25,
        TemporaryScrap = 31,
        Scrapped = 32,
        NewReference = 36,
        OnAircraft = 41,
        NotFollowed = 42,
        ToBeRepaired = 46,
    }
}
