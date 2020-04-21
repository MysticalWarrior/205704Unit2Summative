/*
 * Aidan McClung
 * April 20,2020
 * A program to edit and save contact info from text files.
 */
using System;
using System.Windows;
using System.Diagnostics;//enables the MailTo Process.

namespace _205704Unit2Summative
{
    class Contact
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int yearBorn { get; set; }
        public int monthBorn { get; set; }
        public int dayBorn { get; set; }
        public string email { get; set; }

        /// <summary>
        /// Creates a new contact from a string representation
        /// </summary>
        /// <param name="fileLine">
        /// The string representation of the contact
        /// </param>
        public Contact(string fileLine)
        {
            parseLine(fileLine);
        }

        /// <summary>
        /// Creates a new contact with only the birthday set.
        /// (Used as the base template for a new contact.)
        /// </summary>
        /// <param name="birthday">
        /// The dateTime value to be set as the birthday.
        /// </param>
        public Contact(DateTime birthday)
        {
            yearBorn = birthday.Year;
            monthBorn = birthday.Month;
            dayBorn = birthday.Day;
        }

        /// <summary>
        /// Splits a string representation of a contact and sets the values.
        /// </summary>
        /// <param name="unparsedLine"></param>
        private void parseLine(string unparsedLine)
        {
            string[] data = unparsedLine.Split(',');
            firstName = data[0];
            lastName = data[1];
            int.TryParse(data[2], out int yB); yearBorn = yB;
            int.TryParse(data[3], out int mB); monthBorn = mB;
            int.TryParse(data[4], out int dB); dayBorn = dB;
            email = data[5];
        }

        /// <summary>
        /// A method which returns the current contact's age, and also prompts the user if their birthday is today.
        /// </summary>
        public void GetAge(DateTime dateToday)
        {
            //calculates their age from the date today
            int age = dateToday.Year - yearBorn;

            //Subtracts a year if they haven't yet had their birthday this year
            if (monthBorn >= dateToday.Month && dayBorn > dateToday.Day) { age--;}

            //notifies the user if it's their birthday and prompts them to send a message.
            else if (monthBorn == dateToday.Month && dayBorn == dateToday.Day && MessageBoxResult.Yes == MessageBox.Show("It's " + firstName + "'s Birthday Today! They Turned " + age.ToString() + "! \r Would you like to send them a birthday message?", firstName + "'s birthday", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
            {
                //if user said yes, runs the method and doesnt display the age (because an oversaturation of message boxes is the worst.
                SendBirthdayMessage();
                return;
            }
            
            //Displays a MessageBox with the age
            MessageBox.Show(age.ToString(), firstName + lastName + "'s age:");
        }

        /// <summary>
        /// Converts the Contact to a string representation.
        /// </summary>
        /// <returns> A comma seperated string representing the class.</returns>
        public override string ToString()
        {
            string line = firstName + "," + lastName + "," +
                yearBorn.ToString() + "," + monthBorn.ToString() + "," +
                dayBorn.ToString() + "," + email + '\r';
            return line;
        }

        /// <summary>
        /// Uses the default mail client to open a default birthday message template.
        /// </summary>
        private void SendBirthdayMessage()
        {
            Process.Start("mailto:" + email + "?subject=Happy%20Birthday!&body=Hope%20you%20have%20a%20good%20day!");
        }
    }
}