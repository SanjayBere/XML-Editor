using System.Windows.Forms;
namespace XmlEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

         // viewer
            // 
           
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            XmlEditor.XMLViewerSetting xmlViewerSetting1 = new XmlEditor.XMLViewerSetting();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xmlViewer1 = new XmlEditor.XMLViewer();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.updateXMLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1354, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openXMLToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openXMLToolStripMenuItem
            // 
            this.openXMLToolStripMenuItem.Name = "openXMLToolStripMenuItem";
            this.openXMLToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.openXMLToolStripMenuItem.Text = "Open XML";
            this.openXMLToolStripMenuItem.Click += new System.EventHandler(this.openXMLToolStripMenuItem_Click);
            // 
            // updateXMLToolStripMenuItem
            // 
            this.updateXMLToolStripMenuItem.Name = "updateXMLToolStripMenuItem";
            this.updateXMLToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.updateXMLToolStripMenuItem.Text = "Update XML";
            this.updateXMLToolStripMenuItem.Click += new System.EventHandler(this.updateXMLToolStripMenuItem_Click);
            // 
            // LineNumberTextBox
            // 
            this.LineNumberTextBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LineNumberTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LineNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumberTextBox.ForeColor = System.Drawing.Color.Gray;
            this.LineNumberTextBox.Location = new System.Drawing.Point(15, 54);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.ReadOnly = true;
            this.LineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNumberTextBox.Size = new System.Drawing.Size(58, 770);
            this.LineNumberTextBox.TabIndex = 3;
            this.LineNumberTextBox.Text = "";
            this.LineNumberTextBox.WordWrap = false;
            this.LineNumberTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LineNumberTextBox_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.HideSelection = false;
            this.textBox1.Location = new System.Drawing.Point(1060, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1018, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Find";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1283, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cancel";
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // xmlViewer1
            // 
            this.xmlViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xmlViewer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlViewer1.Location = new System.Drawing.Point(49, 54);
            this.xmlViewer1.Name = "xmlViewer1";
            xmlViewerSetting1.AttributeKey = System.Drawing.Color.Red;
            xmlViewerSetting1.AttributeValue = System.Drawing.Color.Blue;
            xmlViewerSetting1.Element = System.Drawing.Color.DarkRed;
            xmlViewerSetting1.Tag = System.Drawing.Color.Blue;
            xmlViewerSetting1.Value = System.Drawing.Color.Black;
            this.xmlViewer1.Settings = xmlViewerSetting1;
            this.xmlViewer1.Size = new System.Drawing.Size(1377, 770);
            this.xmlViewer1.TabIndex = 4;
            this.xmlViewer1.Text = "";
            this.xmlViewer1.WordWrap = false;
            this.xmlViewer1.VScroll += new System.EventHandler(this.xmlViewer1_VScroll);
            this.xmlViewer1.FontChanged += new System.EventHandler(this.xmlViewer1_FontChanged);
            this.xmlViewer1.TextChanged += new System.EventHandler(this.xmlViewer1_TextChanged_1);
            this.xmlViewer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xmlViewer1_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 617);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xmlViewer1);
            this.Controls.Add(this.LineNumberTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       // private XMLViewer viewer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openXMLToolStripMenuItem;
        private System.Windows.Forms.RichTextBox LineNumberTextBox;
        private XMLViewer xmlViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem updateXMLToolStripMenuItem;
        private Label label2;
        public TextBox textBox1;
        private Label label3;

    }
}

