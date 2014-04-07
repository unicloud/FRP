#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 20:16:46
// 文件名：InstallController
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
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg
{
    /// <summary>
    ///     InstallController聚合根。
    ///     装机控制，定义项与附件之间的多对多关系，维护附件的互换性，及互换的依赖项
    /// </summary>
    public class InstallController : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Dependency> _dependencies;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal InstallController()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime StartDate { get;private set; }

        /// <summary>
        ///  失效日期
        /// </summary>
        public DateTime? EndDate { get; private set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     附件项ID
        /// </summary>
        public int ItemId { get; private set; }

        /// <summary>
        ///     互换附件ID
        /// </summary>
        public int PnRegId { get; private set; }

        /// <summary>
        ///   川航机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 川航机型
        /// </summary>
        public virtual AircraftType AircraftType { get; set; }

        /// <summary>
        /// 附件项
        /// </summary>
        public virtual Item Item { get; set; }

        /// <summary>
        /// 互换附件
        /// </summary>
        public virtual PnReg PnReg { get; set; }

        /// <summary>
        ///     依赖项集合
        /// </summary>
        public virtual ICollection<Dependency> Dependencies
        {
            get { return _dependencies ?? (_dependencies = new HashSet<Dependency>()); }
            set { _dependencies = new HashSet<Dependency>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置启用日期
        /// </summary>
        /// <param name="startDate">启用日期</param>
        public void SetStartDate(DateTime startDate)
        {
            if (startDate == null)
            {
                throw new ArgumentException("启用日期参数为空！");
            }

            StartDate = startDate;
        }

        /// <summary>
        ///     设置结束日期
        /// </summary>
        /// <param name="endDate">结束日期</param>
        public void SetEndDate(DateTime? endDate)
        {
            EndDate = endDate;
        }

        /// <summary>
        ///     设置附件项
        /// </summary>
        /// <param name="item">附件项</param>
        public void SetItem(Item item)
        {
            if (item == null || item.IsTransient())
            {
                throw new ArgumentException("附件项参数为空！");
            }

            ItemId = item.Id;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置互换附件
        /// </summary>
        /// <param name="pnReg">附件</param>
        public void SetPnReg(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("互换附件参数为空！");
            }

            PnRegId = pnReg.Id;
        }

        /// <summary>
        ///     新增依赖项
        /// </summary>
        /// <param name="dependencyPnReg">依赖项附件</param>
        /// <returns>依赖项</returns>
        public Dependency AddNewDependency(PnReg dependencyPnReg)
        {
            var dependency = new Dependency
            {
                InstallControllerId = Id,
            };
            dependency.GenerateNewIdentity();
            dependency.SetPnReg(dependencyPnReg);

            return dependency;
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
