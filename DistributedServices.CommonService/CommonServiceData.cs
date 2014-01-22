#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/02，18:12
// 文件名：CommonServiceData.cs
// 程序集：UniCloud.DistributedServices.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.CommonService
{
    /// <summary>
    ///     公共服务模块数据类
    /// </summary>
    public class CommonServiceData : ExposeData.ExposeData
    {
        private readonly IDocumentAppService _documentAppService;

        public CommonServiceData()
            : base("UniCloud.Application.CommonServiceBC.DTO")
        {
            _documentAppService = DefaultContainer.Resolve<IDocumentAppService>();
        }

        #region 文档相关集合

        /// <summary>
        ///     文件夹下文档及子文档信息。
        /// </summary>
        public IQueryable<DocumentDTO> Documents
        {
            get { return _documentAppService.GetDocuments(); }
        }
        #endregion

        #region 文档类型集合
        /// <summary>
        /// 文档类型集合
        /// </summary>
        public IQueryable<DocumentTypeDTO> DocumentTypes
        {
            get { return _documentAppService.GetDocumentTypes(); }
        }
        #endregion
    }
}