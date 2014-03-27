#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:31:51
// 文件名：FunctionItemDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:31:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     FunctionItem
    /// </summary>
    [DataServiceKey("Id")]
    public class FunctionItemDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     父亲节点
        /// </summary>
        public int? ParentItemId { get; set; }

        /// <summary>
        ///     是否是叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     导航的Url
        /// </summary>
        public string NaviUrl { get; set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 是否是按钮
        /// </summary>
        public bool IsButton { get; set; }

        public bool IsChecked { get; set; }

        private List<FunctionItemDTO> _subFunctionItems;
        /// <summary>
        ///     子项集合
        /// </summary>
        public List<FunctionItemDTO> SubFunctionItems
        {
            get { return _subFunctionItems ?? new List<FunctionItemDTO>(); }
            set { _subFunctionItems = value; }
        }
        #endregion
    }
}
