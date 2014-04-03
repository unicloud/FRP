#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:59:26
// 文件名：ApplicableAircraft
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ScnAgg
{
    /// <summary>
    /// ScnAgg聚合根。
    /// ApplicableAircraft
    /// 适用飞机集合
    /// </summary>
    public class ApplicableAircraft : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ApplicableAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime CompleteDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 合同飞机外键
        /// </summary>
        public int ContractAircraftId
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN外键
        /// </summary>
        public int ScnId
        {
            get;
            internal set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 合同飞机
        /// </summary>
        public virtual ContractAircraft ContractAircraft
        {
            get;
            set;
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置完成日期
        /// </summary>
        /// <param name="date">完成日期</param>
        public void SetCompleteDate(DateTime date)
        {
            CompleteDate = date;
        }

        /// <summary>
        ///     设置费用
        /// </summary>
        /// <param name="cost">费用</param>
        public void SetCost(decimal cost)
        {
            Cost = cost;
        }

        /// <summary>
        ///     设置合同飞机
        /// </summary>
        /// <param name="contractAircraft">合同飞机</param>
        public void SetContractAircraft(ContractAircraft contractAircraft)
        {
            if (contractAircraft == null || contractAircraft.IsTransient())
            {
                throw new ArgumentException("合同飞机参数为空！");
            }

            ContractAircraft = contractAircraft;
            ContractAircraftId = contractAircraft.Id;
        }

        /// <summary>
        ///     设置合同飞机
        /// </summary>
        /// <param name="contractAircraftId">合同飞机</param>
        public void SetContractAircraft(int contractAircraftId)
        {
            ContractAircraftId = contractAircraftId;
        }
        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
