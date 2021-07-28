using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CommonHelper
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnmHelper
    {
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <returns></returns>
        public static string GetEnmName(this Enum sourceValue)
        {
            DisplayAttribute[] attributes = null;
            if (sourceValue != null)
            {
                FieldInfo field = sourceValue.GetType().GetField(sourceValue.ToString());
                if (field != null)
                {
                    attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                }
            }
            if (attributes == null || attributes.Length < 1) return sourceValue.ToString();
            return attributes[0].Name;
        }

        /// <summary>
        /// 把字符串转换为枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ConvertToEnm<T>(this string source)
        {
            return (T)Enum.Parse(typeof(T), source);
        }
    }
}
