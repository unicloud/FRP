#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 22:27:46
// 文件名：ItemDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     Item
    /// </summary>
    [DataServiceKey("Id")]
    public class ItemDTO
    {
        #region 私有字段

        private List<PnRegDTO> _pnRegs;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     项号
        ///     项号与功能标识号有一个为空
        /// </summary>
        public string ItemNo { get; set; }

        /// <summary>
        ///     功能标识号(不为空时表示FI，用于构型的叶子节点)
        ///     项号与功能标识号有一个为空
        /// </summary>
        public string FiNumber { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     项控制组（寿控件必须维护项维修控制组）
        /// </summary>
        public virtual ItemMaintainCtrlDTO ItemMaintainCtrl { get; set; }

        /// <summary>
        ///     互换件集合
        /// </summary>
        public virtual List<PnRegDTO> PnRegs
        {
            get { return _pnRegs ?? (_pnRegs = new List<PnRegDTO>()); }
            set { _pnRegs = value; }
        }

        #endregion
    }
}