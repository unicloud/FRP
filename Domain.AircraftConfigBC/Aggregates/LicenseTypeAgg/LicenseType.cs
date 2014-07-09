#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:54:49
// 文件名：LicenseType
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:54:49
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg
{
    /// <summary>
    ///     证照类型聚合根
    /// </summary>
    public class LicenseType : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal LicenseType()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 是否有文件
        /// </summary>
        public bool HasFile { get; internal set; }
        #endregion

        #region 操作

        /// <summary>
        ///     设置证照类型
        /// </summary>
        /// <param name="type">注册号</param>
        public void SetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("名称参数为空！");
            }

            Type = type;
        }

        #endregion
    }
}
