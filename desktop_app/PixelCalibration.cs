using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Textexemplu2
{
    public partial class PixelCalibration : Form
    {
        private Image<Gray, Byte> camera1_Threshold;
        private Image<Gray, Byte> camera2_Threshold;
        private Image<Gray, Byte> camera3_Threshold;
        private Image<Gray, Byte> camera4_Threshold;

        private Image<Hsv, Byte> hsvImage1;
        private Image<Hsv, Byte> hsvImage2;
        private Image<Hsv, Byte> hsvImage3;
        private Image<Hsv, Byte> hsvImage4;

        Image<Bgr, Byte> convertedImage1, convertedImage2, convertedImage3, convertedImage4;
        VideoCapture downFrontWebcam, downBackWebcam, upFrontWebcam, upBackWebcam;

        private void downFront_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.AppendText("{" + e.X.ToString() + "," + e.Y.ToString() + "}, ");
        }
        private void downBack_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.AppendText("{" + e.X.ToString() + "," + e.Y.ToString() + "}, ");
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void upFront_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.AppendText("{" + e.X.ToString() + "," + e.Y.ToString() + "}, ");
        }

        private void upBack_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.AppendText("{" + e.X.ToString() + "," + e.Y.ToString() + "}, ");
        }

        public PixelCalibration()
        {
            InitializeComponent();
        }

        private void PixelCalibration_Load(object sender, EventArgs e)
        {
            try
            {
                downFrontWebcam = new VideoCapture(1);
                downBackWebcam = new VideoCapture(0);
                upFrontWebcam = new VideoCapture(3);
                upBackWebcam = new VideoCapture(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to read from webcam, error: " + Environment.NewLine + Environment.NewLine +
                                ex.Message + Environment.NewLine + Environment.NewLine +
                                "exiting program");
                Environment.Exit(0);
                return;
            }
            Application.Idle += processFrameAndUpdateGUI;       // add process image function to the application's list of tasks

        }

        void processFrameAndUpdateGUI(object sender, EventArgs arg)
        {
            int filterBlur = 5;
            Mat downFrontCam, downBackCam, upBackCam, upFrontCam;

            downFrontCam = downFrontWebcam.QueryFrame();
            downBackCam = downBackWebcam.QueryFrame();
            upFrontCam = upFrontWebcam.QueryFrame();
            upBackCam = upBackWebcam.QueryFrame();

            if (downFrontCam == null || downBackCam == null || upFrontCam == null || upBackCam == null)
            {
                MessageBox.Show("unable to read from webcam" + Environment.NewLine + Environment.NewLine + "exiting program");
                Environment.Exit(0);
                return;
            }

            //camera 1
            convertedImage1 = downFrontCam.ToImage<Bgr, Byte>();
            convertedImage1 = convertedImage1.SmoothMedian(filterBlur);
            hsvImage1 = downFrontCam.ToImage<Hsv, Byte>();
            CvInvoke.CvtColor(convertedImage1, hsvImage1, ColorConversion.Bgr2Hsv);
            camera1_Threshold = downFrontCam.ToImage<Gray, Byte>();

            //camera 2
            convertedImage2 = downBackCam.ToImage<Bgr, Byte>();
            convertedImage2 = convertedImage2.SmoothMedian(filterBlur);
            hsvImage2 = downBackCam.ToImage<Hsv, Byte>();
            CvInvoke.CvtColor(convertedImage2, hsvImage2, ColorConversion.Bgr2Hsv);
            camera2_Threshold = downBackCam.ToImage<Gray, Byte>();


            //camera 3
            convertedImage3 = upFrontCam.ToImage<Bgr, Byte>();
            convertedImage3 = convertedImage3.SmoothMedian(filterBlur);
            hsvImage3 = upFrontCam.ToImage<Hsv, Byte>();
            CvInvoke.CvtColor(convertedImage3, hsvImage3, ColorConversion.Bgr2Hsv);
            camera3_Threshold = upFrontCam.ToImage<Gray, Byte>();

            //camera 4
            convertedImage4 = upBackCam.ToImage<Bgr, Byte>();
            convertedImage4 = convertedImage4.SmoothMedian(filterBlur);
            hsvImage4 = upBackCam.ToImage<Hsv, Byte>();
            CvInvoke.CvtColor(convertedImage4, hsvImage4, ColorConversion.Bgr2Hsv);
            camera4_Threshold = upBackCam.ToImage<Gray, Byte>();

            drawCircleConvertedImage(hsvImage1, arrayPointsCamera1);
            drawCircleConvertedImage(hsvImage2, arrayPointsCamera2);
            drawCircleConvertedImage(hsvImage3, arrayPointsCamera3);
            drawCircleConvertedImage(hsvImage4, arrayPointsCamera4);

            downFront.Image = hsvImage1;
            downBack.Image = hsvImage2;
            upFront.Image = hsvImage3;
            upBack.Image = hsvImage4;



        }

        private void drawCircleConvertedImage(Image<Hsv, Byte> cameraDraw, int[,] arrayPoints)
        {
            for (int i = 0; i < arrayPoints.GetLength(0); i++)
            {
                CvInvoke.Circle(cameraDraw, new Point(arrayPoints[i, 0], arrayPoints[i, 1]), 4, new MCvScalar(70, 255, 255), -1);
            }
        }

        int[,] arrayPointsCamera1 = new int[,] {
            {245,66}, {245,132}, {244,209}, {181,226}, {130,251}, {200,169}, 
            {302,67}, {307,140}, {303,211}, {354,236}, {395,269}, {341,178},
            {152,300}, {207,282}, {272,260}, {322,285}, {362,312}, {265,298}
        };

        int[,] arrayPointsCamera2 = new int[,] {
            {310,100}, {312,154}, {299,224}, {259,236}, {215,250}, {270,182},
            {353,102}, {354,154}, {361,219}, {402,240}, {434,257}, {390,188},
            {236,289}, {284,272}, {331,255}, {375,278}, {415,295}, {325,286}
        };

        int[,] arrayPointsCamera3 = new int[,]{
            {292,342}, {293,278}, {290,209}, {237,183}, {190,156}, {252,244},
            {344,342}, {346,281}, {349,207}, {407,182}, {453,163}, {389,244},
            {216,113}, {264,138}, {322,161}, {374,139}, {430,117}, {325,121}
        };

        int[,] arrayPointsCamera4 = new int[,]
        {
            {269,339}, {267,279}, {263,213}, {211,180}, {169,161}, {228,243},
            {324,344}, {325,283}, {327,209}, {382,187}, {429,166}, {367,246},
            {196,116}, {240,137}, {291,159}, {353,137}, {417,120}, {300,123}
        };

        private void exitButton_Click(object sender, EventArgs e)
        {
            Form2 automaticDataCube = new Form2();
            Close();
            automaticDataCube.Show();
        }
    }
}
