#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     BasicConfig
    /// </summary>
    [DataServiceKey("Id")]
    public class BasicConfigDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     基本构型组ID
        /// </summary>
        public int BasicConfigGroupId { get; set; }

        /// <summary>
        ///     FI号
        /// </summary>
        public string FiNumber { get; set; }

        /// <summary>
        ///     项号
        /// </summary>
        public string ItemNo { get; set; }

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///     位置信息
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     项(Item)ID
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        ///     父项(AcConfig)ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        ///     根节点(AcConfig)ID
        /// </summary>
        public int RootId { get; set; }

        #endregion

        private List<BasicConfigDTO> _subBasicConfigs;

        /// <summary>
        ///     子基本构型集合
        /// </summary>
        public List<BasicConfigDTO> SubBasicConfigs
        {
            get { return _subBasicConfigs ?? new List<BasicConfigDTO>(); }
            set { _subBasicConfigs = value; }
        }
    }
}