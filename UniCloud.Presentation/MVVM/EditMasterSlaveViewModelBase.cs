#region

using Microsoft.Practices.Prism.Commands;

#endregion

namespace UniCloud.Presentation.MVVM
{
    /// <summary>
    ///     编辑界面中包含主从表的修改。
    /// </summary>
    public abstract class EditMasterSlaveViewModelBase : EditMasterViewModelBase
    {
        #region Private Methods

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AddSlaveEntityCommand = new DelegateCommand<object>(OnAddSlaveEntityExecute, CanAddSlaveEntityExecute);
            RemoveSlaveEntityCommand = new DelegateCommand<object>(OnRemoveSlaveEntityExecute,
                                                                   CanRemoveSlaveEntityExecute);
            ModefySlaveEntityCommand = new DelegateCommand<object>(OnModefySlaveEntityExecute,
                                                                   CanModefySlaveEntityExecute);
        }

        #endregion

        #region Ctor

        protected EditMasterSlaveViewModelBase()
        {
            InitializerCommand(); //初始化执行命令。
        }

        #endregion

        #region Command

        #region 新增子表数据

        /// <summary>
        ///     新增子表命令。
        /// </summary>
        public DelegateCommand<object> AddSlaveEntityCommand { get; set; }

        /// <summary>
        ///     执行新增子表命令。
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnAddSlaveEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断新增子表命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增子表命令是否可用。</returns>
        public virtual bool CanAddSlaveEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除子表

        /// <summary>
        ///     删除子表命令
        /// </summary>
        public DelegateCommand<object> RemoveSlaveEntityCommand { get; set; }

        /// <summary>
        ///     执行删除子表命令
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnRemoveSlaveEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断删除子表命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除子表命令是否可用。</returns>
        public virtual bool CanRemoveSlaveEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 修改子表

        /// <summary>
        ///     修改子表命令。
        /// </summary>
        public DelegateCommand<object> ModefySlaveEntityCommand { get; set; }

        /// <summary>
        ///     执行修改子表命令。
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnModefySlaveEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断修改子表命令是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除子表命令是否可用</returns>
        public bool CanModefySlaveEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion
    }
}