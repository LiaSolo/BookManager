using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Text.RegularExpressions;





namespace Файловый_менеджер
{ 
    public partial class MainForm : Form
    {
        Dictionary<string, string> books; //зачем и почему словарь?
        ColumnSorter columnSorter;
        int numberCurrentPage = 1;
        
        public MainForm()
        {
            columnSorter = new ColumnSorter();
            InitializeComponent();
            InitializeStyle();
                     
            ToolTip tips = new ToolTip();
            tips.ShowAlways = true;
            tips.SetToolTip(this.textBoxBooksOnPage, "Книг на странице");
            tips.SetToolTip(this.buttonSearch, "Найти");
        }

        #region Красота
        private void InitializeStyle()
        {
            comboBoxFont.Text = Settings.GetCurrent().currentFont;
            comboBoxFont_SelectedIndexChanged(comboBoxFont, null);

            comboBoxTheme.Text = Settings.GetCurrent().currentTheme;
            comboBoxTheme_SelectedIndexChanged(comboBoxTheme, null);

            comboBoxTextSize.Text = Settings.GetCurrent().currentTextSize.ToString();
            comboBoxTextSize_SelectedIndexChanged(comboBoxTextSize, null);
        }

        private void ChangeTheme(int red, int green, int blue, Color colorText)
        {         
            buttonSearch.BackColor = Color.FromArgb(red, green, blue);

            listViewBooks.BackColor = Color.FromArgb(red, green, blue);
            textBoxBooksOnPage.BackColor = Color.FromArgb(red, green, blue);
            textBoxSearching.BackColor = Color.FromArgb(red, green, blue);
            comboBoxFont.BackColor = Color.FromArgb(red, green, blue);
            comboBoxTextSize.BackColor = Color.FromArgb(red, green, blue);
            comboBoxTheme.BackColor = Color.FromArgb(red, green, blue);
            labelTheme.BackColor = Color.FromArgb(red, green, blue);
            labelFont.BackColor = Color.FromArgb(red, green, blue);
            labelTextSize.BackColor = Color.FromArgb(red, green, blue);

            listViewBooks.ForeColor = colorText;
            textBoxBooksOnPage.ForeColor = colorText;
            textBoxSearching.ForeColor = colorText;
            comboBoxFont.ForeColor = colorText;
            comboBoxTextSize.ForeColor = colorText;
            comboBoxTheme.ForeColor = colorText;
            labelTextSize.ForeColor = colorText;
            labelFont.ForeColor = colorText;
            labelTheme.ForeColor = colorText;
        }

        private void comboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.GetCurrent().currentTheme = ((ComboBox)sender).Text;
            switch (((ComboBox)sender).Text)
            {
                case "Розовый минимализм":
                    this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.светло_розовый_фон;
                    ChangeTheme(255, 128, 128, Color.White);                  
                    break;
                case "Дождь":
                    this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.фон_дождь;
                    ChangeTheme(107, 97, 103, Color.White);
                    break;
                case "Бумага":
                    this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.фон_бумага;
                    ChangeTheme(255, 255, 255, Color.Black);
                    break;
                case "Письма":
                    this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.фон_письма;
                    ChangeTheme(246, 224, 200, Color.Black); 
                    break;
                case "Книги":
                    this.BackgroundImage = global::Файловый_менеджер.Properties.Resources.фон_книги;
                    ChangeTheme(220, 198, 249, Color.Black); //сменить цвет
                    break;
            }
        }

        //смена шрифта
        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newFont = ((ComboBox)sender).Text;
            Settings.GetCurrent().currentFont = newFont;
            float currentTextSize = Settings.GetCurrent().currentTextSize;
                       
            textBoxSearching.Font = new Font(newFont, currentTextSize);
            listViewBooks.Font = new Font(newFont, currentTextSize);
            textBoxBooksOnPage.Font = new Font(newFont, currentTextSize);
            comboBoxFont.Font = new Font(newFont, currentTextSize);
            comboBoxTextSize.Font = new Font(newFont, currentTextSize);
            comboBoxTheme.Font = new Font(newFont, currentTextSize);        
            labelTheme.Font = new Font(newFont, currentTextSize);
            labelFont.Font = new Font(newFont, currentTextSize);
            labelTextSize.Font = new Font(newFont, currentTextSize);
        }

        //смена размера текста
        private void comboBoxTextSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.GetCurrent().currentTextSize = float.Parse(((ComboBox)sender).Text);
            string currentFont = comboBoxFont.Text;
            float newTextSize = Settings.GetCurrent().currentTextSize;

            textBoxSearching.Font = new Font(currentFont, newTextSize);
            listViewBooks.Font = new Font(currentFont, newTextSize);
            textBoxBooksOnPage.Font = new Font(currentFont, newTextSize);
            comboBoxFont.Font = new Font(currentFont, newTextSize);
            comboBoxTextSize.Font = new Font(currentFont, newTextSize);
            comboBoxTheme.Font = new Font(currentFont, newTextSize);
            labelTheme.Font = new Font(currentFont, newTextSize);
            labelFont.Font = new Font(currentFont, newTextSize);
            labelTextSize.Font = new Font(currentFont, newTextSize);
        }
        #endregion

        //всё это надо понять, пусть миша объясняет

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string adress = @"https://www.amazon.com/s?k=" + textBoxSearching.Text 
                + "&i=stripbooks-intl-ship&ref=nb_sb_noss";
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string s = webClient.DownloadString(adress);

            int countBooks;

            try
            {
                countBooks = int.Parse(textBoxBooksOnPage.Text);
            }
            catch (Exception ex)
            {
                countBooks = 10;
            }

            Regex regexAuthor = new Regex("<a class=\"a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style\" href=\"(.*?)\">(.*?)</a>");

            listViewBooks.Items.Clear();
            books = new Dictionary<string, string>();
            BooksParse(s, countBooks);
        }

        //ничего непонятно, пусть миша объясняет + сортировку лист вью
        internal void BooksParse(string page, int leftNumberBooks)
        {
            string[] numberBooksOnPage = Regex.Matches(page, "span>(.*?) of ")[0].Groups[1].Value.Split('-');
            int numberOfElement = int.Parse(numberBooksOnPage[1]) - int.Parse(numberBooksOnPage[0]) + 1;

            if (numberOfElement < 0) { MessageBox.Show("Найденнок количество книг меньше, чем указанное.\n Выведены все найденные книги по запросу"); return; }

            Regex regexName = new Regex("<span class=\"a-size-medium a-color-base a-text-normal\">(.*?)</span>");
            Regex regexLink = new Regex("<a class=\"a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal\" href=\"(.*?)>");
            Regex regexRating = new Regex("icon-alt\">(.*?) out of 5");
            Regex regexBlock = new Regex("a-section a-spacing-none s-padding-right-small s-title-instructions-style\">(.*?)\"a-row a-size-base a-color-secondary s-align-children-center");
            Regex regexData = new Regex("a-size-base a-color-secondary a-text-normal\">(.*?)</span>");
            Regex regexAuthorWithHref = new Regex("a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style\" href=\"(.*?)\">(.*?)</a>");
            Regex regexAuthorWithoutHref = new Regex("class=\"a-size-base\">(.*?)</span>");
            Regex regexPrice = new Regex("a-price-whole\">(.*?)<span");

            MatchCollection mac = regexBlock.Matches(page);
            foreach (Match item in mac)
            {
                if (leftNumberBooks <= 0) return;
                MatchCollection matches = regexName.Matches(item.Groups[1].Value.Replace("&#x27", ""));
                var listitem = new ListViewItem(matches[0].Groups[1].Value);

                MatchCollection matchesLink = regexLink.Matches(item.Groups[1].Value);
                try
                {
                    books.Add(matches[0].Groups[1].Value, matchesLink[0].Groups[1].Value);
                }
                catch (Exception ex) { }



                MatchCollection matchesAuthor_Href = regexAuthorWithHref.Matches(item.Groups[1].Value);
                MatchCollection matchesAuthor = regexAuthorWithoutHref.Matches(item.Groups[1].Value);
                string authors = "";
                if (matchesAuthor.Count > 1)
                {
                    foreach (Match mc in matchesAuthor)
                    {
                        authors += mc.Groups[1].Value + " ";
                    }
                }

                if (matchesAuthor_Href.Count > 0)
                {
                    foreach (Match mc in matchesAuthor_Href)
                    {
                        authors += mc.Groups[2].Value + " ";
                    }
                }
                authors = authors.Replace("&#x27", "");
                authors = authors.Replace(",", "");
                authors = authors.Replace("by ", "");
                authors = authors.Replace("et al.", "");
                authors = authors.Trim(' ');
                listitem.SubItems.Add(authors);

                matches = regexData.Matches(item.Groups[1].Value);
                if (matches.Count == 0)
                {
                    listitem.SubItems.Add("None");
                }
                else listitem.SubItems.Add(matches[0].Groups[1].Value);

                matches = regexRating.Matches(item.Groups[1].Value);
                if (matches.Count == 0)
                {
                    listitem.SubItems.Add("None");
                }
                else listitem.SubItems.Add(matches[0].Groups[1].Value);

                matches = regexPrice.Matches(item.Groups[1].Value);
                if (matches.Count == 0)
                {
                    listitem.SubItems.Add("None");
                }
                else listitem.SubItems.Add(matches[0].Groups[1].Value);


                listViewBooks.Items.Add(listitem);
                leftNumberBooks--;

            }
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            BooksParse(wc.DownloadString("https://www.amazon.com/s?k=" + textBoxSearching.Text + "&i=stripbooks-intl-ship&page=" + (++numberCurrentPage).ToString()), leftNumberBooks);
        }

        //при двойном щелчке переходит по ссылке в интернет на страничку книги
        private void listViewBooks_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listViewBooks.SelectedIndices;
            try
            {
                var x = listViewBooks.Items[indexes[0]].Text;
                Process.Start(new ProcessStartInfo("https://www.amazon.com/" + $"{books[listViewBooks.Items[indexes[0]].Text]}") { UseShellExecute = true });

            }
            catch (Exception ex) { }
        }
        
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            BinaryFormatter formater = new BinaryFormatter();
            using (FileStream configs = new FileStream("configSettings.txt", FileMode.OpenOrCreate))
            {
                formater.Serialize(configs, Settings.GetCurrent());
            }

            Application.Exit();
        }

        private void listViewBooks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewBooks.ListViewItemSorter = columnSorter;
            if (e.Column == columnSorter.SortedColumn)
            {
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                columnSorter.SortedColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            listViewBooks.Sort();
        }
    }
}
