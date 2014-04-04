#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 16:38:03
// 文件名：BaseManagementService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 16:38:03
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

namespace UniCloud.Presentation.Service.BaseManagement
{
    [Export(typeof(IBaseManagementService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BaseManagementService : ServiceBase, IBaseManagementService
    {
        public BaseManagementService()
        {
            context = new BaseManagementData(AgentHelper.BaseManagementUri);
        }

        #region IBaseManagementService 成员

        public BaseManagementData Context
        {
            get { return context as BaseManagementData; }
        }

        #endregion
    }
}
