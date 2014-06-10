#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 23:29:28
// 文件名：SnRemInstRecordDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     SnRemInstRecord
    /// </summary>
    [DataServiceKey("Id")]
    public class SnRemInstRecordDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     拆装指令号
        /// </summary>
        public string ActionNo { get; set; }

        /// <summary>
        ///     拆换开始日期
        /// </summary>
        public DateTime ActionDate { get; set; }

        /// <summary>
        ///     拆换类型
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        ///     拆装原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string RegNumber { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; set; }

        #endregion

        #region 导航属性

        #endregion
    }
}
