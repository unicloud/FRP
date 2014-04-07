#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：DependencyDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// Dependency
    /// </summary>
    [DataServiceKey("Id")]
    public class DependencyDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 依赖项件号
        /// </summary>
        public string Pn
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 装机控制Id
        /// </summary>
        public int InstallControllerId
        {
            get;
            set;
        }

        /// <summary>
        /// 依赖项附件Id
        /// </summary>
        public int DependencyPnId
        {
            get;
            set;
        }

        #endregion

    }
}
