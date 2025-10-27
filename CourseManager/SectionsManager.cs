using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static CourseManager.TimeParser;

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
            generateBuildings();
            locations = new Dictionary<string, Location>();
            courses = new Dictionary<string, Course>();
        }


        private void generateBuildings()
        {
            buildings["22"] = new Building("22", new Point(370, 320));
            buildings["24"] = new Building("24", new Point(400, 390));
            buildings["7"] = new Building("7", new Point(85, 130));
            buildings["5"] = new Building("5", new Point(217, 158));
            buildings["63"] = new Building("63", new Point(120, 50));
            buildings["59"] = new Building("59", new Point(340, 90));
            buildings["4"] = new Building("4", new Point(150, 145));
            buildings["6"] = new Building("6", new Point(125, 165));
            buildings["42"] = new Building("42", new Point(475, 175));
            buildings["76"] = new Building("76", new Point(350, 320));
            buildings["28"] = new Building("28", new Point(280, 265));
            buildings["9"] = new Building("9", new Point(140, 255));
            buildings["57"] = new Building("57", new Point(265, 185));
            buildings["58"] = new Building("58", new Point(305, 200));
            buildings["78"] = new Building("78", new Point(470, 325));
            buildings["3"] = new Building("3", new Point(105, 155));
            buildings["DTV248"] = new Building("DTV248", new Point(185, 260));
            buildings["1"] = new Building("1", new Point(95, 180));
            buildings["75"] = new Building("75", new Point(95, 75));
            buildings["39"] = new Building("39", new Point(420, 140));
            buildings["407"] = new Building("407", new Point(450, 95));
            buildings["11"] = new Building("11", new Point(245, 350));


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
            Building? building = (!string.IsNullOrEmpty(buildingNo) && buildings.ContainsKey(buildingNo))
                ? buildings[buildingNo]
                : null;
            Location? location = GetOrCreateLocation(building, roomNo);
            Time? time = BuildTimeOrNull(vStart, vEnd, daysString);
            Section section = new Section(crn, course, term, activity, female, secNo, location, time, instructor);
            

            if (!sections.TryGetValue(crn, out var list))
            {
                list = new List<Section>();
                sections[crn] = list;
            }
            list.Add(section);

        }

        // Build Time? using your rule: if start is null -> null; if end is null -> end = start
        private static Time? BuildTimeOrNull(object? vStart, object? vEnd, string daysString)
        {
            if (!TimeParser.TryParse(vStart, out var start))
                return null;

            if (!TimeParser.TryParse(vEnd, out var end))
                end = start; // your rule

            // If your Time ctor enforces end > start, either relax it or nudge by a minute:
            // if (end <= start) end = start.AddMinutes(1);

            return new Time(daysString, start, end);
        }

        public List<Section> getSectionsByCrn(List<int> crns)
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

        public List<Section> getSectionsByDay(char day, List<int> crns, List<Section> sectionsByCrn)  {

            foreach (var section in sectionsByCrn)
            {
                if (section.GetTime() == null)
                {
                    throw new ArgumentException($"Section with CRN: {section.GetCrn()} does not have time");
                }
            }

            return sectionsByCrn
                    .Where(s => s.GetTime().OccursOn(day) == true)
                    .ToList();

        }
        public List<Section> GetSectionsInOrder(List<int> crns, List<Section> sectionsByDay)
        {
            return sectionsByDay
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
