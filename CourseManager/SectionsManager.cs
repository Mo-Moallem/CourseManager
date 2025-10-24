namespace CourseManager
{
    internal class SectionsManager
    {
        private Dictionary<int,Section> sections; //make readonly? //crn, section pairing
        public SectionsManager() 
        {   
            sections = DataReader.Read();
            MessageBox.Show(sections.TryGetValue(11695) != null);
        }

        private List<Section> getSectionsByCrn(List<int> crns)
        {
            List<Section> result = new List<Section>();
            foreach (int crn in crns)
            {
                if (!sections.ContainsKey(crn))
                {
                    throw new ArgumentException($"CRN {crn} does not exist.");
                }
                result.Add(sections[crn]);
            }
            return result;
        }
        private List<Section> getSectionsByDay(char day, List<int> crns) {
            return getSectionsByCrn(crns).Where(s => s.GetTime().OccursOn(day)).ToList();
        }
        public List<Section> GetSectionsInOrder(char day, List<int> crns)
        {
            return getSectionsByDay(day, crns)
                .OrderBy(s => s.GetTime())
                .ThenBy(s => s.GetTime())
                .ToList();
        }


    }

    
}
