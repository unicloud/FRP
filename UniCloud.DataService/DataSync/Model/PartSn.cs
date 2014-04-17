#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/17 14:38:28
// 文件名：PartSn
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
    /// 序号件
    /// </summary>
    public class PartSn
    {
        public string Pn { get; set; }

        public string Sn { get; set; }

        public string SerialNumber { get; set; }

        public string RegNumber { get; set; }

        public int Status { get; set; }

        public DateTime LatestRemoveDate { get; set; }

        public decimal CSN { get; set; }

        public decimal CSO { get; set; }

        public decimal TSN { get; set; }

        public decimal TSO { get; set; }

        public int ATA { get; set; }
    }
}
