#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 22:03:18
// 文件名：RelatedDocAppService
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
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.RelatedDocQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.RelatedDocServices
{
    /// <summary>
    ///     关联文档服务实现
    /// </summary>
    [LogAOP]
    public class RelatedDocAppService : ContextBoundObject, IRelatedDocAppService
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
        ///     新增RelatedDocDTO。
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO。</param>
        [Insert(typeof (RelatedDocDTO))]
        public void InsertRelatedDoc(RelatedDocDTO relatedDoc)
        {
            RelatedDoc newRelatedDoc = RelatedDocFactory.CreateRelatedDoc(relatedDoc.SourceId, relatedDoc.DocumentId,
                relatedDoc.DocumentName);

            _relatedDocRepository.Add(newRelatedDoc);
        }

        /// <summary>
        ///     更新RelatedDocDTO。
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO。</param>
        [Update(typeof (RelatedDocDTO))]
        public void ModifyRelatedDoc(RelatedDocDTO relatedDoc)
        {
            RelatedDoc updateRelatedDoc =
                _relatedDocRepository.GetFiltered(t => t.Id == relatedDoc.Id)
                    .FirstOrDefault();
            //获取需要更新的对象。
            RelatedDoc current = RelatedDocFactory.CreateRelatedDoc(relatedDoc.SourceId, relatedDoc.DocumentId,
                relatedDoc.DocumentName);

            _relatedDocRepository.Merge(updateRelatedDoc, current);
        }

        /// <summary>
        ///     删除RelatedDocDTO。
        /// </summary>
        /// <param name="relatedDoc">RelatedDocDTO。</param>
        [Delete(typeof (RelatedDocDTO))]
        public void DeleteRelatedDoc(RelatedDocDTO relatedDoc)
        {
            RelatedDoc delRelatedDoc =
                _relatedDocRepository.GetFiltered(t => t.Id == relatedDoc.Id)
                    .FirstOrDefault();
            //获取需要删除的对象。
            _relatedDocRepository.Remove(delRelatedDoc); //删除RelatedDocDTO。
        }

        #endregion
    }
}