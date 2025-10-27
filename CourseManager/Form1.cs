using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

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

        private void DisplayScheduleForDay(char day)
        {
            try
            {
                List<int> crns = getCrns();

                if (crns.Count == 0)
                {
                    MessageBox.Show("Please enter CRN numbers.", "No CRNs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pathPoints = null;  // Add this
                    MapPanel.Invalidate();
                    return;
                }

                List<Section> sectionsByCrn = sectionsManager.getSectionsByCrn(crns);
                ValidateSectionsForConflicts(sectionsByCrn);

                List<Section> sectionsByDay = sectionsManager.getSectionsByDay(day, crns, sectionsByCrn);
                currentSections = sectionsManager.GetSectionsInOrder(crns, sectionsByDay);


                List<Building> buildings = new List<Building>();
                foreach (Section section in currentSections)
                {   
                    Location location = section.GetLocation();
                    if (location == null) {
                        MessageBox.Show($"Section {section.GetCourse().GetCourseCode()}-{section.GetSecNo()} does not have and assigned location: you cannot draw the path");
                        return;
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


                if (currentSections.Count == 0)
                {
                    resultTextBox.Text = $"No classes scheduled on {GetDayName(day)}.";
                    pathPoints = null;  // Add this line
                    MapPanel.Invalidate();  // Add this line to clear the panel

                    return;
                }

                
                DisplayResults(GetDayName(day), courses, (sectionsByDay.Select(s => s.GetLocation().GetBuilding()).Distinct()).Count(), (int) DistanceCalculator.CalculateTotalDistance(points) , currentSections);
                
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

            resultTextBox.Text = sb.ToString();
        }



        private List<Point> pathPoints = null;

        private void drawPathOnMap(List<Point> points)
        {
            System.Diagnostics.Debug.WriteLine($"drawPathOnMap called with {points.Count} points");
            pathPoints = points;
            MapPanel.Invalidate();

        }

        // Add this event handler to your MapPanel
        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Paint called at {DateTime.Now:HH:mm:ss.fff}");
            if (pathPoints == null || pathPoints.Count == 0)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw all arrows first
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                DrawAnArrow(pathPoints[i], pathPoints[i + 1], g);
            }

            // Then draw all points with numbers
            for (int i = 0; i < pathPoints.Count; i++)
            {
                DrawAPoint(pathPoints[i], g, (i + 1).ToString());
                
            }
        }

        private void DrawAnArrow(Point p1, Point p2, Graphics g)
        {
            Pen arrowPen = new Pen(Color.Black, 2);
            arrowPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);

            // Calculate the direction vector
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            // Normalize and shorten by 15 pixels (the radius of your circle)
            float shortenBy = 15; // Match the circle radius
            float newLength = length - shortenBy;

            if (newLength > 0)
            {
                float ratio = newLength / length;
                Point newP2 = new Point(
                    (int)(p1.X + dx * ratio),
                    (int)(p1.Y + dy * ratio)
                );

                g.DrawLine(arrowPen, p1, newP2);
            }

            arrowPen.Dispose();
        }


        private void DrawAPoint(Point point, Graphics g, string value) {
            Pen circlePen = new Pen(Color.Black, 2);  // Changed to red and thicker
            Brush circleBrush = new SolidBrush(Color.Yellow);  // Changed to bright green

            Brush txtBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;       
            sf.LineAlignment = StringAlignment.Center;

            g.FillEllipse(circleBrush, point.X - 15, point.Y - 15, 30, 30);  // Made bigger
            g.DrawEllipse(circlePen, point.X - 15, point.Y - 15, 30, 30);
            g.DrawString(value, MapPanel.Font, txtBrush, point, sf);  // Bigger font
            System.Diagnostics.Debug.WriteLine($"after drawing with value {value}");

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

        private void MapPanel_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseArgs)
            {
                Point clickPoint = mouseArgs.Location;
                System.Diagnostics.Debug.WriteLine($"Click at: X={clickPoint.X}, Y={clickPoint.Y}");


            }
        }

        public bool HasTimeConflicts(List<Section> sectionsToCheck)
        {
            for (int i = 0; i < sectionsToCheck.Count; i++)
            {
                var timeA = sectionsToCheck[i].GetTime();
                if (timeA == null) continue;

                for (int j = i + 1; j < sectionsToCheck.Count; j++)
                {
                    var timeB = sectionsToCheck[j].GetTime();
                    if (timeB == null) continue;

                    // If both have time and conflict, return true
                    if (timeA.conflictsWith(timeB))
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Conflict detected between CRN {sectionsToCheck[i].GetCrn()} and CRN {sectionsToCheck[j].GetCrn()}");
                        return true;
                    }
                }
            }

            // No conflicts found
            return false;
        }

        public void ValidateSectionsForConflicts(List<Section> sections)
        {
            if (HasTimeConflicts(sections))
            {
                throw new InvalidOperationException("Time conflict detected between selected sections!");
            }
        }




    }


}