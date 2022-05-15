namespace Файловый_менеджер
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxSearching = new System.Windows.Forms.TextBox();
            this.comboBoxTheme = new System.Windows.Forms.ComboBox();
            this.labelTheme = new System.Windows.Forms.Label();
            this.labelFont = new System.Windows.Forms.Label();
            this.labelTextSize = new System.Windows.Forms.Label();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.comboBoxTextSize = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.listViewBooks = new System.Windows.Forms.ListView();
            this.columnBookName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxBooksOnPage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxSearching
            // 
            this.textBoxSearching.AutoCompleteCustomSource.AddRange(new string[] {
            "python",
            "c",
            "c sharp",
            "f sharp",
            "sql",
            "ruby",
            "pascal",
            "java",
            "haskell",
            "kotlin",
            "delphi",
            "basic",
            "perl",
            "julia",
            "fortran",
            "lua"});
            this.textBoxSearching.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxSearching.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxSearching.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearching.Location = new System.Drawing.Point(15, 28);
            this.textBoxSearching.Name = "textBoxSearching";
            this.textBoxSearching.Size = new System.Drawing.Size(607, 38);
            this.textBoxSearching.TabIndex = 2;
            // 
            // comboBoxTheme
            // 
            this.comboBoxTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTheme.FormattingEnabled = true;
            this.comboBoxTheme.Items.AddRange(new object[] {
            "Розовый минимализм",
            "Бумага",
            "Письма",
            "Книги",
            "Дождь"});
            this.comboBoxTheme.Location = new System.Drawing.Point(15, 438);
            this.comboBoxTheme.Name = "comboBoxTheme";
            this.comboBoxTheme.Size = new System.Drawing.Size(200, 39);
            this.comboBoxTheme.TabIndex = 13;
            this.comboBoxTheme.SelectedIndexChanged += new System.EventHandler(this.comboBoxTheme_SelectedIndexChanged);
            // 
            // labelTheme
            // 
            this.labelTheme.AutoSize = true;
            this.labelTheme.BackColor = System.Drawing.Color.White;
            this.labelTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTheme.Location = new System.Drawing.Point(15, 403);
            this.labelTheme.Name = "labelTheme";
            this.labelTheme.Size = new System.Drawing.Size(83, 32);
            this.labelTheme.TabIndex = 14;
            this.labelTheme.Text = "Тема";
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFont.Location = new System.Drawing.Point(272, 403);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(106, 32);
            this.labelFont.TabIndex = 15;
            this.labelFont.Text = "Шрифт";
            // 
            // labelTextSize
            // 
            this.labelTextSize.AutoSize = true;
            this.labelTextSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTextSize.Location = new System.Drawing.Point(526, 403);
            this.labelTextSize.Name = "labelTextSize";
            this.labelTextSize.Size = new System.Drawing.Size(208, 32);
            this.labelTextSize.TabIndex = 16;
            this.labelTextSize.Text = "Размер текста";
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Items.AddRange(new object[] {
            "Arial",
            "Courier New",
            "Impact",
            "Monotype Corsiva",
            "Times New Roman"});
            this.comboBoxFont.Location = new System.Drawing.Point(278, 438);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(200, 39);
            this.comboBoxFont.TabIndex = 18;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // comboBoxTextSize
            // 
            this.comboBoxTextSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTextSize.FormattingEnabled = true;
            this.comboBoxTextSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14"});
            this.comboBoxTextSize.Location = new System.Drawing.Point(532, 438);
            this.comboBoxTextSize.Name = "comboBoxTextSize";
            this.comboBoxTextSize.Size = new System.Drawing.Size(200, 39);
            this.comboBoxTextSize.TabIndex = 19;
            this.comboBoxTextSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextSize_SelectedIndexChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.White;
            this.buttonSearch.BackgroundImage = global::Файловый_менеджер.Properties.Resources.значок_найти;
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSearch.Location = new System.Drawing.Point(684, 28);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(50, 50);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // listViewBooks
            // 
            this.listViewBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnBookName,
            this.columnAuthor,
            this.columnRating,
            this.columnData,
            this.columnPrice});
            this.listViewBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewBooks.HideSelection = false;
            this.listViewBooks.Location = new System.Drawing.Point(15, 87);
            this.listViewBooks.Name = "listViewBooks";
            this.listViewBooks.Size = new System.Drawing.Size(717, 298);
            this.listViewBooks.TabIndex = 20;
            this.listViewBooks.UseCompatibleStateImageBehavior = false;
            this.listViewBooks.View = System.Windows.Forms.View.Details;
            this.listViewBooks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewBooks_ColumnClick);
            this.listViewBooks.DoubleClick += new System.EventHandler(this.listViewBooks_DoubleClick);
            // 
            // columnBookName
            // 
            this.columnBookName.Text = "Название книги";
            this.columnBookName.Width = 220;
            // 
            // columnAuthor
            // 
            this.columnAuthor.Text = "Автор";
            this.columnAuthor.Width = 100;
            // 
            // columnRating
            // 
            this.columnRating.Text = "Рейтинг";
            this.columnRating.Width = 75;
            // 
            // columnData
            // 
            this.columnData.Text = "Дата выпуска";
            this.columnData.Width = 100;
            // 
            // columnPrice
            // 
            this.columnPrice.Text = "Цена";
            this.columnPrice.Width = 50;
            // 
            // textBoxBooksOnPage
            // 
            this.textBoxBooksOnPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBooksOnPage.Location = new System.Drawing.Point(628, 29);
            this.textBoxBooksOnPage.Name = "textBoxBooksOnPage";
            this.textBoxBooksOnPage.Size = new System.Drawing.Size(50, 38);
            this.textBoxBooksOnPage.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.светло_розовый_фон;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(823, 528);
            this.Controls.Add(this.textBoxBooksOnPage);
            this.Controls.Add(this.listViewBooks);
            this.Controls.Add(this.comboBoxTextSize);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.labelTextSize);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.labelTheme);
            this.Controls.Add(this.comboBoxTheme);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearching);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Книжный менеджер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxSearching;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox comboBoxTheme;
        private System.Windows.Forms.Label labelTheme;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.Label labelTextSize;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.ComboBox comboBoxTextSize;
        private System.Windows.Forms.ListView listViewBooks;
        public System.Windows.Forms.ColumnHeader columnBookName;
        private System.Windows.Forms.ColumnHeader columnAuthor;
        private System.Windows.Forms.ColumnHeader columnRating;
        private System.Windows.Forms.ColumnHeader columnData;
        private System.Windows.Forms.ColumnHeader columnPrice;
        private System.Windows.Forms.TextBox textBoxBooksOnPage;
    }
}

