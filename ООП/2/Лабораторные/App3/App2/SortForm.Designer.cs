namespace App2
{
    partial class SortForm
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
            nameRadioButton = new RadioButton();
            courseRadioButton = new RadioButton();
            markRadioButton = new RadioButton();
            label1 = new Label();
            listBox1 = new ListBox();
            SaveButton1 = new Button();
            menuStrip1 = new MenuStrip();
            вперёдToolStripMenuItem = new ToolStripMenuItem();
            назадToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // nameRadioButton
            // 
            nameRadioButton.AutoSize = true;
            nameRadioButton.Location = new Point(12, 57);
            nameRadioButton.Name = "nameRadioButton";
            nameRadioButton.Size = new Size(118, 24);
            nameRadioButton.TabIndex = 0;
            nameRadioButton.TabStop = true;
            nameRadioButton.Text = "По алфавиту";
            nameRadioButton.UseVisualStyleBackColor = true;
            nameRadioButton.CheckedChanged += nameRadioButton_CheckedChanged;
            // 
            // courseRadioButton
            // 
            courseRadioButton.AutoSize = true;
            courseRadioButton.Location = new Point(148, 57);
            courseRadioButton.Name = "courseRadioButton";
            courseRadioButton.Size = new Size(91, 24);
            courseRadioButton.TabIndex = 1;
            courseRadioButton.TabStop = true;
            courseRadioButton.Text = "По курсу";
            courseRadioButton.UseVisualStyleBackColor = true;
            courseRadioButton.CheckedChanged += courseRadioButton_CheckedChanged;
            // 
            // markRadioButton
            // 
            markRadioButton.AutoSize = true;
            markRadioButton.Location = new Point(259, 57);
            markRadioButton.Name = "markRadioButton";
            markRadioButton.Size = new Size(165, 24);
            markRadioButton.TabIndex = 2;
            markRadioButton.TabStop = true;
            markRadioButton.Text = "По среднему баллу";
            markRadioButton.UseVisualStyleBackColor = true;
            markRadioButton.CheckedChanged += markRadioButton_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(103, 30);
            label1.Name = "label1";
            label1.Size = new Size(233, 24);
            label1.TabIndex = 3;
            label1.Text = "Выберите тип сортировки:";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 98);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(421, 184);
            listBox1.TabIndex = 5;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // SaveButton1
            // 
            SaveButton1.Location = new Point(164, 288);
            SaveButton1.Name = "SaveButton1";
            SaveButton1.Size = new Size(112, 34);
            SaveButton1.TabIndex = 6;
            SaveButton1.Text = "Сохранить";
            SaveButton1.UseVisualStyleBackColor = true;
            SaveButton1.Click += SaveButton1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { вперёдToolStripMenuItem, назадToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(445, 28);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // вперёдToolStripMenuItem
            // 
            вперёдToolStripMenuItem.Name = "вперёдToolStripMenuItem";
            вперёдToolStripMenuItem.Size = new Size(74, 24);
            вперёдToolStripMenuItem.Text = "Вперёд";
            вперёдToolStripMenuItem.Click += вперёдToolStripMenuItem_Click;
            // 
            // назадToolStripMenuItem
            // 
            назадToolStripMenuItem.Name = "назадToolStripMenuItem";
            назадToolStripMenuItem.Size = new Size(65, 24);
            назадToolStripMenuItem.Text = "Назад";
            назадToolStripMenuItem.Click += назадToolStripMenuItem_Click;
            // 
            // SortForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(445, 334);
            Controls.Add(SaveButton1);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(markRadioButton);
            Controls.Add(courseRadioButton);
            Controls.Add(nameRadioButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "SortForm";
            Text = "SortForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton nameRadioButton;
        private RadioButton courseRadioButton;
        private RadioButton markRadioButton;
        private Label label1;
        private ListBox listBox1;
        private Button SaveButton1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem вперёдToolStripMenuItem;
        private ToolStripMenuItem назадToolStripMenuItem;
    }
}