#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/14 13:37:09
// 文件名：PartService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Service.Part
{
    [Export(typeof (IPartService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartService : ServiceBase, IPartService
    {
        public PartService()
        {
            context = new PartData(AgentHelper.PartUri);
        }

        #region IPartService 成员

        public PartData Context
        {
            get { return context as PartData; }
        }

        #region 获取静态数据

        #endregion

        #region 公共属性

        #endregion

        #endregion
    }
}