#region 版本信息
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，11:39
// 方案：FRP
// 项目：Application.PartBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.PartBC.Query.OilMonitorQueries
{
    public class OilMonitorQuery : IOilMonitorQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public OilMonitorQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         
    }
}