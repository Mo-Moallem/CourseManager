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
                List<Section> sectionsByCrn = sectionsManager.getSectionsByCrn(crns);
                List<Section> sectionsByDay;

                sectionsByDay = sectionsManager.getSectionsByDay(day, crns, sectionsByCrn);
                currentSections = sectionsManager.GetSectionsInOrder(crns, sectionsByDay);
                List<Point> points = new List<Point>();
                foreach (Section section in currentSections)
                {
                    Point point = section.GetLocation().GetBuilding().GetPoint();
                    points.Add(point);
                }
                

               
                
                
                Course[] courses = sectionsManager.GetCoursesFromSections(sectionsByCrn);


                if (currentSections.Count == 0)
                {
                    resultTextBox.Text = $"No classes scheduled on {GetDayName(day)}.";
                 
                    return;
                }

                
                DisplayResults(GetDayName(day), courses, (sectionsByDay.Select(s => s.GetLocation().GetBuilding()).Distinct()).Count(), (int) CalculateTotalDistance(points) , currentSections);
                
                // Draw the path on the map
                drawPathOnMap(points);
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
            sb.AppendLine($"Distance Traveled = {totalDistance} m");
            sb.AppendLine();
            foreach (var section in sections)
            {
                sb.AppendLine($"{section.GetCourse().GetCourseCode()} {section.GetTime().GetStart()}");
                
            }

            resultTextBox.Text = sb.ToString();
        }



        private List<Point> pathPoints = null;

        private void drawPathOnMap(List<Point> points)
        {
            pathPoints = points;
            MapPanel.Invalidate(); // Triggers Paint event
        }

        // Add this event handler to your MapPanel
        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pathPoints == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                DrawAnArrow(pathPoints[i], pathPoints[i + 1], g);
            }
            for (int i = 0; i < pathPoints.Count; i++)
            {
                DrawAPoint(pathPoints[i], g, (i + 1).ToString());
            }
        }

        private void DrawAnArrow(Point p1, Point p2, Graphics g)
        {
            Pen arrowPen = new Pen(Color.Black, 2); ;
            g.DrawLine(arrowPen, p1, p2);
        }
        private void DrawAPoint(Point point, Graphics g, string value) {
            Pen circlePen = new Pen(Color.Black, 2);
            Brush circleBrush = new SolidBrush(Color.Yellow);
            Brush txtBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;       
            sf.LineAlignment = StringAlignment.Center;    

            g.FillEllipse(circleBrush, point.X - 15, point.Y - 15, 30, 30);
            g.DrawEllipse(circlePen, point.X - 15, point.Y - 15, 30, 30);
            g.DrawString(value, MapPanel.Font, txtBrush, point, sf);

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
        private double CalculateTotalDistance(List<Point> points)
        {
            if (points.Count < 2)
                return 0;

            double totalDistance = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                totalDistance += CalculateDistance(points[i], points[i + 1]);
            }
            return totalDistance;
        }

        private double CalculateDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

    }
}