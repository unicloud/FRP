#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 10:10:21
// 文件名：DocumentTypeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 10:10:21
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.CommonServiceBC.DTO
{
    /// <summary>
    /// 文档类型
    /// </summary>
    [DataServiceKey("DocumentTypeId")]
    public class DocumentTypeDTO
    {
        #region 属性
        /// <summary>
        /// 文档类型Id
        /// </summary>
        public int DocumentTypeId { get; set; }

        public bool IsChecked { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
