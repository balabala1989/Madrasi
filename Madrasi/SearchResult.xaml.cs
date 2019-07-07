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
    public partial class SearchResult : PhoneApplicationPage
    {
        string wordObtained = "";
        string letterName = String.Empty;
        string wordName = String.Empty;
        string wordMeaning = String.Empty;
        string wordLanguage = String.Empty;
        string wordOrigin = String.Empty;

        public SearchResult()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            loadingData();
           
        }

        public void loadingData()
        {
            wordObtained = this.NavigationContext.QueryString["value"];
            char[] enterInChar = wordObtained.ToCharArray();
            char start = enterInChar[0];
            string word = start.ToString().ToUpper();
            for (int i = 1; i < enterInChar.Length; i++)
                word += enterInChar[i];
            getXmlData(start.ToString().ToUpper(), word);

            StackPanel resultStackPanel = new StackPanel()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };

            TextBlock wordNameTextBlock = new TextBlock() { HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                                                            Margin = new Thickness(0,20,0,0),
                                                            VerticalAlignment = System.Windows.VerticalAlignment.Top,
                                                            Width = 458.0,
                                                            Foreground = new SolidColorBrush(Colors.Red),
                                                            FontFamily = new FontFamily("MTCORSVA.TTF#Monotype Corsiva"),
                                                            FontSize = 50.0,
                                                            TextWrapping = TextWrapping.Wrap
            };

            TextBlock wordMeaningTextBlock = new TextBlock()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(0, 20, 0, 0),
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Width = 458.0,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Gabriola.ttf#Gabriola"),
                FontSize = 35.0,
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock wordOriginLangTextBlock = new TextBlock()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(0, 20, 0, 0),
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Width = 458.0,
                Foreground = new SolidColorBrush(Colors.Brown),
                FontFamily = new FontFamily("Gabriola.ttf#Gabriola"),
                FontSize = 35.0,
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock wordOriginTextBlock = new TextBlock()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Margin = new Thickness(0, 20, 0, 0),
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Width = 458.0,
                Foreground = new SolidColorBrush(Colors.Blue),
                FontFamily = new FontFamily("Gabriola.ttf#Gabriola"),
                FontSize = 35.0,
                TextWrapping = TextWrapping.Wrap
            };


            wordNameTextBlock.Text = wordName;
            wordMeaningTextBlock.Text = "Meaning:-\n    " + wordMeaning;
            wordOriginLangTextBlock.Text = "Originated Language:-\n   " + wordLanguage;
            wordOriginTextBlock.Text = "Origin:-\n    " + wordOrigin;

            resultStackPanel.Children.Add(wordNameTextBlock);
            resultStackPanel.Children.Add(wordMeaningTextBlock);
            resultStackPanel.Children.Add(wordOriginLangTextBlock);
            resultStackPanel.Children.Add(wordOriginTextBlock);
            resultScrollViewer.Content = resultStackPanel;
        }

        public void getXmlData(string letterId, string word)
        {

            XmlReader reader = XmlReader.Create("Madrasi.xml");
            

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

           
            reader.Close();
            // + "\n\nWord Meaning : " + wordMeaning + "\n\n Word Origin Language : " + wordLanguage + "\n\nWord Origin : " + wordOrigin;
        }

    }
}