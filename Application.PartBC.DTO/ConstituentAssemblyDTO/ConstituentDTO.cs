#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/03，14:06
// 方案：FRP
// 项目：Application.PartBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     结构组件DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class ConstituentDTO
    {
        private List<ConstituentDTO> _constituents;
        private List<ControlSchemeDTO> _controlSchemes;

        /// <summary>
        ///     结构组件ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     根节点结构组件ID
        /// </summary>
        public int RootId { get; set; }

        /// <summary>
        ///     序号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        ///     是否寿控件
        /// </summary>
        public bool IsLifeControl { get; set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public decimal CSN { get; set; }

        /// <summary>
        ///     CSR，自上一次修理以来使用循环
        /// </summary>
        public decimal CSR { get; set; }

        /// <summary>
        ///     下级附件集合
        /// </summary>
        public virtual List<ConstituentDTO> EngineParts
        {
            get { return _constituents ?? (_constituents = new List<ConstituentDTO>()); }
            set { _constituents = value; }
        }

        /// <summary>
        ///     控制方案集合
        /// </summary>
        public virtual List<ControlSchemeDTO> ControlSchemes
        {
            get { return _controlSchemes ?? (_controlSchemes = new List<ControlSchemeDTO>()); }
            set { _controlSchemes = value; }
        }
    }
}