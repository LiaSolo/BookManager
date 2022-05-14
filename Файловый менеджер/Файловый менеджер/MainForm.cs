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
using System.Net.Http;
using System.Threading.Tasks;

namespace Файловый_менеджер
{ 
    public partial class MainForm : Form
    {
        Dictionary<string, string> books;
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string adress = "https://www.amazon.com/s?k=" + textBoxSearching.Text
                + "&i=stripbooks-intl-ship&ref=nb_sb_noss";

            //"https://www.amazon.com/s?k=вино из одуванчиков&i=stripbooks-intl-ship&ref=nb_sb_noss";

            int countBooks;

            try
            {
                countBooks = int.Parse(textBoxBooksOnPage.Text);
            }
            catch (Exception ex)
            {
                countBooks = 10;
            }
           
            listViewBooks.Items.Clear();
            books = new Dictionary<string, string>();         
 
            BooksParse(adress, countBooks);
        }

        //<div class="a-section a-spacing-small a-spacing-top-small">
        //        <span>5 results for</span><span> </span><span class="a-color-state a-text-bold">"вино из одуванчиков"</span>

        internal void BooksParse(string adress, int lastBookNumber)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            webClient.Encoding = Encoding.UTF8;
            string page = webClient.DownloadString(adress);

            string[] numberBooksOnPage; // = new string[2];
            int numberOfElements;

            try
            {
                numberBooksOnPage = Regex.Matches(page, "<span>(.*?) of ")[0].Groups[1].Value.Split('-');
            }
            catch(Exception)
            {
                try
                {
                    numberBooksOnPage = new string[2];
                    numberBooksOnPage[0] = "1";
                    numberBooksOnPage[1] = Regex.Matches(page, "<span>(.*?) results for")[0].Groups[1].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Я испахабил код страницы или ничего не нашёл");
                    return;
                }    
            }

            numberOfElements = int.Parse(numberBooksOnPage[1])
                - int.Parse(numberBooksOnPage[0]) + 1;
            if (numberOfElements < lastBookNumber && numberOfElements < 16)
            {
                MessageBox.Show("Выведены все найденные книги:" 
                    + numberOfElements.ToString());
                lastBookNumber = numberOfElements;
            }

            Regex regexName = new Regex("<span class=\"a-size-medium a-color-base a-text-normal\">(.*?)</span>");
            Regex regexLink = new Regex("<a class=\"a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal\" href=\"(.*?)>");
            Regex regexRating = new Regex("icon-alt\">(.*?) out of 5");
            Regex regexHeadBlock = new Regex("a-section a-spacing-none s-padding-right-small s-title-instructions-style\">(.*?)\"a-row a-size-base a-color-secondary s-align-children-center");
            Regex regexData = new Regex("a-size-base a-color-secondary a-text-normal\">(.*?)</span>");

            //есть авторы с ссылками на их страницы, а есть без
            Regex regexAuthorWithHref = new Regex("a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style\" href=\"(.*?)\">(.*?)</a>");
            Regex regexAuthorWithoutHref = new Regex("class=\"a-size-base\">(.*?)</span>");
            Regex regexPrice = new Regex("a-offscreen\">(.*?)</span>");

            //в заглавном блоке находятся название книги, автор, дата и рейтинг
            MatchCollection headBlockMatchCollection = regexHeadBlock.Matches(page);
            foreach (Match item in headBlockMatchCollection)
            {
                MatchCollection matches = regexName.Matches(item.Groups[1].Value.Replace("&#x27", ""));
                ListViewItem listitem = new ListViewItem(matches[0].Groups[1].Value);

                //ссылка на книгу
                MatchCollection matchesLink = regexLink.Matches(item.Groups[1].Value);
                try
                {
                    books.Add(matches[0].Groups[1].Value, matchesLink[0].Groups[1].Value);
                }
                catch (Exception ex) { }

                MatchCollection matchesAuthorHref = regexAuthorWithHref.Matches(item.Groups[1].Value); 
                MatchCollection matchesAuthorNoHref = regexAuthorWithoutHref.Matches(item.Groups[1].Value); 
                string authors = "";

                //если у книги несколько авторов
                if (matchesAuthorNoHref.Count > 1)
                {
                    foreach (Match mc in matchesAuthorNoHref)
                    {
                        authors += mc.Groups[1].Value + " ";
                    }
                }

                if (matchesAuthorHref.Count > 0)
                {
                    foreach (Match mc in matchesAuthorHref)
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

                matches = regexRating.Matches(item.Groups[1].Value);
                if (matches.Count == 0) listitem.SubItems.Add("None");
                else listitem.SubItems.Add(matches[0].Groups[1].Value);

                matches = regexData.Matches(item.Groups[1].Value);
                if (matches.Count == 0) listitem.SubItems.Add("None");
                else listitem.SubItems.Add(matches[0].Groups[1].Value);

                matches = regexPrice.Matches(item.Groups[1].Value);
                if (matches.Count == 0) listitem.SubItems.Add("None");
                else listitem.SubItems.Add(matches[0].Groups[1].Value);

                listViewBooks.Items.Add(listitem);
                lastBookNumber--;
                if (lastBookNumber == 0) return;
            }

            //переход на следующую страницу при необходимости
            string nextAdress = "https://www.amazon.com/s?k=" + textBoxSearching.Text + "&i=stripbooks-intl-ship&page=" + (++numberCurrentPage).ToString();
            BooksParse(nextAdress, lastBookNumber);
        }

        //при двойном щелчке переходит по ссылке в интернет на страничку книги
        private void listViewBooks_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listViewBooks.SelectedIndices;
            try
            {
                var x = listViewBooks.Items[indexes[0]].Text;
                Process.Start(new ProcessStartInfo("https://www.amazon.com/" + 
                    $"{books[listViewBooks.Items[indexes[0]].Text]}"));
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Что-то пошло не так :(");
            }
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

        //сортировка при нажатии на колонку
        private void listViewBooks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewBooks.ListViewItemSorter = columnSorter;
            if (e.Column == columnSorter.SortedColumn)
            {
                if (columnSorter.Order == SortOrder.Ascending) //если уже по возрастанию
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else //если уже по убыванию или никак
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
