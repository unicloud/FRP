#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/26 14:00:50
// 文件名：FlightLogDTO
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/26 14:00:50
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

namespace UniCloud.Application.FlightLogBC.DTO
{
    [DataServiceKey("Id")]
    public class FlightLogDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string AcReg
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机序列号
        /// </summary>
        public string MSN
        {
            get;
            set;
        }

        /// <summary>
        /// 航班日期
        /// </summary>
        public DateTime FlightDate
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行时间
        /// </summary>
        public decimal FlightHours
        {
            get;
            set;
        }

        /// <summary>
        /// 累计循环数
        /// </summary>
        public int TotalCycles
        {
            get;
            set;
        }

        #endregion
    }
}
