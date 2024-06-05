using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace lb5_2
{
    public partial class Form1 : Form
    {
        private PictureContainer _pictureContainer;
        private bool _isMultiThread = false;
        private Stopwatch _fullDrawingTimer = new Stopwatch();
        private Stopwatch _onlyDiagramGenerationTimer = new Stopwatch();
        
        public Form1()
        {
            InitializeComponent();
            _pictureContainer = new PictureContainer(this);
            _pictureContainer.Initialize();
        }

        private void placeRandomPointsButton_click(object sender, EventArgs e)
        {
            _pictureContainer.Clear();
            _pictureContainer.AddRandomPoints((int)randomPointsAmount.Value);
            _pictureContainer.DrawPoints();
            _pictureContainer.Refresh();
        }

        private void buildButton_click(object sender, EventArgs e)
        {
            _fullDrawingTimer.Restart();
            _onlyDiagramGenerationTimer.Restart();
            if (_isMultiThread)
            {
                _pictureContainer.DrawDiagramMultiThread();
            }
            else
            {
                _pictureContainer.DrawDiagramOneThread();
            }
            _onlyDiagramGenerationTimer.Stop();
            
            _pictureContainer.DrawPoints();
            _pictureContainer.Refresh();
            _fullDrawingTimer.Stop();

            LogTimersResults();
        }

        private void LogTimersResults()
        {
            Console.WriteLine("Multithread: " + _isMultiThread);
            Console.WriteLine($"Process of full drawing took: {_fullDrawingTimer.ElapsedMilliseconds}ms");
            Console.WriteLine($"Process of diagram generation took: {_onlyDiagramGenerationTimer.ElapsedMilliseconds}ms");
        }

        private void multithreadToggle_CheckedChanged(object sender, EventArgs e)
        {
            _isMultiThread = (sender as CheckBox).Checked;
        }
    }
}