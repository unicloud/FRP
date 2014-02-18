#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：MaintainCtrlAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.MaintainCtrlQueries;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
#endregion

namespace UniCloud.Application.PartBC.MaintainCtrlServices
{
    /// <summary>
    /// 实现MaintainCtrl的服务接口。
    ///  用于处理MaintainCtrl相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MaintainCtrlAppService : IMaintainCtrlAppService
    {
        private readonly IMaintainCtrlQuery _maintainCtrlQuery;

        public MaintainCtrlAppService(IMaintainCtrlQuery maintainCtrlQuery)
        {
            _maintainCtrlQuery = maintainCtrlQuery;
        }

        #region ItemMaintainCtrlDTO

        /// <summary>
        /// 获取所有ItemMaintainCtrl。
        /// </summary>
        public IQueryable<ItemMaintainCtrlDTO> GetItemMaintainCtrls()
        {
            var queryBuilder =
               new QueryBuilder<ItemMaintainCtrl>();
            return _maintainCtrlQuery.ItemMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Insert(typeof(ItemMaintainCtrlDTO))]
        public void InsertItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  更新ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Update(typeof(ItemMaintainCtrlDTO))]
        public void ModifyItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  删除ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Delete(typeof(ItemMaintainCtrlDTO))]
        public void DeleteItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
        }

        #endregion

        #region PnMaintainCtrlDTO

        /// <summary>
        /// 获取所有PnMaintainCtrl。
        /// </summary>
        public IQueryable<PnMaintainCtrlDTO> GetPnMaintainCtrls()
        {
            var queryBuilder =
               new QueryBuilder<PnMaintainCtrl>();
            return _maintainCtrlQuery.PnMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Insert(typeof(PnMaintainCtrlDTO))]
        public void InsertPnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  更新PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Update(typeof(PnMaintainCtrlDTO))]
        public void ModifyPnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  删除PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Delete(typeof(PnMaintainCtrlDTO))]
        public void DeletePnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
        }

        #endregion
        
        #region SnMaintainCtrlDTO

        /// <summary>
        /// 获取所有SnMaintainCtrl。
        /// </summary>
        public IQueryable<SnMaintainCtrlDTO> GetSnMaintainCtrls()
        {
            var queryBuilder =
               new QueryBuilder<SnMaintainCtrl>();
            return _maintainCtrlQuery.SnMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Insert(typeof(SnMaintainCtrlDTO))]
        public void InsertSnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  更新SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Update(typeof(SnMaintainCtrlDTO))]
        public void ModifySnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
        }

        /// <summary>
        ///  删除SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Delete(typeof(SnMaintainCtrlDTO))]
        public void DeleteSnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
        }

        #endregion

    }
}
