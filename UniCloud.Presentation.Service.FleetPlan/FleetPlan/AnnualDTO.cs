#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/31 17:46:24
// 文件名：AnnualDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class AnnualDTO
    {
        partial void OnIsOpenChanged()
        {
            OnPropertyChanged("OpenState");
        }

        public OpenState OpenState
        {
            get
            {
                if (IsOpen) return OpenState.打开;
                return OpenState.关闭;
            }
        }
    }
}
