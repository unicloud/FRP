using Microsoft.Practices.Prism.Commands;

namespace UniCloud.Presentation.MVVM
{
    /// <summary>
    ///     编辑界面中只包含主表的修改。
    /// </summary>
    public abstract class EditMasterViewModelBase : ViewModelBase
    {
        #region Private Methods

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            SaveCommand = new DelegateCommand<object>(OnSaveExecute, CanSaveExecute); //保存命令。
            AbortCommand = new DelegateCommand<object>(OnAbortExecute, CanAbortExecute); //取消命令。
            AddEntityCommand = new DelegateCommand<object>(OnAddEntityExecute, CanAddEntityExecute); //新增实体。
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntityExecute, CanRemoveEntityExecute); //删除实体。
            ModifyEntityCommand = new DelegateCommand<object>(OnModifyEntityExecute, CanModifyEntityExecute); //修改。
        }

        #endregion

        #region Ctor

        protected EditMasterViewModelBase()
        {
            InitializerCommand(); //初始化执行命令。
        }

        #endregion

        #region Command

        #region 保存

        /// <summary>
        ///     保存命令
        /// </summary>
        public DelegateCommand<object> SaveCommand { get; set; }

        /// <summary>
        ///     执行保存
        /// </summary>
        /// <param name="sender">参数</param>
        public virtual void OnSaveExecute(object sender)
        {
        }

        /// <summary>
        ///     判断保存命令是否可用
        /// </summary>
        /// <param name="sender">参数</param>
        /// <returns>保存命令是否可用</returns>
        public virtual bool CanSaveExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 放弃更改

        /// <summary>
        ///     取消命令。
        /// </summary>
        public DelegateCommand<object> AbortCommand { get; set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnAbortExecute(object sender)
        {
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public virtual bool CanAbortExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 新增主表

        /// <summary>
        ///     新增命令。
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public virtual bool CanAddEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除主表

        /// <summary>
        ///     删除命令。
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnRemoveEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public virtual bool CanRemoveEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 修改主表数据

        /// <summary>
        ///     修改主表命令。
        /// </summary>
        public DelegateCommand<object> ModifyEntityCommand { get; set; }

        /// <summary>
        ///     执行修改主表命令。
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnModifyEntityExecute(object sender)
        {
        }

        /// <summary>
        ///     判断修改主表是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>修改主表是否可用。</returns>
        public virtual bool CanModifyEntityExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion
    }
}