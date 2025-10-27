using System.Collections.Generic;
using System.Linq;

namespace CourseManager
{
    public class ScheduleService
    {
        private SectionsManager sectionsManager;

        public ScheduleService(SectionsManager sectionsManager)
        {
            this.sectionsManager = sectionsManager;
        }

        public ScheduleResult GetScheduleForDay(char day, List<int> crns)
        {
            List<Section> sectionsByCrn = sectionsManager.getSectionsByCrn(crns);
            List<Section> sectionsByDay = sectionsManager.getSectionsByDay(day, crns, sectionsByCrn);
            List<Section> currentSections = sectionsManager.GetSectionsInOrder(crns, sectionsByDay);

            List<Building> buildings = new List<Building>();
            foreach (Section section in currentSections)
            {
                Location location = section.GetLocation();
                if (location == null)
                {
                    throw new InvalidOperationException($"Section {section.GetCourse().GetCourseCode()}-{section.GetSecNo()} does not have and assigned location: you cannot draw the path");
                }
                Building building = location.GetBuilding();
                buildings.Add(building);
            }

            List<Building> cleanBuildings = CollapseConsecutiveDuplicates(buildings);

            List<Point> points = new List<Point>();
            foreach (Building building in cleanBuildings)
            {
                Point point = building.GetPoint();
                points.Add(point);
            }

            Course[] courses = sectionsManager.GetCoursesFromSections(sectionsByCrn);

            int numberOfDifBuildings = sectionsByDay.Select(s => s.GetLocation().GetBuilding()).Distinct().Count();

            return new ScheduleResult
            {
                CurrentSections = currentSections,
                Buildings = cleanBuildings,
                Points = points,
                Courses = courses,
                NumberOfDifferentBuildings = numberOfDifBuildings
            };
        }

        public List<Building> CollapseConsecutiveDuplicates(List<Building> buildings)
        {
            if (buildings == null || buildings.Count == 0)
                return new List<Building>();

            var result = new List<Building> { buildings[0] };

            for (int i = 1; i < buildings.Count; i++)
            {
                if (buildings[i] != buildings[i - 1])
                {
                    result.Add(buildings[i]);
                }
            }

            return result;
        }
    }

    public class ScheduleResult
    {
        public List<Section> CurrentSections { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Point> Points { get; set; }
        public Course[] Courses { get; set; }
        public int NumberOfDifferentBuildings { get; set; }
    }
}