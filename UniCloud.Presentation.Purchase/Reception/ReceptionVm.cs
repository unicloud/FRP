#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/24 20:46:22
// 文件名：ReceptionVm
// 版本：V1.0.0
//
// 修改者： 时间： 
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
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    public class ReceptionVm : EditViewModelBase
    {
        #region 声明、初始化
        protected PurchaseData PurchaseDataService;
        private CategoryCollection _categories;
        private ResourceTypeCollection _workGroups;
        protected SchdeuleExtension.ControlExtension ScheduleExtension;

        public ReceptionVm()
        {
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(PurchaseDataService, PurchaseDataService.Suppliers);

            ScheduleExtension = new SchdeuleExtension.ControlExtension();

            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            //GridView单元格值变更
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            //文档
            RemoveAttachCommand = new DelegateCommand<object>(OnRemoveAttach);
            //ScheduleView
            CreateCommand = new DelegateCommand<object>(OnCreated);
            EditCommand = new DelegateCommand<object>(OnEdited);
            DelCommand = new DelegateCommand<object>(OnDeleted);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            PurchaseDataService = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(PurchaseDataService);
        }

        #endregion

        #region 数据

        public override void LoadData()
        {

        }
        #region 公共属性
        /// <summary>
        /// 供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        public CategoryCollection Categories
        {
            get
            {
                if (this._categories == null)
                {
                    this._categories = new CategoryCollection
                    {
                        new Category("未启动", new SolidColorBrush(Colors.Gray)),
                        new Category("正在进行中…", new SolidColorBrush(Colors.Brown)),
                        new Category("已完成", new SolidColorBrush(Colors.Green)),
                    };
                }
                return this._categories;
            }
        }

        public ResourceTypeCollection WorkGroups
        {
            get
            {
                if (this._workGroups == null)
                {
                    _workGroups=new ResourceTypeCollection();
                    var reType = new ResourceType();
                    reType.Resources.Add(new Resource("机务组", "工作组"));
                    reType.Resources.Add(new Resource("机队管理组", "工作组"));
                    reType.Resources.Add(new Resource("后勤组", "工作组"));
                    reType.Resources.Add(new Resource("其他", "工作组"));
                    _workGroups.Add(reType);
                }
                return _workGroups;
            }
        }
        #endregion

        #endregion

        #region 操作

        #region 新建接收项目

        /// <summary>
        ///     新建接收项目
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        protected virtual void OnNew(object obj)
        {

        }

        protected virtual bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除接收项目

        /// <summary>
        ///     删除接收项目
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        protected virtual void OnRemove(object obj)
        {
        }

        protected virtual bool CanRemove(object obj)
        {
            return true;
        }
        #endregion

        #region 新增接收行
        /// <summary>
        ///     新增接收行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        protected virtual void OnAddEntity(object obj)
        {
        }

        protected virtual bool CanAddEntity(object obj)
        {
            return true;
        }
        #endregion

        #region 删除接收行

        /// <summary>
        ///     删除接收行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        protected virtual void OnRemoveEntity(object obj)
        {
        }

        protected virtual bool CanRemoveEntity(object obj)
        {
            return true;
        }
        #endregion

        #region 移除附件

        public DelegateCommand<object> RemoveAttachCommand { get; set; }

        /// <summary>
        ///     移除附件
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnRemoveAttach(object sender)
        {
        }

        #endregion

        #region GridView单元格变更处理
        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        /// GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
        }


        #endregion

        #region ScheduleView新增处理
        public DelegateCommand<object> CreateCommand { set; get; }

        protected virtual void OnCreated(object sender)
        {
        }

        #endregion

        #region ScheduleView删除处理
        public DelegateCommand<object> DelCommand { set; get; }

        protected virtual void OnDeleted(object sender)
        {
        }

        #endregion

        #region ScheduleView编辑处理
        public DelegateCommand<object> EditCommand { set; get; }

        protected virtual void OnEdited(object sender)
        {

        }

        #endregion


        #endregion
    }
}
