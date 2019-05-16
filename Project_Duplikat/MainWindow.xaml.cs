
using System.Windows;
using AppLibrary;
using System.ComponentModel;

namespace Project_Duplikat
{
    public partial class MainWindow : Window
    {
        string name;
        bool btnOneWasClicked = false;
        bool btnTwoWasClicked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadPdf1_Button_Click(object sender, RoutedEventArgs e)
        {
            name = Utilities.GetPathFromDialog();

            if (name == null) return;

            MessageBox.Show("Ładuje wskazany plik pdf.\nTo może chwile potrwać... \nKliknij OK i poczekaj na kolejny komunikat");

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork1;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

            LoadPdf1_Button.IsEnabled = false;
            LoadPdf2_Button.IsEnabled = false;

            btnOneWasClicked = true;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Udało sie poprawnie załadowac plik");

            if (btnOneWasClicked && btnTwoWasClicked)
            {
                Generate_Button.IsEnabled = true;
            }
            else if (btnOneWasClicked)
            {
                LoadPdf2_Button.IsEnabled = true;
            }
            else if (btnTwoWasClicked)
            {
                LoadPdf1_Button.IsEnabled = true;
            }
        }

        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            Library.UploadPDF(name, Library.list1);
        }
        private void Worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            Library.UploadPDF(name, Library.list2);
        }

        private void LoadPdf2_Button_Click(object sender, RoutedEventArgs e)
        {
            name = Utilities.GetPathFromDialog();

            if (name == null) return;

            MessageBox.Show("Ładuje wskazany plik pdf.\nTo może chwile potrwać... \nKliknij OK i poczekaj na kolejny komunikat");

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork2;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

            LoadPdf1_Button.IsEnabled = false;
            LoadPdf2_Button.IsEnabled = false;

            btnTwoWasClicked = true;
        }


        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            Library.PrintResults(Library.GenerateResult());
            this.Close();
        }
    }
}
