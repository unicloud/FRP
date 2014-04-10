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
            //var newEngineXmlSetting = XmlSettingFactory.CreateEngineXmlSetting();
            //var date = DateTime.Now.Date;
            //var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            //newEngineXmlSetting.SetInvoiceNumber(seq);
            //XmlSettingFactory.SetXmlSetting(newEngineXmlSetting, engineXmlSetting.SerialNumber, 
            //    engineXmlSetting.InvoideCode, engineXmlSetting.InvoiceDate, engineXmlSetting.SupplierName, engineXmlSetting.SupplierId,
            //    engineXmlSetting.InvoiceValue, engineXmlSetting.PaidAmount, engineXmlSetting.OperatorName,
            //    engineXmlSetting.Reviewer, engineXmlSetting.Status, engineXmlSetting.CurrencyId, engineXmlSetting.DocumentName, engineXmlSetting.DocumentId);
            //if (engineXmlSetting.XmlSettingLines != null)
            //{
            //    foreach (var XmlSettingLine in engineXmlSetting.XmlSettingLines)
            //    {
            //        var newXmlSettingLine = XmlSettingFactory.CreateXmlSettingLine();
            //        XmlSettingFactory.SetXmlSettingLine(newXmlSettingLine, XmlSettingLine.MaintainItem, XmlSettingLine.ItemName, XmlSettingLine.UnitPrice,
            //            XmlSettingLine.Amount, XmlSettingLine.Note);
            //        newEngineXmlSetting.XmlSettingLines.Add(newXmlSettingLine);
            //    }
            //}
            //_invoiceRepository.Add(newEngineXmlSetting);
        }


        /// <summary>
        ///     更新配置相关的xml。
        /// </summary>
        /// <param name="xmlSetting">配置相关的xmlDTO。</param>
        [Update(typeof(XmlSettingDTO))]
        public void ModifyXmlSetting(XmlSettingDTO xmlSetting)
        {
            //var updateEngineXmlSetting =
            //    _invoiceRepository.Get(engineXmlSetting.EngineXmlSettingId); //获取需要更新的对象。
            //XmlSettingFactory.SetXmlSetting(updateEngineXmlSetting, engineXmlSetting.SerialNumber, 
            //    engineXmlSetting.InvoideCode, engineXmlSetting.InvoiceDate, engineXmlSetting.SupplierName, engineXmlSetting.SupplierId,
            //    engineXmlSetting.InvoiceValue, engineXmlSetting.PaidAmount, engineXmlSetting.OperatorName,
            //   engineXmlSetting.Reviewer, engineXmlSetting.Status, engineXmlSetting.CurrencyId, engineXmlSetting.DocumentName, engineXmlSetting.DocumentId);
            //UpdateXmlSettingLines(engineXmlSetting.XmlSettingLines, updateEngineXmlSetting);
            //_invoiceRepository.Modify(updateEngineXmlSetting);
        }

        /// <summary>
        ///     删除配置相关的xml。
        /// </summary>
        /// <param name="xmlSetting">配置相关的xmlDTO。</param>
        [Delete(typeof(XmlSettingDTO))]
        public void DeleteXmlSetting(XmlSettingDTO xmlSetting)
        {
            //var deleteEngineXmlSetting =
            //    _invoiceRepository.Get(engineXmlSetting.EngineXmlSettingId); //获取需要删除的对象。
            //UpdateXmlSettingLines(new List<XmlSettingLineDTO>(), deleteEngineXmlSetting);
            //_invoiceRepository.Remove(deleteEngineXmlSetting); //删除配置相关的xml。
        }
        #endregion
    }
}
