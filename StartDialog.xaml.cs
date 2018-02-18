using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GuessTheFlag
{
    public partial class StartDialog : Window
    {
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public StartDialog()
        {
            InitializeComponent();
        }
        
        private void StartBT_Click(object sender, RoutedEventArgs e)
        {
            SettingsDialog dialog = new SettingsDialog();
            Close();
            dialog.ShowDialog();
        }
        private void QuitBT_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }   
    }
}