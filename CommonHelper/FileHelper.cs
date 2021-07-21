using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class FileHelper
    {
        /// <summary>
        /// 创建新文件，当该文件存在时抛异常
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="seedData">种子数据，可选</param>
        public static void CreateNew(string path, string seedData = default)
        {
            if (File.Exists(path)) throw new Exception($"文件:{path}已存在！");
            File.Create(path).Close();

            if (!String.IsNullOrEmpty(seedData))
            {
                AppendData(path, seedData);
            }
        }

        /// <summary>
        /// 当文件不存在时创建
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="seedData">种子数据，可选</param>
        public static void CreateNewIfNoExists(string path, string seedData = default)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                if (!String.IsNullOrEmpty(seedData))
                {
                    AppendData(path, seedData);
                }
            }

        }


        public static async Task<string> GetDataAsync(string path)
        {

            if (!File.Exists(path)) throw new Exception($"路径:{path}不存在！");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return await sr.ReadToEndAsync();
                }
            }

        }

        public static string GetData(string path)
        {
            if (!File.Exists(path)) throw new Exception($"路径:{path}不存在！");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }

        }


        public static bool AppendData(string path, string str)
        {
            if (!File.Exists(path)) throw new Exception($"文件:{path}不存在！");
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sws = new StreamWriter(fs))
                {
                    sws.WriteLine(str);
                    return true;
                }
            }

        }
    }
}
