#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 16:53:05
// 文件名：IssuedUnit
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.UberModel.Aggregates.IssuedUnitAgg
{
    /// <summary>
    ///     发文单位聚合根
    /// </summary>
    public class IssuedUnit : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal IssuedUnit()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     发文单位中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     发文单位中文简称
        /// </summary>
        public string CnShortName { get; set; }

        /// <summary>
        ///     是否内部的发文单位
        /// </summary>
        public bool IsInner { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
