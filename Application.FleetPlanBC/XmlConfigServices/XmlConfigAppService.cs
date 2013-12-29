#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 15:27:47
// 文件名：XmlConfigAppServices
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 15:27:47
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.XmlConfigQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.XmlConfigServices
{
    /// <summary>
    ///     实现分析数据相关xml接口。
    ///     用于处于分析数据相关xml相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class XmlConfigAppService:IXmlConfigAppService
    {
        private readonly IXmlConfigQuery _xmlConfigQuery;
        private readonly IXmlConfigRepository _xmlConfigRepository;

         public XmlConfigAppService(IXmlConfigQuery xmlConfigQuery, IXmlConfigRepository xmlConfigRepository)
        {
            _xmlConfigQuery = xmlConfigQuery;
            _xmlConfigRepository = xmlConfigRepository;
        }

        #region XmlConfigDTO
        /// <summary>
        ///     获取所有分析数据相关xml。
        /// </summary>
        /// <returns>所有分析数据相关xml。</returns>
        public IQueryable<XmlConfigDTO> GetXmlConfigs()
        {
            var queryBuilder = new QueryBuilder<XmlConfig>();
            return _xmlConfigQuery.XmlConfigDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增分析数据相关xml。
        /// </summary>
        /// <param name="xmlConfig">分析数据相关xmlDTO。</param>
        [Insert(typeof(XmlConfigDTO))]
        public void InsertXmlConfig(XmlConfigDTO xmlConfig)
        {
            //var newEngineXmlConfig = XmlConfigFactory.CreateEngineXmlConfig();
            //var date = DateTime.Now.Date;
            //var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            //newEngineXmlConfig.SetInvoiceNumber(seq);
            //XmlConfigFactory.SetXmlConfig(newEngineXmlConfig, engineXmlConfig.SerialNumber, 
            //    engineXmlConfig.InvoideCode, engineXmlConfig.InvoiceDate, engineXmlConfig.SupplierName, engineXmlConfig.SupplierId,
            //    engineXmlConfig.InvoiceValue, engineXmlConfig.PaidAmount, engineXmlConfig.OperatorName,
            //    engineXmlConfig.Reviewer, engineXmlConfig.Status, engineXmlConfig.CurrencyId, engineXmlConfig.DocumentName, engineXmlConfig.DocumentId);
            //if (engineXmlConfig.XmlConfigLines != null)
            //{
            //    foreach (var XmlConfigLine in engineXmlConfig.XmlConfigLines)
            //    {
            //        var newXmlConfigLine = XmlConfigFactory.CreateXmlConfigLine();
            //        XmlConfigFactory.SetXmlConfigLine(newXmlConfigLine, XmlConfigLine.MaintainItem, XmlConfigLine.ItemName, XmlConfigLine.UnitPrice,
            //            XmlConfigLine.Amount, XmlConfigLine.Note);
            //        newEngineXmlConfig.XmlConfigLines.Add(newXmlConfigLine);
            //    }
            //}
            //_invoiceRepository.Add(newEngineXmlConfig);
        }


        /// <summary>
        ///     更新分析数据相关xml。
        /// </summary>
        /// <param name="xmlConfig">分析数据相关xmlDTO。</param>
        [Update(typeof(XmlConfigDTO))]
        public void ModifyXmlConfig(XmlConfigDTO xmlConfig)
        {
            //var updateEngineXmlConfig =
            //    _invoiceRepository.Get(engineXmlConfig.EngineXmlConfigId); //获取需要更新的对象。
            //XmlConfigFactory.SetXmlConfig(updateEngineXmlConfig, engineXmlConfig.SerialNumber, 
            //    engineXmlConfig.InvoideCode, engineXmlConfig.InvoiceDate, engineXmlConfig.SupplierName, engineXmlConfig.SupplierId,
            //    engineXmlConfig.InvoiceValue, engineXmlConfig.PaidAmount, engineXmlConfig.OperatorName,
            //   engineXmlConfig.Reviewer, engineXmlConfig.Status, engineXmlConfig.CurrencyId, engineXmlConfig.DocumentName, engineXmlConfig.DocumentId);
            //UpdateXmlConfigLines(engineXmlConfig.XmlConfigLines, updateEngineXmlConfig);
            //_invoiceRepository.Modify(updateEngineXmlConfig);
        }

        /// <summary>
        ///     删除分析数据相关xml。
        /// </summary>
        /// <param name="xmlConfig">分析数据相关xmlDTO。</param>
        [Delete(typeof(XmlConfigDTO))]
        public void DeleteXmlConfig(XmlConfigDTO xmlConfig)
        {
            //var deleteEngineXmlConfig =
            //    _invoiceRepository.Get(engineXmlConfig.EngineXmlConfigId); //获取需要删除的对象。
            //UpdateXmlConfigLines(new List<XmlConfigLineDTO>(), deleteEngineXmlConfig);
            //_invoiceRepository.Remove(deleteEngineXmlConfig); //删除分析数据相关xml。
        }
        #endregion
    }
}
