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

namespace Madrasi
{
    public partial class About : PhoneApplicationPage
    {
        public string message = "\"Madarasi\" is a collection of colloquioal tamil slang words used in Chennai (Madras) along with their meanings and etymology.\n\n If you want to know information about a particular word,\n 1. Type the word in the search bar. As you type the word list will dynamically change in the space below the search bar.\n2. Just tap the word in the displayed list.\n3. Meaning and etymology of the tapped word will be displayed.\n4. Press \"Back\" key on the phone to search for another word.\n\nThe words are populated in the list alphabetically below the search bar. If you are not aware of the spelling of the word you can directly scroll the list and tap on the word when you find it.\n\n";
    
        public About()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = message;
        }


    }
}