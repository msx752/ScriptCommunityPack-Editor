namespace SphereScp
{
    partial class TabTest
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntMSATabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kapatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msaTabControl2 = new SphereScp.MSATabControl();
            this.cntMSATabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(308, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 24);
            this.button1.TabIndex = 2;
            this.button1.Text = "TEST TAB STRIP";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cntMSATabPage
            // 
            this.cntMSATabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kapatToolStripMenuItem});
            this.cntMSATabPage.Name = "cntMSATabPage";
            this.cntMSATabPage.Size = new System.Drawing.Size(105, 26);
            // 
            // kapatToolStripMenuItem
            // 
            this.kapatToolStripMenuItem.Name = "kapatToolStripMenuItem";
            this.kapatToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.kapatToolStripMenuItem.Text = "Kapat";
            this.kapatToolStripMenuItem.Click += new System.EventHandler(this.kapatToolStripMenuItem_Click);
            // 
            // msaTabControl2
            // 
            this.msaTabControl2.BackColor = System.Drawing.Color.Black;
            this.msaTabControl2.BackgroundImage = global::SphereScp.Properties.Resources.bg1;
            this.msaTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msaTabControl2.Location = new System.Drawing.Point(0, 0);
            this.msaTabControl2.Name = "msaTabControl2";
            this.msaTabControl2.Size = new System.Drawing.Size(479, 302);
            this.msaTabControl2.TabIndex = 1;
            this.msaTabControl2.Text = "msaTabControl2";
            // 
            // TabTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(479, 302);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.msaTabControl2);
            this.Name = "TabTest";
            this.Text = "Tab Control";
            this.cntMSATabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MSATabControl msaTabControl2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip cntMSATabPage;
        private System.Windows.Forms.ToolStripMenuItem kapatToolStripMenuItem;
    }
}