#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：LifeMonitorDTO
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
    /// LifeMonitor
    /// </summary>
    [DataServiceKey("Id")]
    public class LifeMonitorDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 工作代码
        /// </summary>
        public string WorkCode
        {
            get;
            set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            set;
        }

        /// <summary>
        /// 预计到寿开始
        /// </summary>
        public DateTime MointorStart
        {
            get;
            set;
        }

        /// <summary>
        /// 预计到寿期限
        /// </summary>
        public string LifeTimeLimit
        {
            get;
            set;
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 维修工作外键
        /// </summary>
        public int MaintainWorkId
        {
            get;
            set;
        }

        /// <summary>
        /// 序号外键
        /// </summary>
        public int SnRegId
        {
            get;
            set;
        }
        #endregion

    }
}
