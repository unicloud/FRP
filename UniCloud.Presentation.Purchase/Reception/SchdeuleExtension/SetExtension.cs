#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/28 11:08:32
// 文件名：SetExtension
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Purchase.Reception.SchdeuleExtension
{
    public partial class ControlExtension
    {
        private CategoryCollection categories;
        private TimeMarkerCollection timeMarkers;
        private ResourceTypeCollection workGroups; 

        public CategoryCollection Categories
        {
            get
            {
                if (this.categories == null)
                {
                    this.categories = new CategoryCollection();
                    this.categories.Add(new Category("已完成", new SolidColorBrush(Colors.Green)));
                    this.categories.Add(new Category("正在进行中…", new SolidColorBrush(Colors.Brown)));
                    this.categories.Add(new Category("未启动", new SolidColorBrush(Colors.Gray)));
                }
                return this.categories;
            }
        }

        public TimeMarkerCollection TimeMarkers
        {
            get
            {
                if (this.timeMarkers == null)
                {
                    this.timeMarkers = new TimeMarkerCollection();
                    this.timeMarkers.Add(new TimeMarker("高级别", new SolidColorBrush(Colors.Red)));
                    this.timeMarkers.Add(new TimeMarker("中级别" ,new SolidColorBrush(Colors.Green)));
                    this.timeMarkers.Add(new TimeMarker("低级别", new SolidColorBrush(Colors.Gray)));
                }
                return this.timeMarkers;
            }
        }

        public ResourceTypeCollection WorkGroups
        {
            get
            {
                var resourceType = new ResourceTypeCollection();
                
                if (this.workGroups == null)
                {
                    var reType = new ResourceType();
                    reType.Resources.Add(new Resource("机务组", "WorkGroup"));
                    reType.Resources.Add(new Resource("机队管理组", "WorkGroup"));
                    reType.Resources.Add(new Resource("后勤组", "WorkGroup"));
                    resourceType.Add(reType);
                }
                return resourceType;
            }
        }
    }
}
