#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:28:12
// 文件名：XmlSettingAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:28:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.XmlSettingQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.XmlSettingAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.XmlSettingServices
{
    /// <summary>
    ///     实现配置相关的xml接口。
    ///     用于处于维修发票相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class XmlSettingAppService : ContextBoundObject, IXmlSettingAppService
    {
        private readonly IXmlSettingQuery _xmlSettingQuery;
        private readonly IXmlSettingRepository _xmlSettingRepository;

        public XmlSettingAppService(IXmlSettingQuery xmlSettingQuery, IXmlSettingRepository xmlSettingRepository)
        {
            _xmlSettingQuery = xmlSettingQuery;
            _xmlSettingRepository = xmlSettingRepository;
        }

        #region XmlSettingDTO
        /// <summary>
        ///     获取所有配置相关的xml。
        /// </summary>
        /// <returns>所有配置相关的xml。</returns>
        public IQueryable<XmlSettingDTO> GetXmlSettings()
        {
            var queryBuilder = new QueryBuilder<XmlSetting>();
            return _xmlSettingQuery.XmlSettingDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增配置相关的xml。
        /// </summary>
        /// <param name="xmlSetting">配置相关的xmlDTO。</param>
        [Insert(typeof(XmlSettingDTO))]
        public void InsertXmlSetting(XmlSettingDTO xmlSetting)
        {
            //var newXmlSetting = XmlSettingFactory.CreateXmlSetting();
            //var date = DateTime.Now.Date;
            //var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            //newXmlSetting.SetInvoiceNumber(seq);
            //XmlSettingFactory.SetXmlSetting(newXmlSetting, XmlSetting.SerialNumber, 
            //    XmlSetting.InvoideCode, XmlSetting.InvoiceDate, XmlSetting.SupplierName, XmlSetting.SupplierId,
            //    XmlSetting.InvoiceValue, XmlSetting.PaidAmount, XmlSetting.OperatorName,
            //    XmlSetting.Reviewer, XmlSetting.Status, XmlSetting.CurrencyId, XmlSetting.DocumentName, XmlSetting.DocumentId);
            //if (XmlSetting.XmlSettingLines != null)
            //{
            //    foreach (var XmlSettingLine in XmlSetting.XmlSettingLines)
            //    {
            //        var newXmlSettingLine = XmlSettingFactory.CreateXmlSettingLine();
            //        XmlSettingFactory.SetXmlSettingLine(newXmlSettingLine, XmlSettingLine.MaintainItem, XmlSettingLine.ItemName, XmlSettingLine.UnitPrice,
            //            XmlSettingLine.Amount, XmlSettingLine.Note);
            //        newXmlSetting.XmlSettingLines.Add(newXmlSettingLine);
            //    }
            //}
            //_invoiceRepository.Add(newXmlSetting);
        }


        /// <summary>
        ///     更新配置相关的xml。
        /// </summary>
        /// <param name="xmlSetting">配置相关的xmlDTO。</param>
        [Update(typeof(XmlSettingDTO))]
        public void ModifyXmlSetting(XmlSettingDTO xmlSetting)
        {
            var updateXmlSetting = _xmlSettingRepository.Get(xmlSetting.XmlSettingId); //获取需要更新的对象。
            XmlSettingFactory.SetXmlSetting(updateXmlSetting, xmlSetting.SettingContent);
            _xmlSettingRepository.Modify(updateXmlSetting);
        }

        /// <summary>
        ///     删除配置相关的xml。
        /// </summary>
        /// <param name="xmlSetting">配置相关的xmlDTO。</param>
        [Delete(typeof(XmlSettingDTO))]
        public void DeleteXmlSetting(XmlSettingDTO xmlSetting)
        {
            //var deleteXmlSetting =
            //    _invoiceRepository.Get(XmlSetting.XmlSettingId); //获取需要删除的对象。
            //UpdateXmlSettingLines(new List<XmlSettingLineDTO>(), deleteXmlSetting);
            //_invoiceRepository.Remove(deleteXmlSetting); //删除配置相关的xml。
        }
        #endregion
    }
}
