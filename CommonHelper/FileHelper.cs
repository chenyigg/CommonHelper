using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class FileHelper
    {
        public static bool CreateNew(string path, string str)
        {
            if (File.Exists(path)) throw new Exception($"文件:{path}已存在！");
            File.Create(path).Close();
            using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                using (StreamWriter sws = new StreamWriter(fs))
                {
                    sws.WriteLine(str);
                    return true;
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
