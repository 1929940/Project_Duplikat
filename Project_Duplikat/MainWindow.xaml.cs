using System;
using System.Collections.Generic;
using System.Windows;
using AppLibrary;
using System.ComponentModel;

namespace Project_Duplikat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string name;
        bool btnOneWasClicked = false;
        bool btnTwoWasClicked = false;

        public MainWindow()
        {
            InitializeComponent();
            DBManager.ClearDB();
        }

        private void LoadPdf1_Button_Click(object sender, RoutedEventArgs e)
        {
            name = Utilities.GetPathFromDialog();

            if (name == null) return;

            MessageBox.Show("Ładuje wskazany plik pdf.\nTo może chwile potrwać... \nKliknij OK i poczekaj na kolejny komunikat");

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork;
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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            TextOperations.UploadPDF(name);
        }

        private void LoadPdf2_Button_Click(object sender, RoutedEventArgs e)
        {
            name = Utilities.GetPathFromDialog();

            if (name == null) return;

            MessageBox.Show("Ładuje wskazany plik pdf.\nTo może chwile potrwać... \nKliknij OK i poczekaj na kolejny komunikat");

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();

            LoadPdf1_Button.IsEnabled = false;
            LoadPdf2_Button.IsEnabled = false;

            btnTwoWasClicked = true;
        }


        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            DBManager.GetDuplicates();
            this.Close();
        }
    }
}
