#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/25 16:38:49
// 文件名：CompareScnVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/25 16:38:49
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(CompareScnVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CompareScnVm
    {

    }
}
