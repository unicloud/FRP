#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，17:11
// 文件名：InitialContainer.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{ 
    /// <summary>
    /// 初始化数据
    /// </summary>
    public class InitialContainer
    {
        public static InitialContainer Instance;

        private readonly List<IInitialData> _initialDatas = new List<IInitialData>();

        /// <summary>
        ///     创建对象
        /// </summary>
        /// <returns></returns>
        public static InitialContainer CreateInitialContainer()
        {
            return Instance ?? (Instance = new InitialContainer());
        }

        /// <summary>
        ///     注册初始化对上对象。
        /// </summary>
        public InitialContainer Register(IInitialData initialData)
        {
            _initialDatas.Add(initialData);
            return this;
        }

        /// <summary>
        ///     初始化数据
        /// </summary>
        public void InitialData()
        {
            _initialDatas.ForEach(p => p.InitialData());
        }
    }
}