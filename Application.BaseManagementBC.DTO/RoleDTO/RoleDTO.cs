#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     Role
    /// </summary>
    [DataServiceKey("Id")]
    public class RoleDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     角色
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 功能项集合
        /// </summary>
        private List<RoleFunctionDTO> _roleFunctions;
        public List<RoleFunctionDTO> RoleFunctions
        {
            get { return _roleFunctions ?? new List<RoleFunctionDTO>(); }
            set { _roleFunctions = value; }
        }

        #endregion
    }
}