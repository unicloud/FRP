#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/15 17:26:01
// 文件名：MaintainContractQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;

namespace UniCloud.Application.PurchaseBC.Query.ContractQueries
{
    public class MaintainContractQuery : IMaintainContractQuery
    {
        private readonly IMaintainContractRepository _contractRepository;

        public MaintainContractQuery(IMaintainContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        /// <summary>
        ///     发动机维修合同查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机维修合同DTO集合。</returns>
        public IQueryable<EngineMaintainContractDTO> EngineMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query)
        {
            return
                query.ApplyTo(_contractRepository.GetAll().OfType<EngineMaintainContract>()).Select(p => new EngineMaintainContractDTO
                    {
                        EngineMaintainContractId
                            = p.Id,
                        Name = p.Name,
                        Number = p.Number,
                        Signatory = p.Signatory,
                        SignDate = p.SignDate,
                        SignatoryId =
                            p.SignatoryId,
                        Abstract = p.Abstract,
                        CreateDate =
                            p.CreateDate,
                        DocumentId = p.DocumentId,
                        DocumentName = p.DocumentName
                    });
        }


        /// <summary>
        ///     APU维修合同查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>APU维修合同DTO集合。</returns>
        public IQueryable<APUMaintainContractDTO> APUMaintainContractDTOQuery(
           QueryBuilder<MaintainContract> query)
        {
            return
                query.ApplyTo(_contractRepository.GetAll().OfType<APUMaintainContract>()).Select(p => new APUMaintainContractDTO
                                                                                             {
                                                                                                 APUMaintainContractId
                                                                                                     = p.Id,
                                                                                                 Name = p.Name,
                                                                                                 Number = p.Number,
                                                                                                 Signatory = p.Signatory,
                                                                                                 SignDate = p.SignDate,
                                                                                                 SignatoryId =
                                                                                                     p.SignatoryId,
                                                                                                 Abstract = p.Abstract,
                                                                                                 CreateDate =
                                                                                                     p.CreateDate,
                                                                                                 DocumentId = p.DocumentId,
                                                                                                 DocumentName = p.DocumentName
                                                                                             });
        }

        /// <summary>
        ///     起落架维修合同查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>起落架维修合同DTO集合。</returns>
        public IQueryable<UndercartMaintainContractDTO> UndercartMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query)
        {
            return
                query.ApplyTo(_contractRepository.GetAll().OfType<UndercartMaintainContract>())
                     .Select(p => new UndercartMaintainContractDTO
                         {
                             UndercartMaintainContractId
                                 = p.Id,
                             Name = p.Name,
                             Number = p.Number,
                             Signatory = p.Signatory,
                             SignDate = p.SignDate,
                             SignatoryId =
                                 p.SignatoryId,
                             Abstract = p.Abstract,
                             CreateDate =
                                 p.CreateDate,
                             DocumentId = p.DocumentId,
                             DocumentName = p.DocumentName
                         });
        }
    }
}
