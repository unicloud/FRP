#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，23:00
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     工作组DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class WorkGroupDTO
    {
        private List<MemberDTO> _members;

        /// <summary>
        ///     工作组ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     工作组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     工作组管理者ID
        /// </summary>
        public int ManagerUserId { get; set; }

        /// <summary>
        ///     管理者姓名
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        ///     工作组成员集合
        /// </summary>
        public virtual List<MemberDTO> Members
        {
            get { return _members ?? (_members = new List<MemberDTO>()); }
            set { _members = value; }
        }
    }
}