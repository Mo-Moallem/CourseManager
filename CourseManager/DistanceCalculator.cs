using System;
using System.Collections.Generic;
using System.Drawing;

namespace CourseManager
{
    public class DistanceCalculator
    {
        public static double CalculateTotalDistance(List<Point> points)
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

        private static double CalculateDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}