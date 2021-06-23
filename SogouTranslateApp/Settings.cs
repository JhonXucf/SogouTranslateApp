using SogouTranslateApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SogouTranslateApp
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbx_Pid.Text) || string.IsNullOrWhiteSpace(this.tbx_Key.Text))
            {
                MessageBox.Show("请输入有效的Pid和Key!");
                return;
            }
            if (this.tbx_Pid.Text.Length != 32 || this.tbx_Key.Text.Length != 32)
            {
                MessageBox.Show("请输入有效的Pid和Key!");
                return;
            }
            IniHelper iniHelper = new IniHelper();
            iniHelper.SetString("Config", "Pid", this.tbx_Pid.Text);
            iniHelper.SetString("Config", "Key", this.tbx_Key.Text);
            MessageBox.Show("保存成功！");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
