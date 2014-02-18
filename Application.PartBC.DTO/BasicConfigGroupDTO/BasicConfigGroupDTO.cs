#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupDTO
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
    /// BasicConfigGroup
    /// </summary>
    [DataServiceKey("Id")]
    public class BasicConfigGroupDTO
    {
        #region 私有字段

        private List<BasicConfigDTO> _basicConfigs;

        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime StartDate
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

        /// <summary>
        /// 基本构型组号
        /// </summary>
        public string GroupNo
        {
            get;
            set;
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 机型外键
        /// </summary>
        public Guid AircraftTypeId
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        ///     基本构型集合
        /// </summary>
        public virtual List<BasicConfigDTO> BasicConfigs
        {
            get { return _basicConfigs ?? (_basicConfigs = new List<BasicConfigDTO>()); }
            set { _basicConfigs = value; }
        }


        #endregion
    }
}
