using System;

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
            //var ra = new Random((int)DateTime.Now.Ticks);
            //return ra.Next();

            return int.Parse(DateTime.Now.ToString("ddHHmmss"));
        }
    }
}
