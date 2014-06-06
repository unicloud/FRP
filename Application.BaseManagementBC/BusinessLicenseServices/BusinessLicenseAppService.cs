#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 12:03:44
// 文件名：BusinessLicenseAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 12:03:44
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.BusinessLicenseQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.BusinessLicenseServices
{
    /// <summary>
    ///     实现经营证照服务接口。
    ///     用于处理经营证照相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class BusinessLicenseAppService : ContextBoundObject, IBusinessLicenseAppService
    {
        private readonly IBusinessLicenseQuery _businessLicenseQuery;
        private readonly IBusinessLicenseRepository _businessLicenseRepository;

        public BusinessLicenseAppService(IBusinessLicenseQuery businessLicenseQuery,
            IBusinessLicenseRepository businessLicenseRepository)
        {
            _businessLicenseQuery = businessLicenseQuery;
            _businessLicenseRepository = businessLicenseRepository;
        }

        #region BusinessLicenseDTO

        /// <summary>
        ///     获取所有经营证照
        /// </summary>
        /// <returns></returns>
        public IQueryable<BusinessLicenseDTO> GetBusinessLicenses()
        {
            var queryBuilder = new QueryBuilder<BusinessLicense>();
            return _businessLicenseQuery.BusinessLicensesQuery(queryBuilder);
        }

        /// <summary>
        ///     新增经营证照。
        /// </summary>
        /// <param name="businessLicense">经营证照DTO。</param>
        [Insert(typeof (BusinessLicenseDTO))]
        public void InsertBusinessLicense(BusinessLicenseDTO businessLicense)
        {
            BusinessLicense newBusinessLicense = BusinessLicenseFactory.CreateBusinessLicense();
            BusinessLicenseFactory.SetBusinessLicense(newBusinessLicense, businessLicense.Name,
                businessLicense.Description, businessLicense.IssuedUnit, businessLicense.IssuedDate,
                businessLicense.ValidMonths, businessLicense.ExpireDate, businessLicense.State, businessLicense.FileName,
                businessLicense.FileContent);
            _businessLicenseRepository.Add(newBusinessLicense);
        }


        /// <summary>
        ///     更新经营证照。
        /// </summary>
        /// <param name="businessLicense">经营证照DTO。</param>
        [Update(typeof (BusinessLicenseDTO))]
        public void ModifyBusinessLicense(BusinessLicenseDTO businessLicense)
        {
            BusinessLicense updateBusinessLicense = _businessLicenseRepository.Get(businessLicense.Id); //获取需要更新的对象。
            BusinessLicenseFactory.SetBusinessLicense(updateBusinessLicense, businessLicense.Name,
                businessLicense.Description, businessLicense.IssuedUnit, businessLicense.IssuedDate,
                businessLicense.ValidMonths, businessLicense.ExpireDate, businessLicense.State, businessLicense.FileName,
                businessLicense.FileContent);
            _businessLicenseRepository.Modify(updateBusinessLicense);
        }

        /// <summary>
        ///     删除经营证照。
        /// </summary>
        /// <param name="businessLicense">经营证照DTO。</param>
        [Delete(typeof (BusinessLicenseDTO))]
        public void DeleteBusinessLicense(BusinessLicenseDTO businessLicense)
        {
            BusinessLicense deleteBusinessLicense = _businessLicenseRepository.Get(businessLicense.Id); //获取需要删除的对象。
            _businessLicenseRepository.Remove(deleteBusinessLicense); //删除经营证照。
        }

        #endregion
    }
}