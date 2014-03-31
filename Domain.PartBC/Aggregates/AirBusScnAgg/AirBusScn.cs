#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：Scn
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg
{
    /// <summary>
    /// AirBusScn聚合根。
    /// </summary>
    public class AirBusScn : EntityInt, IValidatableObject
    {

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirBusScn()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            internal set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string CSCNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN号
        /// </summary>
        public string ScnNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// SCN状态
        /// </summary>
        public int ScnStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            private set;
        }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性
        #endregion

        #region 操作

        /// <summary>
        ///     设置批次号
        /// </summary>
        /// <param name="cscNumber">批次号</param>
        public void SetCscNumber(string cscNumber)
        {
            if (string.IsNullOrWhiteSpace(cscNumber))
            {
                throw new ArgumentException("批次号参数为空！");
            }

            CSCNumber = cscNumber;
        }

        /// <summary>
        ///     设置MOD号
        /// </summary>
        /// <param name="modNumber">MOD号</param>
        public void SetModNumber(string modNumber)
        {
            if (string.IsNullOrWhiteSpace(modNumber))
            {
                throw new ArgumentException("MOD号参数为空！");
            }

            ModNumber = modNumber;
        }

        /// <summary>
        ///     设置SCN号
        /// </summary>
        /// <param name="scnNumber">SCN号</param>
        public void SetScnNumber(string scnNumber)
        {
            if (string.IsNullOrWhiteSpace(scnNumber))
            {
                throw new ArgumentException("SCN号参数为空！");
            }

            ScnNumber = scnNumber;
        }

        /// <summary>
        ///     设置SCN状态
        /// </summary>
        /// <param name="scnStatus">SCN状态</param>
        public void SetScnStatus(int scnStatus)
        {
            ScnStatus = scnStatus;
        }

        /// <summary>
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {

            Description = description;
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
