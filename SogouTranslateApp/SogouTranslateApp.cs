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
        private string Pid = "7adbba1e985379146ea351cfca9ab3a0";
        private string Key = "fa31b71f409bb871b6721d1caf2e9500";
        private readonly string Url = "http://fanyi.sogou.com/reventondc/api/sogouTranslate";
        public SogouTranslateApp()
        {
            InitializeComponent();
            this.toolStripMenuItem1.Click += ToolStripMenuItem1_Click;
            IniHelper iniHelper = new IniHelper();
            string pid = iniHelper.GetString("Pid", "");
            string key = iniHelper.GetString("Key", "");
            if (!string.IsNullOrWhiteSpace(pid))
            {
                Pid = pid;
                Key = key;
            }
            var languages = iniHelper.GetSections("Language");

            if (languages != null)
            {
                this.cbx_From.DisplayMember = "Value";
                this.cbx_From.ValueMember = "Key";
                this.cbx_From.DataSource = languages.ToList();

                this.cbx_To.DisplayMember = "Value";
                this.cbx_To.ValueMember = "Key";
                this.cbx_To.DataSource = languages.ToList();
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = settings.ShowDialog();
            if (result == DialogResult.OK)
            {
                IniHelper iniHelper = new IniHelper();
                string pid = iniHelper.GetString("Pid", "");
                string key = iniHelper.GetString("Key", "");
                if (!string.IsNullOrWhiteSpace(pid))
                {
                    Pid = pid;
                    Key = key;
                }
            }
        }

        private void btn_Translate_Click(object sender, EventArgs e)
        {
            if (this.cbx_From.Text.Equals(this.cbx_To.Text))
            {
                MessageBox.Show("转换语言不能相同!");
                return;
            }
            Dictionary<string, string> paras = getPublicParams();
            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("cache-control", "no-cache");
            headers.Add("accept", "application/json");
            headers.Add("content-type", "application/x-www-form-urlencoded");

            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var content = convertMap2String(paras);
            request.AddParameter("application/x-www-form-urlencoded", content, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var result = JsonUtils.JsonToObject<TranslateResult>(response.Content);
            if (result != null)
            {
                this.rtb_TranslatedText.Text = result.translation;
                return;
            }
            this.rtb_TranslatedText.Text = JsonUtils.ConvertJsonString(response.Content);
        }
        class TranslateResult
        {
            public string zly { get; set; }
            public string query { get; set; }
            public string translation { get; set; }
            public string errorCode { get; set; }
            public string detect { get; set; }
        }
        /// <summary>
        /// 公共参数组装
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> getPublicParams()
        {
            Dictionary<string, string> paras = new Dictionary<string, string>();

            paras.Add("from", this.cbx_From.SelectedValue.ToString());
            paras.Add("to", this.cbx_To.SelectedValue.ToString());
            paras.Add("pid", Pid);
            string q = this.rtb_QueryText.Text;
            paras.Add("q", q);
            string salt = "1508404016012";
            string sign = GenerateMD5(Pid + q + salt + Key);
            paras.Add("sign", sign);
            paras.Add("salt", salt);

            return paras;
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
