using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuessTheFlag
{
    public partial class ExtraPointsDialog : Window
    {
        private string _correctCapitalCity;
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string CorrectCapitalCity
        {
            get
            {
                return _correctCapitalCity;
            }
            set
            {
                _correctCapitalCity = value;
            }
        }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public ExtraPointsDialog(string correctCapitalCity)
        {
            InitializeComponent();

            _correctCapitalCity = correctCapitalCity;
        }
        private void ExtraPointsConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            CorrectCapitalCity = textbox1.Text;
            Visibility = Visibility.Hidden;
        }
    }
}