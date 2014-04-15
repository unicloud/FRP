using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.SupplierServices;
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
            var suppliers = new List<SupplierDTO>();
            var supplierCompanies = new List<SupplierCompanyDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_ksxx";
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var supplierCompany = new SupplierCompanyDTO { Code = odr.GetOracleString(0).ToString() };
                    var supplier = new SupplierDTO
                                   {
                                       Code = odr.GetOracleString(0).ToString(),
                                       Name = odr.GetOracleString(1).ToString(),
                                       ShortName = odr.GetOracleString(2).ToString(),
                                   };
                    if (!odr.GetOracleString(9).IsNull)
                    {
                        supplier.CreateDate = DateTime.Parse(odr.GetOracleString(9).ToString());
                    }
                    suppliers.Add(supplier);
                    supplierCompanies.Add(supplierCompany);
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
            _supplierAppService.SyncSupplierInfo(supplierCompanies, suppliers);
        }

        public void SyncLinkmanInfo()
        {
            var linkmen = new List<LinkmanDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_lxr";
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var linkman = new LinkmanDTO
                    {
                        CustCode = odr.GetOracleString(0).ToString(),
                        Department = odr.GetOracleString(1).ToString(),
                        Name = odr.GetOracleString(2).ToString(),
                    };

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
        }

        public void SyncBankAccountInfo()
        {
            var bankAccounts = new List<BankAccountDTO>();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleNC"].ToString();
            var conn = new OracleConnection(connectionString);//进行连接           
            try
            {
                conn.Open();//打开指定的连接           
                var com = conn.CreateCommand();
                com.CommandText = "select * from v_jdxt_yhzh";
                OracleDataReader odr = com.ExecuteReader();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了      
                {
                    var bankAccount = new BankAccountDTO
                    {
                        CustCode = odr.GetOracleString(0).ToString(),
                        Address = odr.GetOracleString(1).ToString(),

                    };
                    if (odr.GetOracleString(2).IsNull || odr.GetOracleString(3).IsNull)
                    {
                        continue;
                    }
                    bankAccount.Branch = odr.GetOracleString(2).ToString();
                    bankAccount.Account = odr.GetOracleString(3).ToString();
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
        }
    }
}
