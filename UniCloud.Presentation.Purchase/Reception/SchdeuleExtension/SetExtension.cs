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

using System.Windows.Media;
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
                if (categories == null)
                {
                    categories = new CategoryCollection();
                    categories.Add(new Category("已完成", new SolidColorBrush(Colors.Green)));
                    categories.Add(new Category("正在进行中…", new SolidColorBrush(Colors.Brown)));
                    categories.Add(new Category("未启动", new SolidColorBrush(Colors.Gray)));
                }
                return categories;
            }
        }

        public TimeMarkerCollection TimeMarkers
        {
            get
            {
                if (timeMarkers == null)
                {
                    timeMarkers = new TimeMarkerCollection();
                    timeMarkers.Add(new TimeMarker("高级别", new SolidColorBrush(Colors.Red)));
                    timeMarkers.Add(new TimeMarker("中级别", new SolidColorBrush(Colors.Green)));
                    timeMarkers.Add(new TimeMarker("低级别", new SolidColorBrush(Colors.Gray)));
                }
                return timeMarkers;
            }
        }

        public ResourceTypeCollection WorkGroups
        {
            get
            {
                var resourceType = new ResourceTypeCollection();

                if (workGroups == null)
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