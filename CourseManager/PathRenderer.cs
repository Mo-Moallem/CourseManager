using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CourseManager
{
    public class PathRenderer
    {
        private List<Point> pathPoints = null;

        public void SetPath(List<Point> points)
        {
            System.Diagnostics.Debug.WriteLine($"drawPathOnMap called with {points.Count} points");
            pathPoints = points;
        }

        public void ClearPath()
        {
            pathPoints = null;
        }

        public void Render(PaintEventArgs e, Panel mapPanel)
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
                DrawAPoint(pathPoints[i], g, (i + 1).ToString(), mapPanel);
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

        private void DrawAPoint(Point point, Graphics g, string value, Panel mapPanel)
        {
            Pen circlePen = new Pen(Color.Black, 2);  // Changed to red and thicker
            Brush circleBrush = new SolidBrush(Color.Yellow);  // Changed to bright green

            Brush txtBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.FillEllipse(circleBrush, point.X - 15, point.Y - 15, 30, 30);  // Made bigger
            g.DrawEllipse(circlePen, point.X - 15, point.Y - 15, 30, 30);
            g.DrawString(value, mapPanel.Font, txtBrush, point, sf);  // Bigger font
            System.Diagnostics.Debug.WriteLine($"after drawing with value {value}");
        }
    }
}