#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/15，23:04
// 文件名：ISnRemInstRecordAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.SnRemInstRecordServices
{
    /// <summary>
    ///     拆换记录服务接口。
    /// </summary>
    public interface ISnRemInstRecordAppService
    {
        /// <summary>
        ///     获取所有拆换记录
        /// </summary>
        /// <returns></returns>
        IQueryable<SnRemInstRecordDTO> GetSnRemInstRecords();
    }
}