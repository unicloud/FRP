#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 14:21:48
// 文件名：ProgrammingFileDTO
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 规划文档
    /// </summary>
    [DataServiceKey("Id")]
    public class ProgrammingFileDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime? IssuedDate { get; set; }

        /// <summary>
        ///     发文单位
        /// </summary>
        public Guid IssuedUnitId { get; set; }

        /// <summary>
        ///     规划文号
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocName { get; set; }

        /// <summary>
        ///    文档类型，1--表示民航规划，2--表示川航规划
        /// </summary>
        public int Type { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     规划期间外键
        /// </summary>
        public Guid ProgrammingId { get; set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid DocumentId { get; set; }
        #endregion
    }
}
