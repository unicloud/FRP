﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:15:02
// 文件名：CaacProgramming
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.CaacPorgrammingAgg
{
    /// <summary>
    ///     民航局五年规划聚合根
    /// </summary>
    public class CaacProgramming : EntityGuid
    {

        #region 私有字段

        private HashSet<CaacProgrammingLine> _caacProgrammingLines;

        #endregion


        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal CaacProgramming()
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
        ///     规划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     规划期间
        /// </summary>
        public Guid ProgrammingId { get; private set; }

        /// <summary>
        ///     发文单位
        /// </summary>
        public Guid IssuedUnitId { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     民航局五年规划明细
        /// </summary>
        public virtual ICollection<CaacProgrammingLine> CaacProgrammingLines
        {
            get { return _caacProgrammingLines ?? (_caacProgrammingLines = new HashSet<CaacProgrammingLine>()); }
            set { _caacProgrammingLines = new HashSet<CaacProgrammingLine>(value); }
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
        ///     设置民航五年规划文号
        /// </summary>
        /// <param name="docNumber">规划文号</param>
        public void SetDocNumber(string docNumber)
        {
            DocNumber = docNumber;
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
        /// <param name="programmingId">规划期间</param>
        public void SetProgramming(Guid programmingId)
        {
            if (programmingId == null)
            {
                throw new ArgumentException("规划期间Id参数为空！");
            }

            ProgrammingId = programmingId;
        }

        /// <summary>
        ///     设置五年规划文档
        /// </summary>
        /// <param name="documentId">五年规划文档</param>
        public void SetDocument(Guid documentId)
        {
            if (documentId == null)
            {
                throw new ArgumentException("五年规划文档Id参数为空！");
            }

            DocumentId = documentId;
        }

        /// <summary>
        ///     设置发文单位
        /// </summary>
        /// <param name="issuedUnitId">发文单位</param>
        public void SetIssuedUnit(Guid issuedUnitId)
        {
            if (issuedUnitId == null)
            {
                throw new ArgumentException("发文单位Id参数为空！");
            }

            IssuedUnitId = issuedUnitId;
        }

        /// <summary>
        /// 新增民航局五年规划行
        /// </summary>
        /// <returns></returns>
        public CaacProgrammingLine AddNewAirProgrammingLine()
        {
            var caacProgrammingLine = new CaacProgrammingLine
            {
                CaacProgrammingId = Id,
            };

            caacProgrammingLine.GenerateNewIdentity();
            CaacProgrammingLines.Add(caacProgrammingLine);

            return caacProgrammingLine;
        }


        #endregion
    }
}
