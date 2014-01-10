#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，13:49
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     用户DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class UserDTO
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     员工号
        /// </summary>
        public string EmployeeCode { get; set; }


        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}