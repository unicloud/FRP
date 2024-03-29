#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：IMaintainCtrlAppService
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

namespace UniCloud.Application.PartBC.MaintainCtrlServices
{
    /// <summary>
    /// MaintainCtrl的服务接口。
    /// </summary>
    public interface IMaintainCtrlAppService
    {
        /// <summary>
        /// 获取所有ItemMaintainCtrl。
        /// </summary>
        IQueryable<ItemMaintainCtrlDTO> GetItemMaintainCtrls();

        /// <summary>
        /// 获取所有PnMaintainCtrl。
        /// </summary>
        IQueryable<PnMaintainCtrlDTO> GetPnMaintainCtrls();

        /// <summary>
        /// 获取所有SnMaintainCtrl。
        /// </summary>
        IQueryable<SnMaintainCtrlDTO> GetSnMaintainCtrls();
    }
}
