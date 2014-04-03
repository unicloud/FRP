#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:40:08
// 文件名：IAircraftLeaseReceptionAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

namespace UniCloud.Application.PurchaseBC.ReceptionServices
{
    /// <summary>
    /// 租赁飞机接收项目服务接口
    /// </summary>
    public interface IAircraftLeaseReceptionAppService
    {
        /// <summary>
        /// 获取所有租赁飞机接收项目。
        /// </summary>
        /// <returns>所有租赁飞机接收项目。</returns>
        IQueryable<AircraftLeaseReceptionDTO> GetAircraftLeaseReceptions();
    }
}
