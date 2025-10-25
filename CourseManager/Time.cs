using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;



namespace CourseManager {
	public class Time : IComparable<Time> {
		//first day is sunday
        private bool[] days = new bool[7];
        private TimeOnly start;
        private TimeOnly end;
		
		public Time(string daysString, TimeOnly start, TimeOnly end){
            foreach (char c in daysString)
            {
                switch (char.ToUpperInvariant(c))
                {
                    case 'U':
                        days[0] = true;
                        break;
                    case 'M':
                        days[1] = true;
                        break;
                    case 'T':
                        days[2] = true;
                        break;
                    case 'W':
                        days[3] = true;
                        break;
                    case 'R':
                        days[4] = true;
                        break;
                    case 'F':
                        days[5] = true;
                        break;
                    case 'S':
                        days[6] = true;
                        break;
                }

            }
            if (end <= start) throw new ArgumentException("End must be after Start.");
            this.start = start;
            this.end = end;
        }
        //public bool[] getBinaryDays() {
        //    return days;
        //}
        private static int DayIndex(char c) => char.ToUpperInvariant(c) switch
        {
            'U' => 0,
            'M' => 1,
            'T' => 2,
            'W' => 3,
            'R' => 4,
            'F' => 5,
            'S' => 6,
            _ => throw new ArgumentException($"Unknown day '{c}'.")
        };
        public bool OccursOn(char day) => days[DayIndex(day)];
        public string getDays() {
            StringBuilder sb = new StringBuilder();
            if (days[0]) sb.Append("U");
            if (days[1]) sb.Append("M");
            if (days[2]) sb.Append("T");
            if (days[3]) sb.Append("W");
            if (days[4]) sb.Append("R");
            if (days[5]) sb.Append("F");
            if (days[6]) sb.Append("S");
            return sb.ToString();
        }
        public bool conflictsWith(Time other) {
            for (int i = 0; i < 7; i++) {
                if (this.days[i] && other.days[i]) {
                    if (this.start < other.end && other.start < this.end) {
                        return true;
                    }
                }
            }
            return false;
        }

        public int CompareTo(Time? other)
        {
            if (other is null) return 1;
            int c = start.CompareTo(other.start);
            if (c != 0) return c;
            c = end.CompareTo(other.end);
            if (c != 0) return c;

            // tiny mask for a final tie-breaker
            int m1 = 0, m2 = 0;
            for (int i = 0; i < 7; i++) { if (days[i]) m1 |= 1 << i; }
            for (int i = 0; i < 7; i++) { if (other.days[i]) m2 |= 1 << i; }
            return m1.CompareTo(m2);
        }
        public TimeOnly GetStart() {
            return start;
        }
    }//end Time

}//end namespace System