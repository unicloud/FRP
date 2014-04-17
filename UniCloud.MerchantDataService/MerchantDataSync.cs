using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Domain.Common.Enums;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;

namespace UniCloud.MerchantDataService
{
    public class MerchantDataSync
    {
        private readonly SupplierAppService _supplierAppService;
        public MerchantDataSync()
        {
            IQueryableUnitOfWork unitOfWork = new PurchaseBCUnitOfWork();
            _supplierAppService = new SupplierAppService(new SupplierQuery(unitOfWork), new SupplierRepository(unitOfWork),
                new SupplierRoleRepository(unitOfWork), new SupplierCompanyRepository(unitOfWork), new LinkmanRepository(unitOfWork),
                new BankAccountRepository(unitOfWork), new SupplierCompanyMaterialRepository(unitOfWork));
        }

        public void SyncMerchantInfo()
        {
            Configuration config =  ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            DateTime supplierUpdateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(config.AppSettings.Settings["SupplierUpdateTime"].Value))
            {
                supplierUpdateTime = DateTime.Parse(config.AppSettings.Settings["SupplierUpdateTime"].Value);
            }
            var suppliers = new List<SupplierDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_ksxx t where t.TS > '" + supplierUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")+"'";
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var supplier = new SupplierDTO
                                   {
                                       Code = odr["CUSTCODE"].ToString(), //odr.GetOracleString(0).ToString(),
                                       Name = odr["CUSTNAME"].ToString(), //odr.GetOracleString(1).ToString(),
                                       ShortName = odr["CUSTSHORTNAME"].ToString(), //odr.GetOracleString(2).ToString(),
                                       UpdateDate = DateTime.Parse(odr["TS"].ToString()),
                                   };
                    if (supplier.UpdateDate.CompareTo(supplierUpdateTime)>0)
                    {
                        supplierUpdateTime = supplier.UpdateDate;
                    }
                    if (odr["AREACLNAME"].ToString().Contains("国内"))
                    {
                        supplier.SupplierType = SupplierType.国内.ToString();
                    }
                    else if (odr["AREACLNAME"].ToString().Contains("国外"))
                    {
                        supplier.SupplierType = SupplierType.国外.ToString();
                    }
                    else
                    {
                        continue;
                    }
                    suppliers.Add(supplier);
                }
                odr.Close();//关闭reader.这是一定要写的       
            }
            catch
            {
                //如果发生异常，则提示出错       
            }
            finally
            {
                conn.Close();//关闭打开的连接     
            }
            _supplierAppService.SyncSupplierInfo(suppliers);
            config.AppSettings.Settings["SupplierUpdateTime"].Value = supplierUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void SyncLinkmanInfo()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            DateTime linkmanUpdateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(config.AppSettings.Settings["LinkmanUpdateTime"].Value))
            {
                linkmanUpdateTime = DateTime.Parse(config.AppSettings.Settings["LinkmanUpdateTime"].Value);
            }

            var linkmen = new List<LinkmanDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_lxr t where t.TS > '" + linkmanUpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'"; ;
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var linkman = new LinkmanDTO
                    {
                        CustCode = odr["CUSTCODE"].ToString(), //odr.GetOracleString(0).ToString(),
                        Department = odr["CUSTNAME"].ToString(), //odr.GetOracleString(1).ToString(),
                        Name = odr["LINKMAN1"].ToString(), //odr.GetOracleString(2).ToString(),
                        UpdateDate = DateTime.Parse(odr["TS"].ToString()),
                    };
                    if (linkman.UpdateDate.CompareTo(linkmanUpdateTime) > 0)
                    {
                        linkmanUpdateTime = linkman.UpdateDate;
                    }
                    linkmen.Add(linkman);
                }
                odr.Close();//关闭reader.这是一定要写的       
            }
            catch
            {
                //如果发生异常，则提示出错       
            }
            finally
            {
                conn.Close();//关闭打开的连接     
            }
            _supplierAppService.SyncLinkmanInfo(linkmen);
            config.AppSettings.Settings["LinkmanUpdateTime"].Value = linkmanUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void SyncBankAccountInfo()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            DateTime bankAccountUpdateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(config.AppSettings.Settings["BankAccountUpdateTime"].Value))
            {
                bankAccountUpdateTime = DateTime.Parse(config.AppSettings.Settings["BankAccountUpdateTime"].Value);
            }
            var bankAccounts = new List<BankAccountDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_yhzh t where t.TS > '" + bankAccountUpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'"; ;
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var bankAccount = new BankAccountDTO
                    {
                        CustCode = odr["CUSTCODE"].ToString(), //odr.GetOracleString(0).ToString(),
                        Address = odr["CUSTNAME"].ToString(), //odr.GetOracleString(1).ToString(),
                        UpdateDate = DateTime.Parse(odr["TS"].ToString()),
                    };
                    if (bankAccount.UpdateDate.CompareTo(bankAccountUpdateTime) > 0)
                    {
                        bankAccountUpdateTime = bankAccount.UpdateDate;
                    }
                    if (odr.GetOracleString(2).IsNull || odr.GetOracleString(3).IsNull)
                    {
                        continue;
                    }
                    bankAccount.Branch = odr["BANKDOCNAME"].ToString(); // odr.GetOracleString(2).ToString();
                    bankAccount.Account = odr["ACCOUNTCODE"].ToString(); // odr.GetOracleString(3).ToString();
                    bankAccounts.Add(bankAccount);
                }
                odr.Close();//关闭reader.这是一定要写的       
            }
            catch
            {
                //如果发生异常，则提示出错       
            }
            finally
            {
                conn.Close();//关闭打开的连接     
            }
            _supplierAppService.SyncBankAccountInfo(bankAccounts);
            config.AppSettings.Settings["BankAccountUpdateTime"].Value = bankAccountUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
