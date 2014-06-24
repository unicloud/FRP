#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 9:14:27
// 文件名：FleetTransferService
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 9:14:27
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.FleetPlanBC.DTO.DataTransfer;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.DocumentAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Security;
using UniCloud.Mail;
using Aircraft = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.Aircraft;
using AircraftBusiness = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.AircraftBusiness;
using ApprovalDoc = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.ApprovalDoc;
using ApprovalHistory = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.ApprovalHistory;
using ChangePlan = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.ChangePlan;
using OperationHistory = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.OperationHistory;
using OperationPlan = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.OperationPlan;
using OwnershipHistory = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.OwnershipHistory;
using Plan = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.Plan;
using PlanAircraft = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.PlanAircraft;
using PlanHistory = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.PlanHistory;
using PlanPublishStatus = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.PlanPublishStatus;
using PlanStatus = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.PlanStatus;
using Request = UniCloud.Application.FleetPlanBC.DTO.DataTransfer.Request;

#endregion

namespace UniCloud.Application.FleetPlanBC.FleetTransferServices
{
    [LogAOP]
    public class FleetTransferService : ContextBoundObject, IFleetTransferService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IApprovalDocRepository _approvalDocRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IMailAddressRepository _mailAddressRepository;
        private readonly IPlanAircraftRepository _planAircraftRepository;
        private readonly IPlanHistoryRepository _planHistoryRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IRequestRepository _requestRepository;

        public FleetTransferService(IAirlinesRepository airlinesRepository,
            IAircraftRepository aircraftRepository,
            IApprovalDocRepository approvalDocRepository,
            IDocumentRepository documentRepository,
            IMailAddressRepository mailAddressRepository,
            IPlanRepository planRepository,
            IPlanAircraftRepository planAircraftRepository,
            IPlanHistoryRepository planHistoryRepository,
            IRequestRepository requestRepository)
        {
            _airlinesRepository = airlinesRepository;
            _aircraftRepository = aircraftRepository;
            _approvalDocRepository = approvalDocRepository;
            _documentRepository = documentRepository;
            _mailAddressRepository = mailAddressRepository;
            _planRepository = planRepository;
            _planAircraftRepository = planAircraftRepository;
            _planHistoryRepository = planHistoryRepository;
            _requestRepository = requestRepository;
        }

        #region 数据传输

        /// <summary>
        ///     传输申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentRequest"></param>
        public bool TransferRequest(Guid currentAirlines, Guid currentRequest)
        {
            // 获取需要发送的对象
            var req = _requestRepository.Get(currentRequest);
            var obj = TransformRequest(req);
            return obj != null && TransferMail(currentAirlines, obj, obj.Title, "Request");
        }

        /// <summary>
        ///     传输计划
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        public bool TransferPlan(Guid currentAirlines, Guid currentPlan)
        {
            // 获取需要发送的对象
            var plan = _planRepository.Get(currentPlan);
            var obj = TransformPlan(plan);
            return obj != null && TransferMail(currentAirlines, obj, obj.Title, "Plan");
        }

        /// <summary>
        ///     传输计划申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <returns></returns>
        public bool TransferPlanAndRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest)
        {
            // 获取需要发送的对象
            return TransferPlan(currentAirlines, currentPlan) && TransferRequest(currentAirlines, currentRequest);
        }

        /// <summary>
        ///     传输计划申请批文（针对指标飞机数据）
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <param name="currentApproval"></param>
        /// <returns></returns>
        public bool TransferApprovalRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest,
            Guid currentApproval)
        {
            // 获取需要发送的对象
            return TransferPlan(currentAirlines, currentPlan) && TransferRequest(currentAirlines, currentRequest) &&
                   TransferApprovalDoc(currentAirlines, currentApproval);
        }

        /// <summary>
        ///     传输批文
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentApprovalDoc"></param>
        public bool TransferApprovalDoc(Guid currentAirlines, Guid currentApprovalDoc)
        {
            // 获取需要发送的对象
            var approvalDoc =
                _approvalDocRepository.Get(currentApprovalDoc);
            var obj = TransformApprovalDoc(approvalDoc);
            return TransferMail(currentAirlines, obj, "批文", "ApprovalDoc");
        }

        /// <summary>
        ///     传输运营历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOperationHistory"></param>
        /// <returns></returns>
        public bool TransferOperationHistory(Guid currentAirlines, Guid currentOperationHistory)
        {
            // 获取需要发送的对象
            var operationHistory =
                _aircraftRepository.GetPh(currentOperationHistory);
            var obj = TransformOperationHistory(operationHistory);
            return TransferMail(currentAirlines, obj, "运营历史", "OperationHistory");
        }


        /// <summary>
        ///     传输商业数据
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentAircraftBusiness"></param>
        public bool TransferAircraftBusiness(Guid currentAirlines, Guid currentAircraftBusiness)
        {
            // 获取需要发送的对象
            var aircraftBusiness =
                _aircraftRepository.GetAb(currentAircraftBusiness);
            var obj = TransformAircraftBusiness(aircraftBusiness);
            return TransferMail(currentAirlines, obj, "商业数据", "AircraftBusiness");
        }

        /// <summary>
        ///     传输所有权历史
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOwnershipHistory"></param>
        public bool TransferOwnershipHistory(Guid currentAirlines, Guid currentOwnershipHistory)
        {
            // 获取需要发送的对象
            var ownershipHistory =
                _aircraftRepository.GetOh(currentOwnershipHistory);
            var obj = TransformOwnershipHistory(ownershipHistory);
            return TransferMail(currentAirlines, obj, "所有权历史", "OwnershipHistory");
        }

        public bool TransferPlanHistory(Guid currentAirlines, Guid currentPlanHistory)
        {
            //获取计划历史
            var dbPlanHistory =
                _planHistoryRepository.Get(currentPlanHistory);
            var planHistory = TransformPlanHistory(dbPlanHistory);
            if (planHistory != null && planHistory.GetType() != typeof (OperationPlan))
            {
                //获取商业数据Id
                var changePlan = planHistory as ChangePlan;
                if (changePlan != null)
                {
                    var aircraftBusinessId = changePlan.AircraftBusinessID;
                    //获取商业数据
                    var dbAirBusiness =
                        _aircraftRepository.GetAb(aircraftBusinessId);
                    var airBusiness = TransformAircraftBusiness(dbAirBusiness);
                    //发送数据
                    return TransferMail(currentAirlines, planHistory, "计划历史", "PlanHistory")
                           && TransferMail(currentAirlines, airBusiness, "商业数据", "AircraftBusinesses");
                }
            }
            else
            {
                //新建飞机时，在新建运营计划的同时新建商业数据，发送的同时发送商业数据
                //获取运营计划ID
                var operationPlan = planHistory as OperationPlan;
                if (operationPlan != null)
                {
                    var operationHistoryId = operationPlan.OperationHistoryID;
                    //获取运营历史
                    var dbOperationHistory =
                        _aircraftRepository.GetPh(operationHistoryId);
                    if (dbOperationHistory != null)
                    {
                        var operationHistory = TransformOperationHistory(dbOperationHistory);
                        var dbAircraft =
                            _aircraftRepository.Get(operationHistory.AircraftID);
                        if (dbAircraft.AircraftBusinesses.Count == 1)
                        {
                            var aircraftBusiness =
                                dbAircraft.AircraftBusinesses.FirstOrDefault();
                            if (aircraftBusiness != null)
                            {
                                var aircraftBusinessId = aircraftBusiness.Id;
                                var dbAirBusiness =
                                    _aircraftRepository.GetAb(aircraftBusinessId);
                                var airBusiness = TransformAircraftBusiness(dbAirBusiness);
                                return TransferMail(currentAirlines, planHistory, "计划历史", "PlanHistory")
                                       && TransferMail(currentAirlines, operationHistory, "运营历史", "OperationHistory")
                                       && TransferMail(currentAirlines, airBusiness, "商业数据", "AircraftBusinesses");
                            }
                        }
                        else
                        {
                            return TransferMail(currentAirlines, planHistory, "计划历史", "PlanHistory")
                                   && TransferMail(currentAirlines, operationHistory, "运营历史", "OperationHistory");
                        }
                    }
                }
            }
            return false;
        }

        /// 民航局批文
        /// 发改委批文
        /// <summary>
        ///     通过邮件发送数据文件的方法
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="obj">发送的对象</param>
        /// <param name="mailSubject">发送邮件的主题</param>
        /// <param name="attName">附件的名称</param>
        private bool TransferMail(Guid currentAirlines, object obj, string mailSubject, string attName)
        {
            var caacGuid = Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9");
            var dbSender = _mailAddressRepository.Get(currentAirlines);
            var dbreceiver = _mailAddressRepository.Get(caacGuid);
            if (dbSender == null)
            {
                return false;
            }
            if (dbreceiver == null)
            {
                return false;
            }
            var sender = new System.Net.Mail.MailAddress(dbSender.Address, dbSender.DisplayName);
            var receive = new System.Net.Mail.MailAddress(dbreceiver.Address, dbreceiver.DisplayName);
            //邮件中增加航空公司信息
            var airlines = _airlinesRepository.Get(currentAirlines);
            if (airlines != null)
            {
                mailSubject = airlines.CnName + "发送" + mailSubject;
            }
            var stream = ModelObjToAttachmentStream(obj);
            //发送
            var sm = new SendMail();
            var message = sm.GenMail(sender, receive, stream, mailSubject, attName);
            var blSend = sm.SendNormalMail(TransformMailAddress(dbSender), message);
            if (blSend == -1)
            {
                return sm.SendNormalMail(TransformMailAddress(dbSender), message) == 0;
            }
            return blSend == 0;
        }

        public static Stream ModelObjToAttachmentStream(object obj)
        {
            if (obj != null)
            {
                var ms = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                //加密
                Cryptography.EncryptStream(ref ms);
                return ms;
            }
            return null;
        }

        #endregion

        #region  两套模型数据装换

        private BaseMailAccount TransformMailAddress(MailAddress dbMail)
        {
            var mailAddress = MailAccountHelper.GetMailAccountFromAddr(dbMail.Address, dbMail.DisplayName,
                dbMail.LoginUser, dbMail.LoginPassword,
                dbMail.Pop3Host, dbMail.ReceivePort, dbMail.ReceiveSSL, dbMail.SmtpHost, dbMail.SendPort, dbMail.SendSSL,
                dbMail.StartTLS);
            return mailAddress;
        }

        private Aircraft TransformAircraft(Domain.FleetPlanBC.Aggregates.AircraftAgg.Aircraft dbAircraft)
        {
            var aircraft = new Aircraft
            {
                AircraftID = dbAircraft.Id,
                RegNumber = dbAircraft.RegNumber,
                SerialNumber = dbAircraft.SerialNumber,
                AircraftTypeID = dbAircraft.AircraftTypeId,
                AirlinesID = dbAircraft.AirlinesId,
                CarryingCapacity = dbAircraft.CarryingCapacity,
                CreateDate = dbAircraft.CreateDate,
                ImportDate = dbAircraft.ImportDate,
                ExportDate = dbAircraft.ExportDate,
                FactoryDate = dbAircraft.FactoryDate,
                IsOperation = dbAircraft.IsOperation,
                SeatingCapacity = dbAircraft.SeatingCapacity,
            };
            if (dbAircraft.Supplier != null && dbAircraft.Supplier.SupplierType == SupplierType.国外)
                //SupplierType：0--国外，1--国内
            {
                aircraft.OwnerID = Guid.Parse("76B17B09-A452-41CE-811C-29688595EFB7"); //民航局数据库Owner表中国外供应商外键
            }
            else if (dbAircraft.Supplier != null && dbAircraft.Supplier.SupplierType == SupplierType.国内)
            {
                aircraft.OwnerID = Guid.Parse("5256C5F4-CC0E-49E6-A382-903B031BFC12"); //民航局数据库Owner表中国外供应商外键
            }
            return aircraft;
        }

        private PlanAircraft TransformPlanAircraft(
            Domain.FleetPlanBC.Aggregates.PlanAircraftAgg.PlanAircraft dbPlanAircraft)
        {
            var planAircraft = new PlanAircraft
            {
                PlanAircraftID = dbPlanAircraft.Id,
                AircraftID = dbPlanAircraft.AircraftId,
                AircraftTypeID = dbPlanAircraft.AircraftTypeId,
                AirlinesID = dbPlanAircraft.AirlinesId,
                IsLock = dbPlanAircraft.IsLock,
                IsOwn = dbPlanAircraft.IsOwn,
                Status = (int) dbPlanAircraft.Status,
            };
            return planAircraft;
        }

        private PlanHistory TransformPlanHistory(
            Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg.PlanHistory dbPlanHistory)
        {
            if (dbPlanHistory.GetType() != typeof (Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg.OperationPlan))
            {
                var planHistory = new OperationPlan
                {
                    PlanHistoryID = dbPlanHistory.Id,
                    PlanAircraftID = dbPlanHistory.PlanAircraftId,
                    PlanID = dbPlanHistory.PlanId,
                    ApprovalHistoryID = dbPlanHistory.ApprovalHistoryId,
                    ActionCategoryID = dbPlanHistory.ActionCategoryId,
                    TargetCategoryID = dbPlanHistory.TargetCategoryId,
                    AircraftTypeID = dbPlanHistory.AircraftTypeId,
                    AirlinesID = dbPlanHistory.AirlinesId,
                    PerformAnnualID = dbPlanHistory.PerformAnnualId,
                    CarryingCapacity = dbPlanHistory.CarryingCapacity,
                    SeatingCapacity = dbPlanHistory.SeatingCapacity,
                    IsValid = dbPlanHistory.IsValid,
                    Note = dbPlanHistory.Note,
                    IsSubmit = dbPlanHistory.IsSubmit,
                };
                var operationPlan = dbPlanHistory as Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg.OperationPlan;
                if (operationPlan != null)
                {
                    planHistory.OperationHistoryID = operationPlan.OperationHistoryId;
                }
                if (dbPlanHistory.PlanAircraft != null)
                {
                    planHistory.PlanAircraft = TransformPlanAircraft(dbPlanHistory.PlanAircraft);
                    if (planHistory.PlanAircraft != null && dbPlanHistory.PlanAircraft.Aircraft != null)
                    {
                        planHistory.PlanAircraft.Aircraft = TransformAircraft(dbPlanHistory.PlanAircraft.Aircraft);
                    }
                }
                return planHistory;
            }
            if (dbPlanHistory.GetType() != typeof (Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg.ChangePlan))
            {
                var planHistory = new ChangePlan
                {
                    PlanHistoryID = dbPlanHistory.Id,
                    PlanAircraftID = dbPlanHistory.PlanAircraftId,
                    PlanID = dbPlanHistory.PlanId,
                    ApprovalHistoryID = dbPlanHistory.ApprovalHistoryId,
                    ActionCategoryID = dbPlanHistory.ActionCategoryId,
                    TargetCategoryID = dbPlanHistory.TargetCategoryId,
                    AircraftTypeID = dbPlanHistory.AircraftTypeId,
                    AirlinesID = dbPlanHistory.AirlinesId,
                    PerformAnnualID = dbPlanHistory.PerformAnnualId,
                    CarryingCapacity = dbPlanHistory.CarryingCapacity,
                    SeatingCapacity = dbPlanHistory.SeatingCapacity,
                    IsValid = dbPlanHistory.IsValid,
                    Note = dbPlanHistory.Note,
                    IsSubmit = dbPlanHistory.IsSubmit,
                };
                var changePlan = dbPlanHistory as Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg.ChangePlan;
                if (changePlan != null)
                {
                    planHistory.AircraftBusinessID = changePlan.AircraftBusinessId;
                }
                if (dbPlanHistory.PlanAircraft != null)
                {
                    planHistory.PlanAircraft = TransformPlanAircraft(dbPlanHistory.PlanAircraft);
                    if (planHistory.PlanAircraft != null && dbPlanHistory.PlanAircraft.Aircraft != null)
                    {
                        planHistory.PlanAircraft.Aircraft = TransformAircraft(dbPlanHistory.PlanAircraft.Aircraft);
                    }
                }
                return planHistory;
            }
            return null;
        }

        private Plan TransformPlan(Domain.FleetPlanBC.Aggregates.AircraftPlanAgg.Plan dbPlan)
        {
            var plan = new Plan
            {
                PlanID = dbPlan.Id,
                AirlinesID = dbPlan.AirlinesId,
                AnnualID = dbPlan.AnnualId,
                Title = dbPlan.Title,
                VersionNumber = dbPlan.VersionNumber,
                IsCurrentVersion = dbPlan.IsCurrentVersion,
                IsValid = dbPlan.IsValid,
                CreateDate = dbPlan.CreateDate,
                SubmitDate = dbPlan.SubmitDate,
                IsFinished = dbPlan.IsFinished,
                DocNumber = dbPlan.DocNumber,
                AttachDocFileName = dbPlan.DocName,
                Status = (int) dbPlan.Status,
                PlanStatus = (PlanStatus) dbPlan.Status,
                PublishStatus = (int) dbPlan.PublishStatus,
                PlanPublishStatus = (PlanPublishStatus) dbPlan.PublishStatus,
            };
            var document = _documentRepository.Get(dbPlan.DocumentId);
            if (document != null)
            {
                plan.AttachDoc = document.FileStorage;
            }
            var planHistories =
                _planHistoryRepository.GetAll().Where(p => p.PlanId == dbPlan.Id && p.IsSubmit).ToList(); //只提交需要上报的计划明细
            if (planHistories.Any())
            {
                planHistories.ForEach(p => plan.PlanHistories.Add(TransformPlanHistory(p)));
            }
            return plan;
        }

        private Request TransformRequest(Domain.FleetPlanBC.Aggregates.RequestAgg.Request dbReq)
        {
            var request = new Request
            {
                RequestID = dbReq.Id,
                AirlinesID = dbReq.AirlinesId,
                ApprovalDocID = dbReq.ApprovalDocId,
                Title = dbReq.Title,
                CreateDate = dbReq.CreateDate,
                SubmitDate = dbReq.SubmitDate,
                IsFinished = dbReq.IsFinished,
                DocNumber = dbReq.CaacDocNumber,
                AttachDocFileName = dbReq.CaacDocumentName,
                Status = (int) dbReq.Status,
                ReqStatus = (ReqStatus) dbReq.Status,
            };
            var document = _documentRepository.Get(dbReq.CaacDocumentId);
            if (document != null)
            {
                request.AttachDoc = document.FileStorage;
            }
            dbReq.ApprovalHistories.ToList().ForEach(p =>
            {
                var approvalHistory = new ApprovalHistory
                {
                    ApprovalHistoryID = p.Id,
                    AirlinesID = p.AirlinesId,
                    PlanAircraftID = p.PlanAircraftId,
                    RequestID = p.RequestId,
                    ImportCategoryID = p.ImportCategoryId,
                    SeatingCapacity = p.SeatingCapacity,
                    CarryingCapacity = p.CarryingCapacity,
                    RequestDeliverAnnualID = p.RequestDeliverAnnualId,
                    RequestDeliverMonth = p.RequestDeliverMonth,
                    IsApproved = p.IsApproved,
                    Note = p.Note,
                };
                if (approvalHistory.PlanAircraftID != Guid.Empty)
                {
                    var dbPlanAircraft =
                        _planAircraftRepository.Get(approvalHistory.PlanAircraftID);
                    if (dbPlanAircraft != null)
                    {
                        approvalHistory.PlanAircraft = TransformPlanAircraft(dbPlanAircraft);
                        if (approvalHistory.PlanAircraft != null && dbPlanAircraft.AircraftId != null)
                        {
                            var dbAircraft =
                                _aircraftRepository.Get(dbPlanAircraft.AircraftId);
                            if (dbAircraft != null)
                            {
                                approvalHistory.PlanAircraft.Aircraft = TransformAircraft(dbAircraft);
                            }
                        }
                    }
                }
                request.ApprovalHistories.Add(approvalHistory);
            });
            return request;
        }

        private ApprovalDoc TransformApprovalDoc(Domain.FleetPlanBC.Aggregates.ApprovalDocAgg.ApprovalDoc dbApprovalDoc)
        {
            var approvalDoc = new ApprovalDoc
            {
                ApprovalDocID = dbApprovalDoc.Id,
                DispatchUnitID = Guid.Parse(dbApprovalDoc.DispatchUnitId.ToString()),
                ExamineDate = dbApprovalDoc.CaacExamineDate,
                ApprovalDocFileName = dbApprovalDoc.CaacDocumentName,
                ApprovalNumber = dbApprovalDoc.CaacApprovalNumber,
                Status = (int) dbApprovalDoc.Status,
                OpStatus = (OpStatus) dbApprovalDoc.Status,
            };
            var document = _documentRepository.Get(dbApprovalDoc.CaacDocumentId);
            if (document != null)
            {
                approvalDoc.AttachDoc = document.FileStorage;
            }
            var requests =
                _requestRepository.GetAll().Where(p => p.ApprovalDocId == dbApprovalDoc.Id).ToList();
            if (requests.Any())
            {
                requests.ForEach(p => approvalDoc.Requests.Add(TransformRequest(p)));
            }
            return approvalDoc;
        }

        private OperationHistory TransformOperationHistory(
            Domain.FleetPlanBC.Aggregates.AircraftAgg.OperationHistory dbOperationHistory)
        {
            var operationHistory = new OperationHistory
            {
                OperationHistoryID = dbOperationHistory.Id,
                AirlinesID = dbOperationHistory.AirlinesId,
                AircraftID = dbOperationHistory.AircraftId,
                ImportCategoryID = dbOperationHistory.ImportCategoryId,
                ExportCategoryID = dbOperationHistory.ExportCategoryId,
                RegNumber = dbOperationHistory.RegNumber,
                TechReceiptDate = dbOperationHistory.TechReceiptDate,
                ReceiptDate = dbOperationHistory.ReceiptDate,
                StartDate = dbOperationHistory.StartDate,
                StopDate = dbOperationHistory.StopDate,
                TechDeliveryDate = dbOperationHistory.TechDeliveryDate,
                EndDate = dbOperationHistory.EndDate,
                OnHireDate = dbOperationHistory.OnHireDate,
                Note = dbOperationHistory.Note,
                Status = (int) dbOperationHistory.Status,
                OpStatus = (OpStatus) dbOperationHistory.Status,
            };
            var aircraft =
                _aircraftRepository.Get(dbOperationHistory.AircraftId);
            if (aircraft != null)
            {
                operationHistory.Aircraft = TransformAircraft(aircraft);
                var planAircrafts =
                    _planAircraftRepository.GetAll().Where(p => p.AircraftId == aircraft.Id).ToList();
                if (operationHistory.Aircraft != null && planAircrafts.Any())
                {
                    planAircrafts.ForEach(p => operationHistory.Aircraft.PlanAircrafts.Add(TransformPlanAircraft(p)));
                }
            }
            return operationHistory;
        }

        private AircraftBusiness TransformAircraftBusiness(
            Domain.FleetPlanBC.Aggregates.AircraftAgg.AircraftBusiness dbAircraftBusiness)
        {
            var aircraftBusiness = new AircraftBusiness
            {
                AircraftBusinessID = dbAircraftBusiness.Id,
                AircraftID = dbAircraftBusiness.AircraftId,
                AircraftTypeID = dbAircraftBusiness.AircraftTypeId,
                ImportCategoryID = dbAircraftBusiness.ImportCategoryId,
                SeatingCapacity = dbAircraftBusiness.SeatingCapacity,
                CarryingCapacity = dbAircraftBusiness.CarryingCapacity,
                StartDate = dbAircraftBusiness.StartDate,
                EndDate = dbAircraftBusiness.EndDate,
                Status = (int) dbAircraftBusiness.Status,
                OpStatus = (OpStatus) dbAircraftBusiness.Status,
            };
            var aircraft =
                _aircraftRepository.Get(dbAircraftBusiness.AircraftId);
            if (aircraft != null)
            {
                aircraftBusiness.Aircraft = TransformAircraft(aircraft);
                var planAircrafts =
                    _planAircraftRepository.GetAll().Where(p => p.AircraftId == aircraft.Id).ToList();
                if (planAircrafts.Any())
                {
                    planAircrafts.ForEach(p => aircraftBusiness.Aircraft.PlanAircrafts.Add(TransformPlanAircraft(p)));
                }
            }
            return aircraftBusiness;
        }

        private OwnershipHistory TransformOwnershipHistory(
            Domain.FleetPlanBC.Aggregates.AircraftAgg.OwnershipHistory dbOwnershipHistory)
        {
            var ownershipHistory = new OwnershipHistory
            {
                OwnershipHistoryID = dbOwnershipHistory.Id,
                AircraftID = dbOwnershipHistory.AircraftId,
                StartDate = dbOwnershipHistory.StartDate,
                EndDate = dbOwnershipHistory.EndDate,
                Status = (int) dbOwnershipHistory.Status,
                OpStatus = (OpStatus) dbOwnershipHistory.Status,
            };
            if (dbOwnershipHistory.Supplier.SupplierType == SupplierType.国外) //SupplierType：0--国外，1--国内
            {
                ownershipHistory.OwnerID = Guid.Parse("76B17B09-A452-41CE-811C-29688595EFB7"); //民航局数据库Owner表中国外供应商外键
            }
            else if (dbOwnershipHistory.Supplier.SupplierType == SupplierType.国内)
            {
                ownershipHistory.OwnerID = Guid.Parse("5256C5F4-CC0E-49E6-A382-903B031BFC12"); //民航局数据库Owner表中国外供应商外键
            }
            var aircraft =
                _aircraftRepository.Get(dbOwnershipHistory.AircraftId);
            if (aircraft != null)
            {
                ownershipHistory.Aircraft = TransformAircraft(aircraft);
                var planAircrafts =
                    _planAircraftRepository.GetAll().Where(p => p.AircraftId == aircraft.Id).ToList();
                if (planAircrafts.Any())
                {
                    planAircrafts.ForEach(p => ownershipHistory.Aircraft.PlanAircrafts.Add(TransformPlanAircraft(p)));
                }
            }
            return ownershipHistory;
        }

        #endregion
    }
}