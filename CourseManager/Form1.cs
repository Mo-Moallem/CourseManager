using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CourseManager
{
    public partial class Form1 : Form
    {
        private SectionsManager sectionsManager;
        private List<Section> currentSections;
        private Graphics mapGraphics;

        public Form1()
        {
            InitializeComponent();
            sectionsManager = new SectionsManager();
            DataReader.ReadTo(sectionsManager);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Redraw the path when panel repaints
            //if (currentSections.Any())
            //{
            //    DrawPathOnMap(e.Graphics);
            //}
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the map graphics
            //if (panel1 != null)
            //{
            //    mapGraphics = panel1.CreateGraphics();
            //}
        }

        private void sundayBtn_Click(object sender, EventArgs e)
        {
            DisplayScheduleForDay('U');
        }

        private void mondayBtn_Click(object sender, EventArgs e)
        {
            DisplayScheduleForDay('M');
        }

        private void tuesdayBtn_Click(object sender, EventArgs e)
        {
            DisplayScheduleForDay('T');
        }

        private void wednesdayBtn_Click(object sender, EventArgs e)
        {
            DisplayScheduleForDay('W');
        }

        private void trusdayBtn_Click(object sender, EventArgs e)
        {
            DisplayScheduleForDay('R');
        }

        private void DisplayScheduleForDay(char day)
        {
            try
            {
                List<int> crns = getCrns();

                if (crns.Count == 0)
                {
                    MessageBox.Show("Please enter CRN numbers.", "No CRNs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                currentSections = sectionsManager.GetSectionsInOrder(day, crns);
                Course[] courses = sectionsManager.GetCoursesFromSections(currentSections);


                if (currentSections.Count == 0)
                {
                    resultTextBox.Text = $"No classes scheduled on {GetDayName(day)}.";
                 
                    return;
                }

                
                DisplayResults(GetDayName(day), courses, 3, 4,currentSections);

                // Draw the path on the map
                //panel1.Invalidate(); // Trigger panel1_Paint
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayResults(string day, Course[] courses, int numberOfDifBuildings, int totalDistance, List<Section> sections)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine($"Scheduled Day : {day}");
            sb.AppendLine($"Number of Courses = {courses.Length}");
            for (int i = 0; i < courses.Length; i ++) 
            {
                sb.AppendLine($"{i+1}- {courses[i].GetCourseCode()} : {courses[i].GetCourseTitle()}");
            }
            sb.AppendLine($"Number of Different Buildings = {numberOfDifBuildings}");
            sb.AppendLine($"Distance Traveled = {totalDistance}");
            sb.AppendLine();
            foreach (var section in sections)
            {
                sb.AppendLine($"{section.GetCourse().GetCourseCode()} {section.GetTime().GetStart()}");
                
            }

            resultTextBox.Text = sb.ToString();
        }

        private object GetUniqueBuildingsCount(List<Section> currentSections)
        {
            return 3;
        }

        private double CalculateTotalDistance(List<Section> sections)
        {
            // You'll need to implement distance calculation between buildings
            // This is a placeholder
            return 780; // Example value in meters
        }

        private string GetDayName(char day)
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

        public List<int> getCrns()
        {
            List<int> crns = new List<int>();
            string[] crnsStr = crnsTextBox.Text.Split(new[] { ' ', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string crn in crnsStr)
            {
                if (int.TryParse(crn.Trim(), out int crnInt))
                {
                    crns.Add(crnInt);
                }
            }

            return crns;
        }
    }
}