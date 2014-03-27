#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegAppService
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
using UniCloud.Application.PartBC.Query.PnRegQueries;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
#endregion

namespace UniCloud.Application.PartBC.PnRegServices
{
    /// <summary>
    /// 实现PnReg的服务接口。
    ///  用于处理PnReg相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PnRegAppService : ContextBoundObject, IPnRegAppService
    {
        private readonly IPnRegQuery _pnRegQuery;
        private readonly IPnRegRepository _pnRegRepository;
        public PnRegAppService(IPnRegQuery pnRegQuery,IPnRegRepository pnRegRepository)
        {
            _pnRegQuery = pnRegQuery;
            _pnRegRepository = pnRegRepository;
        }

        #region PnRegDTO

        /// <summary>
        /// 获取所有PnReg。
        /// </summary>
        public IQueryable<PnRegDTO> GetPnRegs()
        {
            var queryBuilder =
               new QueryBuilder<PnReg>();
            return _pnRegQuery.PnRegDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Insert(typeof(PnRegDTO))]
        public void InsertPnReg(PnRegDTO dto)
        {
            var newPnReg = PnRegFactory.CreatePnReg();

            newPnReg.SetPn(dto.Pn);
            newPnReg.SetIsLife(dto.IsLife);
            _pnRegRepository.Add(newPnReg);
        }

        /// <summary>
        ///  更新PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Update(typeof(PnRegDTO))]
        public void ModifyPnReg(PnRegDTO dto)
        {
            var updatePnReg = _pnRegRepository.Get(dto.Id); //获取需要更新的对象。

            //更新。
            updatePnReg.SetPn(dto.Pn);
            updatePnReg.SetIsLife(dto.IsLife);
            _pnRegRepository.Modify(updatePnReg);
        }

        /// <summary>
        ///  删除PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Delete(typeof(PnRegDTO))]
        public void DeletePnReg(PnRegDTO dto)
        {
            var delPnReg = _pnRegRepository.Get(dto.Id); //获取需要删除的对象。
            _pnRegRepository.Remove(delPnReg); //删除PnReg。
        }

        #endregion

    }
}
