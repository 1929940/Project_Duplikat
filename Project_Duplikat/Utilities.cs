using Microsoft.Win32;

namespace Project_Duplikat
{
    class Utilities
    {
        public static string GetPathFromDialog()
        {
            var dialog = new OpenFileDialog() { Filter = "PDF |*.pdf" };
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }
    }
}
