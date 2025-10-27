using System.Collections.Generic;
using System.Text;

namespace CourseManager
{
    public class ScheduleFormatter
    {
        public string FormatSchedule(string day, Course[] courses, int numberOfDifBuildings, int totalDistance, List<Section> sections)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Scheduled Day : {day}");
            sb.AppendLine($"Number of Courses = {courses.Length}");
            for (int i = 0; i < courses.Length; i++)
            {
                sb.AppendLine($"{i + 1}- {courses[i].GetCourseCode()} : {courses[i].GetCourseTitle()}");
            }
            sb.AppendLine($"Number of Different Buildings = {numberOfDifBuildings}");
            sb.AppendLine($"Distance Traveled = {totalDistance} m");
            sb.AppendLine();
            foreach (var section in sections)
            {
                sb.AppendLine($"{section.GetCourse().GetCourseCode()} {section.GetTime().GetStart()}");
            }

            return sb.ToString();
        }

        public string GetDayName(char day)
        {
            return day switch
            {
                'U' => "Sunday",
                'M' => "Monday",
                'T' => "Tuesday",
                'W' => "Wednesday",
                'R' => "Thursday",
                'F' => "Friday",
                'S' => "Saturday",
                _ => "Unknown"
            };
        }
    }
}