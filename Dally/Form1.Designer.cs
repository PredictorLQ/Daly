namespace Daly
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDALYDataToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.преобразованиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полныйПакетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.частичныйПакетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetDALYCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.таблицыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.населениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дожитиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.таблицыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDALYDataToFileToolStripMenuItem,
            this.преобразованиеToolStripMenuItem,
            this.resetDALYCalculatorToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            this.fileToolStripMenuItem.MouseHover += new System.EventHandler(this.fileToolStripMenuItem_MouseHover);
            // 
            // saveDALYDataToFileToolStripMenuItem
            // 
            this.saveDALYDataToFileToolStripMenuItem.Name = "saveDALYDataToFileToolStripMenuItem";
            this.saveDALYDataToFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveDALYDataToFileToolStripMenuItem.Text = "Сохранить";
            this.saveDALYDataToFileToolStripMenuItem.Click += new System.EventHandler(this.saveDALYDataToFileToolStripMenuItem_Click);
            // 
            // преобразованиеToolStripMenuItem
            // 
            this.преобразованиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.полныйПакетToolStripMenuItem,
            this.частичныйПакетToolStripMenuItem});
            this.преобразованиеToolStripMenuItem.Name = "преобразованиеToolStripMenuItem";
            this.преобразованиеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.преобразованиеToolStripMenuItem.Text = "Преобразование";
            // 
            // полныйПакетToolStripMenuItem
            // 
            this.полныйПакетToolStripMenuItem.Name = "полныйПакетToolStripMenuItem";
            this.полныйПакетToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.полныйПакетToolStripMenuItem.Text = "Полный пакет";
            this.полныйПакетToolStripMenuItem.Click += new System.EventHandler(this.полныйПакетToolStripMenuItem_Click);
            // 
            // частичныйПакетToolStripMenuItem
            // 
            this.частичныйПакетToolStripMenuItem.Name = "частичныйПакетToolStripMenuItem";
            this.частичныйПакетToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.частичныйПакетToolStripMenuItem.Text = "Частичный пакет";
            this.частичныйПакетToolStripMenuItem.Click += new System.EventHandler(this.частичныйПакетToolStripMenuItem_Click);
            // 
            // resetDALYCalculatorToolStripMenuItem
            // 
            this.resetDALYCalculatorToolStripMenuItem.Name = "resetDALYCalculatorToolStripMenuItem";
            this.resetDALYCalculatorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resetDALYCalculatorToolStripMenuItem.Text = "Перезапустить";
            this.resetDALYCalculatorToolStripMenuItem.Click += new System.EventHandler(this.resetDALYCalculatorToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.settingToolStripMenuItem.Text = "Настройки";
            // 
            // constantToolStripMenuItem
            // 
            this.constantToolStripMenuItem.Name = "constantToolStripMenuItem";
            this.constantToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.constantToolStripMenuItem.Text = "Константы";
            this.constantToolStripMenuItem.Click += new System.EventHandler(this.constantToolStripMenuItem_Click);
            // 
            // таблицыToolStripMenuItem
            // 
            this.таблицыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.населениеToolStripMenuItem,
            this.дожитиеToolStripMenuItem});
            this.таблицыToolStripMenuItem.Name = "таблицыToolStripMenuItem";
            this.таблицыToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.таблицыToolStripMenuItem.Text = "Таблицы";
            this.таблицыToolStripMenuItem.MouseHover += new System.EventHandler(this.таблицыToolStripMenuItem_MouseHover);
            // 
            // населениеToolStripMenuItem
            // 
            this.населениеToolStripMenuItem.Name = "населениеToolStripMenuItem";
            this.населениеToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.населениеToolStripMenuItem.Text = "Население";
            this.населениеToolStripMenuItem.Click += new System.EventHandler(this.населениеToolStripMenuItem_Click);
            // 
            // дожитиеToolStripMenuItem
            // 
            this.дожитиеToolStripMenuItem.Name = "дожитиеToolStripMenuItem";
            this.дожитиеToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.дожитиеToolStripMenuItem.Text = "Дожитие";
            this.дожитиеToolStripMenuItem.Click += new System.EventHandler(this.дожитиеToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Локализации";
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.PaleGreen;
            this.button10.Location = new System.Drawing.Point(263, 482);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(292, 41);
            this.button10.TabIndex = 32;
            this.button10.Text = "Рассчитать";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 105);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(23, 67);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(71, 17);
            this.checkBox3.TabIndex = 34;
            this.checkBox3.Text = "Все года";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(23, 43);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(91, 17);
            this.checkBox2.TabIndex = 33;
            this.checkBox2.Text = "Все регионы";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 17);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "Все заболевания";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(765, 337);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Годы";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(598, 43);
            this.listBox3.Name = "listBox3";
            this.listBox3.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox3.Size = new System.Drawing.Size(135, 277);
            this.listBox3.TabIndex = 37;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Регионы";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(322, 43);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox2.Size = new System.Drawing.Size(250, 277);
            this.listBox2.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(23, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(270, 277);
            this.listBox1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Получение популяции";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(361, 47);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(140, 17);
            this.progressBar1.Step = 5;
            this.progressBar1.TabIndex = 36;
            this.progressBar1.DockChanged += new System.EventHandler(this.progressBar1_DockChanged);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(361, 71);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(140, 17);
            this.progressBar2.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Получение заболеваний";
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(361, 95);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(140, 17);
            this.progressBar3.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Преобразование данных";
            // 
            // progressBar4
            // 
            this.progressBar4.Location = new System.Drawing.Point(637, 47);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(140, 17);
            this.progressBar4.Step = 5;
            this.progressBar4.TabIndex = 42;
            this.progressBar4.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(505, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Получение популяции";
            this.label7.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 535);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Daly калькулятор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDALYDataToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetDALYCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ToolStripMenuItem constantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem таблицыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem населениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дожитиеToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem преобразованиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem полныйПакетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem частичныйПакетToolStripMenuItem;
    }
}

