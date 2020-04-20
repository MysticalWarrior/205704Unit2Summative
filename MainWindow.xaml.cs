using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.Win32;

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
            ReadFromFile("contacts.txt");

            dgEditor.ItemsSource = contacts;
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            bool? dialogResult = ofd.ShowDialog();
            if ((bool)dialogResult)
            {
                ReadFromFile(ofd.FileName);
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            bool? fileSelected = sfd.ShowDialog();
            if ((bool)fileSelected)
            {
                SavetoFile(sfd.FileName);
            }
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
                sr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Reading file at URI: " + fileURI);
                throw;
            }
            dgEditor.ItemsSource = null;
            dgEditor.ItemsSource = contacts;
        }//end read from file method

        public void SavetoFile(string fileName)
        {
            string save = "";
            foreach(Contact c in contacts)
            {
                save += c.ToString();
            }

            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                sw.Write(save);
                sw.Flush();
            }
            catch (Exception)
            {
                MessageBox.Show("An error was encountered saving to the selected file");
                throw;
            }
            //and close the writer, but do it on the same level it was opened in case of errors.
            sw.Close();
        }//end SaveToFile method.

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //prompts the user to cancel
            if (MessageBoxResult.Yes == MessageBox.Show("The program is closing and will automatically save the current contacts, would you like to discard changes since the last save?", "Program Closing", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                MessageBox.Show("Autosave cancelled.");
            }
            else
            {
                SavetoFile("contacts.txt");
            }
        }

        private void menuShowAge_Click(object sender, RoutedEventArgs e)
        {
           /* if (dgEditor.SelectedItem.GetType() == Contact)
            {
                MessageBox.Show(dgEditor.SelectedItem.Age.ToString);
            }
            MessageBox.Show(dgEditor.SelectedItem.ToString());*/
        }
    }
}