#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/03，10:04
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
    ///     发动机DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class EnginePartDTO
    {
        private List<EnginePartDTO> _engineParts;

        /// <summary>
        ///     发动机滑油用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     序号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; set; }

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
        public virtual List<EnginePartDTO> EngineParts
        {
            get { return _engineParts ?? (_engineParts = new List<EnginePartDTO>()); }
            set { _engineParts = value; }
        }
    }
}