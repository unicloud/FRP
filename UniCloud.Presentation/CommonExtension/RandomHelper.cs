using System;
using System.Collections.Generic;

namespace UniCloud.Presentation.CommonExtension
{
    public static class RandomHelper
    {
        /// <summary>
        /// 返回随机数
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            var ra = new Random(Guid.NewGuid().GetHashCode());
            int randomValue = ra.Next();
            while (Randoms.Contains(randomValue))
            {
                randomValue = ra.Next();
            }
            Randoms.Add(randomValue);
            return randomValue;
        }

        private static readonly List<int> Randoms = new List<int>();
    }
}
