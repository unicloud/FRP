#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 12:05:16
// 文件名：ProgrammingFile
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg
{
    /// <summary>
    ///     规划文档聚合根
    /// </summary>
    public class ProgrammingFile : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ProgrammingFile()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime? IssuedDate { get; private set; }

        /// <summary>
        ///     规划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocName { get; private set; }

        /// <summary>
        ///    文档类型，1--表示民航规划，2--表示川航规划
        /// </summary>
        public int Type { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     规划期间外键
        /// </summary>
        public Guid ProgrammingId { get; internal set; }

        /// <summary>
        ///     发文单位
        /// </summary>
        public Guid IssuedUnitId { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid DocumentId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     规划期间
        /// </summary>
        public virtual Programming Programming { get; private set; }

        /// <summary>
        ///     发文单位
        /// </summary>
        public virtual Manager IssuedUnit { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置发文日期
        /// </summary>
        /// <param name="date">发文日期</param>
        public void SetIssuedDate(DateTime? date)
        {
            IssuedDate = date;
        }

        /// <summary>
        ///     设置民航规划相关文档文号
        /// </summary>
        /// <param name="docNumber">规划相关文档文号</param>
        public void SetDocNumber(string docNumber)
        {
            DocNumber = docNumber;
        }

        /// <summary>
        ///     设置规划期间
        /// </summary>
        /// <param name="programming">规划期间</param>
        public void SetProgramming(Programming programming)
        {
            if (programming == null || programming.IsTransient())
            {
                throw new ArgumentException("规划期间参数为空！");
            }

            Programming = programming;
            ProgrammingId = programming.Id;
        }

        /// <summary>
        ///     设置规划文档
        /// </summary>
        /// <param name="documentId">规划文档</param>
        /// <param name="docName">规划文档名称</param>
        public void SetDocument(Guid documentId, string docName)
        {
            if (documentId == Guid.Empty)
            {
                throw new ArgumentException("规划文档Id参数为空！");
            }

            DocumentId = documentId;
            DocName = docName;
        }

        /// <summary>
        ///     设置发文单位
        /// </summary>
        /// <param name="issuedUnit">发文单位</param>
        public void SetIssuedUnit(Manager issuedUnit)
        {
            if (issuedUnit == null || issuedUnit.IsTransient())
            {
                throw new ArgumentException("发文单位Id参数为空！");
            }

            IssuedUnit = issuedUnit;
            IssuedUnitId = issuedUnit.Id;
        }

        #endregion
    }
}