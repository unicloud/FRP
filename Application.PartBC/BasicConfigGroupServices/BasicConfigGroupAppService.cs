#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupAppService
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
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;

#endregion

namespace UniCloud.Application.PartBC.BasicConfigGroupServices
{
    /// <summary>
    ///     实现BasicConfigGroup的服务接口。
    ///     用于处理BasicConfigGroup相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class BasicConfigGroupAppService : ContextBoundObject, IBasicConfigGroupAppService
    {
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IBasicConfigGroupQuery _basicConfigGroupQuery;
        private readonly IBasicConfigGroupRepository _basicConfigGroupRepository;

        public BasicConfigGroupAppService(IBasicConfigGroupQuery basicConfigGroupQuery,
            IBasicConfigGroupRepository basicConfigGroupRepository,
            IAircraftTypeRepository aircraftTypeRepository)
        {
            _basicConfigGroupQuery = basicConfigGroupQuery;
            _basicConfigGroupRepository = basicConfigGroupRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
        }

        #region BasicConfigGroupDTO

        /// <summary>
        ///     获取所有BasicConfigGroup。
        /// </summary>
        public IQueryable<BasicConfigGroupDTO> GetBasicConfigGroups()
        {
            var queryBuilder =
                new QueryBuilder<BasicConfigGroup>();
            return _basicConfigGroupQuery.BasicConfigGroupDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Insert(typeof (BasicConfigGroupDTO))]
        public void InsertBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            AircraftType aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId); //获取机型

            //创建基本构型组
            BasicConfigGroup newBasicConfigGroup = BasicConfigGroupFactory.CreateBasicConfigGroup(aircraftType,
                dto.Description, dto.GroupNo);

            newBasicConfigGroup.ChangeCurrentIdentity(dto.Id);

            _basicConfigGroupRepository.Add(newBasicConfigGroup);
        }

        /// <summary>
        ///     更新BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Update(typeof (BasicConfigGroupDTO))]
        public void ModifyBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            AircraftType aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId); //获取机型

            //获取需要更新的对象
            BasicConfigGroup dbBasicConfigGroup = _basicConfigGroupRepository.Get(dto.Id);
            if (dbBasicConfigGroup != null)
            {
                BasicConfigGroup updateBasicConfigGroup = BasicConfigGroupFactory.CreateBasicConfigGroup(aircraftType,
                    dto.Description, dto.GroupNo);
                updateBasicConfigGroup.ChangeCurrentIdentity(dbBasicConfigGroup.Id);
                _basicConfigGroupRepository.Modify(updateBasicConfigGroup);
            }
        }

        /// <summary>
        ///     删除BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Delete(typeof (BasicConfigGroupDTO))]
        public void DeleteBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            BasicConfigGroup delBasicConfigGroup = _basicConfigGroupRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delBasicConfigGroup != null)
            {
                _basicConfigGroupRepository.Remove(delBasicConfigGroup); //删除基本构型组。
            }
        }

        #endregion
    }
}