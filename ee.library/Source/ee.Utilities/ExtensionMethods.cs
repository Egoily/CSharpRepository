using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ee.Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Bitmap转Base64String
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ToBase64String(this Image img)
        {
            return Convert.ToBase64String(ToBytes(img));
        }

        /// <summary>
        /// Bitmap转byte[]
        /// </summary>
        /// <param name="img"></param>
        /// <returns>byte[]</returns>
        public static byte[] ToBytes(this Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                var buffer = ms.ToArray();
                return buffer;
            }
        }

        public static string JoinString(this DateTime from, DateTime to)
        {
            if (from.Date.Equals(DateTime.MinValue.Date))
                return string.Empty;
            var result = from.ToString("yyyy-MM-dd HH:mm");
            if (to.Year.Equals(from.Year) && to.Date.Equals(from.Date))
            {
                result += " ~ " + to.ToString("HH:mm");
            }
            else if (to.Year.Equals(from.Year))
            {
                result += " ~ " + to.ToString("MM-dd HH:mm");
            }
            else
            {
                result += " ~ " + to.ToString("yyyy-MM-dd HH:mm");
            }
            return result;
        }

        public static void Sort<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }

        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sortedList = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sortedList.Count(); i++)
            {
                collection.Move(collection.IndexOf(sortedList[i]), i);
            }
        }

        public static DateTime? ToDateTime(this long jsTimeStamp)
        {
            if (jsTimeStamp <= 0) return null;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(jsTimeStamp);

            return dt;
        }

        public static long ToJsTimeStamp(this DateTime? dt)
        {
            if (!dt.HasValue) return 0;
            DateTime dt1970 = new DateTime(1970, 1, 1);
            TimeSpan ts = dt.Value - dt1970;
            return (long)ts.TotalMilliseconds;
        }
    }
}