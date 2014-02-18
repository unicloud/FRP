#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 10:10:40

// 文件名：Aircraft
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AircraftAgg
{
    /// <summary>
    /// Aircraft聚合根。
    /// 运营飞机
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Aircraft()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; protected set; }

        /// <summary>
        ///     序列号
        /// </summary>
        public string SerialNumber { get; protected set; }

        /// <summary>
        ///     运营状态
        /// </summary>
        public bool IsOperation { get; protected set; }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion

    }
}

