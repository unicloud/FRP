#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/26 17:53:40
// 文件名：AirBusScnDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/26 17:53:40
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AirBusScn
    /// </summary>
    [DataServiceKey("Id")]
    public class AirBusScnDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string CSCNumber
        {
            get;
            set;
        }

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            set;
        }

        /// <summary>
        /// SCN号
        /// </summary>
        public string ScnNumber
        {
            get;
            set;
        }

        
        /// <summary>
        /// SCN状态
        /// </summary>
        public int ScnStatus
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

        #endregion
    }
}
