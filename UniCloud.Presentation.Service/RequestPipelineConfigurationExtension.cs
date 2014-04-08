#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/20，13:01
// 文件名：DataServiceClientRequestPipelineConfiguration.cs
// 程序集：UniCloud.Presentation.Service
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData;

#endregion

namespace System.Data.Services.Client
{
    public static class RequestPipelineConfigurationExtension
    {
        public static DataServiceClientRequestPipelineConfiguration RemoveProperties<T>(
            this DataServiceClientRequestPipelineConfiguration requestPipeline,
            params string[] propertiesToRemove)
        {
            return requestPipeline.OnEntryEnding(args =>
            {
                if (typeof (T).IsAssignableFrom(args.Entity.GetType()))
                {
                    args.Entry.RemoveProperties(propertiesToRemove);
                }
            });
        }

        public static void RemoveProperties(this ODataEntry entry, params string[] propertyNames)
        {
            var properties = entry.Properties as List<ODataProperty>;
            if (properties == null)
            {
                properties = new List<ODataProperty>(entry.Properties);
            }
            var propertiesToRemove = properties.Where(p => propertyNames.Any(rp => rp == p.Name));
            foreach (var propertyToRemove in propertiesToRemove.ToArray())
            {
                properties.Remove(propertyToRemove);
            }
            entry.Properties = properties;
        }
    }
}