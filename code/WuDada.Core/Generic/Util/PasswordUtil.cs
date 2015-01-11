using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WuDada.Core.Generic.Util
{
    public class PasswordUtil
    {
        private static RNGCryptoServiceProvider rngp = new RNGCryptoServiceProvider();
        private static byte[] rb = new byte[4];

        /// <summary>
        /// 產生一個非負數的亂數
        /// </summary>
        public static int Next()
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            if (value < 0)
            {
                value = -value;
            }
            return value;
        }
        /// <summary>
        /// 產生一個非負數且最大值 max 以下的亂數
        /// </summary>
        /// <param name="max">最大值</param>
        public static int Next(int max)
        {
            rngp.GetBytes(rb);
            int value = BitConverter.ToInt32(rb, 0);
            value = value % (max + 1);
            if (value < 0)
            {
                value = -value;
            }
            return value;
        }
        /// <summary>
        /// 產生一個非負數且最小值在 min 以上最大值在 max 以下的亂數
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }

        /// <summary>
        /// 取得新的密碼字串
        /// </summary>
        /// <param name="lengthMin">最小長度</param>
        /// <param name="lengthMax">最大長度</param>
        /// <returns></returns>
        public static string GetRandomPass(int lengthMin, int lengthMax)
        {
            StringBuilder sb = new StringBuilder();
            char[] chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int length = Next(lengthMin, lengthMax);
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[Next(chars.Length - 1)]);
            }
            string newPass = sb.ToString();

            return newPass;
        }
    }
}
