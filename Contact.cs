using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int age;

        public Contact (string fileLine)
        {
            parseLine(fileLine);
        }
        private void parseLine (string unparsedLine)
        {
            string Line = unparsedLine;
            firstName = Line.Substring(0, Line.IndexOf(","));
            Line = Line.Substring(Line.IndexOf(",") + 1, Line.Length - (Line.IndexOf(",") + 1));
            lastName = Line.Substring(0, Line.IndexOf(","));
            Line = Line.Substring(Line.IndexOf(",") + 1, Line.Length - (Line.IndexOf(",") + 1));
            int.TryParse(Line.Substring(0, Line.IndexOf(",")), out int yB);
            yearBorn = yB;
            Line = Line.Substring(Line.IndexOf(",") + 1, Line.Length - (Line.IndexOf(",") + 1));
            int.TryParse(Line.Substring(0, Line.IndexOf(",")), out int mB);
            monthBorn = mB;
            Line = Line.Substring(Line.IndexOf(",") + 1, Line.Length - (Line.IndexOf(",") + 1));
            int.TryParse(Line.Substring(0, Line.IndexOf(",")), out int dB);
            dayBorn = dB;
            Line = Line.Substring(Line.IndexOf(",") + 1, Line.Length - (Line.IndexOf(",") + 1));
            email = Line.Substring(0, Line.IndexOf(","));
        }
        private void SetAge()
        {
            DateTime birthDay = new DateTime(yearBorn, monthBorn, dayBorn);
            DateTime dateToday = DateTime.Today;
            TimeSpan TSage = dateToday - birthDay;
            age = (TSage.Days) / 1461 / 4;
        }
    }
}
