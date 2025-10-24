using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;



namespace CourseManager {
	public class Time {
		//first day is sunday
        private bool[] days = new bool[7];
        private TimeOnly start;
        private TimeOnly end;
		
		public Time(string daysString, TimeOnly start, TimeOnly end){
            foreach (char c in daysString)
            {
                switch (c)
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
            this.start = start;
            this.end = end;
        }
        //public bool[] getBinaryDays() {
        //    return days;
        //}
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
                    //check time overlap
                    if (this.start < other.end && other.start < this.end) {
                        return true;
                    }
                }
            }
            return false;
        }


    }//end Time

}//end namespace System