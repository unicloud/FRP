#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/20，15:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainContractAgg
{
    /// <summary>
    ///     维修合同工厂
    /// </summary>
    public static class MaintainContractFactory
    {
        /// <summary>
        ///     新增发动机维修合同
        /// </summary>
        public static EngineMaintainContract CreateEngineMaintainContract()
        {
            var engineMaintainContract = new EngineMaintainContract
            {
                CreateDate = DateTime.Now
            };
            engineMaintainContract.GenerateNewIdentity();
            return engineMaintainContract;
        }

        /// <summary>
        ///     新增APU维修合同
        /// </summary>
        public static APUMaintainContract CreateApuMaintainContract()
        {
            var apuMaintainContract = new APUMaintainContract
            {
                CreateDate = DateTime.Now
            };
            apuMaintainContract.GenerateNewIdentity();
            return apuMaintainContract;
        }

        /// <summary>
        ///     新增起落架维修合同
        /// </summary>
        public static UndercartMaintainContract CreateUndercartMaintainContract()
        {
            var undercartMaintainContract = new UndercartMaintainContract
            {
                CreateDate = DateTime.Now
            };
            undercartMaintainContract.GenerateNewIdentity();
            return undercartMaintainContract;
        }

        /// <summary>
        ///     设置维修合同属性
        /// </summary>
        /// <param name="maintainContract">维修合同</param>
        /// <param name="name">合同名称</param>
        /// <param name="number">合同号</param>
        /// <param name="signatory">签约对象</param>
        /// <param name="signDate">签约时间</param>
        /// <param name="abstractContent">摘要</param>
        /// <param name="signatoryId">签约对象ID</param>
        /// <param name="documentId">文档ID</param>
        /// <param name="documentName">文档名字</param>
        public static void SetMaintainContract(MaintainContract maintainContract, string name, string number,
            string signatory, DateTime signDate, string abstractContent, int signatoryId, Guid documentId, string documentName)
        {
            maintainContract.Name = name;
            maintainContract.Number = number;
            maintainContract.Signatory = signatory;
            maintainContract.SignDate = signDate;
            maintainContract.SignatoryId = signatoryId;
            maintainContract.Abstract = abstractContent;
            maintainContract.DocumentId = documentId;
            maintainContract.DocumentName = documentName;
            if (maintainContract is EngineMaintainContract)
            {
            }
            else if (maintainContract is APUMaintainContract)
            {
            }
            else if (maintainContract is UndercartMaintainContract)
            {
            }
        }
    }
}