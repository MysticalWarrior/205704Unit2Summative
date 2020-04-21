/*
 * Aidan McClung
 * April 20,2020
 * A program to edit and save contact info from text files.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.Win32;//provides file dialog windows.
using System.Diagnostics;//provides https usage (&Mailto:)
using System.Linq;//ofType<Type> for saving

namespace _205704Unit2Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        DateTime dateToday;
        public MainWindow()
        {
            InitializeComponent();

            dateToday = DateTime.Today;

            //Reads the program's default saved file and binds it to the editor.
            contacts = new List<Contact>();
            ReadFromFile("contacts.txt");
            dgEditor.ItemsSource = contacts;
        }

        /// <summary>
        /// Reads the data from a .txt or .csv file (or a similar type) and imports any saved contacts.
        /// </summary>
        /// <param name="fileName">
        /// The name and path (if not relative) of the file to read from.</param>
        public void ReadFromFile(String fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
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
                MessageBox.Show("Error Reading file: " + fileName);
                throw;
            }
            dgEditor.ItemsSource = null;
            dgEditor.ItemsSource = contacts;
        }//end read from file method

        /// <summary>
        /// //attempts to write the string representations of the contacts list to the passed file.
        /// </summary>
        /// <param name="fileName">
        /// The name of the file to save to.
        /// </param>
        public void SavetoFile(string fileName)
        {
            //saves the change if the user saves while in editing mode.
            if (!dgEditor.CommitEdit()) { MessageBox.Show("Please commit edits before saving"); }

            //creates a string and adds the contacts to it.
            string save = "";
            foreach (Contact c in dgEditor.Items.OfType<Contact>())
            {
                save += c.ToString();
            }

            //and saves them to the file.
            StreamWriter sw = new StreamWriter(fileName);
            try
            {
                sw.Write(save);
                //makes sure all data is written.
                sw.Flush();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error was encountered saving to the selected file /r/r Specific Error:" + e.ToString());
                throw;
            }
            //and close the writer, but do it on the same level it was opened in case of errors.
            sw.Close();
        }

        /// <summary>
        /// Handles confirmation of window closing and change saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {//return;//uncomment to disable messageboxes and autosaving during troubleshooting
            //prompts the user to cancel autosave or closing
            MessageBoxResult prompt = MessageBox.Show("The program is closing and will automatically save the current contacts, would you like to *discard* changes since the last save?", "Program Closing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (prompt == MessageBoxResult.Yes)
            {
                MessageBox.Show("Changes discarded"); return;
            }
            //Note: Cancelling closing the program was canceled. (change to yesNoCancel to enable)
            else if (prompt == MessageBoxResult.Cancel)
            {
                //cancels the closing of the program
                e.Cancel = true;
            }
            //To use AutoSave during debugging operations, set this to the project folder path.
            SavetoFile("contacts.txt");
        }

        /// <summary>
        /// Uses a FileDialog to import contacts from a saved file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            bool? dialogResult = ofd.ShowDialog();
            if ((bool)dialogResult)
            {
                ReadFromFile(ofd.FileName);
            }
        }

        /// <summary>
        /// Opens a FileDialog and saves to a selected file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            bool? fileSelected = sfd.ShowDialog();
            if ((bool)fileSelected)
            {
                SavetoFile(sfd.FileName);
            }
        }

        /// <summary>
        /// Runs the Contact.GetAge method, provided a contact has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowAge_Click(object sender, RoutedEventArgs e)
        {
            //In order to run the method we have to make sure that the selected item is a contact, otherwise there's errors.
            Contact temp = (Contact)dgEditor.SelectedItem;
            if (temp != null) { temp.GetAge(dateToday); }
            //(date today is a global variable because it is used by other methods.)
        }

        /// <summary>
        /// Adds a new contact with today's date as their birthday.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAdd_Click(object sender, RoutedEventArgs e)
        {
            contacts.Add(new Contact(dateToday));
            dgEditor.ItemsSource = null;
            dgEditor.ItemsSource = contacts;
        }

        /// <summary>
        /// Redirects user to the program info file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            //the program info is currently in the github ReadMe.
            Process.Start("https://raw.githubusercontent.com/MysticalWarrior/205704Unit2Summative/master/README.md");
        }
    }
}