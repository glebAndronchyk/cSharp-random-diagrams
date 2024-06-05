using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb5_2
{
    public class PictureContainer
    {
        private Form _ctx;
        private Bitmap _bmp;
        private PictureBox _picture;
        private int _width;
        private int _height;
        private bool _filled = false;

        private List<BuildPoint> _buildPoints = new List<BuildPoint>();
        
        public PictureContainer(Form ctx)
        {
            _ctx = ctx;
            _width = ctx.Width;
            _height = ctx.Height;
        }

        public void Initialize()
        {
            _bmp = new Bitmap(_width, _height);
            
            _picture = new PictureBox();
            _picture.Image = _bmp;
            _picture.Size = new Size(_width, _height);
            _picture.MouseClick += OnClick;
            
            _ctx.Controls.Add(_picture);
        }

        public async void DrawDiagramMultiThread()
        {
            Graphics g = Graphics.FromImage(_bmp);
            
            int segmentWidth = (int)Math.Ceiling((double)_bmp.Width / Environment.ProcessorCount);
            int segmentHeight = _bmp.Height;

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int startX = i * segmentWidth;
                int endX = Math.Min((i + 1) * segmentWidth, _bmp.Width);
                    
                tasks.Add(Task.Run(() =>
                {
                    for (int x = startX; x < endX; x++)
                    {
                        for (int y = 0; y < segmentHeight; y++)
                        {
                            Point px = new Point(x, y);
                        
                            BuildPoint nearestPoint = Pixel.FindClosestPoint(px, _buildPoints);
                            
                            lock (g)
                            {
                                Pixel.Draw(px, nearestPoint.AssociatedColor, g);
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            _filled = true;
        }

        public void DrawDiagramOneThread()
        {
            var g = Graphics.FromImage(_bmp);
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Point px = new Point(x, y);
                    
                    BuildPoint nearestPoint = Pixel.FindClosestPoint(px, _buildPoints);
                    
                    Pixel.Draw(px, nearestPoint.AssociatedColor, g);
                }
            }
            
            _filled = true;
        }

        public void Refresh()
        {
            _picture.Refresh();
        }

        public void Clear()
        {
            Graphics g = Graphics.FromImage(_bmp);
            g.Clear(Color.White);

            _buildPoints?.Clear();
        }

        public void DrawPoints()
        {
            var g = Graphics.FromImage(_bmp);
            
            foreach (var point in _buildPoints)
            {
                point.Draw(g);
            }
        }
        
        public void AddRandomPoints(int maxPoints)
        {
            for (int i = 0; i < maxPoints; i++)
            {
                int rndX = BuildPoint.GetRandomCoordinate(_width);
                int rndY = BuildPoint.GetRandomCoordinate(_height);
                
                var p = new BuildPoint(new Point(rndX, rndY));
                _buildPoints.Add(p);
            }

            _filled = false;
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            int X = e.X;
            int Y = e.Y;
            
            switch (e.Button)
            {
                case MouseButtons.Right:
                    OnRemovePointManually(X, Y);
                    return;
                case MouseButtons.Left:
                    OnAddPointManually(X, Y);
                    return;
                default:
                    throw new NotImplementedException("No such combination implemented");
            }
        }

        private void OnRemovePointManually(int X, int Y)
        {
            int fillArea = 20;
            var point = _buildPoints.FirstOrDefault(p => Math.Abs(p.X - X) <= 5 && Math.Abs(p.Y - Y) <= 5);

            if (point != null)
            {
                var g = Graphics.FromImage(_bmp);

                g.FillRectangle(new SolidBrush(Color.White), X - fillArea / 2, Y - fillArea / 2, fillArea, fillArea);
                _buildPoints.Remove(point);
                
                if (_filled)
                {
                    g.Clear(Color.White);
                    DrawPoints();
                    _filled = false;
                }
            }

            Refresh();
        }

        private void OnAddPointManually(int X, int Y)
        {
            _buildPoints.Add(new BuildPoint(new Point(X, Y)));
            _buildPoints.Last().Draw(Graphics.FromImage(_bmp));
            Refresh();
        }
    }
}