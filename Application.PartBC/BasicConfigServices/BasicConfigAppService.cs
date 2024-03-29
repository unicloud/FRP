#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：BasicConfigAppService
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
using UniCloud.Application.PartBC.Query.BasicConfigQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Application.PartBC.BasicConfigServices
{
    /// <summary>
    ///     实现BasicConfig的服务接口。
    ///     用于处理BasicConfig相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class BasicConfigAppService : ContextBoundObject, IBasicConfigAppService
    {
        private readonly IBasicConfigGroupRepository _basicConfigGroupRepository;
        private readonly IBasicConfigQuery _basicConfigQuery;
        private readonly IBasicConfigRepository _basicConfigRepository;
        private readonly IItemRepository _itemRepository;

        public BasicConfigAppService(IBasicConfigQuery basicConfigQuery, IBasicConfigRepository basicConfigRepository,
            IBasicConfigGroupRepository basicConfigGroupRepository,
            IItemRepository itemRepository)
        {
            _basicConfigQuery = basicConfigQuery;
            _basicConfigRepository = basicConfigRepository;
            _basicConfigGroupRepository = basicConfigGroupRepository;
            _itemRepository = itemRepository;
        }

        #region BasicConfigDTO

        /// <summary>
        ///     获取所有BasicConfig。
        /// </summary>
        public IQueryable<BasicConfigDTO> GetBasicConfigs()
        {
            var queryBuilder =
                new QueryBuilder<BasicConfig>();
            return _basicConfigQuery.BasicConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增BasicConfig。
        /// </summary>
        /// <param name="dto">BasicConfigDTO。</param>
        [Insert(typeof (BasicConfigDTO))]
        public void InsertBasicConfig(BasicConfigDTO dto)
        {
            var item = _itemRepository.Get(dto.ItemId);
            AcConfig parentAcConfig = _basicConfigRepository.Get(dto.ParentId);
            var basicConfigGroup = _basicConfigGroupRepository.Get(dto.BasicConfigGroupId);

            //创建基本构型
            var newBasicConfig = BasicConfigFactory.CreateBasicConfig(dto.Position, dto.Description, item,
                parentAcConfig, basicConfigGroup);
            newBasicConfig.ChangeCurrentIdentity(dto.Id);
            _basicConfigRepository.Add(newBasicConfig);
        }

        /// <summary>
        ///     更新BasicConfig。
        /// </summary>
        /// <param name="dto">BasicConfigDTO。</param>
        [Update(typeof (BasicConfigDTO))]
        public void ModifyBasicConfig(BasicConfigDTO dto)
        {
            var item = _itemRepository.Get(dto.ItemId);
            AcConfig parentAcConfig = _basicConfigRepository.Get(dto.ParentId);

            //获取需要更新的对象
            var updateBasicConfig = _basicConfigRepository.Get(dto.Id);

            if (updateBasicConfig != null)
            {
                updateBasicConfig.SetDescription(dto.Description);
                updateBasicConfig.SetItem(item);
                updateBasicConfig.SetParentItem(parentAcConfig);
                updateBasicConfig.SetPosition((Position)dto.Position);
                _basicConfigRepository.Modify(updateBasicConfig);
            }
        }

        /// <summary>
        ///     删除BasicConfig。
        /// </summary>
        /// <param name="dto">BasicConfigDTO。</param>
        [Delete(typeof (BasicConfigDTO))]
        public void DeleteBasicConfig(BasicConfigDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delBasicConfig = _basicConfigRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delBasicConfig != null)
            {
                _basicConfigRepository.Remove(delBasicConfig); //删除基本构型。
            }
        }

        #endregion
    }
}