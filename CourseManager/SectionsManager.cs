using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace CourseManager
{
    public class SectionsManager
    {
        private Dictionary<int,List<Section>> sections; //make readonly? //crn, section pairing
        private Dictionary<string, Building> buildings;
        private Dictionary<string, Location> locations;
        private Dictionary<string, Course> courses;
        public SectionsManager() 
        {
            sections = new Dictionary<int, List<Section>>();
            buildings = new Dictionary<string, Building>();
            locations = new Dictionary<string, Location>();
            courses = new Dictionary<string, Course>();
        }
        public void LoadData()
        {
            DataReader.ReadTo(this); // Pass 'this' for callbacks
        }

        public Building? GetOrCreateBuilding(string buildingNo)
        {
            // Handle null or empty building numbers
            if (string.IsNullOrEmpty(buildingNo))
            {
                return null;
            }

            if(!buildings.TryGetValue(buildingNo, out var building))
            {
                building = new Building(buildingNo);
                buildings[buildingNo] = building;
            }
            return building;
        }

        public Location? GetOrCreateLocation(Building? building, string? roomNo)
        {
            if (building == null || roomNo == null) {
                return null;
            }
            string key = $"{building.GetBuildingNo()}_{roomNo}";
            if (!locations.TryGetValue(key, out var location))
            {
                location = new Location(building, roomNo);
                locations[key] = location;
            }
            return location;
        }

        public Course GetOrCreateCourse(string dept, string courseCode, string title)
        {
            string key = $"{dept}_{courseCode}";
            if (!courses.TryGetValue(key, out var course))
            {
                course = new Course(dept, courseCode, title);
                courses[key] = course;
            }
            return course;
        }

        

        public void AddSection(int term, int crn, string courseCode, string dept, string secNoString,string title, string activity, string daysString, 
            object vStart, object vEnd, string buildingNo, string roomNo, string instructor) {

            bool female = false;
            if (secNoString.StartsWith("F"))
            {
                secNoString = secNoString[1..];
                female = true;
            }
            int secNo = int.Parse(secNoString);


            Course course = GetOrCreateCourse(dept, courseCode, title);
            Building building = GetOrCreateBuilding(buildingNo);
            Location location = GetOrCreateLocation(building, roomNo);
            Time? time = BuildTimeOrNull(vStart, vEnd, daysString);
            Section section = new Section(crn, course, term, activity, female, secNo, location, time, instructor);
            

            if (!sections.TryGetValue(crn, out var list))
            {
                list = new List<Section>();
                sections[crn] = list;
            }
            list.Add(section);

        }

        private static bool TryParseTime(object? v, out TimeOnly t)
        {
            t = default;
            if (v is null) return false;

            if (v is double d) { t = TimeOnly.FromDateTime(DateTime.FromOADate(d)); return true; }

            var s = Convert.ToString(v)?.Trim();
            if (string.IsNullOrEmpty(s)) return false;

            // exact "HHmm" like 0800
            if (s.Length == 4 &&
                int.TryParse(s.AsSpan(0, 2), out var hh) &&
                int.TryParse(s.AsSpan(2, 2), out var mm))
            { t = new TimeOnly(hh, mm); return true; }

            // fallback (e.g., "8:00", "8:00 AM")
            return TimeOnly.TryParse(s, out t);
        }




        // Build Time? using your rule: if start is null -> null; if end is null -> end = start
        private static Time? BuildTimeOrNull(object? vStart, object? vEnd, string daysString)
        {
            if (!TryParseTime(vStart, out var start))
                return null;

            if (!TryParseTime(vEnd, out var end))
                end = start; // your rule

            // If your Time ctor enforces end > start, either relax it or nudge by a minute:
            // if (end <= start) end = start.AddMinutes(1);

            return new Time(daysString, start, end);
        }

        private List<Section> getSectionsByCrn(List<int> crns)
        {
            List<Section> result = new List<Section>();
            foreach (int crn in crns)
            {

                if (sections.TryGetValue(crn, out var list))
                {
                    result.AddRange(list);
                }
                else
                {
                    throw new ArgumentException($"CRN {crn} does not exist.");
                }
            }
            return result;
        }

        private List<Section> getSectionsByDay(char day, List<int> crns) {
            return getSectionsByCrn(crns)
                    .Where(s => s.GetTime().OccursOn(day) == true)
                    .ToList();

        }
        public List<Section> GetSectionsInOrder(char day, List<int> crns)
        {
            return getSectionsByDay(day, crns)
                .OrderBy(s => s.GetTime())  
                .ToList();
        }
        

        public Course[] GetCoursesFromSections(List<Section> currentSections)
        {
            if (currentSections == null || currentSections.Count == 0)
            {
                return Array.Empty<Course>();
            }

            // Extract unique courses using LINQ
            return currentSections
                .Select(section => section.GetCourse())
                .Distinct()
                .ToArray();
        }
    }

    
}
