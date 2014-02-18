#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegAppService
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
using UniCloud.Application.PartBC.Query.SnRegQueries;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
#endregion

namespace UniCloud.Application.PartBC.SnRegServices
{
    /// <summary>
    /// 实现SnReg的服务接口。
    ///  用于处理SnReg相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class SnRegAppService : ISnRegAppService
    {
        private readonly ISnRegQuery _snRegQuery;

        public SnRegAppService(ISnRegQuery snRegQuery)
        {
            _snRegQuery = snRegQuery;
        }

        #region SnRegDTO

        /// <summary>
        /// 获取所有SnReg。
        /// </summary>
        public IQueryable<SnRegDTO> GetSnRegs()
        {
            var queryBuilder =
               new QueryBuilder<SnReg>();
            return _snRegQuery.SnRegDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Insert(typeof(SnRegDTO))]
        public void InsertSnReg(SnRegDTO dto)
        {
        }

        /// <summary>
        ///  更新SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Update(typeof(SnRegDTO))]
        public void ModifySnReg(SnRegDTO dto)
        {
        }

        /// <summary>
        ///  删除SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Delete(typeof(SnRegDTO))]
        public void DeleteSnReg(SnRegDTO dto)
        {
        }

        #endregion

    }
}
