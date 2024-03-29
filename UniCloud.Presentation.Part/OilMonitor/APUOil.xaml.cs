﻿#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014-07-11，17:07
// 方案：FRP
// 项目：Part
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export]
    public partial class APUOil
    {
        public APUOil()
        {
            InitializeComponent();
        }

        [Import]
        public APUOilVM ViewModel
        {
            get { return DataContext as APUOilVM; }
            set { DataContext = value; }
        }
    }
}