#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 21:05:49
// 文件名：InstallControllerDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     Item
    /// </summary>
    [DataServiceKey("Id")]
    public class InstallControllerDTO
    {
        #region 私有字段

        private List<DependencyDTO> _dependencies;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     启用日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     失效日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     项号
        /// </summary>
        public string ItemNo { get; set; }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///     可互换附件件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        ///     可互换附件件号描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public string AircraftTypeName { get; set; }
        
        #endregion

        #region 外键属性

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     附件项外键
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        ///     可互换附件外键
        /// </summary>
        public int PnRegId { get; set; }
        #endregion

        #region 导航属性

        /// <summary>
        ///     互换件集合
        /// </summary>
        public virtual List<DependencyDTO> Dependencies
        {
            get { return _dependencies ?? (_dependencies = new List<DependencyDTO>()); }
            set { _dependencies = value; }
        }

        #endregion
    }
}
