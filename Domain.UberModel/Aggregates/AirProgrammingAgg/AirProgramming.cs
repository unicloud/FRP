﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:18:56
// 文件名：AirProgramming
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.UberModel.Aggregates.ManagerAgg;
using UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     航空公司五年规划聚合根
    /// </summary>
    public class AirProgramming : EntityGuid
    {

        #region 私有字段

        private HashSet<AirProgrammingLine> _airProgrammingLines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirProgramming()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     规划名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime? CreateDate { get; internal set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime? IssuedDate { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocName { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///     规划期间
        /// </summary>
        public Guid ProgrammingId { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid DocumentId { get; private set; }

        /// <summary>
        ///   发文单位
        /// </summary>
        public int IssuedUnitId { get; private set; }

        #endregion

        #region 导航属性
        /// <summary>
        /// 规划期间
        /// </summary>
        public virtual Programming Programming { get; private set; }

        /// <summary>
        /// 发文单位
        /// </summary>
        public virtual IssuedUnit IssuedUnit { get; private set; }

        /// <summary>
        ///     航空公司五年规划明细
        /// </summary>
        public virtual ICollection<AirProgrammingLine> AirProgrammingLines
        {
            get { return _airProgrammingLines ?? (_airProgrammingLines = new HashSet<AirProgrammingLine>()); }
            set { _airProgrammingLines = new HashSet<AirProgrammingLine>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置规划名称
        /// </summary>
        /// <param name="name">规划名称</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("规划名称参数为空！");
            }

            Name = name;
        }

        /// <summary>
        ///     设置发文日期
        /// </summary>
        /// <param name="date">发文日期</param>
        public void SetIssuedDate(DateTime? date)
        {
            IssuedDate = date;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            Note = note;
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
        ///     设置五年规划文档
        /// </summary>
        /// <param name="documentId">五年规划文档</param>
        /// <param name="docName">规划文档名称</param>
        public void SetDocument(Guid documentId,string docName)
        {
            //if (documentId == Guid.Empty)
            //{
            //    throw new ArgumentException("五年规划文档Id参数为空！");
            //}

            DocumentId = documentId;
            DocName = docName;
        }

        /// <summary>
        ///     设置发文单位
        /// </summary>
        /// <param name="issuedUnit">发文单位</param>
        public void SetIssuedUnit(IssuedUnit issuedUnit)
        {
            if (issuedUnit == null || issuedUnit.IsTransient())
            {
                throw new ArgumentException("发文单位参数为空！");
            }

            IssuedUnit = issuedUnit;
            IssuedUnitId = issuedUnit.Id;
        }

        /// <summary>
        /// 新增航空公司五年规划行
        /// </summary>
        /// <returns></returns>
        public AirProgrammingLine AddNewAirProgrammingLine()
        {
            var airProgrammingLine = new AirProgrammingLine
            {
                AirProgrammingId = Id,
            };

            airProgrammingLine.GenerateNewIdentity();
            AirProgrammingLines.Add(airProgrammingLine);

            return airProgrammingLine;
        }

        #endregion
    }
}
