using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml;

namespace Madrasi
{
    public partial class MainPage : PhoneApplicationPage
    {

        string letterName = String.Empty;
        string wordName = String.Empty;
        string wordMeaning = String.Empty;
        string wordLanguage = String.Empty;
        string wordOrigin = String.Empty;
        int count = 0;
        string[] wordNames;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void searchTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Text = "";
        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(searchTextBox.Text.Length == 0)
            {
                searchTextBox.Text = "Tap Here to Search";
                loadingWords();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "Tap Here to Search";
            
            loadingWords();
        }

        public string getXmlData()
        {

            XmlReader reader = XmlReader.Create("Madrasi.xml");
           
            wordName = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "letter")
                    {
                        if (true == reader.MoveToFirstAttribute())
                            letterName = reader.Value;
                    }

                    else if (reader.Name == "word")
                    {
                        if (true == reader.MoveToFirstAttribute())
                        {
                            count++;
                            wordName += "/" + reader.Value;
                        }
                    }
                }

                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "dictionary")
                        break;
                }

                else if (reader.NodeType == XmlNodeType.Text)
                {
                    wordOrigin = reader.Value;
                }

            }

            reader.Close();
            return wordName;
          }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
           
            string entered = searchTextBox.Text;
            if (entered.Length == 0)
            {
                loadingWords();
            }
            else if (entered.Length == 1)
            {
                wordNames = getXmlData(entered.ToUpper(), entered.ToUpper()).Split('/');
                searchingWords(wordNames);
            }
            else
            {
                char[] enterInChar = entered.ToCharArray();
                char start = enterInChar[0];
                string word = start.ToString().ToUpper();
                for (int i = 1; i < enterInChar.Length; i++)
                    word += enterInChar[i];
                wordNames =  getXmlData(start.ToString().ToUpper(), word).Split('/');
                searchingWords(wordNames);
            }
        }

        public void loadingWords()
        {
            count = 0;
            wordNames = getXmlData().Split('/');
            

            StackPanel stackPanelSeason = new StackPanel() { HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                                                             VerticalAlignment = System.Windows.VerticalAlignment.Center
                                                           };

           Button[] button = new Button[count];

            for (int i = 0; i < count; i++)
            {
                button[i] = buttonIntialize(button[i]);
                button[i] = setButtonAttribute(button[i], i);
                button[i].Click += new RoutedEventHandler(ButPage_Click);

                stackPanelSeason.Children.Add(button[i]);
            }

            wordsScrollViewer.Content = stackPanelSeason;
           
        }

        public Button buttonIntialize(Button button)
        {
            button = new Button();
            return button;
        }

        public Button setButtonAttribute(Button button, int i)
        {
            button.Name = wordNames[i + 1];
            button.Content = wordNames[i + 1];
            button.FontSize = 40.0;
            button.Foreground = new SolidColorBrush(Colors.Red);
            button.Width = 400.0;
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            button.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            button.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            button.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            button.Margin = new Thickness(0, 5, 0, 0);
            button.BorderBrush = new SolidColorBrush(Colors.Transparent);
            button.FontFamily = new FontFamily("Gabriola.ttf#Gabriola");
            return button;
        }

        void ButPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            string butCon = button.Name.ToString().Replace(" ", "%20");

            string url = "/SearchResult.xaml?value=" + butCon;
            this.NavigationService.Navigate(new Uri(url, UriKind.Relative));

        }


        public void searchingWords(string[] worNam)
        {
           
           if (worNam[1] != "Sorry!!!! No words found")
            {
                StackPanel stackPanelSeason = new StackPanel()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    //VerticalAlignment = System.Windows.VerticalAlignment.Center
                };

                Button[] button = new Button[count];

                for (int i = 0; i < count; i++)
                {
                    button[i] = buttonIntialize(button[i]);
                    button[i] = setButtAttribute(button[i], worNam[i+1]);
                    button[i].Click += new RoutedEventHandler(ButPage_Click);

                    stackPanelSeason.Children.Add(button[i]);
                }

                wordsScrollViewer.Content = stackPanelSeason;
            }
            else
            {
                TextBlock errorTextBlock = new TextBlock() { FontSize = 40.0, Foreground = new SolidColorBrush(Colors.Red), FontFamily = new FontFamily("Gabriola.ttf#Gabriola") };
                errorTextBlock.Text = worNam[1];
                wordsScrollViewer.Content = errorTextBlock;
            }
        }


        public Button setButtAttribute(Button button,string worNam)
        {
            button.Name = worNam;
            button.Content = worNam;
            button.FontSize = 40.0;
            button.Foreground = new SolidColorBrush(Colors.Red);
            button.Width = 400.0;
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            button.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            button.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            button.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            button.Margin = new Thickness(0, 5, 0, 0);
            button.BorderBrush = new SolidColorBrush(Colors.Transparent);
            button.FontFamily = new FontFamily("Gabriola.ttf#Gabriola");

            return button;
        }

        public string getXmlData(string letterId, string word)
        {

            XmlReader reader = XmlReader.Create("Madrasi.xml");
            bool flag = false;
            string returnString = "";
            count = 0;
            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element)
                {

                    if (reader.Name == "letter")
                    {
                        if (true == reader.MoveToFirstAttribute())
                            letterName = reader.Value;
                    }

                    else if (reader.Name == "word" && letterName == letterId)
                    {
                        if (true == reader.MoveToFirstAttribute())
                            wordName = reader.Value;

                        if (String.Compare(word, 0, wordName, 0, word.Length) == 0)
                        {
                            if (true == reader.MoveToNextAttribute())
                                wordMeaning = reader.Value;

                            if (true == reader.MoveToNextAttribute())
                                wordLanguage = reader.Value;
                            returnString += "/" + wordName;
                            count++;
                            flag = true;
                        }
                        else
                        {

                            wordName = "";
                            wordMeaning = "";
                            wordLanguage = "";
                            wordOrigin = "";
                        }
                    }

                }

                else if (reader.NodeType == XmlNodeType.EndElement && letterName == letterId && wordName == word)
                {

                    if (reader.Name == "word")
                        break;

                }

                else if (reader.NodeType == XmlNodeType.Text && letterName == letterId && wordName == word)
                {
                    wordOrigin = reader.Value;
                }

            }

            if (!flag)
               returnString = "/Sorry!!!! No words found";

            reader.Close();
            return returnString;
            // + "\n\nWord Meaning : " + wordMeaning + "\n\n Word Origin Language : " + wordLanguage + "\n\nWord Origin : " + wordOrigin;
        }

       private void refresh_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "Tap Here to Search";

            loadingWords();
        }

       private void aboutMadrasi_Click(object sender, RoutedEventArgs e)
       {
           this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
       }


    }
}