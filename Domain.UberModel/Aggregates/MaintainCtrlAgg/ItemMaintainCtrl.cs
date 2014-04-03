#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：ItemMaintainCtrl
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl聚合根。
    ///     项控制组
    /// </summary>
    public class ItemMaintainCtrl : MaintainCtrl
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ItemMaintainCtrl()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     项号
        /// </summary>
        public string ItemNo { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件项ID
        /// </summary>
        public int ItemId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件项
        /// </summary>
        /// <param name="item">附件项</param>
        public void SetItem(Item item)
        {
            if (item == null || item.IsTransient())
            {
                throw new ArgumentException("附件项参数为空！");
            }

            ItemId = item.Id;
            ItemNo = item.ItemNo;
        }

        #endregion
    }
}