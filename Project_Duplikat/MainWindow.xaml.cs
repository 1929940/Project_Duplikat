using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //TextOperations.GenerateReport();
            DBManager.GetDuplicates();
        }
    }
}
