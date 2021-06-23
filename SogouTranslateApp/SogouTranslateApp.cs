using RestSharp;
using SogouTranslateApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SogouTranslateApp
{
    public partial class SogouTranslateApp : Form
    {
        private readonly string Pid = "7adbba1e985379146ea351cfca9ab3a0";
        private readonly string Key = "fa31b71f409bb871b6721d1caf2e9500";
        public SogouTranslateApp()
        {
            InitializeComponent();
        }

        private void btn_Translate_Click(object sender, EventArgs e)
        {
            if (this.cbx_From.Text.Equals(this.cbx_To.Text))
            {
                MessageBox.Show("转换语言不能一样");
                return;
            }
            Dictionary<string, string> paras = getPublicParams();
            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("cache-control", "no-cache");
            headers.Add("accept", "application/json");
            headers.Add("content-type", "application/x-www-form-urlencoded");

            var client = new RestClient("http://fanyi.sogou.com/reventondc/api/sogouTranslate");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var content = convertMap2String(paras);
            request.AddParameter("application/x-www-form-urlencoded", content, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            this.rtb_TranslatedText.Text = JsonUtils.ConvertJsonString(response.Content);
        }
        /// <summary>
        /// 公共参数组装
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> getPublicParams()
        {
            Dictionary<string, string> paras = new Dictionary<string, string>();

            paras.Add("from", this.cbx_From.Text);
            paras.Add("to", this.cbx_To.Text);
            paras.Add("pid", Pid);
            string q = this.rtb_QueryText.Text;
            paras.Add("q", q);
            string salt = getNowTimeStamp();
            string sign = GenerateMD5(Pid + q + salt + Key);
            paras.Add("sign", sign);
            paras.Add("salt", salt);

            return paras;
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static String getNowTimeStamp()
        {
            String nowTimeStamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000).ToString();
            return nowTimeStamp;
        }
        /// <summary>
        /// key=value&的形式拼接
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public String convertMap2String(Dictionary<string, string> map)
        {
            if (map == null)
            {
                return "";
            }

            if (map.Count <= 0)
            {
                return "";
            }

            StringBuilder builder = new StringBuilder();
            int index = 0;
            foreach (KeyValuePair<string, string> kv in map)
            {
                if (kv.Value == null)
                    continue;
                if (index == map.Count - 1)
                {
                    builder.Append(kv.Key)
                       .Append("=")
                       .Append(kv.Value);
                    break;
                }
                builder.Append(kv.Key)
                        .Append("=")
                        .Append(kv.Value)
                        .Append("&");
                index++;
            }
            String t_string = builder.ToString(0, builder.Length);
            return t_string;
        }
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder(newBuffer.Length);
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
