using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CourseManager
{
    internal class SectionsManager
    {
        private Dictionary<int,List<Section>> sections; //make readonly? //crn, section pairing
        public SectionsManager() 
        {   
            sections = DataReader.Read();
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
