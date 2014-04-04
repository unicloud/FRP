#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，13:01
// 文件名：EngineLease.xaml.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class EngineLease
    {
        public EngineLease()
        {
            InitializeComponent();
        }

        [Import(typeof (EngineLeaseVM))]
        public EngineLeaseVM ViewModel
        {
            get { return DataContext as EngineLeaseVM; }
            set { DataContext = value; }
        }
    }
}