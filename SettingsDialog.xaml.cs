using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace GuessTheFlag
{

    public partial class SettingsDialog : Window
    {
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void EasyRadioBtn_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void MediumRadioBtn_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void HardRadioBtn_Checked(object sender, RoutedEventArgs e)
        {

        }
        //Metoda sprawdzająca zaznaczenia w RadioButton'ach
        private void RadioBtnChecking()
        {
            MainWindow mainWindow2 = new MainWindow();
            StartDialog s1 = new StartDialog();

            if (EasyRadioBtn.IsChecked == true)
            {
                mainWindow2.SetPath = Path.Combine(Environment.CurrentDirectory, @"..\..\Questions_E.txt");
                mainWindow2.Show();
                Close();
                mainWindow2.StartGame();

            }
            else if (MediumRadioBtn.IsChecked == true)
            {
                mainWindow2.SetPath = Path.Combine(Environment.CurrentDirectory, @"..\..\Questions_M.txt");
                mainWindow2.Show();
                Close();
                mainWindow2.StartGame();
            }
            else if (HardRadioBtn.IsChecked == true)
            {
                mainWindow2.SetPath = Path.Combine(Environment.CurrentDirectory, @"..\..\Questions_H.txt");
                mainWindow2.Show();
                Close();
                mainWindow2.StartGame();
            }
            else
            {
                MessageBox.Show("Poziom trudności musi być zaznaczony!");
                Close();
                s1.ShowDialog();

            }

        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            RadioBtnChecking();
            Close();
        }


    }
}