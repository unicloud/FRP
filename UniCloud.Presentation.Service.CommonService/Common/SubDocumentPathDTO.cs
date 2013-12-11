#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/11，16:12
// 文件名：SubDocumentPathDTO.cs
// 程序集：UniCloud.Presentation.Service.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Windows.Media;
using UniCloud.Presentation.Service.CommonService.DocumentExtension;

#endregion

namespace UniCloud.Presentation.Service.CommonService.Common
{
    public partial class SubDocumentPathDTO
    {
        /// <summary>
        ///     图片路径
        /// </summary>
        public ImageSource BigIconPath
        {
            get { return ImagePathHelper.GetBigImageSource(Extension); }
        }

        /// <summary>
        ///     缩略图路径
        /// </summary>
        public ImageSource SmallIconPath
        {
            get { return ImagePathHelper.GetSmallImageSource(Extension); }
        }
    }
}