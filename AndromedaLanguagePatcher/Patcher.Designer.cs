namespace AndromedaLanguagePatcher
{
	partial class Patcher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patcher));
			this.comboBox_GameLanguage = new System.Windows.Forms.ComboBox();
			this.label_GameLanguage = new System.Windows.Forms.Label();
			this.button_Patch = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Path = new System.Windows.Forms.TextBox();
			this.button_Path = new System.Windows.Forms.Button();
			this.button_Restore = new System.Windows.Forms.Button();
			this.comboBox_TargetLanguage = new System.Windows.Forms.ComboBox();
			this.label_TargetLanguage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBox_GameLanguage
			// 
			this.comboBox_GameLanguage.FormattingEnabled = true;
			this.comboBox_GameLanguage.Items.AddRange(new object[] {
            "Polish",
            "Russian",
            "English_US",
            "English_UK"});
			this.comboBox_GameLanguage.Location = new System.Drawing.Point(44, 25);
			this.comboBox_GameLanguage.Name = "comboBox_GameLanguage";
			this.comboBox_GameLanguage.Size = new System.Drawing.Size(121, 21);
			this.comboBox_GameLanguage.TabIndex = 0;
			this.comboBox_GameLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBox_GameLanguage_SelectedIndexChanged);
			// 
			// label_GameLanguage
			// 
			this.label_GameLanguage.AutoSize = true;
			this.label_GameLanguage.Location = new System.Drawing.Point(41, 9);
			this.label_GameLanguage.Name = "label_GameLanguage";
			this.label_GameLanguage.Size = new System.Drawing.Size(105, 13);
			this.label_GameLanguage.TabIndex = 1;
			this.label_GameLanguage.Text = "Your game language";
			// 
			// button_Patch
			// 
			this.button_Patch.Enabled = false;
			this.button_Patch.Location = new System.Drawing.Point(377, 106);
			this.button_Patch.Name = "button_Patch";
			this.button_Patch.Size = new System.Drawing.Size(75, 23);
			this.button_Patch.TabIndex = 2;
			this.button_Patch.Text = "Patch";
			this.button_Patch.UseVisualStyleBackColor = true;
			this.button_Patch.Click += new System.EventHandler(this.button_Patch_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(41, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "ME:A Path";
			// 
			// textBox_Path
			// 
			this.textBox_Path.Enabled = false;
			this.textBox_Path.Location = new System.Drawing.Point(44, 65);
			this.textBox_Path.Name = "textBox_Path";
			this.textBox_Path.Size = new System.Drawing.Size(300, 20);
			this.textBox_Path.TabIndex = 4;
			// 
			// button_Path
			// 
			this.button_Path.Enabled = false;
			this.button_Path.Location = new System.Drawing.Point(377, 63);
			this.button_Path.Name = "button_Path";
			this.button_Path.Size = new System.Drawing.Size(75, 23);
			this.button_Path.TabIndex = 5;
			this.button_Path.Text = "Browse";
			this.button_Path.UseVisualStyleBackColor = true;
			this.button_Path.Click += new System.EventHandler(this.button_Path_Click);
			// 
			// button_Restore
			// 
			this.button_Restore.Enabled = false;
			this.button_Restore.Location = new System.Drawing.Point(44, 106);
			this.button_Restore.Name = "button_Restore";
			this.button_Restore.Size = new System.Drawing.Size(121, 23);
			this.button_Restore.TabIndex = 6;
			this.button_Restore.Text = "Restore original exe";
			this.button_Restore.UseVisualStyleBackColor = true;
			this.button_Restore.Click += new System.EventHandler(this.button_Restore_Click);
			// 
			// comboBox_TargetLanguage
			// 
			this.comboBox_TargetLanguage.FormattingEnabled = true;
			this.comboBox_TargetLanguage.Items.AddRange(new object[] {
            "Polish",
            "Russian",
            "English_US",
            "English_UK"});
			this.comboBox_TargetLanguage.Location = new System.Drawing.Point(223, 25);
			this.comboBox_TargetLanguage.Name = "comboBox_TargetLanguage";
			this.comboBox_TargetLanguage.Size = new System.Drawing.Size(121, 21);
			this.comboBox_TargetLanguage.TabIndex = 7;
			this.comboBox_TargetLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBox_TargetLanguage_SelectedIndexChanged);
			// 
			// label_TargetLanguage
			// 
			this.label_TargetLanguage.AutoSize = true;
			this.label_TargetLanguage.Location = new System.Drawing.Point(223, 9);
			this.label_TargetLanguage.Name = "label_TargetLanguage";
			this.label_TargetLanguage.Size = new System.Drawing.Size(85, 13);
			this.label_TargetLanguage.TabIndex = 8;
			this.label_TargetLanguage.Text = "Target language";
			// 
			// Patcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 141);
			this.Controls.Add(this.label_TargetLanguage);
			this.Controls.Add(this.comboBox_TargetLanguage);
			this.Controls.Add(this.button_Restore);
			this.Controls.Add(this.button_Path);
			this.Controls.Add(this.textBox_Path);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_Patch);
			this.Controls.Add(this.label_GameLanguage);
			this.Controls.Add(this.comboBox_GameLanguage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(480, 180);
			this.MinimumSize = new System.Drawing.Size(480, 180);
			this.Name = "Patcher";
			this.Text = "Andromeda Language Patcher";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox_GameLanguage;
		private System.Windows.Forms.Label label_GameLanguage;
		private System.Windows.Forms.Button button_Patch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_Path;
		private System.Windows.Forms.Button button_Path;
		private System.Windows.Forms.Button button_Restore;
		private System.Windows.Forms.ComboBox comboBox_TargetLanguage;
		private System.Windows.Forms.Label label_TargetLanguage;
	}
}

