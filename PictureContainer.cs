using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb5_2
{
    public class PictureContainer
    {
        private int _maxPoints = 20;
        
        private Form _ctx;
        private Bitmap _bmp;
        private PictureBox _picture;
        private int _width;
        private int _height;

        private List<BuildPoint> _buildPoints;
        
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
            
            _ctx.Controls.Add(_picture);
        }

        public async void DrawDiagramMultiThread(int maxThreads)
        {
            var bitmapData = _bmp.LockBits(new Rectangle(0, 0, _width, _height), ImageLockMode.ReadWrite,
                _bmp.PixelFormat);
            Console.WriteLine("bd");
            var depth = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8; // bytes per pixel
            Console.WriteLine("depth");
            int sectorWidth = _width / maxThreads;
            
            byte[] buffer = new byte[bitmapData.Width * bitmapData.Height * depth];
            Marshal.Copy(bitmapData.Scan0, buffer, 0, buffer.Length);

            List<Task> tasks = new List<Task>();
            
            for (int i = 0; i < maxThreads; i++)
            {
                int startSectorX = i * sectorWidth;
                int endSectorX = (i + 1) * sectorWidth;
                
                tasks.Add(Task.Run(() => ProcessMultiThreadDrawing(buffer, startSectorX, endSectorX, depth)));
            }

            // await Task.WhenAll(tasks.ToArray());

            Marshal.Copy(buffer, 0, bitmapData.Scan0, buffer.Length);
            _bmp.UnlockBits(bitmapData);

            _bmp.Save("test1111.png");
        }

        private void ProcessMultiThreadDrawing(byte[] buffer, int startX, int endX, int depth)
        {
            for (int i = startX; i < endX; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Point px = new Point(i, j);
                    
                    BuildPoint nearestPoint = Pixel.FindClosestPoint(px, _buildPoints);
                    
                    var offset = (j * _width + i) * depth;
                    
                    buffer[offset + 0] = nearestPoint.AssociatedColor.Color.R;
                    buffer[offset + 1] = nearestPoint.AssociatedColor.Color.G;
                    buffer[offset + 2] = nearestPoint.AssociatedColor.Color.B;
                }
            }
        }

        public void DrawDiagramOneThread()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Point px = new Point(x, y);
                    
                    BuildPoint nearestPoint = Pixel.FindClosestPoint(px, _buildPoints);
                    
                    Pixel.Draw(px, nearestPoint.AssociatedColor, Graphics.FromImage(_bmp));
                }
            }
        }

        public void DrawPoints()
        {
            foreach (var point in _buildPoints)
            {
                point.Draw(Graphics.FromImage(_bmp));
            }
        }

        public void AddRandomPoints()
        {
            _buildPoints = new List<BuildPoint>();

            for (int i = 0; i < _maxPoints; i++)
            {
                int rndX = BuildPoint.GetRandomCoordinate(_width);
                int rndY = BuildPoint.GetRandomCoordinate(_height);
                
                var p = new BuildPoint(new Point(rndX, rndY));
                _buildPoints.Add(p);
            }
        }
    }
}