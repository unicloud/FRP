#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:14:41

// 文件名：BasicConfig
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

using System;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg
{
    /// <summary>
    ///     BasicConfig聚合根.
    ///     基本构型
    /// </summary>
    public class BasicConfig : AcConfig
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BasicConfig()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     基本构型组ID
        /// </summary>
        public int BasicConfigGroupId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     基本构型组
        /// </summary>
        public BasicConfigGroup BasicConfigGroup { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置基本构型组外键
        /// </summary>
        /// <param name="basicConfigGroup">基本构型组</param>
        public void SetBasicConfigGroup(BasicConfigGroup basicConfigGroup)
        {
            if (basicConfigGroup == null || basicConfigGroup.IsTransient())
            {
                throw new ArgumentException("基本构型组参数为空！");
            }

            BasicConfigGroup = basicConfigGroup;
            BasicConfigGroupId = basicConfigGroup.Id;
        }
        #endregion
    }
}