#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：SpecialConfig
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.UberModel.Aggregates.SpecialConfigAgg
{
    /// <summary>
    /// SpecialConfig聚合根。
    /// </summary>
    public class SpecialConfig : AcConfig
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SpecialConfig()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            internal set;
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 合同飞机ID
        /// </summary>
        public int ContractAircraftId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        /// <summary>
        ///     设置开始时间
        /// </summary>
        /// <param name="date">开始时间</param>
        public void SetStartDate(DateTime date)
        {
            StartDate = date;
        }

        /// <summary>
        ///     设置结束时间
        /// </summary>
        /// <param name="date">结束时间</param>
        public void SetEndDate(DateTime? date)
        {
            EndDate = date;
        }

        /// <summary>
        ///     设置是否有效
        /// </summary>
        /// <param name="isValid">是否有效</param>
        public void SetIsValid(bool isValid)
        {
            IsValid = isValid;
        }
        #endregion
    }
}
