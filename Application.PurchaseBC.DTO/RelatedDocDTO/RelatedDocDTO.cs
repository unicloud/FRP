#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 16:24:08
// 文件名：RelatedDocDTO
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

namespace UniCloud.Application.PurchaseBC.DTO
{
    public partial class RelatedDocDTO
    {
        #region 属性
        // 业务外键
        public Guid SourceId { get; set; }
        // 文档外键
        public Guid DocumentId { get; set; }
        //文档名称 
        public string DocumentName { get; set; }

        #endregion
    }
}
