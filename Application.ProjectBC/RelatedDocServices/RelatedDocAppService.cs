#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，11:01
// 方案：FRP
// 项目：Application.ProjectBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Application.ProjectBC.Query.RelatedDocQueries;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Application.ProjectBC.RelatedDocServices
{
    /// <summary>
    ///     关联文档服务实现
    /// </summary>
    public class RelatedDocAppService : IRelatedDocAppService
    {
        private readonly IRelatedDocQuery _relatedDocQuery;
        private readonly IRelatedDocRepository _relatedDocRepository;

        public RelatedDocAppService(IRelatedDocQuery relatedDocQuery,
            IRelatedDocRepository relatedDocRepository)
        {
            _relatedDocQuery = relatedDocQuery;
            _relatedDocRepository = relatedDocRepository;
        }

        #region RelatedDocDTO

        /// <summary>
        ///     获取所有RelatedDocDTO
        /// </summary>
        /// <returns></returns>
        public IQueryable<RelatedDocDTO> GetRelatedDocs()
        {
            var queryBuilder =
                new QueryBuilder<RelatedDoc>();
            return _relatedDocQuery.RelatedDocDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增RelatedDocDTO
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO</param>
        [Insert(typeof (RelatedDocDTO))]
        public void InsertRelatedDoc(RelatedDocDTO relatedDoc)
        {
            var newRelatedDoc = RelatedDocFactory.CreateRelatedDoc(relatedDoc.SourceId, relatedDoc.DocumentId,
                relatedDoc.DocumentName);

            _relatedDocRepository.Add(newRelatedDoc);
        }

        /// <summary>
        ///     更新RelatedDocDTO
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO</param>
        [Update(typeof (RelatedDocDTO))]
        public void ModifyRelatedDoc(RelatedDocDTO relatedDoc)
        {
            var updateRelatedDoc =
                _relatedDocRepository.GetFiltered(t => t.Id == relatedDoc.Id)
                    .FirstOrDefault();
            //获取需要更新的对象。
            var current = RelatedDocFactory.CreateRelatedDoc(relatedDoc.SourceId, relatedDoc.DocumentId,
                relatedDoc.DocumentName);

            _relatedDocRepository.Merge(updateRelatedDoc, current);
        }

        /// <summary>
        ///     删除RelatedDocDTO
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO</param>
        [Delete(typeof (RelatedDocDTO))]
        public void DeleteRelatedDoc(RelatedDocDTO relatedDoc)
        {
            var delRelatedDoc =
                _relatedDocRepository.GetFiltered(t => t.Id == relatedDoc.Id)
                    .FirstOrDefault();
            //获取需要删除的对象。
            _relatedDocRepository.Remove(delRelatedDoc); //删除RelatedDocDTO。
        }

        #endregion
    }
}