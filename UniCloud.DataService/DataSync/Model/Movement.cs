#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/18 15:26:42
// 文件名：Movement
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.DataService.DataSync.Model
{
    /// <summary>
    /// Amasis中序号件移动记录
    /// </summary>
    public class Movement
    {
        public string Pn { get; set; }

        public string Sn { get; set; }
        
        public DateTime MovementDate { get; set; }

        public string ActionNo { get; set; }

        public string MoveType { get; set; }

        public string RegNumber { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
