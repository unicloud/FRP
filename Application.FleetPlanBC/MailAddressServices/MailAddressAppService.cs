#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：MailAddressAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.MailAddressQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Infrastructure.Security;

#endregion

namespace UniCloud.Application.FleetPlanBC.MailAddressServices
{
    /// <summary>
    ///     实现邮箱账号服务接口。
    ///     用于处理邮箱账号相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class MailAddressAppService : ContextBoundObject, IMailAddressAppService
    {
        private readonly IMailAddressQuery _mailAddressQuery;
        private readonly IMailAddressRepository _mailAddressRepository;

        public MailAddressAppService(IMailAddressQuery mailAddressQuery,
            IMailAddressRepository mailAddressRepository)
        {
            _mailAddressQuery = mailAddressQuery;
            _mailAddressRepository = mailAddressRepository;
        }

        #region MailAddressDTO

        /// <summary>
        ///     获取所有邮箱账号
        /// </summary>
        /// <returns></returns>
        public IQueryable<MailAddressDTO> GetMailAddresses()
        {
            var queryBuilder =
                new QueryBuilder<MailAddress>();
            return _mailAddressQuery.MailAddressDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增邮箱账号。
        /// </summary>
        /// <param name="dto">邮箱账号DTO。</param>
        [Insert(typeof (MailAddressDTO))]
        public void InsertMailAddress(MailAddressDTO dto)
        {
            //创建邮箱账号
            MailAddress newMailAddress = MailAddressFactory.CreateMailAddress();
            newMailAddress.ChangeCurrentIdentity(dto.Id);
            newMailAddress.SetAddress(dto.Address);
            newMailAddress.SetDisplayName(dto.DisplayName);
            string password = dto.LoginPassword;//密码进行加密存储
            newMailAddress.SetLoginPassword(password);
            newMailAddress.SetLoginUser(dto.LoginUser);
            newMailAddress.SetPop3Host(dto.Pop3Host);
            newMailAddress.SetReceivePort(dto.ReceivePort);
            newMailAddress.SetReceiveSSL(dto.ReceiveSSL);
            newMailAddress.SetSendPort(dto.SendPort);
            newMailAddress.SetSendSSL(dto.SendSSL);
            newMailAddress.SetServerType(dto.ServerType);
            newMailAddress.SetSmtpHost(dto.SmtpHost);
            newMailAddress.SetStartTLS(dto.StartTLS);

            _mailAddressRepository.Add(newMailAddress);
        }

        /// <summary>
        ///     更新邮箱账号。
        /// </summary>
        /// <param name="dto">邮箱账号DTO。</param>
        [Update(typeof (MailAddressDTO))]
        public void ModifyMailAddress(MailAddressDTO dto)
        {
            //获取需要更新的对象
            MailAddress updateMailAddress = _mailAddressRepository.Get(dto.Id);

            if (updateMailAddress != null)
            {
                //更新主表：
                updateMailAddress.SetAddress(dto.Address);
                updateMailAddress.SetDisplayName(dto.DisplayName);
                string password = dto.LoginPassword;//密码进行加密存储
                updateMailAddress.SetLoginPassword(password);
                updateMailAddress.SetLoginUser(dto.LoginUser);
                updateMailAddress.SetPop3Host(dto.Pop3Host);
                updateMailAddress.SetReceivePort(dto.ReceivePort);
                updateMailAddress.SetReceiveSSL(dto.ReceiveSSL);
                updateMailAddress.SetSendPort(dto.SendPort);
                updateMailAddress.SetSendSSL(dto.SendSSL);
                updateMailAddress.SetServerType(dto.ServerType);
                updateMailAddress.SetSmtpHost(dto.SmtpHost);
                updateMailAddress.SetStartTLS(dto.StartTLS);
            }
            _mailAddressRepository.Modify(updateMailAddress);
        }

        /// <summary>
        ///     删除邮箱账号。
        /// </summary>
        /// <param name="dto">邮箱账号DTO。</param>
        [Delete(typeof (MailAddressDTO))]
        public void DeleteMailAddress(MailAddressDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            MailAddress delMailAddress = _mailAddressRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delMailAddress != null)
            {
                _mailAddressRepository.Remove(delMailAddress); //删除邮箱账号。
            }
        }

        #endregion
    }
}