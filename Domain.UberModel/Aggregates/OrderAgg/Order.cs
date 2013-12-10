#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.CurrencyAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    /// </summary>
    public abstract class Order : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ContractContent> _contractContents;
        private HashSet<OrderLine> _lines;

        #endregion

        #region 属性

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; internal set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; private set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///     撤销日期
        /// </summary>
        public DateTime? RepealDate { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public OrderStatus Status { get; private set; }

        /// <summary>
        ///     合同文档检索ID
        /// </summary>
        public Guid ContractDocGuid { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     交易ID
        /// </summary>
        public int TradeId { get; private set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; private set; }

        /// <summary>
        ///     联系人ID
        /// </summary>
        public int? LinkmanId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     交易
        /// </summary>
        public virtual Trade Trade { get; private set; }

        /// <summary>
        ///     币种
        /// </summary>
        public virtual Currency Currency { get; private set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public virtual Linkman Linkman { get; private set; }

        /// <summary>
        ///     订单行
        /// </summary>
        public virtual ICollection<OrderLine> OrderLines
        {
            get { return _lines ?? (_lines = new HashSet<OrderLine>()); }
            set { _lines = new HashSet<OrderLine>(value); }
        }

        /// <summary>
        ///     合同分解内容
        /// </summary>
        public virtual ICollection<ContractContent> ContractContents
        {
            get { return _contractContents ?? (_contractContents = new HashSet<ContractContent>()); }
            set { _contractContents = new HashSet<ContractContent>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置合同编号
        /// </summary>
        /// <param name="seq">流水号</param>
        public void SetContractNumber(int seq)
        {
            if (seq < 1)
            {
                throw new ArgumentException("流水号参数为空！");
            }

            var date = DateTime.Now;
            ContractNumber = string.Format("{0:yyyyMMdd}{1}", date, seq.ToString("D2"));
        }

        /// <summary>
        ///     设置合同编号
        /// </summary>
        /// <param name="contractNumber">合同编号</param>
        public void SetTradeNumber(string contractNumber)
        {
            if (string.IsNullOrWhiteSpace(contractNumber))
            {
                throw new ArgumentException("合同编号参数为空！");
            }

            ContractNumber = contractNumber;
        }

        /// <summary>
        ///     添加合同分解内容
        /// </summary>
        /// <param name="doc">合同分解内容</param>
        /// <returns></returns>
        public ContractContent AddNewContractContent(byte[] doc)
        {
            var contractContent = new ContractContent
            {
                OrderId = Id,
                ContentDoc = doc
            };

            contractContent.GenerateNewIdentity();
            ContractContents.Add(contractContent);

            return contractContent;
        }

        /// <summary>
        ///     设置完成
        /// </summary>
        public void SetCompleted()
        {
            // TODO：待完善
            IsCompleted = true;
        }

        /// <summary>
        ///     设置订单状态
        /// </summary>
        /// <param name="status">订单状态</param>
        public void SetOrderStatus(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.草稿:
                    Status = OrderStatus.草稿;
                    break;
                case OrderStatus.待审核:
                    Status = OrderStatus.待审核;
                    break;
                case OrderStatus.已审核:
                    Status = OrderStatus.已审核;
                    IsValid = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置交易
        /// </summary>
        /// <param name="trade">交易</param>
        public void SetTrade(Trade trade)
        {
            if (trade == null || trade.IsTransient())
            {
                throw new ArgumentException("交易参数为空！");
            }

            Trade = trade;
            TradeId = trade.Id;
        }

        /// <summary>
        ///     设置币种
        /// </summary>
        /// <param name="currency">币种</param>
        public void SetCurrency(Currency currency)
        {
            if (currency == null || currency.IsTransient())
            {
                throw new ArgumentException("币种参数为空！");
            }

            Currency = currency;
            CurrencyId = currency.Id;
        }

        /// <summary>
        ///     设置联系人
        /// </summary>
        /// <param name="linkman">联系人</param>
        public void SetLinkman(Linkman linkman)
        {
            if (linkman == null || linkman.IsTransient())
            {
                throw new ArgumentException("联系人参数为空！");
            }

            Linkman = linkman;
            LinkmanId = linkman.Id;
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}