#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 14:42:54
// 文件名：ItemFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间



#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ItemAgg
{
    /// <summary>
    ///     Item工厂。
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        ///     创建附件项。
        /// </summary>
        /// <returns>Item</returns>
        public static Item CreateItem()
        {
            var item = new Item();
            item.GenerateNewIdentity();
            return item;
        }

        /// <summary>
        ///     创建附件项。
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="itemNo">项号</param>
        /// <param name="fiNumber">功能标识号</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static Item CreateItem(string name, string itemNo, string fiNumber, string description,bool isLife)
        {
            var item = new Item();
            item.GenerateNewIdentity();
            item.SetDescription(description);
            item.SetName(name);
            item.SetIsLife(isLife);
            item.SetItemNoOrFiNumber(itemNo, fiNumber);
            return item;
        }
    }
}