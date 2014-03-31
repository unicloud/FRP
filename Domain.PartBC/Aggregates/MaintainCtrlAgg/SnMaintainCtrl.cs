#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：SnMaintainCtrl
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl聚合根。
    ///     序号件控制组
    /// </summary>
    public class SnMaintainCtrl : MaintainCtrl
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnMaintainCtrl()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     序号范围
        /// </summary>
        public string SnScope { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置序号范围
        /// </summary>
        /// <param name="snScope">序号范围</param>
        public void SetSnScope(string snScope)
        {
            if (string.IsNullOrWhiteSpace(snScope))
            {
                throw new ArgumentException("序号范围参数为空！");
            }

            SnScope = snScope;
        }

        #endregion
    }
}