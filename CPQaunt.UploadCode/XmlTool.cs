using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ParkingSqlHelper
{
    public class XmlHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string Serialize<T>(T t)
        {
            MemoryStream ms;
            XmlWriter xw;
            XmlWriterSettings xws;
            XmlSerializerNamespaces ns;
            XmlSerializer xml;

            ms = new MemoryStream();
            ns = new XmlSerializerNamespaces();
            ns.Add("", ""); //这样就 去掉 attribute 里面的 xmlns:xsi 和 xmlns:xsd
            xws = new XmlWriterSettings();
            xws.Encoding = new UTF8Encoding(false); //设置编码,不能用Encoding.UTF8,会导致带有BOM标记
            xws.Indent = true; //是否格式化文档。
            xml = new XmlSerializer(typeof(T));
            using (xw = XmlWriter.Create(ms, xws))
            {
                //序列化对象
                xml.Serialize(xw, t, ns);
            }

            return xws.Encoding.GetString(ms.ToArray()).Trim();
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="s">对象序列化后的</param>
        /// <returns></returns>
        public static T Deserialize<T>(string s)
        {
            T obj;
            XmlReader xr;
            XmlSerializer xs;

            xr = XmlReader.Create(new StringReader(s));
            xs = new XmlSerializer(typeof(T));
            obj = (T)xs.Deserialize(xr);

            return obj;
        }

        public static void SaveObjectToFile<T>(T obj, string FilePath)
        {
            FileStream fs;
            byte[] buffer;

            if (null == obj || null == FilePath || FilePath.Trim().Length <= 0)
            {
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }

            fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            buffer = new UTF8Encoding(false).GetBytes(Serialize<T>(obj));
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }

        public static T ReadObjectFromFile<T>(string FilePath)
        {
            FileStream fs;
            byte[] buffer;
            string content;

            if (null == FilePath || FilePath.Trim().Length <= 0 || !File.Exists(FilePath))
            {
                return default(T);
            }

            fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            content = GetUTF8String(buffer);
            fs.Close();

            return Deserialize<T>(content);
        }

        /// <summary>
        /// XML文档用记事本保存一下就会导致文件带有BOM标记，在解析带BOM标记的XML时会出错。此方法自动识别是否带有BOM标记，有则去掉BOM标记。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        protected static string GetUTF8String(byte[] buffer)
        {
            if (buffer == null)
                return null;

            if (buffer.Length <= 3)
            {
                return Encoding.UTF8.GetString(buffer);
            }

            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf }; //BOM标记

            if (buffer[0] == bomBuffer[0]
            && buffer[1] == bomBuffer[1]
            && buffer[2] == bomBuffer[2])
            {
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            }

            return Encoding.UTF8.GetString(buffer);
        }
    }
}

