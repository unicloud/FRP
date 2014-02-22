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
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;

#endregion

namespace UniCloud.Application.PartBC.BasicConfigGroupServices
{
    /// <summary>
    /// 实现BasicConfigGroup的服务接口。
    ///  用于处理BasicConfigGroup相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class BasicConfigGroupAppService : IBasicConfigGroupAppService
    {
        private readonly IBasicConfigGroupQuery _basicConfigGroupQuery;
        private readonly IBasicConfigGroupRepository _basicConfigGroupRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly ITechnicalSolutionRepository _technicalSolutionRepository;

        public BasicConfigGroupAppService(IBasicConfigGroupQuery basicConfigGroupQuery,
            IBasicConfigGroupRepository basicConfigGroupRepository,
            IAircraftTypeRepository aircraftTypeRepository,
            ITechnicalSolutionRepository technicalSolutionRepository)
        {
            _basicConfigGroupQuery = basicConfigGroupQuery;
            _basicConfigGroupRepository = basicConfigGroupRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _technicalSolutionRepository = technicalSolutionRepository;
        }

        #region BasicConfigGroupDTO

        /// <summary>
        /// 获取所有BasicConfigGroup。
        /// </summary>
        public IQueryable<BasicConfigGroupDTO> GetBasicConfigGroups()
        {
            var queryBuilder =
               new QueryBuilder<BasicConfigGroup>();
            return _basicConfigGroupQuery.BasicConfigGroupDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Insert(typeof(BasicConfigGroupDTO))]
        public void InsertBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);//获取机型

            //创建基本构型组
            var newBasicConfigGroup = BasicConfigGroupFactory.CreateBasicConfigGroup();
            newBasicConfigGroup.SetAircraftType(aircraftType);
            newBasicConfigGroup.SetDescription(dto.Description);
            newBasicConfigGroup.SetGroupNo(dto.GroupNo);
            newBasicConfigGroup.SetStartDate(dto.StartDate);

            //添加基本构型
            dto.BasicConfigs.ToList().ForEach(basicConfig => InsertBasicConfig(newBasicConfigGroup, basicConfig));

            _basicConfigGroupRepository.Add(newBasicConfigGroup);
        }

        /// <summary>
        ///  更新BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Update(typeof(BasicConfigGroupDTO))]
        public void ModifyBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);//获取机型

            //获取需要更新的对象
            var updateBasicConfigGroup = _basicConfigGroupRepository.Get(dto.Id);

            if (updateBasicConfigGroup != null)
            {
                //更新主表：
                updateBasicConfigGroup.SetAircraftType(aircraftType);
                updateBasicConfigGroup.SetDescription(dto.Description);
                updateBasicConfigGroup.SetGroupNo(dto.GroupNo);
                updateBasicConfigGroup.SetStartDate(dto.StartDate);

                //更新基本构型集合：
                var dtoBasicConfigs = dto.BasicConfigs;
                var basicConfigs = updateBasicConfigGroup.BasicConfigs;
                DataHelper.DetailHandle(dtoBasicConfigs.ToArray(),
                    basicConfigs.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertBasicConfig(updateBasicConfigGroup, i),
                    UpdateBasicConfig,
                    d => _basicConfigGroupRepository.RemoveBasicConfig(d));
            }
            _basicConfigGroupRepository.Modify(updateBasicConfigGroup);
        }

        /// <summary>
        ///  删除BasicConfigGroup。
        /// </summary>
        /// <param name="dto">BasicConfigGroupDTO。</param>
        [Delete(typeof(BasicConfigGroupDTO))]
        public void DeleteBasicConfigGroup(BasicConfigGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delBasicConfigGroup = _basicConfigGroupRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delBasicConfigGroup != null)
            {
                _basicConfigGroupRepository.DeleteBasicConfigGroup(delBasicConfigGroup); //删除基本构型组。
            }
        }

        #region 处理基本构型

        /// <summary>
        ///     插入基本构型
        /// </summary>
        /// <param name="basicConfigGroup">基本构型组</param>
        /// <param name="basicConfigDto">基本构型DTO</param>
        private void InsertBasicConfig(BasicConfigGroup basicConfigGroup, BasicConfigDTO basicConfigDto)
        {
            //获取
            var ts = _technicalSolutionRepository.Get(basicConfigDto.TsId);

            // 添加基本构型
            var newBasicConfig = basicConfigGroup.AddNewBasicConfig();
            newBasicConfig.SetDescription(basicConfigDto.Description);
            newBasicConfig.SetFiNumber(basicConfigDto.FiNumber);
            newBasicConfig.SetItemNo(basicConfigDto.ItemNo);
            newBasicConfig.SetParentAcConfigId(basicConfigDto.ParentId);
            newBasicConfig.SetTechnicalSolution(ts);
        }

        /// <summary>
        ///     更新基本构型
        /// </summary>
        /// <param name="basicConfigDto">基本构型DTO</param>
        /// <param name="basicConfig">基本构型</param>
        private void UpdateBasicConfig(BasicConfigDTO basicConfigDto, BasicConfig basicConfig)
        {
            //获取
            var ts = _technicalSolutionRepository.Get(basicConfigDto.TsId);

            // 更新订单行
            basicConfig.SetDescription(basicConfigDto.Description);
            basicConfig.SetFiNumber(basicConfigDto.FiNumber);
            basicConfig.SetItemNo(basicConfigDto.ItemNo);
            basicConfig.SetParentAcConfigId(basicConfigDto.ParentId);
            basicConfig.SetTechnicalSolution(ts);
        }

        #endregion
        #endregion

    }
}
