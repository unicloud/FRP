#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof (RequestVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RequestVM
    {
          /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public RequestVM()
        {
        }
    }
}