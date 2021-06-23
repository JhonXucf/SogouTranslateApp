
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_QueryText = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer_translate = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_translate)).BeginInit();
            this.splitContainer_translate.Panel1.SuspendLayout();
            this.splitContainer_translate.Panel2.SuspendLayout();
            this.splitContainer_translate.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbx_To
            // 
            this.cbx_To.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_To.FormattingEnabled = true;
            this.cbx_To.Items.AddRange(new object[] {
            "zh-CHS",
            "en"});
            this.cbx_To.Location = new System.Drawing.Point(203, 14);
            this.cbx_To.Name = "cbx_To";
            this.cbx_To.Size = new System.Drawing.Size(70, 25);
            this.cbx_To.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "to";
            // 
            // cbx_From
            // 
            this.cbx_From.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_From.FormattingEnabled = true;
            this.cbx_From.Items.AddRange(new object[] {
            "zh-CHS",
            "en"});
            this.cbx_From.Location = new System.Drawing.Point(91, 14);
            this.cbx_From.Name = "cbx_From";
            this.cbx_From.Size = new System.Drawing.Size(70, 25);
            this.cbx_From.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "from";
            // 
            // btn_Translate
            // 
            this.btn_Translate.Location = new System.Drawing.Point(279, 10);
            this.btn_Translate.Name = "btn_Translate";
            this.btn_Translate.Size = new System.Drawing.Size(97, 32);
            this.btn_Translate.TabIndex = 9;
            this.btn_Translate.Text = "翻译";
            this.btn_Translate.UseVisualStyleBackColor = true;
            this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
            // 
            // rtb_TranslatedText
            // 
            this.rtb_TranslatedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_TranslatedText.Location = new System.Drawing.Point(0, 0);
            this.rtb_TranslatedText.Name = "rtb_TranslatedText";
            this.rtb_TranslatedText.Size = new System.Drawing.Size(698, 675);
            this.rtb_TranslatedText.TabIndex = 8;
            this.rtb_TranslatedText.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtb_QueryText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 733);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入待翻译文本";
            // 
            // rtb_QueryText
            // 
            this.rtb_QueryText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_QueryText.Location = new System.Drawing.Point(3, 19);
            this.rtb_QueryText.Name = "rtb_QueryText";
            this.rtb_QueryText.Size = new System.Drawing.Size(544, 711);
            this.rtb_QueryText.TabIndex = 8;
            this.rtb_QueryText.Text = "";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer_translate);
            this.splitContainer.Size = new System.Drawing.Size(1252, 733);
            this.splitContainer.SplitterDistance = 550;
            this.splitContainer.TabIndex = 15;
            // 
            // splitContainer_translate
            // 
            this.splitContainer_translate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_translate.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_translate.Name = "splitContainer_translate";
            this.splitContainer_translate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_translate.Panel1
            // 
            this.splitContainer_translate.Panel1.Controls.Add(this.cbx_From);
            this.splitContainer_translate.Panel1.Controls.Add(this.label1);
            this.splitContainer_translate.Panel1.Controls.Add(this.cbx_To);
            this.splitContainer_translate.Panel1.Controls.Add(this.label2);
            this.splitContainer_translate.Panel1.Controls.Add(this.btn_Translate);
            // 
            // splitContainer_translate.Panel2
            // 
            this.splitContainer_translate.Panel2.Controls.Add(this.rtb_TranslatedText);
            this.splitContainer_translate.Size = new System.Drawing.Size(698, 733);
            this.splitContainer_translate.SplitterDistance = 54;
            this.splitContainer_translate.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1252, 25);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "设置";
            // 
            // SogouTranslateApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 758);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SogouTranslateApp";
            this.Text = "搜狗翻译";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer_translate.Panel1.ResumeLayout(false);
            this.splitContainer_translate.Panel1.PerformLayout();
            this.splitContainer_translate.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_translate)).EndInit();
            this.splitContainer_translate.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtb_QueryText;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer_translate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

