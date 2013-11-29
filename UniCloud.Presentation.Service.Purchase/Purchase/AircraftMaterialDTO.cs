#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Presentation.Service.Purchase.AgentService.Purchase;

#endregion

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class AircraftMaterialDTO
    {
        partial void OnAircraftTypeIdChanging(System.Guid value)
        {
            if (value.Equals(Guid.Empty))
            {
                throw new Exception("机型不能为空");
            }
        }
    }
}