#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，15:02
// 方案：FRP
// 项目：Part
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export]
    public partial class EngineOil
    {
        public EngineOil()
        {
            InitializeComponent();
        }

        [Import(typeof (EngineOilVM))]
        public EngineOilVM ViewModel
        {
            get { return DataContext as EngineOilVM; }
            set { DataContext = value; }
        }
    }
}