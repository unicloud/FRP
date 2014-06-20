#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：ItemAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.ItemQueries;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Application.PartBC.ItemServices
{
    /// <summary>
    ///     实现附件项服务接口。
    ///     用于处理附件项相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ItemAppService : ContextBoundObject, IItemAppService
    {
        private readonly IItemQuery _itemQuery;
        private readonly IItemRepository _itemRepository;

        public ItemAppService(IItemQuery itemQuery, IItemRepository itemRepository)
        {
            _itemQuery = itemQuery;
            _itemRepository = itemRepository;
        }

        #region ItemDTO

        /// <summary>
        ///     获取所有附件项
        /// </summary>
        /// <returns></returns>
        public IQueryable<ItemDTO> GetItems()
        {
            var queryBuilder =
                new QueryBuilder<Item>();
            return _itemQuery.ItemDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     获取机型对应的项的集合
        /// </summary>
        /// <param name="aircraftTypeId"></param>
        /// <returns></returns>
        public List<ItemDTO> GetItemsByAircraftType(Guid aircraftTypeId)
        {
            return _itemQuery.GetItemsByAircraftType(aircraftTypeId);
        }


        /// <summary>
        ///     新增Item。
        /// </summary>
        /// <param name="dto">ItemDTO。</param>
        [Insert(typeof (ItemDTO))]
        public void InsertItem(ItemDTO dto)
        {
            Item newItem = ItemFactory.CreateItem(dto.Name, dto.ItemNo, dto.FiNumber, dto.Description, dto.IsLife);
            newItem.ChangeCurrentIdentity(dto.Id);
            _itemRepository.Add(newItem);
        }

        /// <summary>
        ///     更新Item。
        /// </summary>
        /// <param name="dto">ItemDTO。</param>
        [Update(typeof (ItemDTO))]
        public void ModifyItem(ItemDTO dto)
        {
            Item updateItem = _itemRepository.Get(dto.Id); //获取需要更新的对象。

            if (updateItem != null)
            {
                updateItem.SetDescription(dto.Description);
                updateItem.SetIsLife(dto.IsLife);
                updateItem.SetItemNoOrFiNumber(dto.ItemNo, dto.FiNumber);
                updateItem.SetName(dto.Name);
                _itemRepository.Modify(updateItem);
            }
        }

        /// <summary>
        ///     删除Item。
        /// </summary>
        /// <param name="dto">ItemDTO。</param>
        [Delete(typeof (ItemDTO))]
        public void DeleteItem(ItemDTO dto)
        {
            Item delItem = _itemRepository.Get(dto.Id); //获取需要删除的对象。
            _itemRepository.Remove(delItem); //删除Item。
        }

        #endregion
    }
}