#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 15:17:44
// 文件名：DataSync
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.DataService.DataSync
{
    /// <summary>
    /// 数据同步基类
    /// </summary>
    public abstract class DataSync
    {
        /// <summary>
        /// 从Amasis中获取需要同步的数据
        /// </summary>
        public abstract void ImportAmasisData();

        /// <summary>
        /// 从FRP中获取需要同步的数据
        /// </summary>
        public abstract void ImportFrpData();

        public string GetDb2Connection()
        {
            return ConfigurationManager.ConnectionStrings["SqlAmasis"].ToString();
        }

        public string GetSqlServerConnection()
        {
            return ConfigurationManager.ConnectionStrings["SqlFRP"].ToString();
        }

        /// <summary>
        /// 处理数据同步
        /// </summary>
        public abstract void DataSynchronous();
    }
}
