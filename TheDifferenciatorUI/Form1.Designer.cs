
namespace TheDifferenciatorUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.folderPathLabel = new System.Windows.Forms.Label();
            this.FolderBrowse = new System.Windows.Forms.Button();
            this.imageName = new System.Windows.Forms.TextBox();
            this.imageNameLabel = new System.Windows.Forms.Label();
            this.ProduceCSV = new System.Windows.Forms.Button();
            this.StopProcesses = new System.Windows.Forms.Button();
            this.TestCrop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FolderBrowseOutput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.foldernameOutput = new System.Windows.Forms.TextBox();
            this.scoreTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(80, 118);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(860, 395);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(764, 532);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 75);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(254, 15);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(371, 20);
            this.folderPath.TabIndex = 9;
            this.folderPath.TextChanged += new System.EventHandler(this.folderPath_TextChanged);
            // 
            // folderPathLabel
            // 
            this.folderPathLabel.AutoSize = true;
            this.folderPathLabel.Location = new System.Drawing.Point(190, 17);
            this.folderPathLabel.Name = "folderPathLabel";
            this.folderPathLabel.Size = new System.Drawing.Size(58, 13);
            this.folderPathLabel.TabIndex = 10;
            this.folderPathLabel.Text = "FolderPath";
            // 
            // FolderBrowse
            // 
            this.FolderBrowse.Location = new System.Drawing.Point(102, 12);
            this.FolderBrowse.Name = "FolderBrowse";
            this.FolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.FolderBrowse.TabIndex = 11;
            this.FolderBrowse.Text = "Browse Folder";
            this.FolderBrowse.UseVisualStyleBackColor = true;
            // 
            // imageName
            // 
            this.imageName.Enabled = false;
            this.imageName.Location = new System.Drawing.Point(144, 80);
            this.imageName.Name = "imageName";
            this.imageName.Size = new System.Drawing.Size(244, 20);
            this.imageName.TabIndex = 13;
            // 
            // imageNameLabel
            // 
            this.imageNameLabel.AutoSize = true;
            this.imageNameLabel.Location = new System.Drawing.Point(52, 83);
            this.imageNameLabel.Name = "imageNameLabel";
            this.imageNameLabel.Size = new System.Drawing.Size(67, 13);
            this.imageNameLabel.TabIndex = 12;
            this.imageNameLabel.Text = "Image Name";
            // 
            // ProduceCSV
            // 
            this.ProduceCSV.Location = new System.Drawing.Point(71, 550);
            this.ProduceCSV.Name = "ProduceCSV";
            this.ProduceCSV.Size = new System.Drawing.Size(106, 30);
            this.ProduceCSV.TabIndex = 14;
            this.ProduceCSV.Text = "Produce CSV";
            this.ProduceCSV.UseVisualStyleBackColor = true;
            this.ProduceCSV.Click += new System.EventHandler(this.ProduceCSV_Click);
            // 
            // StopProcesses
            // 
            this.StopProcesses.Location = new System.Drawing.Point(102, 586);
            this.StopProcesses.Name = "StopProcesses";
            this.StopProcesses.Size = new System.Drawing.Size(154, 23);
            this.StopProcesses.TabIndex = 30;
            this.StopProcesses.Text = "Stop Process";
            this.StopProcesses.UseVisualStyleBackColor = true;
            this.StopProcesses.Click += new System.EventHandler(this.StopProcesses_Click);
            // 
            // TestCrop
            // 
            this.TestCrop.Location = new System.Drawing.Point(180, 550);
            this.TestCrop.Name = "TestCrop";
            this.TestCrop.Size = new System.Drawing.Size(106, 30);
            this.TestCrop.TabIndex = 31;
            this.TestCrop.Text = "TestCrop";
            this.TestCrop.UseVisualStyleBackColor = true;
            this.TestCrop.Click += new System.EventHandler(this.TestCrop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "INPUT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "OUTPUT";
            // 
            // FolderBrowseOutput
            // 
            this.FolderBrowseOutput.Location = new System.Drawing.Point(102, 41);
            this.FolderBrowseOutput.Name = "FolderBrowseOutput";
            this.FolderBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.FolderBrowseOutput.TabIndex = 35;
            this.FolderBrowseOutput.Text = "Browse Folder";
            this.FolderBrowseOutput.UseVisualStyleBackColor = true;
            this.FolderBrowseOutput.Click += new System.EventHandler(this.FolderBrowseOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "FolderPath";
            // 
            // foldernameOutput
            // 
            this.foldernameOutput.Location = new System.Drawing.Point(254, 44);
            this.foldernameOutput.Name = "foldernameOutput";
            this.foldernameOutput.Size = new System.Drawing.Size(371, 20);
            this.foldernameOutput.TabIndex = 33;
            this.foldernameOutput.TextChanged += new System.EventHandler(this.foldernameOutput_TextChanged);
            // 
            // scoreTextbox
            // 
            this.scoreTextbox.Location = new System.Drawing.Point(782, 44);
            this.scoreTextbox.Name = "scoreTextbox";
            this.scoreTextbox.Size = new System.Drawing.Size(100, 20);
            this.scoreTextbox.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(683, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Darkness Score";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(673, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Less than this score will be Wet more than will be Dry";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 627);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scoreTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FolderBrowseOutput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.foldernameOutput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TestCrop);
            this.Controls.Add(this.StopProcesses);
            this.Controls.Add(this.ProduceCSV);
            this.Controls.Add(this.imageName);
            this.Controls.Add(this.imageNameLabel);
            this.Controls.Add(this.FolderBrowse);
            this.Controls.Add(this.folderPathLabel);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Label folderPathLabel;
        private System.Windows.Forms.Button FolderBrowse;
        private System.Windows.Forms.TextBox imageName;
        private System.Windows.Forms.Label imageNameLabel;
        private System.Windows.Forms.Button ProduceCSV;
        private System.Windows.Forms.Button StopProcesses;
        private System.Windows.Forms.Button TestCrop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button FolderBrowseOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox foldernameOutput;
        private System.Windows.Forms.TextBox scoreTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

