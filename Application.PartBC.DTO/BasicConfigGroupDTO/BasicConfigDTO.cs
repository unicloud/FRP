#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigDTO
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
    /// BasicConfig
    /// </summary>
    [DataServiceKey("Id")]
    public class BasicConfigDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 基本构型组ID
        /// </summary>
        public int BasicConfigGroupId
        {
            get;
            set;
        }

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            set;
        }

        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 项号
        /// </summary>
        public string ItemNo
        {
            get;
            set;
        }

        /// <summary>
        /// 上层项号
        /// </summary>
        public string ParentItemNo
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 技术解决方案ID
        /// </summary>
        public int? TsId
        {
            get;
            set;
        }

        /// <summary>
        /// 父项ID
        /// </summary>
        public int? ParentId
        {
            get;
            set;
        }

        #endregion

    }
}
