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
using System.Linq;
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
    public class PnRegAppService : IPnRegAppService
    {
        private readonly IPnRegQuery _pnRegQuery;

        public PnRegAppService(IPnRegQuery pnRegQuery)
        {
            _pnRegQuery = pnRegQuery;
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
        }

        /// <summary>
        ///  更新PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Update(typeof(PnRegDTO))]
        public void ModifyPnReg(PnRegDTO dto)
        {
        }

        /// <summary>
        ///  删除PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Delete(typeof(PnRegDTO))]
        public void DeletePnReg(PnRegDTO dto)
        {
        }

        #endregion

    }
}
