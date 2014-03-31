#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：TsLineDTO
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
    /// TsLine
    /// </summary>
    [DataServiceKey("Id")]
    public class TsLineDTO
    {
        #region 私有字段

        private List<DependencyDTO> _dependencies;

        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// TS号
        /// </summary>
        public string TsNumber
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 技术解决方案ID
        /// </summary>
        public int TsId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        ///     依赖项
        /// </summary>
        public virtual List<DependencyDTO> Dependencies
        {
            get { return _dependencies ?? (_dependencies = new List<DependencyDTO>()); }
            set { _dependencies = value; }
        }

        #endregion
    }
}
