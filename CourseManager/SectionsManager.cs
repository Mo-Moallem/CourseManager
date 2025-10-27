using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;
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
            buildings["7"] = new Building("7", new Point(245, 232));
            buildings["5"] = new Building("5", new Point(205, 159));
            buildings["63"] = new Building("63", new Point(122, 50));
            buildings["59"] = new Building("59", new Point(317, 192));
            buildings["4"] = new Building("4", new Point(177, 134));
            buildings["6"] = new Building("6", new Point(246, 204));
            buildings["42"] = new Building("42", new Point(510, 47));
            buildings["76"] = new Building("76", new Point(460, 348));
            buildings["28"] = new Building("28", new Point(510, 47));
            buildings["9"] = new Building("9", new Point(301, 285));
            buildings["57"] = new Building("57", new Point(509, 0));
            buildings["58"] = new Building("58", new Point(520, 0));
            buildings["78"] = new Building("78", new Point(514, 313));
            buildings["3"] = new Building("3", new Point(130, 123));
            buildings["DTV248"] = new Building("DTV248", new Point(254, 0));
            buildings["1"] = new Building("1", new Point(84, 144));//11006
            buildings["75"] = new Building("75", new Point(107, 93));
            buildings["39"] = new Building("39", new Point(358, 83));
            buildings["407"] = new Building("407", new Point(552, 0));
            buildings["11"] = new Building("11", new Point(347, 374));


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



        public void AddSection(int term, int crn, string courseCode, string dept, string secNoString, string title, string activity, string daysString,
    object vStart, object vEnd, string buildingNo, string roomNo, string instructor)
        {

            try
            {
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

                // Debug logging
                System.Diagnostics.Debug.WriteLine($"CRN {crn}: vStart={vStart}, vEnd={vEnd}, days={daysString}");

                Time? time = BuildTimeOrNull(vStart, vEnd, daysString);

                if (time == null)
                {
                    System.Diagnostics.Debug.WriteLine($"WARNING: CRN {crn} has null time! vStart={vStart}, vEnd={vEnd}");
                }

                Section section = new Section(crn, course, term, activity, female, secNo, location, time, instructor);

                if (!sections.TryGetValue(crn, out var list))
                {
                    list = new List<Section>();
                    sections[crn] = list;
                }
                list.Add(section);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR adding section CRN {crn}: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw to see the error
            }
        }

        // Also update BuildTimeOrNull with better error handling:
        private static Time? BuildTimeOrNull(object? vStart, object? vEnd, string daysString)
        {
            System.Diagnostics.Debug.WriteLine($"BuildTimeOrNull: vStart type={vStart?.GetType().Name}, value={vStart}");
            System.Diagnostics.Debug.WriteLine($"BuildTimeOrNull: vEnd type={vEnd?.GetType().Name}, value={vEnd}");

            if (!TimeParser.TryParse(vStart, out var start))
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse start time: {vStart}");
                return null;
            }

            if (!TimeParser.TryParse(vEnd, out var end))
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse end time: {vEnd}, using start time instead");
                end = start;
            }

            System.Diagnostics.Debug.WriteLine($"Parsed times: start={start}, end={end}");

            // Check if times are equal (which would cause the exception)
            if (end <= start)
            {
                System.Diagnostics.Debug.WriteLine($"WARNING: end <= start, adjusting end time by 1 minute");
                end = start.AddMinutes(1);
            }

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
