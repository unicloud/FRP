#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IScnAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
#endregion

namespace UniCloud.Application.PartBC.ScnServices
{
    /// <summary>
    /// Scn的服务接口。
    /// </summary>
    public interface IScnAppService
    {
        /// <summary>
        /// 获取所有Scn。
        /// </summary>
        IQueryable<ScnDTO> GetScns();
    }
}
