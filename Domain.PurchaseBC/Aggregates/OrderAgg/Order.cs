#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 方案：FRP
// 项目：Domain.PurchaseBC
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
using System.Text;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Domain.PurchaseBC.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    /// </summary>
    public class Order : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ContractContent> _contractContents;
        private HashSet<OrderLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Order()
        {
        }

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
        public string Name { get; private set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; internal set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; internal set; }

        /// <summary>
        ///     撤销日期
        /// </summary>
        public DateTime? RepealDate { get; private set; }

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
        ///     合同文件名
        /// </summary>
        public string ContractName { get; private set; }

        /// <summary>
        ///     合同文档检索ID
        /// </summary>
        public Guid ContractDocGuid { get; private set; }

        /// <summary>
        ///     源GUID
        /// </summary>
        public Guid SourceGuid { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; private set; }

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
        ///     设置合同名称
        /// </summary>
        /// <param name="name">合同名称</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("合同名称参数为空！");
            }

            Name = name;
        }

        /// <summary>
        ///     设置经办人
        /// </summary>
        /// <param name="operatorName">经办人</param>
        public void SetOperatorName(string operatorName)
        {
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentException("经办人参数为空！");
            }

            OperatorName = operatorName;
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
        ///     设置撤销日期
        /// </summary>
        /// <param name="date">撤销日期</param>
        public void SetRepealDate(DateTime date)
        {
            RepealDate = date;
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
        ///     设置交易
        /// </summary>
        /// <param name="id">交易ID</param>
        public void SetTrade(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("交易ID参数为空！");
            }

            TradeId = id;
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
        ///     设置币种
        /// </summary>
        /// <param name="id">币种ID</param>
        public void SetCurrency(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("币种ID参数为空！");
            }

            CurrencyId = id;
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

        /// <summary>
        ///     设置联系人
        /// </summary>
        /// <param name="id">联系人ID</param>
        public void SetLinkman(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("联系人ID参数为空！");
            }

            LinkmanId = id;
        }

        /// <summary>
        ///     设置合同文档
        /// </summary>
        /// <param name="doc">合同文档</param>
        public void SetContractDoc(Document doc)
        {
            if (doc == null || doc.IsTransient())
            {
                throw new ArgumentException("合同文档参数为空！");
            }

            ContractDocGuid = doc.Id;
            ContractName = doc.FileName;
        }

        /// <summary>
        ///     设置合同文档
        /// </summary>
        /// <param name="docId">文档ID</param>
        /// <param name="fileName">文档名称</param>
        public void SetContractDoc(Guid docId, string fileName)
        {
            if (docId == null || docId == Guid.Empty)
            {
                throw new ArgumentException("合同文档ID参数为空！");
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("合同文档名称参数为空！");
            }

            ContractDocGuid = docId;
            ContractName = fileName;
        }

        /// <summary>
        ///     设置源GUID
        /// </summary>
        /// <param name="id">源GUID</param>
        public void SetSourceGuid(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentException("源GUID参数为空！");
            }

            SourceGuid = id;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note))
            {
                throw new ArgumentException("合同说明参数为空！");
            }

            var sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.Date.ToShortDateString());
            sb.AppendLine(note);
            sb.AppendLine();
            sb.Append(Note);

            Note = sb.ToString();
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