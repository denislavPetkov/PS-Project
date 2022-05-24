namespace WindowsFormsApp1
{
	partial class FormMain
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
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelT = new System.Windows.Forms.Panel();
            this.listViewMain = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelLogo = new System.Windows.Forms.Label();
            this.panelB = new System.Windows.Forms.Panel();
            this.panelB.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelT
            // 
            this.panelT.BackColor = System.Drawing.SystemColors.Info;
            this.panelT.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelT.Location = new System.Drawing.Point(0, 0);
            this.panelT.Name = "panelT";
            this.panelT.Size = new System.Drawing.Size(324, 24);
            this.panelT.TabIndex = 2;
            // 
            // listViewMain
            // 
            this.listViewMain.BackColor = System.Drawing.SystemColors.Info;
            this.listViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMain.FullRowSelect = true;
            this.listViewMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMain.HideSelection = false;
            this.listViewMain.LargeImageList = this.imageList;
            this.listViewMain.Location = new System.Drawing.Point(0, 24);
            this.listViewMain.MultiSelect = false;
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(324, 622);
            this.listViewMain.SmallImageList = this.imageList;
            this.listViewMain.StateImageList = this.imageList;
            this.listViewMain.TabIndex = 3;
            this.listViewMain.UseCompatibleStateImageBehavior = false;
            this.listViewMain.View = System.Windows.Forms.View.Details;
            this.listViewMain.SelectedIndexChanged += new System.EventHandler(this.listViewMain_SelectedIndexChanged);
            this.listViewMain.Click += new System.EventHandler(this.listViewMain_DoubleClick);
            this.listViewMain.MouseLeave += new System.EventHandler(this.listViewMain_MouseLeave);
            this.listViewMain.MouseHover += new System.EventHandler(this.listViewMain_MouseHover);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 305;
            // 
            // labelLogo
            // 
            this.labelLogo.BackColor = System.Drawing.SystemColors.Control;
            this.labelLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelLogo.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelLogo.Location = new System.Drawing.Point(191, 0);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(133, 24);
            this.labelLogo.TabIndex = 2;
            this.labelLogo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelB
            // 
            this.panelB.Controls.Add(this.labelLogo);
            this.panelB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelB.Location = new System.Drawing.Point(0, 646);
            this.panelB.Name = "panelB";
            this.panelB.Size = new System.Drawing.Size(324, 24);
            this.panelB.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(324, 670);
            this.Controls.Add(this.listViewMain);
            this.Controls.Add(this.panelT);
            this.Controls.Add(this.panelB);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelB.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Panel panelT;
        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.Panel panelB;
    }
}

