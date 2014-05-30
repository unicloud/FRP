#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：MaintainCtrl
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
using System.Xml.Linq;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl聚合根
    ///     维修控制组
    /// </summary>
    public class MaintainCtrl : EntityInt, IValidatableObject
    {
        #region 私有字段


        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainCtrl()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     控制策略
        ///     目前分为：“先到为准”和“后到为准”两种策略
        /// </summary>
        public ControlStrategy CtrlStrategy { get; private set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 维修控制明细
        /// </summary>
        public string CtrlDetail { get; private set; }

        /// <summary>
        /// 维修控制明细
        /// </summary>
        public XElement XmlContent
        {
            get { return XElement.Parse(CtrlDetail); }
            set { CtrlDetail = value.ToString(); }
        }

        #endregion

        #region 外键属性

        public int? MaintainWorkId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 维修工作
        /// </summary>
        public MaintainWork MaintainWork { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置控制策略
        /// </summary>
        /// <param name="ctrlStrategy">控制策略</param>
        public void SetCtrlStrategy(ControlStrategy ctrlStrategy)
        {
            switch (ctrlStrategy)
            {
                case ControlStrategy.先到为准:
                    CtrlStrategy = ControlStrategy.先到为准;
                    break;
                case ControlStrategy.后到为准:
                    CtrlStrategy = ControlStrategy.后到为准;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("ctrlStrategy");
            }
        }


        /// <summary>
        ///     设置维修工作
        /// </summary>
        /// <param name="maintainWork">维修工作</param>
        public void SetMaintainWork(MaintainWork maintainWork)
        {
            if (maintainWork == null)
            {
                MaintainWork = null;
                MaintainWorkId = null;
            }
            else
            {
                MaintainWork = maintainWork;
                MaintainWorkId = maintainWork.Id;
            }
        }

        /// <summary>
        /// 设置描述信息
        /// </summary>
        /// <param name="description"></param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// 设置维修控制明细
        /// </summary>
        /// <param name="ctrlDetail"></param>
        public void SetCtrlDetail(string ctrlDetail)
        {
            CtrlDetail = ctrlDetail;
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