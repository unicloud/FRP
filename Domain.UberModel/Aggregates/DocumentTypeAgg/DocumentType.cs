#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 9:42:45
// 文件名：DocumentType
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 9:42:45
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.UberModel.Aggregates.DocumentTypeAgg
{
    /// <summary>
    ///     文档类型聚合根
    /// </summary>
    public class DocumentType : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal DocumentType()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; internal set; }
        #endregion
    }
}
