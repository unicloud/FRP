#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:46:20
// 文件名：IContractEngineAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.ContractEngineServices
{
    public interface IContractEngineAppService
    {

        /// <summary>
        ///     获取所有合同发动机
        /// </summary>
        /// <returns></returns>
        IQueryable<ContractEngineDTO> GetContractEngines();
    }
}
