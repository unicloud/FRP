#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：CtrlUnitAppService
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
using UniCloud.Application.PartBC.Query.CtrlUnitQueries;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
#endregion

namespace UniCloud.Application.PartBC.CtrlUnitServices
{
    /// <summary>
    /// 实现CtrlUnit的服务接口。
    ///  用于处理CtrlUnit相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class CtrlUnitAppService : ICtrlUnitAppService
    {
        private readonly ICtrlUnitQuery _ctrlUnitQuery;

        public CtrlUnitAppService(ICtrlUnitQuery ctrlUnitQuery)
        {
            _ctrlUnitQuery = ctrlUnitQuery;
        }

        #region CtrlUnitDTO

        /// <summary>
        /// 获取所有CtrlUnit。
        /// </summary>
        public IQueryable<CtrlUnitDTO> GetCtrlUnits()
        {
            var queryBuilder =
               new QueryBuilder<CtrlUnit>();
            return _ctrlUnitQuery.CtrlUnitDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增CtrlUnit。
        /// </summary>
        /// <param name="dto">CtrlUnitDTO。</param>
        [Insert(typeof(CtrlUnitDTO))]
        public void InsertCtrlUnit(CtrlUnitDTO dto)
        {
        }

        /// <summary>
        ///  更新CtrlUnit。
        /// </summary>
        /// <param name="dto">CtrlUnitDTO。</param>
        [Update(typeof(CtrlUnitDTO))]
        public void ModifyCtrlUnit(CtrlUnitDTO dto)
        {
        }

        /// <summary>
        ///  删除CtrlUnit。
        /// </summary>
        /// <param name="dto">CtrlUnitDTO。</param>
        [Delete(typeof(CtrlUnitDTO))]
        public void DeleteCtrlUnit(CtrlUnitDTO dto)
        {
        }

        #endregion

    }
}
