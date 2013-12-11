#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，14:12
// 文件名：ImagePathHelper.cs
// 程序集：UniCloud.Presentation.Service.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Windows.Media;

#endregion

namespace UniCloud.Presentation.Service.CommonService.DocumentExtension
{
    /// <summary>
    ///     文件夹文档转化ListBoxItem
    /// </summary>
    public static class ImagePathHelper
    {
        private static readonly ImageSourceConverter Isc;

        static ImagePathHelper()
        {
            Isc = new ImageSourceConverter();
        }

        /// <summary>
        ///     获取图标路径
        /// </summary>
        /// <param name="extendType">扩展名</param>
        /// <returns></returns>
        public static ImageSource GetBigImageSource(string extendType)
        {
            switch (extendType)
            {
                case "pdf":
                    return (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/pdf.png");
                case "doc":
                case "docx":
                    return (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/doc.png");
                case "zip":
                case "rar":
                    return (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/rar.png");
                case "jpg":
                case "png":
                case "bmp":
                case "jpeg":
                case "gif":
                    return (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/jpg.png");
                case "xlsx":
                case "xls":
                    return (ImageSource) Isc.ConvertFromString("/UniCloud.Presentation;component/Images/xls.png");
                default:
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/bigFolder.png");
            }
        }

        /// <summary>
        ///     获取图标路径
        /// </summary>
        /// <param name="extendType">扩展名</param>
        /// <returns></returns>
        public static ImageSource GetSmallImageSource(string extendType)
        {
            switch (extendType)
            {
                case "pdf":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/pdf.png");
                case "doc":
                case "docx":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/doc.png");
                case "zip":
                case "rar":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/rar.png");
                case "jpg":
                case "png":
                case "bmp":
                case "jpeg":
                case "gif":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/jpg.png");
                case "xlsx":
                case "xls":
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/xls.png");
                default:
                    return (ImageSource)Isc.ConvertFromString("/UniCloud.Presentation;component/Images/folder.png");
            }
        }
    }
}