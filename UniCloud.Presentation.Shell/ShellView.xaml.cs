#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/09，22:11
// 文件名：ShellView.xaml.cs
// 程序集：UniCloud.Presentation.Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Shell
{
    [Export]
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
            ViewModel = new ShellViewModel();
        }

        [Import(typeof(ShellViewModel))]
        public ShellViewModel ViewModel
        {
            get { return DataContext as ShellViewModel; }
            set { DataContext = value; }
        }
    }
}