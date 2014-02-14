#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/14 13:37:25
// 文件名：IPartService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Service.Part
{
    public interface IPartService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        PartData Context { get; }

        #region 获取静态数据

        #endregion

        #region 公共属性

        #endregion
    }
}