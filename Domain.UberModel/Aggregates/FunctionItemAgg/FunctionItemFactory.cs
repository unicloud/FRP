#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:48:02
// 文件名：FunctionItemFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:48:02
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.UberModel.Aggregates.FunctionItemAgg
{
    public static class FunctionItemFactory
    {
        public static FunctionItem CreateFunctionItem(string name, int? parentItemId, int sort, bool isLeaf, bool isButtion,
             string naviUrl, string description = null, bool isValid = true, string imageUrl = null)
        {
            var functionItem = new FunctionItem
                   {
                       Name = name,
                       ParentItemId = parentItemId,
                       IsLeaf = isLeaf,
                       IsButton = isButtion,
                       NaviUrl = naviUrl,
                       Description = description,
                       IsValid = isValid,
                       ImageUrl = imageUrl,
                       Sort = sort,
                   };
            functionItem.GenerateNewIdentity();
            return functionItem;
        }
    }
}
