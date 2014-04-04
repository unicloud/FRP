﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 16:11:12
// 文件名：OrganizationAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 16:11:12
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.OrganizationQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;

namespace UniCloud.Application.BaseManagementBC.OrganizationServices
{
    /// <summary>
    /// 实现Organization的服务接口。
    ///  用于处理Organization相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class OrganizationAppService : ContextBoundObject, IOrganizationAppService
    {
        private readonly IOrganizationQuery _organizationQuery;
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationAppService(IOrganizationQuery organizationQuery, IOrganizationRepository organizationRepository)
        {
            _organizationQuery = organizationQuery;
            _organizationRepository = organizationRepository;
        }

        /// <summary>
        /// 获取所有Organization。
        /// </summary>
        public IQueryable<OrganizationDTO> GetOrganizations()
        {
            var queryBuilder =
               new QueryBuilder<Organization>();
            return _organizationQuery.OrganizationsQuery(queryBuilder);
        }

        /// <summary>
        ///     新增Organization。
        /// </summary>
        /// <param name="organization">OrganizationDTO。</param>
        [Insert(typeof(OrganizationDTO))]
        public void InsertOrganization(OrganizationDTO organization)
        {
            var newOrganization = OrganizationFactory.CreateOrganization(organization.Code, organization.Name,
                organization.Sort, organization.Description, organization.IsValid);
            _organizationRepository.Add(newOrganization);
        }

        public void SyncOrganizationInfo(List<OrganizationDTO> organizations)
        {
            foreach (var organization in organizations)
            {
                Expression<Func<Organization, bool>> condition = p => p.Code == organization.Code;

                var tempOrganization = _organizationRepository.GetOrganization(condition);
                if (tempOrganization != null)
                {
                    OrganizationFactory.SetOrganization(tempOrganization, organization.Code, organization.Name,
                         organization.Sort, organization.Description, organization.IsValid);
                    _organizationRepository.Modify(tempOrganization);
                }
                else
                {
                    InsertOrganization(organization);
                }
            }
            _organizationRepository.UnitOfWork.CommitAndRefreshChanges();
        }
    }
}
