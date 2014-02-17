#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10，13:11
// 方案：FRP
// 项目：DistributedServices.Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Part
{
    /// <summary>
    ///     附件管理模块数据类
    /// </summary>
    public class PartData : ExposeData.ExposeData
    {
        //private readonly IScnAppService _scnAppService;

        public PartData()
            : base("UniCloud.Application.PartBC.DTO")
        {
            //_scnAppService = DefaultContainer.Resolve<IScnAppService>();
        }

        #region Scn相关集合

        ///// <summary>
        /////     Scn信息。
        ///// </summary>
        //public IQueryable<ScnDTO> Scns
        //{
        //    get { return _scnAppService.GetScns(); }
        //}

        #endregion
    }
}