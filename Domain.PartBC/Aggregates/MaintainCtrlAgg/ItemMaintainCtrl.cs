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

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    /// ItemMaintainCtrl聚合根。
    /// 项控制组
    /// </summary>
    public class ItemMaintainCtrl : MaintainCtrl
    {
        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal ItemMaintainCtrl()
        {
        }
        #endregion

        #region 属性

        /// <summary>
        /// 项号
        /// </summary>
        public string ItemNo
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 构型ID
        /// </summary>
        public int AcConfigId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}