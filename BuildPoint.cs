using System;
using System.Drawing;

namespace lb5_2
{
    public class BuildPoint: IPoint
    {
        private static readonly Random _rnd = new Random();
        
        private readonly int _radius = 5;
        private readonly Brush _brush = Brushes.Black;
        
        private readonly Point _coordinates;
        public readonly SolidBrush AssociatedColor;

        public int X => _coordinates.X;
        public int Y => _coordinates.Y;

        public BuildPoint(Point coordinates)
        {
            AssociatedColor = GetRandomAssociatedColor();
            
            _coordinates = coordinates;
        }

        public void Draw(Graphics g) => g.FillEllipse(_brush, _coordinates.X - _radius, _coordinates.Y - _radius, 2 * _radius, 2 * _radius);

        public static int GetRandomCoordinate(int max) => _rnd.Next(max);

        private static SolidBrush GetRandomAssociatedColor()
        {
            Func<int> getRnd256Value = () => _rnd.Next(256);

            int r = getRnd256Value();
            int g = getRnd256Value();
            int b = getRnd256Value();
            
            return new SolidBrush(Color.FromArgb(r, g, b));
        }
    }
}