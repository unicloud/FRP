#region 命名空间

using System.Windows.Media;
using UniCloud.Presentation.Service.CommonService.DocumentExtension;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace UniCloud.Presentation.Service.CommonService.Common
{
    public partial class DocumentPathDTO
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public ImageSource BigIconPath
        {
            get { return ImagePathHelper.GetBigImageSource(Extension); }
        }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public ImageSource SmallIconPath
        {
            get { return ImagePathHelper.GetSmallImageSource(Extension); }
        }

      

    }
}