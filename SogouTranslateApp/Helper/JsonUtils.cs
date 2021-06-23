using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SogouTranslateApp.Helper
{
    /// <summary> Json 工具类
    /// </summary>
    public static class JsonUtils
    {

        /// <summary> 设置 Json 序列化格式
        /// </summary>
        /// <param name="isFormatJson">是否格式化 Json 字符串，默认 true</param>
        /// <param name="isIgnoreNull">是否忽略 null 值，默认 false</param>
        /// <param name="DtmFormat">日期格式化参数，默认 "yyyy-MM-dd HH:mm:ss"</param>
        /// <returns></returns>
        public static JsonSerializerSettings SetJsonSerializerFormat(bool isFormatJson = false,
                                                                     bool isIgnoreNull = false,
                                                                     string DtmFormat = "yyyy-MM-dd HH:mm:ss")
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();

            IList<JsonConverter> jcList = new List<JsonConverter>();

            //日期时间格式化处理
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = DtmFormat; //日期时间格式化处理

            jcList.Add(timeConverter);

            jss.Converters = jcList;

            if (isFormatJson)
            {
                jss.Formatting = Newtonsoft.Json.Formatting.Indented; //格式化Json字符串    
            }

            if (isIgnoreNull)
            {
                jss.NullValueHandling = NullValueHandling.Ignore; //忽略null值    
            }

            return jss;
        }

        /// <summary> object 对象转换为 Json 字符串
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            string json = string.Empty;

            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                json = JsonConvert.SerializeObject(obj, SetJsonSerializerFormat());
                //json = "";
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return json;
        }

        /// <summary> Json 格式的字符串转换为 T 对象
        /// </summary>
        /// <typeparam name="T">泛型 类型</typeparam>
        /// <param name="json">json 格式的字符串</param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            T t_object = default(T);

            if (json == null || json == "")
            {
                return default(T);
            }

            try
            {
                t_object = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }

            return t_object;
        }

        /// <summary> Json 格式的字符串转换为 object 对象
        /// </summary>
        /// <param name="json">json 格式的字符串</param>
        /// <returns></returns>
        public static object JsonToObject(string json)
        {
            object obj = null;

            if (json == null || json == "")
            {
                return null;
            }

            try
            {
                obj = JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                return null;
            }

            return obj;
        }

        // by litao
        /*
		/// <summary> Json 格式的字符串转换为 dynamic 匿名对象
		/// </summary>
		/// <param name="json">json 格式的字符串</param>
		/// <returns></returns>
		public static dynamic JsonToDynamic(string json)
		{
			if (json == null || json == "")
			{
				return null;
			}

			//创建匿名对象
			dynamic dyData = default(dynamic);

			try
			{
				dyData = JsonConvert.DeserializeObject(json);
			}
			catch (Exception ex)
			{
				dyData = null;
			}

			return dyData;
		}
		*/

        /// <summary> 读取 Json 内容的文件，并转化为 object 对象
        /// </summary>
        /// <param name="path">Json 文件的绝对路径</param>
        /// <param name="encoding">字符编码，默认 UTF-8</param>
        /// <returns></returns>
        public static object JsonFileToObject(string path, Encoding encoding = null)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            object obj = null;

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                string jsonStr = null;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, encoding))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
                if (jsonStr == null || jsonStr == "")
                {
                    obj = null;
                }
                else
                {
                    obj = JsonToObject(jsonStr);
                }
            }
            catch (Exception ex)
            {
                obj = null;
            }

            return obj;
        }

        /// <summary> 读取 Json 内容的文件，并转化为 T 对象
        /// </summary>
        /// <typeparam name="T">泛型 类型</typeparam>
        /// <param name="path">Json 文件的绝对路径</param>
        /// <param name="encoding">字符编码，默认 UTF-8</param>
        /// <returns></returns>
        public static T JsonFileToObject<T>(string path, Encoding encoding = null)
        {
            T t_obj = default(T);

            if (!File.Exists(path))
            {
                return default(T);
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                string jsonStr = null;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, encoding))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
                if (jsonStr == null || jsonStr == "")
                {
                    t_obj = default(T);
                }
                else
                {
                    t_obj = JsonToObject<T>(jsonStr);
                }
            }
            catch (Exception ex)
            {
                t_obj = default(T);
            }

            return t_obj;
        }

        /// <summary> object 对象转换为 Json 内容的文件
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="path">Json 文件的绝对路径（建议文件格式用 .json 后缀）</param>
        /// <param name="encoding">字符编码，默认 UTF-8</param>
        /// <returns></returns>
        public static bool ObjectToJsonFile(object obj, string path, Encoding encoding = null)
        {
            if (obj == null || path == null || path == "")
            {
                return false;
            }

            bool isFinish = false;

            string jsonStr = ObjectToJson(obj);
            if (jsonStr == null || jsonStr == "")
            {
                isFinish = false;
            }
            else
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }

                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, encoding))
                        {
                            sw.Write(jsonStr);
                        }
                    }
                    isFinish = true;
                }
                catch (Exception ex)
                {
                    isFinish = false;
                }
            }

            return isFinish;
        }
        public static string ConvertJsonString(string str)
        {
            try
            {
                //格式化json字符串
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }

                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
