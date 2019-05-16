using System;
using System.Collections.Generic;
using System.Windows;
using AppLibrary;

namespace Project_Duplikat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DBManager.ClearDB();
        }

        private void LoadPdf1_Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Utilities.GetPathFromDialog();
            TextOperations.PdfToString(name);
        }

        private void LoadPdf2_Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Utilities.GetPathFromDialog();
            TextOperations.PdfToString(name);
        }

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            DBManager.GetDuplicates();
        }
    }
}
