#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 16:15:23
// 文件名：IThresholdAppService
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 16:15:23
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.ThresholdServices
{
    /// <summary>
    /// Threshold的服务接口。
    /// </summary>
    public interface IThresholdAppService
    {
        /// <summary>
        /// 获取所有Threshold。
        /// </summary>
        IQueryable<ThresholdDTO> GetThresholds();
    }
}
