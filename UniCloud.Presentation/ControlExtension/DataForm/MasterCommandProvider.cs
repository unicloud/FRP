#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013-11-29，13:11
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Windows;
using Telerik.Windows.Controls.Data.DataForm;
using Telerik.Windows.Controls.DataServices;
using UniCloud.Presentation.MVVM;

#endregion

namespace UniCloud.Presentation.ControlExtension.DataForm
{
    public class MasterCommandProvider : DataFormCommandProvider
    {
        public static readonly DependencyProperty QueryableDataServiceCollectionViewProperty =
            DependencyProperty.Register("QueryableDataServiceCollectionView",
                typeof (QueryableDataServiceCollectionViewBase), typeof (MasterCommandProvider),
                new PropertyMetadata(OnViewModelChanged));

        public MasterCommandProvider()
            : base(null)
        {
        }

        /// <summary>
        ///     编辑界面。
        /// </summary>
        public QueryableDataServiceCollectionViewBase QueryableDataServiceCollectionView
        {
            get
            {
                return (QueryableDataServiceCollectionViewBase) GetValue(QueryableDataServiceCollectionViewProperty);
            }
            set { SetValue(QueryableDataServiceCollectionViewProperty, value); }
        }

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        ///     重载提交方法。
        /// </summary>
        protected override void CommitEdit()
        {
            if (DataForm == null || !DataForm.ValidateItem()) return;
            DataForm.CommitEdit();
            Commit();
        }

        /// <summary>
        ///     重载取消方法。
        /// </summary>
        protected override void CancelEdit()
        {
            base.CancelEdit();
            Cancel();
        }

        protected override bool CanMoveCurrentToFirstExecute()
        {
            return QueryableDataServiceCollectionView == null
                ? DataForm.CanMoveCurrentToFirst
                : !QueryableDataServiceCollectionView.HasChanges;
        }

        protected override bool CanMoveCurrentToNextExecute()
        {
            return QueryableDataServiceCollectionView == null
                ? DataForm.CanMoveCurrentToNext
                : !QueryableDataServiceCollectionView.HasChanges;
        }

        protected override bool CanMoveCurrentToLastExecute()
        {
            return QueryableDataServiceCollectionView == null
                ? DataForm.CanMoveCurrentToLast
                : !QueryableDataServiceCollectionView.HasChanges;
        }

        protected override bool CanMoveCurrentToPreviousExecute()
        {
            return QueryableDataServiceCollectionView == null
                ? DataForm.CanMoveCurrentToPrevious
                : !QueryableDataServiceCollectionView.HasChanges;
        }


        /// <summary>
        ///     重载删除方法。
        /// </summary>
        protected override void Delete()
        {
            MessageDialogs.Confirm("提示", "确定是否删除该记录？", (sender, e) =>
            {
                if (e.DialogResult != true)
                    return;
                base.Delete();
                Commit();
            });
        }

        /// <summary>
        ///     提交。
        /// </summary>
        private void Commit()
        {
            QueryableDataServiceCollectionView.SubmitChanges();
            QueryableDataServiceCollectionView.SubmittedChanges -= QueryableDataServiceCollectionView_SubmittedChanges;
            QueryableDataServiceCollectionView.SubmittedChanges += QueryableDataServiceCollectionView_SubmittedChanges;
        }

        private void QueryableDataServiceCollectionView_SubmittedChanges(object sender,
            DataServiceSubmittedChangesEventArgs e)
        {
            MessageDialogs.Alert("提示", e.HasError ? "保存失败，请检查。" : "保存成功。");
        }


        /// <summary>
        ///     取消。
        /// </summary>
        private void Cancel()
        {
            QueryableDataServiceCollectionView.RejectChanges();
        }
    }
}