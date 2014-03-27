#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/02/25，10:02
// 文件名：ConfigGroupQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.ConfigGroupQueries
{
    public class ConfigGroupQuery : IConfigGroupQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        private readonly IBasicConfigGroupRepository _basicConfigGroupRepository;
        private readonly IBasicConfigRepository _basicConfigRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;
        private readonly ISpecialConfigRepository _specialConfigRepository;
        private readonly ITechnicalSolutionRepository _technicalSolutionRepository;

        public ConfigGroupQuery(IQueryableUnitOfWork unitOfWork,
            IBasicConfigGroupRepository basicConfigGroupRepository,
            IBasicConfigRepository basicConfigRepository,
            IContractAircraftRepository contractAircraftRepository,
            ISpecialConfigRepository specialConfigRepository,
            ITechnicalSolutionRepository technicalSolutionRepository)
        {
            _unitOfWork = unitOfWork;
            _basicConfigGroupRepository = basicConfigGroupRepository;
            _basicConfigRepository = basicConfigRepository;
            _contractAircraftRepository = contractAircraftRepository;
            _specialConfigRepository = specialConfigRepository;
            _technicalSolutionRepository = technicalSolutionRepository;
        }

        /// <summary>
        ///     构型组查询。
        /// </summary>
        /// <returns>构型组DTO集合。</returns>
        public List<ConfigGroupDTO> ConfigGroupDTOQuery()
        {
            var configGroups = new List<ConfigGroupDTO>();
            List<CaDTO> caLists = CaDTOQuery();
            int i = 1;
            foreach (var ca in caLists)
            {
                //产生第一个构型组
                if (configGroups.Count == 0)
                {
                    var configGroup = NewConifgGroup(i,ca);
                    configGroups.Add(configGroup);
                    i++;
                }
                else
                {
                    //如果已经有相应的构型组，在构型组中添加这个合同飞机作为组内飞机
                    if (configGroups.Any(p => p.TechnicalSolutions.Equals(ca.TechnicalSolutions))) //TODO:比较两架飞机的构型情况方法要重写
                    {
                        var configGroup = configGroups.FirstOrDefault(p => p.TechnicalSolutions.Equals(ca.TechnicalSolutions));
                        if (configGroup != null)
                            configGroup.GroupAcs.Add(new GroupAcDTO
                             {
                                 Id = ca.Id,
                                 ConfigGroupId = i,
                                 ContractName = ca.ContractName,
                                 ContractNumber = ca.ContractNumber,
                                 CSCNumber = ca.CSCNumber,
                                 RankNumber = ca.RankNumber,
                                 SerialNumber = ca.SerialNumber,
                             });
                    }
                    else //新增构型组
                    {
                        var configGroup = NewConifgGroup(i, ca);
                        configGroups.Add(configGroup);
                        i++;
                    }
                }
            }

            return configGroups;
        }

        /// <summary>
        /// 查询合同飞机（带技术解决方案集合）
        /// </summary>
        /// <returns></returns>
        public List<CaDTO> CaDTOQuery()
        {
            var dbBcGroup = _basicConfigGroupRepository.GetAll().ToList();
            var dbBc = _basicConfigRepository.GetAll().ToList();
            var dbTs = _technicalSolutionRepository.GetAll().ToList();
            var dbCa = _contractAircraftRepository.GetAll().ToList();
            var dbSpecialConfig = _specialConfigRepository.GetAll().ToList();

            var caDtos = new List<CaDTO>();
            foreach (var ca in dbCa)
            {
                var caDto = new CaDTO()
                {
                    Id = ca.Id,
                    BcGroupId = ca.BasicConfigGroupId,
                    ContractName = ca.ContractName,
                    ContractNumber = ca.ContractNumber,
                    CSCNumber = ca.CSCNumber,
                    IsValid = ca.IsValid,
                    RankNumber = ca.RankNumber,
                    SerialNumber = ca.SerialNumber,
                };
                //通过合同飞机的基本构型组Id,获取飞机的基本构型组
                var bcg = dbBcGroup.FirstOrDefault(p => p.Id == ca.BasicConfigGroupId);
                if (bcg != null)
                {
                    var bcs = dbBc.Where(p => p.BasicConfigGroupId == bcg.Id);
                    //获取基本构型组中基本构型关联的技术解决方案TechnicalSolution，装换为TsDTO,存入CaDTO的List<TsDTO>中
                    foreach (var bc in bcs)
                    {
                        var ts = dbTs.FirstOrDefault(p => p.Id == bc.TsId);
                        if (ts != null)
                        {
                            caDto.TechnicalSolutions.Add(new TsDTO
                            {
                                Id = ts.Id,
                                TsNumber = ts.TsNumber,
                                FiNumber = ts.FiNumber,
                                Position = ts.Position,
                                Type = "基本构型",
                            });
                        }
                    }
                }

                //获取合同飞机的特定选型集合
                var specialConfigs = dbSpecialConfig.Where(p => p.ContractAircraftId == ca.Id).ToList();
                //获取特定选型关联的技术解决方案TechnicalSolution，装换为TsDTO,存入CaDTO的List<TsDTO>中
                foreach (var specialConfig in specialConfigs)
                {
                    var ts = dbTs.FirstOrDefault(p => p.Id == specialConfig.TsId);
                    if (ts != null)
                    {
                        caDto.TechnicalSolutions.Add(new TsDTO
                        {
                            Id = ts.Id,
                            TsNumber = ts.TsNumber,
                            FiNumber = ts.FiNumber,
                            Position = ts.Position,
                            Type = "特定选型",
                        });
                    }
                }
                caDtos.Add(caDto);
            }
            return caDtos;
        }

        /// <summary>
        /// 创建构型组
        /// </summary>
        /// <param name="i"></param>
        /// <param name="ca"></param>
        /// <returns></returns>
        public ConfigGroupDTO NewConifgGroup(int i, CaDTO ca)
        {
            var configGroup = new ConfigGroupDTO()
            {
                Id = i,
                GroupName = "构型组" + i,
                GroupNo = "Engine" + i,
                TechnicalSolutions = ca.TechnicalSolutions,
            };
            configGroup.GroupAcs.Add(new GroupAcDTO
            {
                Id = ca.Id,
                ConfigGroupId = i,
                ContractName = ca.ContractName,
                ContractNumber = ca.ContractNumber,
                CSCNumber = ca.CSCNumber,
                RankNumber = ca.RankNumber,
                SerialNumber = ca.SerialNumber,
            });
            return configGroup;
        }
    }
}