#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/21 10:49:03
// 文件名：IDocumentSearchAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/21 10:49:03
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using UniCloud.Application.CommonServiceBC.DTO;

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch
{
   /// <summary>
    ///     表示用于处理文档搜索服务接口。
    /// </summary>
    public interface IDocumentSearchAppService
   {
        List<DocumentDTO> Search(string keyword);
   }
}
