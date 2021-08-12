using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CommonHelper
{
    public static class LinqHelper
    {
        public static bool IsNull(object str)
        {
            if (str == null) return false;
            if (str is string && string.IsNullOrEmpty(str as string)) return true;
            return false;
        }

        /// <summary>
        /// 获取序列化后的对象
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <param name="obj">源对象</param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T GetJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        /// <summary>
        /// DataTable转为List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            List<T> colletion = new List<T>();
            PropertyInfo[] pInfos = typeof(T).GetProperties();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pInfo in pInfos)
                {
                    if (!pInfo.CanWrite) continue;
                    if (!dt.Columns.Contains(pInfo.Name)) continue;
                    pInfo.SetValue(t, (dr[pInfo.Name] is DBNull) ? null : dr[pInfo.Name]);
                }
                colletion.Add(t);
            }
            return colletion;
        }

        /// <summary>
        /// string转为Int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            if (IsNull(str)) throw new NullReferenceException("方法：ToInt，异常信息：要转化的字符串为空");
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 转换成时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            if (IsNull(str)) throw new NullReferenceException("方法：ToInt，异常信息：要转化的字符串为空");
            return new DateTime(str.Substring(0, 4).ToInt(), str.Substring(4, 2).ToInt(), str.Substring(6, 2).ToInt(), str.Substring(8, 2).ToInt(), str.Substring(10, 2).ToInt(), 00);
        }


        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
