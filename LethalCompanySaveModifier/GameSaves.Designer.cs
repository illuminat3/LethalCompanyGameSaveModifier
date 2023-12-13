namespace LethalCompanySaveModifier
{
    partial class GameSaves
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
            listBox1 = new ListBox();
            LoadButton = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(150, 10);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(150, 289);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(150, 375);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(150, 50);
            LoadButton.TabIndex = 1;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // GameSaves
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 450);
            Controls.Add(LoadButton);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GameSaves";
            Text = "GameSaves";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button LoadButton;
    }
}