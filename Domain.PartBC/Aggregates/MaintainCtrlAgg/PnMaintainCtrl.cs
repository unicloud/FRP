#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：PnMaintainCtrl
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl聚合根。
    ///     附件控制组
    /// </summary>
    public class PnMaintainCtrl : MaintainCtrl
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PnMaintainCtrl()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件ID
        /// </summary>
        public int PnRegId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 附件
        /// </summary>
        public virtual PnReg PnReg { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="pnReg">附件</param>
        public void SetPnReg(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("附件参数为空！");
            }

            Pn = pnReg.Pn;
            PnRegId = pnReg.Id;
        }

        #endregion
    }
}