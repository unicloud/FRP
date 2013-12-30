#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export(typeof(ApprovalVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApprovalVM
    {
        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public ApprovalVM()
        {
        }
    }
}