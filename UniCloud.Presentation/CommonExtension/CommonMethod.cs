#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 11:04:45
// 文件名：CommonMethod
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 11:04:45
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.CommonExtension
{
    public class CommonMethod
    {
        /// <summary>
        /// 显示弹出窗体
        /// </summary>
        /// <param name="radwindow"></param>
        public void ShowRadWindow(RadWindow radwindow)
        {
            radwindow.WindowState = WindowState.Normal;
            radwindow.Show();
        }

        /// <summary>
        /// 将字符串转换成Color的ARGB类型 
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public Color GetColor(string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new Color
                   {
                       A = Convert.ToByte((v >> 24) & 255),
                       R = Convert.ToByte((v >> 16) & 255),
                       G = Convert.ToByte((v >> 8) & 255),
                       B = Convert.ToByte((v >> 0) & 255)
                   };
        }

        /// <summary>
        /// 获取随机的ARGB颜色
        /// </summary>
        /// <returns></returns>
        public string GetRandomColor()
        {
            var colorlist = new List<string>
                            {
                                "#FFE51400",
                                "#FF339933",
                                "#FF1BA1E2",
                                "#FFF09609",
                                "#FF8CBF26",
                                "#FF00ABA9",
                                "#FFFF0097",
                                "#FFE671B8",
                                "#FF996600",
                                "#FFA200FF"
                            };
            Guid randSeedGuid = Guid.NewGuid();
            var rand = new Random(BitConverter.ToInt32(randSeedGuid.ToByteArray(), 0));
            return colorlist[rand.Next(10)];
        }


        /// <summary>
        ///创建运营飞机RadGridView
        /// </summary>
        /// <returns></returns>
        public RadGridView CreatOperationGridView()
        {
            var rgView = new RadGridView
                         {
                             ShowGroupPanel = true,
                             AutoGenerateColumns = false,
                             IsReadOnly = true,
                             Name = "aircraftDetail",
                             RowIndicatorVisibility = Visibility.Collapsed,
                             IsFilteringAllowed = true
                         };

            var gvColumn1 = new GridViewDataColumn
                            {
                                Header = "注册号",
                                DataMemberBinding = new System.Windows.Data.Binding("RegNumber")
                            };


            var gvColumn2 = new GridViewDataColumn
                            {
                                Header = "运营权人",
                                DataMemberBinding =
                                    new System.Windows.Data.Binding("ThenAirlineName")
                            };

            var gvColumn3 = new GridViewDataColumn
                            {
                                Header = "所有权人",
                                DataMemberBinding = new System.Windows.Data.Binding("ThenOwnerName")
                            };

            var gvColumn4 = new GridViewDataColumn
                            {
                                Header = "制造商",
                                DataMemberBinding =
                                    new System.Windows.Data.Binding("AircraftType.Manufacturer.Name")
                            };

            var gvColumn5 = new GridViewDataColumn
                            {
                                Header = "座级",
                                DataMemberBinding = new System.Windows.Data.Binding("ThenRegional")
                            };

            var gvColumn6 = new GridViewDataColumn
                            {
                                Header = "机型",
                                DataMemberBinding =
                                    new System.Windows.Data.Binding("ThenAircraftTypeName")
                            };

            var gvColumn7 = new GridViewDataColumn
                            {
                                Header = "引进方式",
                                DataMemberBinding =
                                    new System.Windows.Data.Binding("ThenActionName")
                            };

            var gvColumn8 = new GridViewDataColumn { Header = "出厂日期" };
            var bingding8 = new System.Windows.Data.Binding("FactoryDate") { StringFormat = "yyyy/M/d" };
            gvColumn8.DataMemberBinding = bingding8;


            var gvColumn9 = new GridViewDataColumn
                            {
                                Header = "座位数(座)",
                                DataMemberBinding =
                                    new System.Windows.Data.Binding("ThenSeatingCapacity")
                            };


            var gvColumn10 = new GridViewDataColumn
                             {
                                 Header = "商载量(吨)",
                                 DataMemberBinding =
                                     new System.Windows.Data.Binding("ThenCarryingCapacity")
                             };
            //System.Windows.Data.Binding bingding10 = new System.Windows.Data.Binding("AircraftBusinesses");
            //bingding10.Converter = new SelectedTimeConverter();
            //bingding10.ConverterParameter = "CarryingCapacity";
            //gvColumn10.DataMemberBinding = bingding10;

            var gvColumn11 = new DataPageSerialColumn();
            var dictionary = new ResourceDictionary();
            //dictionary.Source = new Uri("/UniCloud.Infrastructure.Common;component/Resources/CafmStyle.xaml", UriKind.Relative);
            gvColumn11.Header = "序号";
            gvColumn11.CellStyle = (Style)dictionary["style"];


            rgView.Columns.Add(gvColumn11);
            rgView.Columns.Add(gvColumn1);
            rgView.Columns.Add(gvColumn2);
            rgView.Columns.Add(gvColumn3);
            rgView.Columns.Add(gvColumn4);
            rgView.Columns.Add(gvColumn5);
            rgView.Columns.Add(gvColumn6);
            rgView.Columns.Add(gvColumn7);
            rgView.Columns.Add(gvColumn8);
            rgView.Columns.Add(gvColumn9);
            rgView.Columns.Add(gvColumn10);
            return rgView;
        }

        ///// <summary>
        ///// 获取指定时间点的当时飞机信息
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="selecttime"></param>
        ///// <returns></returns>
        //public List<AircraftDataObject> GetAircraftByTime(List<AircraftDataObject> list, DateTime selecttime)
        //{
        //    //    if (list == null)
        //    //        return null;

        //    List<AircraftDataObject> AircraftList = new List<AircraftDataObject>();
        //    //    foreach (var item in list)
        //    //    {
        //    //        //item.ThenSeatingCapacity = 0;
        //    //        //item.ThenCarryingCapacity = 0;
        //    //        //item.ThenRegional = string.Empty;
        //    //        //item.ThenAircraftTypeName = string.Empty;
        //    //        //item.ThenActionName = string.Empty;
        //    //        //item.ThenAirlineName = string.Empty;
        //    //        //item.ThenOwnerName = string.Empty;
        //    //        item.SeatingCapacity = 0;
        //    //        item.CarryingCapacity = 0;
        //    //        item.RegNumber = string.Empty;
        //    //        item.AircraftTypeID = Guid.Empty;
        //    //        item.ImportCategoryID = Guid.Empty;
        //    //        item.AirlinesID = Guid.Empty;
        //    //        item.OwnerID = Guid.Empty;

        //    //        if (aircraftBusinessDataObjectList != null)
        //    //        {
        //    //        var aircraftbusiness = aircraftBusinessDataObjectList.
        //    //        //AircraftBusiness aircraftbusiness = item.AircraftBusinesses.FirstOrDefault(p => p.StartDate <= selecttime && !(p.EndDate != null && p.EndDate < selecttime));

        //    //            //座位
        //    //            item.ThenSeatingCapacity = aircraftbusiness.SeatingCapacity;
        //    //            //商载
        //    //            item.ThenCarryingCapacity = aircraftbusiness.CarryingCapacity;
        //    //            //座级
        //    //            item.ThenRegional = aircraftbusiness.AircraftType.AircraftCategory.Regional;
        //    //            //机型
        //    //            item.ThenAircraftTypeName = aircraftbusiness.AircraftType.Name;
        //    //        }

        //    //        OperationHistory operationhistory = item.OperationHistories.FirstOrDefault(p => p.StartDate <= selecttime && !(p.EndDate != null && p.EndDate < selecttime));
        //    //        if (operationhistory != null)
        //    //        {
        //    //            //引进方式
        //    //            item.ThenActionName = operationhistory.ImportCategory.ActionName;

        //    //            //运营权人名称
        //    //            if (operationhistory.SubOperationCategorys != null && operationhistory.SubOperationCategorys.Any(p => p.StartDate <= selecttime && !(p.EndDate != null && p.EndDate < selecttime)))
        //    //            {
        //    //                SubOperationHistory suboperationhistory = operationhistory.SubOperationCategorys.FirstOrDefault(p => p.StartDate <= selecttime && !(p.EndDate != null && p.EndDate < selecttime));
        //    //                item.ThenAirlineName = suboperationhistory.Airlines.Name;
        //    //            }
        //    //            else
        //    //            {
        //    //                item.ThenAirlineName = operationhistory.Airlines.Name;
        //    //            }
        //    //        }
        //    //        //所有权人
        //    //        OwnershipHistory ownershiphistory = item.OwnershipHistorys.FirstOrDefault(p => p.StartDate <= selecttime && !(p.EndDate != null && p.EndDate < selecttime));
        //    //        if (ownershiphistory != null)
        //    //        {
        //    //            item.ThenOwnerName = ownershiphistory.Owner.Name;
        //    //        }

        //    //        AircraftList.Add(item);
        //    //    }
        //    return AircraftList;
        //}

        /// <summary>
        /// 根据传入的名称创建Binding的对象
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public Telerik.Windows.Controls.ChartView.DataPointBinding CreateBinding(string propertyName)
        {
            var binding = new Telerik.Windows.Controls.ChartView.PropertyNameDataPointBinding
                          {
                              PropertyName = propertyName
                          };
            return binding;
        }


        /// <summary>
        /// 根据传入的控件导出相应的图片
        /// </summary>
        /// <param name="element">导出的控件</param>
        public void ExportToImage(FrameworkElement element)
        {
            var dialog = new SaveFileDialog
                         {
                             DefaultExt = "png",
                             Filter = "Png (*.png)|*.png"
                         };

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    Telerik.Windows.Media.Imaging.ExportExtensions.ExportToImage(
                         element, stream, new Telerik.Windows.Media.Imaging.PngBitmapEncoder());
                }
            }
        }

        ///// <summary>
        ///// 同步计划的运营历史操作
        ///// </summary>
        ///// <param name="planHistory">当前计划</param>
        ///// <param name="tag"></param>
        //public void SyncOperationHistroy(DateTime startDate, PlanHistory planHistory, OperationType tag)
        //{
        //    switch (tag)
        //    {
        //        case OperationType.OperationLastYearPlan:
        //            //找到上个年度的计划，补上当前计划运营历史或者商业数据
        //            var searchPlanHistory = planHistory.PlanAircraft.PlanHistories
        //                 .Where(p => p.Plan.Annual.Year == planHistory.Plan.Annual.Year - 1 && p.PlanAircraft == planHistory.PlanAircraft).FirstOrDefault();
        //            if (searchPlanHistory != null)
        //            {
        //                //如果运营日期在上一执行年度，则需要反向同步
        //                if (startDate.Year <= planHistory.Plan.Annual.Year - 1)
        //                {
        //                    if (planHistory.GetType() == typeof(OperationPlan) && searchPlanHistory.GetType() == typeof(OperationPlan) && (searchPlanHistory as OperationPlan).OperationHistory == null)
        //                    {
        //                        (searchPlanHistory as OperationPlan).OperationHistoryID = (planHistory as OperationPlan).OperationHistoryID;
        //                    }
        //                    if (planHistory.GetType() == typeof(ChangePlan) && searchPlanHistory.GetType() == typeof(ChangePlan) && (searchPlanHistory as ChangePlan).AircraftBusiness == null)
        //                    {
        //                        (searchPlanHistory as ChangePlan).AircraftBusinessID = (planHistory as ChangePlan).AircraftBusinessID;
        //                    }
        //                }
        //                else //如果不需要反向同步的需要,要将可能由于运营时间得改动,有做过反向同步的给取消掉
        //                {
        //                    if (planHistory.GetType() == typeof(OperationPlan) && searchPlanHistory.GetType() == typeof(OperationPlan) && (searchPlanHistory as OperationPlan).OperationHistory != null)
        //                    {
        //                        (searchPlanHistory as OperationPlan).OperationHistoryID = null;
        //                    }
        //                    if (planHistory.GetType() == typeof(ChangePlan) && searchPlanHistory.GetType() == typeof(ChangePlan) && (searchPlanHistory as ChangePlan).AircraftBusiness != null)
        //                    {
        //                        (searchPlanHistory as ChangePlan).AircraftBusinessID = null;
        //                    }
        //                }
        //            }
        //            break;
        //        case OperationType.OperationNextYearPlan:
        //            //找到大于当前年度的计划，补上当前计划运营历史或者商业数据
        //            var searchNextPlanHistories = planHistory.PlanAircraft.PlanHistories
        //                 .Where(p => p.Plan.Annual.Year > planHistory.Plan.Annual.Year && p.PlanAircraft == planHistory.PlanAircraft);
        //            if (searchNextPlanHistories.Any())
        //            {
        //                searchNextPlanHistories.ToList().ForEach
        //                    (
        //                      f =>
        //                      {
        //                          if (planHistory.GetType() == typeof(OperationPlan) && f.GetType() == typeof(OperationPlan) && (f as OperationPlan).OperationHistory == null)
        //                          {
        //                              (f as OperationPlan).OperationHistoryID = (planHistory as OperationPlan).OperationHistoryID;
        //                          }
        //                          if (planHistory.GetType() == typeof(ChangePlan) && f.GetType() == typeof(ChangePlan) && (f as ChangePlan).AircraftBusiness == null)
        //                          {
        //                              (f as ChangePlan).AircraftBusinessID = (planHistory as ChangePlan).AircraftBusinessID;
        //                          }
        //                      }
        //                    );
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
