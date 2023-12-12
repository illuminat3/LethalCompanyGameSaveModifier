namespace LethalCompanySaveModifier
{
    partial class Form1
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
            DecryptButton = new Button();
            EncryptButton = new Button();
            SuspendLayout();
            // 
            // DecryptButton
            // 
            DecryptButton.Location = new Point(265, 21);
            DecryptButton.Name = "DecryptButton";
            DecryptButton.Size = new Size(239, 99);
            DecryptButton.TabIndex = 0;
            DecryptButton.Text = "Decrypt";
            DecryptButton.UseVisualStyleBackColor = true;
            DecryptButton.Click += DecryptButton_Click;
            // 
            // EncryptButton
            // 
            EncryptButton.Location = new Point(265, 320);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(239, 118);
            EncryptButton.TabIndex = 1;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(EncryptButton);
            Controls.Add(DecryptButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button DecryptButton;
        private Button EncryptButton;
    }
}
