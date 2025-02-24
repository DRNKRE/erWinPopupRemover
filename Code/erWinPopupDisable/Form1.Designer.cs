namespace erWinBatchPopupDisable {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            labelDir = new Label();
            labelInstructions = new Label();
            textBoxDir = new TextBox();
            btnBrowser = new Button();
            checkBoxRecursive = new CheckBox();
            btnStop = new Button();
            btnGo = new Button();
            progressBar1 = new ProgressBar();
            labelMessage = new Label();
            SuspendLayout();
            // 
            // labelDir
            // 
            labelDir.AutoSize = true;
            labelDir.Location = new Point(9, 44);
            labelDir.Name = "labelDir";
            labelDir.Size = new Size(89, 15);
            labelDir.TabIndex = 0;
            labelDir.Text = "Select Directory";
            // 
            // labelInstructions
            // 
            labelInstructions.AutoSize = true;
            labelInstructions.Location = new Point(8, 13);
            labelInstructions.Name = "labelInstructions";
            labelInstructions.Size = new Size(641, 15);
            labelInstructions.TabIndex = 1;
            labelInstructions.Text = "Select a directory that has your VAG PDF manuals in it. Make a backup because this will overwrite the file in the directory.";
            // 
            // textBoxDir
            // 
            textBoxDir.Location = new Point(104, 41);
            textBoxDir.Name = "textBoxDir";
            textBoxDir.Size = new Size(472, 23);
            textBoxDir.TabIndex = 2;
            // 
            // btnBrowser
            // 
            btnBrowser.Location = new Point(582, 39);
            btnBrowser.Name = "btnBrowser";
            btnBrowser.Size = new Size(91, 25);
            btnBrowser.TabIndex = 3;
            btnBrowser.Text = "Browse";
            btnBrowser.UseVisualStyleBackColor = true;
            btnBrowser.Click += btnBrowser_Click;
            // 
            // checkBoxRecursive
            // 
            checkBoxRecursive.AutoSize = true;
            checkBoxRecursive.Location = new Point(104, 70);
            checkBoxRecursive.Name = "checkBoxRecursive";
            checkBoxRecursive.Size = new Size(76, 19);
            checkBoxRecursive.TabIndex = 5;
            checkBoxRecursive.Text = "Recursive";
            checkBoxRecursive.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(7, 93);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(91, 25);
            btnStop.TabIndex = 6;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnGo
            // 
            btnGo.Location = new Point(582, 93);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(91, 25);
            btnGo.TabIndex = 7;
            btnGo.Text = "Go";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(104, 94);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(472, 24);
            progressBar1.TabIndex = 8;
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.BackColor = Color.Transparent;
            labelMessage.Location = new Point(7, 121);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(68, 15);
            labelMessage.TabIndex = 9;
            labelMessage.Text = "Current File";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 143);
            Controls.Add(labelMessage);
            Controls.Add(progressBar1);
            Controls.Add(btnGo);
            Controls.Add(btnStop);
            Controls.Add(checkBoxRecursive);
            Controls.Add(btnBrowser);
            Controls.Add(textBoxDir);
            Controls.Add(labelInstructions);
            Controls.Add(labelDir);
            Name = "Form1";
            Text = "erWin Popup Remover";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDir;
        private Label labelInstructions;
        private TextBox textBoxDir;
        private Button btnBrowser;
        private CheckBox checkBoxRecursive;
        private Button btnStop;
        private Button btnGo;
        private ProgressBar progressBar1;
        private Label labelMessage;
    }
}