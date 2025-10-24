namespace CourseManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            SectionsManager sectionsManager = new SectionsManager();
            List<int> crns = new List<int> { 11695, 15369, 12649, 10106, 11544 };
            List<Section> orderedSections = sectionsManager.GetSectionsInOrder('M', crns);
            foreach (Section section in orderedSections)
            {
                MessageBox.Show($"CRN: {section.GetCrn()} Time: {section.GetTime()}");
            }

        }

        
    }
}