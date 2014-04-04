#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/7 10:53:23
// 文件名：AircraftSeriesDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/7 10:53:23
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Presentation.Service.AircraftConfig.AircraftConfig
{
    public partial class AircraftSeriesDTO
    {
        partial void OnNameChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("名称不能为空!");
            }
        }
    }
}
