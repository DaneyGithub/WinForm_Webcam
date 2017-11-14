using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebCam_Test
{
    public partial class Form1 : Form
    {

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoDevice in webcam)
            {
                devicesComboBox.Items.Add(videoDevice.Name);
            }

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webcam[devicesComboBox.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
        }

        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            webCamPicBox.Image = bit;
        }
        private void btnCapture_Click(object sender, EventArgs e)
        {
            saveFileDialog01.InitialDirectory = @"C:\Users\Windows\Pictures";
            if (saveFileDialog01.ShowDialog() == DialogResult.OK)
            {
                webCamPicBox.Image.Save(saveFileDialog01.FileName);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (cam.IsRunning)
                {
                    cam.Stop();
                    webCamPicBox.Image = null;

                }
            }

            catch
            {
                Console.WriteLine("Camera is not open");
            }
        }

        
    }
}
