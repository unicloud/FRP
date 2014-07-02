#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，17:33
// 方案：FRP
// 项目：Service.Portal
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

//using UniCloud.Presentation.Service.Portal.Portal;
using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Portal.Portal;

#endregion

namespace UniCloud.Presentation.Service.Portal
{
    [Export(typeof (IPortalService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PortalService : ServiceBase, IPortalService
    {
        public PortalService()
        {
            context = new PortalData(AgentHelper.PortalUri);
        }

        #region IPortalService 成员

        public PortalData Context
        {
            get { return context as PortalData; }
        }

        #endregion
    }
}