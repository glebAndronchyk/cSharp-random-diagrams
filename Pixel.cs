using System;
using System.Collections.Generic;
using System.Drawing;

namespace lb5_2
{
    public class Pixel
    {
        public static void Draw(Point location, SolidBrush brush, Graphics g)
        {
            g.FillRectangle(brush, location.X, location.Y, 1, 1);
        }
        
        public static T FindClosestPoint<T>(Point currentPixel, List<T> points) where T: IPoint
        {

            T closestPoint = points[0];
            double shortestDistance = Distance(currentPixel, closestPoint);

            foreach (T point in points)
            {
                double distance = Distance(currentPixel, point);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPoint = point;
                }
            }

            return closestPoint;
        }
        
        private static double Distance<T>(Point p1, T p2) where T: IPoint
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}