#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，11:12
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.GuaranteeAgg
{
    /// <summary>
    ///     聚合根
    /// </summary>
    public class MaintainGuarantee : Guarantee
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainGuarantee()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     维修合同ID
        /// </summary>
        public int MaintainContractId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置维修合同ID
        /// </summary>
        /// <param name="id">维修合同ID</param>
        public void SetMaintainContractId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("维修合同ID参数为空！");
            }

            MaintainContractId = id;
        }

        #endregion
    }
}