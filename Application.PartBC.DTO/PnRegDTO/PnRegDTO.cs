#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using System.Collections.Generic;
using System.Data.Services.Common;
#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// PnReg
    /// </summary>
    [DataServiceKey("Id")]
    public class PnRegDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        /// 是否寿控
        /// </summary>
        public bool IsLife
        {
            get;
            set;
        }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项号
        /// </summary>
        public string ItemNo { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        /// 项外键(带相同项外键的附件，属于可互换的件）
        /// </summary>
        public int? ItemId { get; set; }

        #endregion

        #region 导航属性

        #endregion
    }
}
