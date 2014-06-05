#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/5 21:07:38
// 文件名：PnMaintainCtrlDTO
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/5 21:07:38
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;

#endregion

namespace UniCloud.Presentation.Service.Part.Part
{
    public partial class PnMaintainCtrlDTO
    {
        /// <summary>
        /// 维修控制明细
        /// </summary>
        internal XElement XmlContent
        {
            get { return CtrlDetail == null ? null : XElement.Parse(CtrlDetail); }
            set { CtrlDetail = value.ToString(); }
        }
    }
}
