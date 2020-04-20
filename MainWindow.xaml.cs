using System;
using System.Collections.Generic;
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
using System.IO;

namespace _205704Unit2Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadFromFile("contact.txt");
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            bool? dialogResult = ofd.ShowDialog();
            if ((bool)ofd.ShowDialog())
            {
                ReadFromFile(ofd.FileName);
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ReadFromFile(String fileURI)
        {
            try
            {
                StreamReader sr = new StreamReader(fileURI);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Contact temp = new Contact(line);
                    contacts.Add(temp);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Reading file at URI: " + fileURI);
                throw;
            }
        }
    }
}
