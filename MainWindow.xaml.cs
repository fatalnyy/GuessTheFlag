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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Question[] questions;
        private int currentQuestionNumber;
        private string _path;
        private string _flagFolderPath;
        private int _score = 40;
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string SetPath { get => _path; set => _path = value; }
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public string FlagFolderPath { get => _flagFolderPath; set => _flagFolderPath = value; }

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        public MainWindow()
        {
            InitializeComponent();
           
        }

        #region Gameplay methods
        ///<Summary>
        /// Gets the answer
        ///</Summary>
        
        //Metoda wczytująca pierwsze 8 linijek pliku .txt z czego pierwsze 4 są wyświetlane w oknie MainWindow
        public void StartGame()
        {

           
            questions = new Question[25];
            try
            {
                int lineNumber = 1;
                string line;
                int questionNumber = 0;
                using (StreamReader file = new StreamReader(SetPath, Encoding.GetEncoding("iso-8859-2")))
                {
                    questions[questionNumber] = new Question();
                    while ((line = file.ReadLine()) != null)
                    {
                        switch (lineNumber)
                        {
                            case 1:
                                questions[questionNumber].AnswerA = line;
                                break;
                            case 2:
                                questions[questionNumber].AnswerB = line;
                                break;
                            case 3:
                                questions[questionNumber].AnswerC = line;
                                break;
                            case 4:
                                questions[questionNumber].AnswerD = line;
                                break;
                            case 5:
                                questions[questionNumber].GoodAnswer = line;
                                break;
                            case 6:
                                questions[questionNumber].CapitalCity = line;
                                break;
                            case 7:
                                questions[questionNumber].Tip = line;
                                break;

                            case 8:
                                questions[questionNumber].ImagePath = line;
                                break;
                        }
                        if (lineNumber == 8)
                        {
                            GetImage(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\resources"));
                            lineNumber = 0;
                            questionNumber++;
                            if (questionNumber > 24)
                                break;
                            questions[questionNumber] = new Question();

                        }
                        lineNumber++;
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Nie można odnaleźć pliku");


            }          
            currentQuestionNumber = 0;
            SetNextQuestion();
        }    
        // Metoda ustawiająca na przyciskach A, B, C, D pierwsze 4 linijki z pliku .txt zależne od danego numeru pytania
        private void SetNextQuestion()
        {
            A.Content = questions[currentQuestionNumber].AnswerA;
            B.Content = questions[currentQuestionNumber].AnswerB;
            C.Content = questions[currentQuestionNumber].AnswerC;
            D.Content = questions[currentQuestionNumber].AnswerD;
        }
        //Metoda sprawdzająca poprawność odpowiedzi i dodająca lub odejmująca punkty
        private void ValidateAnswer(string answer)
        {
            ExtraPointsDialog extraPoints = new ExtraPointsDialog(questions[currentQuestionNumber].GoodAnswer);

            if (answer[0] == questions[currentQuestionNumber].GoodAnswer[0])
            {
                _score += 20;
                Score.Content = _score.ToString();
                MessageBox.Show("Gratulacje! Poprawna odpowiedź, otrzymujesz 20 punktów.");
                extraPoints.ShowDialog();
                if (extraPoints.CorrectCapitalCity.ToLower() == questions[currentQuestionNumber].CapitalCity.ToLower())
                {
                    _score += 5;
                    Score.Content = _score.ToString();
                    MessageBox.Show("Gratulacje! Otrzymujesz dodatkowe 5 punktów!");
                }
                else
                {
                    MessageBox.Show("Niestety nie udało się zdobyć dodatkowych punktów. Prawidłowa odpowiedź to: " + questions[currentQuestionNumber].CapitalCity);
                }            
            }
            else
            {
                MessageBox.Show("Niestety! Zła odpowiedź, otrzymujesz ujemne 20 punktów.");
                MessageBox.Show("Poprawna odpowiedź to: " + questions[currentQuestionNumber].GoodAnswer);
                _score -= 20;
                Score.Content = _score.ToString();
            }
        }
        //Metoda sprawdzająca czy gra się nie skończyła
        private void ProceedQuestion()
        {
            currentQuestionNumber++;
            if (currentQuestionNumber <= 24)
            {
                panelImages.Children.Clear();
                faceImages.Children.Clear();
                SetNextQuestion();
            }
            else
            {
                MessageBox.Show("Koniec Gry. Twój wynik to: " + _score);
                Close();
                Application.Current.Shutdown();
            }
            if (_score < 0)
            {
                MessageBox.Show("Koniec Gry. Twój wynik jest mniejszy niż 0");
                Close();
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region Click and hover events
        //Metoda kliknięcia na przycisk odpowiedzi A, B, C lub D
        private void Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ValidateAnswer(button.Content.ToString());
            ProceedQuestion();
            GetImage(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\resources"));
            GetFaceImage(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\faces"));
        }
        //Metoda zdarzenia pojawienia się wskazówki
        private void ShowTipEvent(object sender, ToolTipEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = questions[currentQuestionNumber].Tip;
            _score -= 3;
            Score.Content = _score.ToString();
        }
        //Metoda opisująca zachowanie przycisku "Pół na pół"
        private void FiftyFifty_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if ((questions[currentQuestionNumber].AnswerA[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                    (questions[currentQuestionNumber].AnswerB[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    A.Content = "";
                    B.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
                else if ((questions[currentQuestionNumber].AnswerA[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                         (questions[currentQuestionNumber].AnswerC[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    A.Content = "";
                    C.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
                else if ((questions[currentQuestionNumber].AnswerA[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                         (questions[currentQuestionNumber].AnswerD[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    A.Content = "";
                    D.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
                else if ((questions[currentQuestionNumber].AnswerB[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                         (questions[currentQuestionNumber].AnswerC[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    B.Content = "";
                    C.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
                else if ((questions[currentQuestionNumber].AnswerB[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                         (questions[currentQuestionNumber].AnswerD[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    B.Content = "";
                    D.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
                else if ((questions[currentQuestionNumber].AnswerC[0] != questions[currentQuestionNumber].GoodAnswer[0]) &&
                         (questions[currentQuestionNumber].AnswerD[0] != questions[currentQuestionNumber].GoodAnswer[0]))
                {
                    C.Content = "";
                    D.Content = "";
                    _score -= 10;
                    Score.Content = _score.ToString();
                    FiftyFifty.IsEnabled = false;
                    break;
                }
            }
            FiftyFifty.IsEnabled = true;
        }
        //Metoda opisująca kliknięcie "New Game"
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            SettingsDialog dialog2 = new SettingsDialog();
            dialog2.ShowDialog();
            Close();
        }
        //Metoda opisująca kliknięcie "Quit Game"
        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();

        }
        #endregion

        #region Images
        //Metody pobierające ścieżkę folderu z zasobami i dodające obrazki
        private void GetImage(String folderPath)
        {
            try
            {
                if (folderPath == "") return;

                DirectoryInfo folder = new DirectoryInfo(folderPath);
                if (folder.Exists)
                {
                    foreach (FileInfo fileInfo in folder.GetFiles())
                    {
                        if (".png| .jpg| .jpeg".Contains(fileInfo.Extension.ToLower()))
                        {
                            if (fileInfo.Name == questions[currentQuestionNumber].ImagePath)
                            {
                                AddImage(fileInfo.FullName);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void GetFaceImage(String folderPath)
        {
            try
            {
                if (folderPath == "") return;

                DirectoryInfo folder = new DirectoryInfo(folderPath);
                if (folder.Exists)
                {
                    foreach (FileInfo fileInfo in folder.GetFiles())
                    {
                        if (".png| .jpg| .jpeg".Contains(fileInfo.Extension.ToLower()))
                        {
                            if ((fileInfo.Name == "sad.png") && _score < 90)
                            {
                                AddFaceImage(fileInfo.FullName);
                                Score.Foreground = Brushes.Red;
                            }
                            else if ((fileInfo.Name == "happy.png") && (_score >= 90 && _score < 210))
                            {
                                AddFaceImage(fileInfo.FullName);
                                Score.Foreground = Brushes.Yellow;
                            }
                            else if ((fileInfo.Name == "excited.png") && (_score >= 210 && _score < 700))
                            {
                                AddFaceImage(fileInfo.FullName);
                                Score.Foreground = Brushes.SpringGreen;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void AddImage(String pathImage)
        {

            Image newImage = new Image();

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(pathImage, UriKind.Absolute);
            src.EndInit();
            newImage.Source = src;

            panelImages.Children.Add(newImage);
        }
        private void AddFaceImage(String pathImage)
        {

            Image faceImage = new Image();

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(pathImage, UriKind.Absolute);
            src.EndInit();
            faceImage.Source = src;

            faceImages.Children.Add(faceImage);
        }
        #endregion
    }
}
