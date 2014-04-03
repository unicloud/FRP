#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，17:11
// 方案：FRP
// 项目：Domain.CommonServiceBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;

#endregion

namespace UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档聚合根
    /// </summary>
    public abstract class Document : EntityGuid, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Document()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     文件名
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; internal set; }

        /// <summary>
        ///     文档
        /// </summary>
        public byte[] FileStorage { get; internal set; }

        /// <summary>
        /// 文档文本内容
        /// </summary>
        public string FileContent { get; internal set; }

        /// <summary>
        ///     摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     上传者
        /// </summary>
        public string Uploader { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateTime { get; internal set; }

        /// <summary>
        ///     索引状态
        /// </summary>
        public IndexStatus Status { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        /// 文档类型Id
        /// </summary>
        public int DocumentTypeId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 文档类型
        /// </summary>
        public virtual DocumentType DocumentType { get; set; }
        #endregion

        #region 操作

        /// <summary>
        ///     设置文件名
        /// </summary>
        /// <param name="file">文件</param>
        public void SetFileName(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentException("文件参数为空！");
            }

            FileName = file.Name;
            Extension = file.Extension;
        }

        /// <summary>
        ///     设置索引状态
        /// </summary>
        /// <param name="status">索引状态</param>
        public void SetIndexStatus(IndexStatus status)
        {
            switch (status)
            {
                case IndexStatus.未建:
                    Status = IndexStatus.未建;
                    break;
                case IndexStatus.已建:
                    Status = IndexStatus.已建;
                    break;
                case IndexStatus.需要更新:
                    Status = IndexStatus.需要更新;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}