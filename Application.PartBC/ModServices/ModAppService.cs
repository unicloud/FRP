#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ModAppService
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
using UniCloud.Application.PartBC.Query.ModQueries;
using UniCloud.Domain.PartBC.Aggregates.ModAgg;
#endregion

namespace UniCloud.Application.PartBC.ModServices
{
    /// <summary>
    /// 实现Mod的服务接口。
    ///  用于处理Mod相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class ModAppService : IModAppService
    {
        private readonly IModQuery _modQuery;

        public ModAppService(IModQuery modQuery)
        {
            _modQuery = modQuery;
        }

        #region ModDTO

        /// <summary>
        /// 获取所有Mod。
        /// </summary>
        public IQueryable<ModDTO> GetMods()
        {
            var queryBuilder =
               new QueryBuilder<Mod>();
            return _modQuery.ModDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增Mod。
        /// </summary>
        /// <param name="dto">ModDTO。</param>
        [Insert(typeof(ModDTO))]
        public void InsertMod(ModDTO dto)
        {
        }

        /// <summary>
        ///  更新Mod。
        /// </summary>
        /// <param name="dto">ModDTO。</param>
        [Update(typeof(ModDTO))]
        public void ModifyMod(ModDTO dto)
        {
        }

        /// <summary>
        ///  删除Mod。
        /// </summary>
        /// <param name="dto">ModDTO。</param>
        [Delete(typeof(ModDTO))]
        public void DeleteMod(ModDTO dto)
        {
        }

        #endregion

    }
}
