#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/13 21:18:18
// 文件名：AcConfigDTO
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     AcConfigDTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AcConfigDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }
        
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
        public Position Position { get; set; }

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

        /// <summary>
        /// 用标记差异的构型
        /// </summary>
        public string Color { get; set; }

        #endregion

        private List<AcConfigDTO> _subAcConfigs;

        /// <summary>
        ///     子构型集合
        /// </summary>
        public List<AcConfigDTO> SubAcConfigs
        {
            get { return _subAcConfigs ?? new List<AcConfigDTO>(); }
            set { _subAcConfigs = value; }
        }
    }
}
