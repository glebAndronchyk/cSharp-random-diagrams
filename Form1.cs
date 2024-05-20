using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lb5_2
{
    public partial class Form1 : Form
    {
        private PictureContainer _pictureContainer;
        
        public Form1()
        {
            InitializeComponent();
            _pictureContainer = new PictureContainer(this);
            _pictureContainer.Initialize();
            _pictureContainer.AddRandomPoints();
            _pictureContainer.DrawDiagramMultiThread();
        }
    }
}