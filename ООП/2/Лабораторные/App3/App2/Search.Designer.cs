namespace App2
{
    partial class Search
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
            searchTextBox = new TextBox();
            exactMatchRadioButton = new RadioButton();
            diapasonRadioButton = new RadioButton();
            searchButton = new Button();
            saveResultsButton = new Button();
            listBoxResults = new ListBox();
            letterRadioButton = new RadioButton();
            menuStrip1 = new MenuStrip();
            вперёдToolStripMenuItem = new ToolStripMenuItem();
            назадToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(12, 52);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(286, 27);
            searchTextBox.TabIndex = 0;
            // 
            // exactMatchRadioButton
            // 
            exactMatchRadioButton.AutoSize = true;
            exactMatchRadioButton.Location = new Point(12, 85);
            exactMatchRadioButton.Name = "exactMatchRadioButton";
            exactMatchRadioButton.Size = new Size(84, 24);
            exactMatchRadioButton.TabIndex = 1;
            exactMatchRadioButton.TabStop = true;
            exactMatchRadioButton.Text = "Полное";
            exactMatchRadioButton.UseVisualStyleBackColor = true;
            // 
            // diapasonRadioButton
            // 
            diapasonRadioButton.AutoSize = true;
            diapasonRadioButton.Location = new Point(12, 145);
            diapasonRadioButton.Name = "diapasonRadioButton";
            diapasonRadioButton.Size = new Size(99, 24);
            diapasonRadioButton.TabIndex = 2;
            diapasonRadioButton.TabStop = true;
            diapasonRadioButton.Text = "Диапазон";
            diapasonRadioButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(326, 67);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(94, 29);
            searchButton.TabIndex = 3;
            searchButton.Text = "Поиск";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // saveResultsButton
            // 
            saveResultsButton.ForeColor = SystemColors.ActiveCaptionText;
            saveResultsButton.Location = new Point(304, 115);
            saveResultsButton.Name = "saveResultsButton";
            saveResultsButton.Size = new Size(135, 34);
            saveResultsButton.TabIndex = 5;
            saveResultsButton.Text = "Сохранить";
            saveResultsButton.UseVisualStyleBackColor = true;
            saveResultsButton.Click += saveResultsButton_Click;
            // 
            // listBoxResults
            // 
            listBoxResults.FormattingEnabled = true;
            listBoxResults.Location = new Point(12, 196);
            listBoxResults.Name = "listBoxResults";
            listBoxResults.Size = new Size(427, 184);
            listBoxResults.TabIndex = 6;
            // 
            // letterRadioButton
            // 
            letterRadioButton.AutoSize = true;
            letterRadioButton.Location = new Point(12, 115);
            letterRadioButton.Name = "letterRadioButton";
            letterRadioButton.Size = new Size(126, 24);
            letterRadioButton.TabIndex = 7;
            letterRadioButton.TabStop = true;
            letterRadioButton.Text = "Наличие букв";
            letterRadioButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { вперёдToolStripMenuItem, назадToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(458, 28);
            menuStrip1.TabIndex = 8;
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
            // Search
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(458, 392);
            Controls.Add(letterRadioButton);
            Controls.Add(listBoxResults);
            Controls.Add(saveResultsButton);
            Controls.Add(searchButton);
            Controls.Add(diapasonRadioButton);
            Controls.Add(exactMatchRadioButton);
            Controls.Add(searchTextBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Search";
            Text = "Search";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox searchTextBox;
        private RadioButton exactMatchRadioButton;
        private RadioButton diapasonRadioButton;
        private Button searchButton;
        private Button saveResultsButton;
        private ListBox listBoxResults;
        private RadioButton letterRadioButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem вперёдToolStripMenuItem;
        private ToolStripMenuItem назадToolStripMenuItem;
    }
}