#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：MaintainWorkAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.MaintainWorkQueries;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
#endregion

namespace UniCloud.Application.PartBC.MaintainWorkServices
{
    /// <summary>
    /// 实现MaintainWork的服务接口。
    ///  用于处理MaintainWork相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class MaintainWorkAppService : ContextBoundObject, IMaintainWorkAppService
    {
        private readonly IMaintainWorkQuery _maintainWorkQuery;
        private readonly IMaintainWorkRepository _maintainWorkRepository;
        public MaintainWorkAppService(IMaintainWorkQuery maintainWorkQuery,
            IMaintainWorkRepository maintainWorkRepository)
        {
            _maintainWorkQuery = maintainWorkQuery;
            _maintainWorkRepository = maintainWorkRepository;
        }

        #region MaintainWorkDTO

        /// <summary>
        /// 获取所有MaintainWork。
        /// </summary>
        public IQueryable<MaintainWorkDTO> GetMaintainWorks()
        {
            var queryBuilder =
               new QueryBuilder<MaintainWork>();
            return _maintainWorkQuery.MaintainWorkDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Insert(typeof(MaintainWorkDTO))]
        public void InsertMaintainWork(MaintainWorkDTO dto)
        {
            var newMaintainWork = MaintainWorkFactory.CreateMaintainWork();

            newMaintainWork.SetWorkCode(dto.WorkCode);
            newMaintainWork.SetDescription(dto.Description);
            _maintainWorkRepository.Add(newMaintainWork);
        }

        /// <summary>
        ///  更新MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Update(typeof(MaintainWorkDTO))]
        public void ModifyMaintainWork(MaintainWorkDTO dto)
        {
            var updateMaintainWork = _maintainWorkRepository.Get(dto.Id); //获取需要更新的对象。

            //更新。
            updateMaintainWork.SetWorkCode(dto.WorkCode);
            updateMaintainWork.SetDescription(dto.Description);
            _maintainWorkRepository.Modify(updateMaintainWork);
        }

        /// <summary>
        ///  删除MaintainWork。
        /// </summary>
        /// <param name="dto">MaintainWorkDTO。</param>
        [Delete(typeof(MaintainWorkDTO))]
        public void DeleteMaintainWork(MaintainWorkDTO dto)
        {
            var delMaintainWork = _maintainWorkRepository.Get(dto.Id); //获取需要删除的对象。
            _maintainWorkRepository.Remove(delMaintainWork); //删除MaintainWork。
        }

        #endregion

    }
}
