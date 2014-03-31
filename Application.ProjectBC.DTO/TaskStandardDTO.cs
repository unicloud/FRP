#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，21:21
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     任务标准DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class TaskStandardDTO
    {
        private List<MemberDTO> _members;
        private List<RelatedDocDTO> _relatedDocs;
        private List<TaskCaseDTO> _taskCases;

        /// <summary>
        ///     任务标准ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     乐观时间
        /// </summary>
        public TimeSpan OptimisticTime { get; set; }

        /// <summary>
        ///     悲观时间
        /// </summary>
        public TimeSpan PessimisticTime { get; set; }

        /// <summary>
        ///     正常时间
        /// </summary>
        public TimeSpan NormalTime { get; set; }

        /// <summary>
        ///     源GUID
        /// </summary>
        public Guid SourceGuid { get; set; }

        /// <summary>
        ///     是否自定义
        /// </summary>
        public bool IsCustom { get; set; }

        /// <summary>
        ///     任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        ///     工作组ID
        /// </summary>
        public int WorkGroupId { get; set; }

        /// <summary>
        ///     任务案例集合
        /// </summary>
        public virtual List<TaskCaseDTO> TaskCases
        {
            get { return _taskCases ?? (_taskCases = new List<TaskCaseDTO>()); }
            set { _taskCases = value; }
        }

        /// <summary>
        ///     关联文档集合
        /// </summary>
        public virtual List<RelatedDocDTO> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = new List<RelatedDocDTO>()); }
            set { _relatedDocs = value; }
        }

        /// <summary>
        ///     成员集合
        /// </summary>
        public virtual List<MemberDTO> Members
        {
            get { return _members ?? (_members = new List<MemberDTO>()); }
            set { _members = value; }
        }
    }
}