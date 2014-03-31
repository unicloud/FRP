#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:17:15
// 文件名：Airlines
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg
{
    /// <summary>
    ///     航空公司聚合根
    /// </summary>
    public class Airlines : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Airlines()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     公司中文名称
        /// </summary>
        public string CnName { get; protected set; }

        /// <summary>
        ///     公司英文名称
        /// </summary>
        public string EnName { get; protected set; }

        /// <summary>
        ///     公司中文简称
        /// </summary>
        public string CnShortName { get; protected set; }

        /// <summary>
        ///     公司英文简称
        /// </summary>
        public string EnShortName { get; protected set; }

        /// <summary>
        ///     三字码
        /// </summary>
        public string ICAOCode { get; protected set; }

        /// <summary>
        ///     二字码
        /// </summary>
        public string IATACode { get; protected set; }

        /// <summary>
        ///     是否当前航空公司
        /// </summary>
        public bool IsCurrent { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
