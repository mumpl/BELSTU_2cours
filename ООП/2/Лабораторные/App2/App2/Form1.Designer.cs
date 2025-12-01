namespace App2
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
            rb1 = new RadioButton();
            surname = new TextBox();
            name = new TextBox();
            lastname = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            monthCalendar1 = new MonthCalendar();
            label5 = new Label();
            comboSpecialization = new ComboBox();
            label6 = new Label();
            rb2 = new RadioButton();
            rb3 = new RadioButton();
            rb4 = new RadioButton();
            label7 = new Label();
            comboGroup = new ComboBox();
            label8 = new Label();
            trackMark = new TrackBar();
            label10 = new Label();
            rbMale = new RadioButton();
            rbFemale = new RadioButton();
            dataGridViewStudents = new DataGridView();
            button_save_xml = new Button();
            button_writeData = new Button();
            button_budget = new Button();
            budget_result = new Label();
            label9 = new Label();
            saveResult = new Label();
            dataResult = new Label();
            label11 = new Label();
            label12 = new Label();
            company = new TextBox();
            label13 = new Label();
            position = new TextBox();
            trackExperience = new TextBox();
            label14 = new Label();
            groupBox1 = new GroupBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)trackMark).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudents).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // rb1
            // 
            rb1.AutoSize = true;
            rb1.Location = new Point(85, 247);
            rb1.Name = "rb1";
            rb1.Size = new Size(38, 24);
            rb1.TabIndex = 0;
            rb1.TabStop = true;
            rb1.Text = "1";
            rb1.UseVisualStyleBackColor = true;
            // 
            // surname
            // 
            surname.Location = new Point(21, 42);
            surname.Name = "surname";
            surname.Size = new Size(225, 27);
            surname.TabIndex = 1;
            // 
            // name
            // 
            name.Location = new Point(21, 95);
            name.Name = "name";
            name.Size = new Size(225, 27);
            name.TabIndex = 2;
            name.TextChanged += textBox2_TextChanged;
            // 
            // lastname
            // 
            lastname.Location = new Point(21, 148);
            lastname.Name = "lastname";
            lastname.Size = new Size(225, 27);
            lastname.TabIndex = 3;
            lastname.TextChanged += textBox3_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 4;
            label1.Text = "Фамилия";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 72);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 5;
            label2.Text = "Имя";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 125);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 6;
            label3.Text = "Отчество";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(269, 19);
            label4.Name = "label4";
            label4.Size = new Size(116, 20);
            label4.TabIndex = 7;
            label4.Text = "Дата рождения";
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(269, 42);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 8;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 190);
            label5.Name = "label5";
            label5.Size = new Size(116, 20);
            label5.TabIndex = 9;
            label5.Text = "Специальность";
            label5.Click += label5_Click;
            // 
            // comboSpecialization
            // 
            comboSpecialization.FormattingEnabled = true;
            comboSpecialization.Items.AddRange(new object[] { "Информационные системы и технологии", "Цифровой дизайн", "Программная инженерия" });
            comboSpecialization.Location = new Point(24, 213);
            comboSpecialization.Name = "comboSpecialization";
            comboSpecialization.Size = new Size(151, 28);
            comboSpecialization.TabIndex = 10;
            comboSpecialization.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 258);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 11;
            label6.Text = "Курс";
            // 
            // rb2
            // 
            rb2.AutoSize = true;
            rb2.Location = new Point(135, 247);
            rb2.Name = "rb2";
            rb2.Size = new Size(38, 24);
            rb2.TabIndex = 12;
            rb2.TabStop = true;
            rb2.Text = "2";
            rb2.UseVisualStyleBackColor = true;
            rb2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // rb3
            // 
            rb3.AutoSize = true;
            rb3.Location = new Point(85, 277);
            rb3.Name = "rb3";
            rb3.Size = new Size(38, 24);
            rb3.TabIndex = 13;
            rb3.TabStop = true;
            rb3.Text = "3";
            rb3.UseVisualStyleBackColor = true;
            // 
            // rb4
            // 
            rb4.AutoSize = true;
            rb4.Location = new Point(135, 277);
            rb4.Name = "rb4";
            rb4.Size = new Size(38, 24);
            rb4.TabIndex = 14;
            rb4.TabStop = true;
            rb4.Text = "4";
            rb4.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 311);
            label7.Name = "label7";
            label7.Size = new Size(58, 20);
            label7.TabIndex = 15;
            label7.Text = "Группа";
            // 
            // comboGroup
            // 
            comboGroup.FormattingEnabled = true;
            comboGroup.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            comboGroup.Location = new Point(24, 334);
            comboGroup.Name = "comboGroup";
            comboGroup.Size = new Size(151, 28);
            comboGroup.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(269, 277);
            label8.Name = "label8";
            label8.Size = new Size(110, 20);
            label8.TabIndex = 17;
            label8.Text = "Средний балл:";
            // 
            // trackMark
            // 
            trackMark.Location = new Point(269, 300);
            trackMark.Name = "trackMark";
            trackMark.Size = new Size(192, 56);
            trackMark.TabIndex = 10;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(24, 384);
            label10.Name = "label10";
            label10.Size = new Size(37, 20);
            label10.TabIndex = 19;
            label10.Text = "Пол";
            // 
            // rbMale
            // 
            rbMale.AutoSize = true;
            rbMale.Location = new Point(85, 384);
            rbMale.Name = "rbMale";
            rbMale.Size = new Size(43, 24);
            rbMale.TabIndex = 20;
            rbMale.TabStop = true;
            rbMale.Text = "М";
            rbMale.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            rbFemale.AutoSize = true;
            rbFemale.Location = new Point(153, 384);
            rbFemale.Name = "rbFemale";
            rbFemale.Size = new Size(43, 24);
            rbFemale.TabIndex = 21;
            rbFemale.TabStop = true;
            rbFemale.Text = "Ж";
            rbFemale.UseVisualStyleBackColor = true;
            // 
            // dataGridViewStudents
            // 
            dataGridViewStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewStudents.Location = new Point(473, 75);
            dataGridViewStudents.Name = "dataGridViewStudents";
            dataGridViewStudents.RowHeadersWidth = 51;
            dataGridViewStudents.Size = new Size(586, 604);
            dataGridViewStudents.TabIndex = 22;
            dataGridViewStudents.CellContentClick += dataGridViewStudents_CellContentClick;
            // 
            // button_save_xml
            // 
            button_save_xml.Location = new Point(62, 658);
            button_save_xml.Name = "button_save_xml";
            button_save_xml.Size = new Size(147, 50);
            button_save_xml.TabIndex = 23;
            button_save_xml.Text = "Сохранить";
            button_save_xml.UseVisualStyleBackColor = true;
            button_save_xml.Click += button_save_xml_Click;
            // 
            // button_writeData
            // 
            button_writeData.Location = new Point(518, 30);
            button_writeData.Name = "button_writeData";
            button_writeData.Size = new Size(147, 39);
            button_writeData.TabIndex = 24;
            button_writeData.Text = "Вывести данные";
            button_writeData.UseVisualStyleBackColor = true;
            button_writeData.Click += button_writeData_Click;
            // 
            // button_budget
            // 
            button_budget.Location = new Point(33, 34);
            button_budget.Name = "button_budget";
            button_budget.Size = new Size(165, 39);
            button_budget.TabIndex = 25;
            button_budget.Text = "Посчитать бюджет";
            button_budget.UseVisualStyleBackColor = true;
            button_budget.Click += button_budget_Click;
            // 
            // budget_result
            // 
            budget_result.AutoSize = true;
            budget_result.Location = new Point(229, 53);
            budget_result.Name = "budget_result";
            budget_result.Size = new Size(45, 20);
            budget_result.TabIndex = 26;
            budget_result.Text = "result";
            budget_result.Click += budget_result_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(218, 23);
            label9.Name = "label9";
            label9.Size = new Size(66, 20);
            label9.TabIndex = 27;
            label9.Text = "Бюджет:";
            // 
            // saveResult
            // 
            saveResult.AutoSize = true;
            saveResult.Location = new Point(163, 738);
            saveResult.Name = "saveResult";
            saveResult.Size = new Size(0, 20);
            saveResult.TabIndex = 28;
            // 
            // dataResult
            // 
            dataResult.AutoSize = true;
            dataResult.Location = new Point(725, 39);
            dataResult.Name = "dataResult";
            dataResult.Size = new Size(0, 20);
            dataResult.TabIndex = 29;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(24, 423);
            label11.Name = "label11";
            label11.Size = new Size(141, 20);
            label11.TabIndex = 30;
            label11.Text = "ДОПОЛНИТЕЛЬНО";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(28, 454);
            label12.Name = "label12";
            label12.Size = new Size(108, 20);
            label12.TabIndex = 31;
            label12.Text = "Место работы";
            // 
            // company
            // 
            company.Location = new Point(24, 477);
            company.Name = "company";
            company.Size = new Size(225, 27);
            company.TabIndex = 32;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(24, 520);
            label13.Name = "label13";
            label13.Size = new Size(86, 20);
            label13.TabIndex = 33;
            label13.Text = "Должность";
            // 
            // position
            // 
            position.Location = new Point(24, 543);
            position.Name = "position";
            position.Size = new Size(225, 27);
            position.TabIndex = 34;
            // 
            // trackExperience
            // 
            trackExperience.Location = new Point(24, 611);
            trackExperience.Name = "trackExperience";
            trackExperience.Size = new Size(225, 27);
            trackExperience.TabIndex = 35;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(24, 588);
            label14.Name = "label14";
            label14.Size = new Size(43, 20);
            label14.TabIndex = 36;
            label14.Text = "Стаж";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button_budget);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(budget_result);
            groupBox1.Location = new Point(563, 685);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(386, 101);
            groupBox1.TabIndex = 37;
            groupBox1.TabStop = false;
            groupBox1.Text = "Рассчёт бюджета";
            // 
            // button1
            // 
            button1.Location = new Point(287, 675);
            button1.Name = "button1";
            button1.Size = new Size(142, 87);
            button1.TabIndex = 38;
            button1.Text = "Вторая форма";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1071, 798);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(label14);
            Controls.Add(trackExperience);
            Controls.Add(position);
            Controls.Add(label13);
            Controls.Add(company);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(dataResult);
            Controls.Add(saveResult);
            Controls.Add(button_writeData);
            Controls.Add(button_save_xml);
            Controls.Add(dataGridViewStudents);
            Controls.Add(rbFemale);
            Controls.Add(rbMale);
            Controls.Add(label10);
            Controls.Add(trackMark);
            Controls.Add(label8);
            Controls.Add(comboGroup);
            Controls.Add(label7);
            Controls.Add(rb4);
            Controls.Add(rb3);
            Controls.Add(rb2);
            Controls.Add(label6);
            Controls.Add(comboSpecialization);
            Controls.Add(label5);
            Controls.Add(monthCalendar1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lastname);
            Controls.Add(name);
            Controls.Add(surname);
            Controls.Add(rb1);
            Name = "Form1";
            Text = "Объект Студент";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackMark).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewStudents).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rb1;
        private TextBox surname;
        private TextBox name;
        private TextBox lastname;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private MonthCalendar monthCalendar1;
        private Label label5;
        private ComboBox comboSpecialization;
        private Label label6;
        private RadioButton rb2;
        private RadioButton rb3;
        private RadioButton rb4;
        private Label label7;
        private ComboBox comboGroup;
        private Label label8;
        private TrackBar trackMark;
        private Label label10;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private DataGridView dataGridViewStudents;
        private Button button_save_xml;
        private Button button_writeData;
        private Button button_budget;
        private Label budget_result;
        private Label label9;
        private Label saveResult;
        private Label dataResult;
        private Label label11;
        private Label label12;
        private TextBox company;
        private Label label13;
        private TextBox position;
        private TextBox trackExperience;
        private Label label14;
        private GroupBox groupBox1;
        private Button button1;
    }
}
