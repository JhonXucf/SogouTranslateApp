
namespace SogouTranslateApp
{
    partial class SogouTranslateApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbx_To = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_From = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Translate = new System.Windows.Forms.Button();
            this.rtb_TranslatedText = new System.Windows.Forms.RichTextBox();
            this.rtb_QueryText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // cbx_To
            // 
            this.cbx_To.FormattingEnabled = true;
            this.cbx_To.Items.AddRange(new object[] {
            "zh-CHS",
            "en"});
            this.cbx_To.Location = new System.Drawing.Point(771, 10);
            this.cbx_To.Name = "cbx_To";
            this.cbx_To.Size = new System.Drawing.Size(70, 25);
            this.cbx_To.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(740, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "to";
            // 
            // cbx_From
            // 
            this.cbx_From.FormattingEnabled = true;
            this.cbx_From.Items.AddRange(new object[] {
            "zh-CHS",
            "en"});
            this.cbx_From.Location = new System.Drawing.Point(659, 10);
            this.cbx_From.Name = "cbx_From";
            this.cbx_From.Size = new System.Drawing.Size(70, 25);
            this.cbx_From.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(612, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "from";
            // 
            // btn_Translate
            // 
            this.btn_Translate.Location = new System.Drawing.Point(847, 6);
            this.btn_Translate.Name = "btn_Translate";
            this.btn_Translate.Size = new System.Drawing.Size(97, 32);
            this.btn_Translate.TabIndex = 9;
            this.btn_Translate.Text = "翻译";
            this.btn_Translate.UseVisualStyleBackColor = true;
            this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
            // 
            // rtb_TranslatedText
            // 
            this.rtb_TranslatedText.Location = new System.Drawing.Point(612, 41);
            this.rtb_TranslatedText.Name = "rtb_TranslatedText";
            this.rtb_TranslatedText.Size = new System.Drawing.Size(638, 706);
            this.rtb_TranslatedText.TabIndex = 8;
            this.rtb_TranslatedText.Text = "";
            // 
            // rtb_QueryText
            // 
            this.rtb_QueryText.Location = new System.Drawing.Point(8, 10);
            this.rtb_QueryText.Name = "rtb_QueryText";
            this.rtb_QueryText.Size = new System.Drawing.Size(598, 737);
            this.rtb_QueryText.TabIndex = 7;
            this.rtb_QueryText.Text = "";
            // 
            // SogouTranslateApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 758);
            this.Controls.Add(this.cbx_To);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbx_From);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Translate);
            this.Controls.Add(this.rtb_TranslatedText);
            this.Controls.Add(this.rtb_QueryText);
            this.Name = "SogouTranslateApp";
            this.Text = "搜狗翻译";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbx_To;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_From;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Translate;
        private System.Windows.Forms.RichTextBox rtb_TranslatedText;
        private System.Windows.Forms.RichTextBox rtb_QueryText;
    }
}

