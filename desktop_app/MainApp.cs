using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Textexemplu2.Kociemba;
using Textexemplu2.Blindfolded;

namespace Textexemplu2
{
    public partial class Form2 : Form
    {
        private string blindfoldedFinalStringSolution;
        private string kociembaFinalStringSolution;
        private string scrambleTheCubeSolution;
        // switch Manual Automatic
        CubeArrays switchBoolManulaAutomatic = new CubeArrays();

        // serialConnection and regex
        bool isConnected = false;
        String[] ports;
        SerialPort port;

        int kociembaCountMoves, bldCornersCountMoves, bldEdgesCountMoves, kociembaCountFullString, bldCornersCountFullString, bldEdgesCountFullString;
        bool firstSendKociemba = false, firstSendBLDCorners = false, firstSendBLDEdges = false;


        string value = "U2 L2 B2 R  U2 L' D2 U2 R  .  U  F  D' R' F  L2 D  B' L  U  L2 (20f)";
        string bldCornersValue = "C F U P C P (6f)";
        string bldEdgesValue = "P W F (3f)";

        // Kociemba
        Search search = new Search();
        private int maxDepth = 20;
        private int maxTime = 50;
        bool showLength = true;
        bool useSeparator = true;
        bool inverse = false;
        public static bool loop1 = true;

        string fileName = "m2p" + (Search.USE_TWIST_FLIP_PRUN ? "T" : "") + ".data";

        // BLD
        LettersAlgorithms blindfoldedSolution = new LettersAlgorithms();
        CubeArrays copiedArrays = new CubeArrays();

        //Initial
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

        string[] rgbFinal1 = new string[9];

        Char[] camera1ColorsArray = new Char[18];
        Char[] camera2ColorsArray = new Char[18];
        Char[] camera3ColorsArray = new Char[18];
        Char[] camera4ColorsArray = new Char[18];

        public static Char[] upFaceArray = new Char[9];
        public static Char[] downFaceArray = new Char[9];
        public static Char[] frontFaceArray = new Char[9];
        public static Char[] backFaceArray = new Char[9];
        public static Char[] leftFaceArray = new Char[9];
        public static Char[] rightFaceArray = new Char[9];

        string finalSolutionString = "";

        HSVColorRange colorsHSVRange = new HSVColorRange();
        // RgbHsv rgbToHsvConvert = new RgbHsv();
        public bool imageProcessingCheck = false;
        public bool solutionOk = false;
        public bool solutionsSend = false;
        public bool gripedUn = false;



        public Form2()
        {
            InitializeComponent();
            // serial connection
            DisableControls();
            getAvailableComPorts();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
            //Kociemba
            try
            {
                BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
                Tools.InitFrom(br);
                br.Close();
            }
            catch (FileNotFoundException)
            {
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                Environment.Exit(1);
            }
            if (!Search.IsInited())
            {
                Search.Init();
                try
                {
                    BinaryWriter bw = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
                    Tools.SaveTo(bw);
                    bw.Close();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                    Environment.Exit(1);
                }
            }
            //
        }

        private void SolveCube_Click(object sender, EventArgs e)
        {
            SolveCubeMethod();
        }

        // Serial Connection functions

        // Port Read
        private void SerialPortRead()
        {
            port.DataReceived += getInformationFromArduino;
        }

        private void getInformationFromArduino(object sender, SerialDataReceivedEventArgs e)
        {
            if (isConnected)
            {
                string line = port.ReadLine();
                BeginInvoke(new LineReceiverEvent(LineReceived), line);
            }
        }
        //check strings for errors
        string kociembaMoves = "0";
        string blindfoldedCornersMoves = "0";
        string blindfoldedEdgesMoves = "0";
        string kociembaFullString = "0";
        string blindfoldedCornersFullString = "0";
        string blindfoldedEdgesFullString = "0";
        string modifiedLine = "";
        string cubeSolvedArduino = "";
        //
        private delegate void LineReceiverEvent(string line);
        private void LineReceived(string line)
        {
            try
            {
                modifiedLine = line.Substring(1, 7);
                kociembaMoves = line.Substring(IndexOfBeginigChar(line, 'K'), LengthNumber(line, 'C', 'K'));
                blindfoldedCornersMoves = line.Substring(IndexOfBeginigChar(line, 'C'), LengthNumber(line, 'E', 'C'));
                blindfoldedEdgesMoves = line.Substring(IndexOfBeginigChar(line, 'E'), LengthNumber(line, 'k', 'E'));
                kociembaFullString = line.Substring(IndexOfBeginigChar(line, 'k'), LengthNumber(line, 'c', 'k'));
                blindfoldedCornersFullString = line.Substring(IndexOfBeginigChar(line, 'c'), LengthNumber(line, 'e', 'c'));
                blindfoldedEdgesFullString = line.Substring(IndexOfBeginigChar(line, 'e'), LengthNumber(line, 'D', 'e'));
                cubeSolvedArduino = line.Substring(IndexOfBeginigChar(line, 'D'), LengthNumber(line, 'X', 'D'));
            }
            catch (Exception)
            {
                // throw;
            }
            if (firstSendKociemba == true || firstSendBLDCorners == true || firstSendBLDEdges == true)
            {
                incomingMessagesTB.AppendText("K:[" + kociembaCountMoves + "|" + kociembaCountFullString + "]-[" + kociembaMoves + "|" + kociembaFullString + "]" +
                    "BC:["+ bldCornersCountFullString + "]-["+ blindfoldedCornersFullString + "]" +
                    "BE:["+bldEdgesCountFullString + "]-["+ blindfoldedEdgesFullString + "]\r\n");
            }

            if (firstSendKociemba == true && kociembaCountFullString!=0)
            {
                if ((kociembaCountFullString != Int32.Parse(kociembaFullString)))
                {
                    SendKociembaStringToArduino();
                }
                else
                {
                    Console.WriteLine("Egale :) ");
                    SendBLDCornersStringToArduino();
                    firstSendKociemba = false;
                }
            }

            if (firstSendBLDCorners == true)
            {
                if ((bldCornersCountFullString != Int32.Parse(blindfoldedCornersFullString)))
                {
                    SendBLDCornersStringToArduino();
                }
                else
                {
                    Console.WriteLine("A trimis si bld Corners!");
                    SendBLDEdgesStringToArduino();
                    firstSendBLDCorners = false;

                }
            }

            if (firstSendBLDEdges == true)
            {
                if ((bldEdgesCountFullString != Int32.Parse(blindfoldedEdgesFullString)))
                {
                    SendBLDEdgesStringToArduino();
                }
                else
                {
                    Console.WriteLine("A trimis si bld Edges!");
                    firstSendBLDEdges = false;
                    LetterOk();
                }
            }


            if (cubeSolvedArduino.Equals("true"))
            {
                // resetAutomaticSendScramble();
                Console.WriteLine("True? : " + cubeSolvedArduino);
            }

        }
        // 
        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void EnableControls()
        {
            resetBTN.Enabled = true;
            solveUsingKociembaBTN.Enabled = true;
            solveUsingBLD.Enabled = true;
            scrambleTheCubeBTN.Enabled = true;
            randomScrambleBTN.Enabled = true;
            gripTheCubeBTN.Enabled = true;
            releaseTheCubeBTN.Enabled = true;
            manualRB.Enabled = true;
            semiAutomaticRB.Enabled = true;
            automaticRB.Enabled = true;
            motorSpeedTrackBar.Enabled = true;
        }

        private void DisableControls()
        {
            resetBTN.Enabled = false;
            solveUsingKociembaBTN.Enabled = false;
            solveUsingBLD.Enabled = false;
            scrambleTheCubeBTN.Enabled = false;
            randomScrambleBTN.Enabled = false;
            gripTheCubeBTN.Enabled = false;
            releaseTheCubeBTN.Enabled = false;
            manualRB.Enabled = false;
            semiAutomaticRB.Enabled = false;
            automaticRB.Enabled = false;
            motorSpeedTrackBar.Enabled = false;
        }

        private void ResetDefaults()
        {
            
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                ConnectToArduino();
            }
            else
            {
                DisconnectFromArduino();
            }
        }

        private void ConnectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#STAR\n");
            port.Write("#STAR\n");
            Connect.Text = "Disconnect";
            EnableControls();
            SerialPortRead();
        }

        private void DisconnectFromArduino()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            Connect.Text = "Connect";
            DisableControls();
            ResetDefaults();
        }

        //sendind automatic, strings to arduino
        private void sendKB_Click(object sender, EventArgs e)
        {
            SendKociembaStringToArduino();
        }

        private void SendKociembaStringToArduino()
        {
            kociembaCountMoves = NumberOfMoves(kociembaFinalStringSolution);
            kociembaCountFullString = SolutionReplace(kociembaFinalStringSolution).Length;

            if (isConnected)
            {
                if (kociembaCountMoves == 1)
                {
                    kociembaCountFullString++;
                    port.Write("#KMBA" + SolutionReplace(kociembaFinalStringSolution) + " #\n");
                }
                else
                {
                    port.Write("#KMBA" + SolutionReplace(kociembaFinalStringSolution) + "#\n");
                }
            }
            firstSendKociemba = true;
        }
        private void SendBLDCornersStringToArduino()
        {
            Console.WriteLine("BLD Corners sent!");
            bldCornersCountMoves = blindfoldedSolution.totalLettersCorners;
            bldCornersCountFullString = ReplaceWhiteSpaces(blindfoldedSolution.lettersCorners).Length;
            if (isConnected)
            {
                port.Write("#BLDC" + ReplaceWhiteSpaces(blindfoldedSolution.lettersCorners) + "#\n");
                Console.WriteLine("--#BLDC" + ReplaceWhiteSpaces(blindfoldedSolution.lettersCorners) + "#\n");
                Console.WriteLine("--Count moves:  " + bldCornersCountMoves);
            }
            firstSendBLDCorners = true;
        }
        private void SendBLDEdgesStringToArduino()
        {
            Console.WriteLine("BLD Edges sent!");
            bldEdgesCountMoves = blindfoldedSolution.totalLettersEdges;
            bldEdgesCountFullString = ReplaceWhiteSpaces(blindfoldedSolution.lettersEdges).Length;
            if (isConnected)
            {
                port.Write("#BLDE" + ReplaceWhiteSpaces(blindfoldedSolution.lettersEdges) + "#\n");
                Console.WriteLine("----#BLDE" + ReplaceWhiteSpaces(blindfoldedSolution.lettersEdges) + "#\n");
                Console.WriteLine("----Count moves:  " + bldEdgesCountMoves);
            }
            firstSendBLDEdges = true;
        }
        private void LetterOk()
        {
            if (isConnected)
            {
                port.Write("#KBDD#\n");
            }
        }
        // kociemba, blindfolded, scramble, random pattern
        private void solveUsingKociembaBTN_Click(object sender, EventArgs e)
        {
            
            if (isConnected)
            {
                port.Write("#SOLC" + "KMB" + "#\n");
            }
        }

        private void solveUsingBLD_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#SOLC" + "BLD" + "#\n");
            }
        }

        private void scrambleTheCubeBTN_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#SOLC" + "SCM" + "#\n");
            }
        }

        private void randomScrambleBTN_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#SOLC" + "RDM" + "#\n");
            }
        }
        //grip and release
        private void gripTheCubeBTN_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                gripedUn = true;
                port.Write("#GRPC" + "GRP" + "#\n");
                resetAutomaticSendScramble();
            }
        }

        private void releaseTheCubeBTN_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                gripedUn = false;
                port.Write("#GRPC" + "REL" + "#\n");
            }
        }

        //radio buttons
        private void manualRB_CheckedChanged(object sender, EventArgs e)
        {
            gripTheCubeBTN.Enabled = false;
            releaseTheCubeBTN.Enabled = false;

            if (isConnected && manualRB.Checked == true)
            {
                port.Write("#MODE" + "MAN" + "#\n");
            }
        }

        private void semiAutomaticRB_CheckedChanged(object sender, EventArgs e)
        {
            gripTheCubeBTN.Enabled = false;
            releaseTheCubeBTN.Enabled = true;

            if (isConnected && semiAutomaticRB.Checked == true)
            {
                port.Write("#MODE" + "SAU" + "#\n");
            }
        }

        private void automaticRB_CheckedChanged(object sender, EventArgs e)
        {
            gripTheCubeBTN.Enabled = true;
            releaseTheCubeBTN.Enabled = true;

            if (isConnected && automaticRB.Checked == true)
            {
                port.Write("#MODE" + "AUT" + "#\n");
            }
        }

        //kociemba methods
        private void SolveCubeMethod()
        {
            //string cubeString = "UDUDUDUDURLRLRLRLRFBFBFBFBFDUDUDUDUDLRLRLRLRLBFBFBFBFB";
            //string cubeString = "LRLDUBFUDRLBBRURFLLBBUFDBDURRFFDLFLDBFURLLDUUDFURBBFDR";

            //string cubeString = "URRDURURLFDDLRFRUFFBDFFDLBBDLUFDLLBDRFLBLUURBBUFLBDRUB";
            //string cubeString = Tools.RandomCube();

            // string cubeString = "RDFRULRDRBFURRUFDDULDBFUUUDLFRBDFLUFFFBLLRBDBLRUBBBLLD";
            string cubeString = finalSolutionString;
           // string cubeString = Kociemba.Tools.RandomCube();

            Console.WriteLine("Scramble: " + cubeString);

            int mask = 0;
            mask |= useSeparator ? Search.USE_SEPARATOR : 0;
            mask |= inverse ? Search.INVERSE_SOLUTION : 0;
            mask |= showLength ? Search.APPEND_LENGTH : 0;
            var t = Stopwatch.StartNew().Elapsed.TotalMilliseconds;
            Console.WriteLine("Timp inainte de solutie: " + t);
            string result = search.Solution(cubeString, maxDepth, 100, 0, mask);
            long n_probe = search.NumberOfProbes();
            var asdd = Stopwatch.StartNew().Elapsed.TotalMilliseconds - t;
            Console.WriteLine(asdd + "\n\r" + t);
            while (result.StartsWith("Error 8", StringComparison.Ordinal) && (Stopwatch.StartNew().Elapsed.TotalMilliseconds - t) < maxTime * 1.0e3)
            {
                result = search.Next(100, 0, mask);
                n_probe += search.NumberOfProbes();
            }

            t = Stopwatch.StartNew().Elapsed.TotalMilliseconds - t;
            Console.WriteLine(t);

            if (result.Contains("Error"))
            {
                switch (result[result.Length - 1])
                {
                    case '1':
                        result = "There are not exactly nine facelets of each color!";
                        break;
                    case '2':
                        result = "Not all 12 edges exist exactly once!";
                        break;
                    case '3':
                        result = "Flip error: One edge has to be flipped!";
                        break;
                    case '4':
                        result = "Not all 8 corners exist exactly once!";
                        break;
                    case '5':
                        result = "Twist error: One corner has to be twisted!";
                        break;
                    case '6':
                        result = "Parity error: Two corners or two edges have to be exchanged!";
                        break;
                    case '7':
                        result = "No solution exists for the given maximum move number!";
                        break;
                    case '8':
                        result = "Timeout, no solution found within given maximum time!";
                        break;
                }
                Console.WriteLine(result + " -|- " + Convert.ToString((t / 1000) / 1000.0) + " ms | " + " n_probe" + " probes");
            }
            else
            {
                kociembaFinalStringSolution = result;
                Console.WriteLine("totul e ok :)");
                BLDSolutionAndShowText();

            }
            Console.WriteLine("nlan: "+result);
            kociembaTextBox.Text = result;
            //kociembaFinalStringSolution = result;
        }
        //

        // BLD 
        private void bldSolution_Click(object sender, EventArgs e)
        {
            BLDSolutionAndShowText();
        }

        private void BLDSolutionAndShowText()
        {
            blindfoldedSolution.FinalSolutionCornersAndEdges(false);

            bldTextBox.Text = "Edges: " + blindfoldedSolution.lettersEdges + "\r\n Corners: " + blindfoldedSolution.lettersCorners + "\r\n";

            if (blindfoldedSolution.parityCheck == true)
            {
                bldTextBox.AppendText("Edges:\r\n" + blindfoldedSolution.finalSolutionEdges + "(" + blindfoldedSolution.totalEM + ")" + "\r\nParity: \r\n" + blindfoldedSolution.parity2m + "\r\nCorners:\r\n" + blindfoldedSolution.finalSolutionCorners + "(" + blindfoldedSolution.totalCM + ")" + "\r\nTotal moves: " + blindfoldedSolution.totalMovesBLD);
                blindfoldedFinalStringSolution = "-" + blindfoldedSolution.lettersEdges + "-P-" + blindfoldedSolution.lettersCorners + "-";
            }
            else
            {
                blindfoldedFinalStringSolution = "-" + blindfoldedSolution.lettersEdges + "-" + blindfoldedSolution.lettersCorners + "-";
                bldTextBox.AppendText("Edges:\r\n" + blindfoldedSolution.finalSolutionEdges + "(" + blindfoldedSolution.totalEM + ")" + "\r\nCorners:\r\n" + blindfoldedSolution.finalSolutionCorners + "(" + blindfoldedSolution.totalCM + ")" + "\r\nTotal moves: " + blindfoldedSolution.totalMovesBLD);
            }

            bldTextBox.AppendText("\r\n"+blindfoldedFinalStringSolution);
            bldTextBox.AppendText("\r\nEdges: -" + blindfoldedSolution.lettersEdges + "-" + blindfoldedSolution.totalLettersEdges + "-\r\n");
            bldTextBox.AppendText("\r\nCorners: -" + blindfoldedSolution.lettersCorners + "-" + blindfoldedSolution.totalLettersCorners + "-\r\n");

            if (solutionsSend == false)
            {
                SendKociembaStringToArduino();
                solutionsSend = true;
            }
        }
        //
        //random 
        void resetAutomaticSendScramble()
        {
            imageProcessingCheck = false;
            solutionOk = false;
            solutionsSend = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            automaticRB.Checked = true;
            InitializeTrackBars();

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
                //  Environment.Exit(0);
                // return;

            }

            try
            {
                Application.Idle += processFrameAndUpdateGUI;       // add process image function to the application's list of tasks
            }
            catch (Exception ex)
            {
                MessageBox.Show("processFrameAndUpdateGUI error: " + Environment.NewLine + Environment.NewLine +
                               ex.Message + Environment.NewLine + Environment.NewLine +
                               "exiting program");
                throw;
            }

        }
        
        private Color finalColor(string color)
        {
            int r = 0, g = 0, b = 0;

            switch (color)
            {
                case "red":
                    r = 204; g = 0; b = 0;
                    break;
                case "blue":
                    r = 0; g = 0; b = 153;
                    break;
                case "green":
                    r = 0; g = 102; b = 0;
                    break;
                case "white":
                    r = 255; g = 255; b = 255;
                    break;
                case "orange":
                    r = 255; g = 102; b = 0;
                    break;
                case "yellow":
                    r = 255; g = 204; b = 0;
                    break;
                default:
                    r = 0; g = 0; b = 0;
                    break;
            }

            return Color.FromArgb(r,g,b);

        }
        private bool automaticManualCameraError = false;

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
                //MessageBox.Show("unable to read from webcam" + Environment.NewLine + Environment.NewLine +"exiting program");
               // Environment.Exit(0);
              //  return;
              /*
              if (automaticManualCameraError == false)
                {
                    ManualDataAcquisition manualDataCube = new ManualDataAcquisition();
                    manualDataCube.Show();
                    Hide();
                    automaticManualCameraError = true;
                }
                */
                
            }
            else
            {
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



                ProduceThresholdImage();
                /*
                getHSVPixels(hsvImage1.SmoothMedian(7), arrayPointsCamera1);
                getHSVPixels(hsvImage2.SmoothMedian(7), arrayPointsCamera2);
                getHSVPixels(hsvImage3.SmoothMedian(7), arrayPointsCamera3);
                getHSVPixels(hsvImage4.SmoothMedian(7), arrayPointsCamera4);
                */

                getHSVPixels(hsvImage1, arrayPointsCamera1);
                getHSVPixels(hsvImage2, arrayPointsCamera2);
                getHSVPixels(hsvImage3, arrayPointsCamera3);
                getHSVPixels(hsvImage4, arrayPointsCamera4);
                //SaveColorsAndShow();

                drawCircleConvertedImage(hsvImage1, arrayPointsCamera1);
                drawCircleConvertedImage(hsvImage2, arrayPointsCamera2);
                drawCircleConvertedImage(hsvImage3, arrayPointsCamera3);
                drawCircleConvertedImage(hsvImage4, arrayPointsCamera4);

                DFwebcam.Image = hsvImage1;
                DBwebcam.Image = hsvImage2;
                UFwebcam.Image = hsvImage3;
                UBwebcam.Image = hsvImage4;

                DFwebcamFiltered.Image = camera1_Threshold;
                DBwebcamFiltered.Image = camera2_Threshold;
                UFwebcamFiltered.Image = camera3_Threshold;
                UBwebcamFiltered.Image = camera4_Threshold;

                SaveColorsAndShow();
                if (imageProcessingCheck == false && isConnected == true && gripedUn == true)
                {
                    CheckFinalArrays();  
                }  
            }  
        }

        
       
        private void drawCircleConvertedImage(Image<Hsv, Byte> cameraDraw, int[,] arrayPoints)
        {
            for (int i = 0; i < arrayPoints.GetLength(0); i++)
            {
                CvInvoke.Circle(cameraDraw, new Point(arrayPoints[i, 0], arrayPoints[i, 1]), 4, new MCvScalar(70, 255, 255), -1);
            }
        }

        string motorsSpeedToSend = "";

        private void motorSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            int motorsSpeed = motorSpeedTrackBar.Value + 159;
            motorSpeedLabel.Text = "Motors speed: " + motorsSpeed.ToString()+ " RPM";

            if (motorsSpeed < 210)
            {
                motorSpeedLabel.ForeColor = Color.Orange;
            } else if (motorsSpeed>=210 && motorsSpeed<=250)
            {
                motorSpeedLabel.ForeColor = Color.ForestGreen;
            }
            else
            {
                motorSpeedLabel.ForeColor = Color.Red;
            }
            motorsSpeedToSend = motorsSpeed.ToString();

        }

        private void sendMotorSpeed_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#SPDM" + motorsSpeedToSend + "#\n");
            }
        }

        private void InitializeTrackBars()
        {
            motorSpeedTrackBar.Value = 9;
            motorSpeedLabel.Text = "Motors speed: 250 RPM";
            motorSpeedLabel.ForeColor = Color.ForestGreen;

            trackHueLow.Value = 0;
            trackSatLow.Value = 0;
            trackValLow.Value = 0;
            trackHueHigh.Value = 180;
            trackSatHigh.Value = 255;
            trackValHigh.Value = 120;
        }

        private void ProduceThresholdImage()
        {
            int HueLow = trackHueLow.Value;
            int SatLow = trackSatLow.Value;
            int ValLow = trackValLow.Value;
            int HueHigh = trackHueHigh.Value;
            int SatHigh = trackSatHigh.Value;
            int ValHigh = trackValHigh.Value;

            lblHueLow.Text = "Low Hue = " + HueLow.ToString() + "°";
            lblHueHigh.Text = "High Hue = " + HueHigh.ToString() + "°";
            lblSatLow.Text = "Low Sat = " + SatLow.ToString();
            lblSatHigh.Text = "High Sat = " + SatHigh.ToString();
            lblValLow.Text = "Low Val = " + ValLow.ToString();
            lblValHigh.Text = "High Val = " + ValHigh.ToString();

            camera1_Threshold = hsvImage1.InRange(new Hsv(HueLow,SatLow,ValLow), new Hsv(HueHigh,SatHigh,ValHigh));
            camera2_Threshold = hsvImage2.InRange(new Hsv(HueLow, SatLow, ValLow), new Hsv(HueHigh, SatHigh, ValHigh));
            camera3_Threshold = hsvImage3.InRange(new Hsv(HueLow, SatLow, ValLow), new Hsv(HueHigh, SatHigh, ValHigh));
            camera4_Threshold = hsvImage4.InRange(new Hsv(HueLow, SatLow, ValLow), new Hsv(HueHigh, SatHigh, ValHigh));
        }

        private void trackHueLow_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void trackHueHigh_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void trackSatLow_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void trackSatHigh_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void trackValLow_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void trackValHigh_Scroll(object sender, EventArgs e)
        {
            ProduceThresholdImage();
        }

        private void SaveColorsAndShow()
        {
            saveCameraColorsToArray(huePixels, saturationPixels, valuePixels);

        }

        int[,] arrayPointsCamera1 = new int[,] {
            //{245,66}, {245,132}, {244,209}, {181,226}, {130,251}, {200,169},
            //{302,67}, {307,140}, {303,211}, {354,236}, {395,269}, {341,178},
            //{152,300}, {207,282}, {272,260}, {322,285}, {362,312}, {265,298}
            {247,69}, {247,128}, {236,215}, {183,228}, {128,248}, {199,169}, 
            {302,69}, {299,130}, {305,209}, {351,234}, {395,256}, {340,175}, 
            {157,302}, {206,278}, {264,254}, {315,278}, {365,304}, {260,294}

        };

        int[,] arrayPointsCamera2 = new int[,] {
            //{310,100}, {312,154}, {299,224}, {259,236}, {215,250}, {270,182}, 
            //{353,102}, {354,154}, {361,219}, {402,240}, {434,257}, {390,188}, 
            //{236,289}, {284,272}, {331,255}, {375,278}, {415,295}, {325,286}

            {306,98}, {307,149}, {301,208}, {255,230}, {209,252}, {269,181}, 
            {353,97}, {353,153}, {354,217}, {394,231}, {436,253}, {386,183}, 
            {234,289}, {279,271}, {325,251}, {371,272}, {410,290}, {325,283}

        };

        int[,] arrayPointsCamera3 = new int[,]{
            //{292,342}, {293,278}, {290,209}, {237,183}, {190,156}, {252,244},
            //{344,342}, {346,281}, {349,207}, {407,182}, {453,163}, {389,244}, 
            //{216,113}, {264,138}, {322,161}, {374,139}, {430,117}, {325,121}

            {292,345}, {295,286}, {294,213}, {240,184}, {193,158}, {254,244}, 
            {343,348}, {351,282}, {354,219}, {412,190}, {459,168}, {394,247}, 
            {221,114}, {273,138}, {325,165}, {388,139}, {437,117}, {330,124}

        };

        int[,] arrayPointsCamera4 = new int[,]
        {
            //{269,339}, {267,279}, {263,213}, {211,180}, {169,161}, {228,243}, 
            //{324,344}, {325,283}, {327,209}, {382,187}, {429,166}, {367,246},
            //{196,116}, {240,137}, {291,159}, {353,137}, {417,120}, {300,123}

            {272,336}, {275,273}, {275,205}, {221,182}, {176,154}, {235,237},
            {328,340}, {331,278}, {335,204}, {388,177}, {435,158}, {376,237},
            {202,109}, {250,129}, {304,156}, {362,133}, {414,112}, {305,116}

        };


        private void drawCircle(Mat cameraDraw, int[,] arrayPoints)
        {
            for (int i=0; i< arrayPoints.GetLength(0); i++)
            {
                CvInvoke.Circle(cameraDraw, new Point(arrayPoints[i,0], arrayPoints[i, 1]), 4, new MCvScalar(0, 255, 0), -1);
            }
        }

        int[] bluePixel = new int[72];
        int[] greenPixel = new int[72];
        int[] redPixel = new int[72];

        int[] huePixels = new int[72];
        int[] saturationPixels = new int[72];
        int[] valuePixels = new int[72];

        private void getHSVPixels(Image<Hsv, Byte> imageUsed, int[,] arrayUsed)
        {
            int switchArrayMax = 0, switchArrayMin = 0;

            if (arrayUsed.Equals(arrayPointsCamera1))
            {
                switchArrayMin = 0;
                switchArrayMax = 18;
            }
            else if (arrayUsed.Equals(arrayPointsCamera2))
            {
                switchArrayMin = 18;
                switchArrayMax = 36;
            }
            else if (arrayUsed.Equals(arrayPointsCamera3))
            {
                switchArrayMin = 36;
                switchArrayMax = 54;
            }
            else if (arrayUsed.Equals(arrayPointsCamera4))
            {
                switchArrayMin = 54;
                switchArrayMax = 72;
            }

            for (int i = switchArrayMin; i < switchArrayMax; i++)
            {
                huePixels[i] = imageUsed.Data[arrayUsed[i - switchArrayMin, 1], arrayUsed[i - switchArrayMin, 0], 0];
                saturationPixels[i] = imageUsed.Data[arrayUsed[i - switchArrayMin, 1], arrayUsed[i - switchArrayMin, 0], 1];
                valuePixels[i] = imageUsed.Data[arrayUsed[i - switchArrayMin, 1], arrayUsed[i - switchArrayMin, 0], 2];
            }
        }

        private void saveCameraColorsToArray(int[] huePixel, int[] saturationPixel, int[] valuePixel)
        {
            for (int i = 0; i < 18; i++)
            {
                camera1ColorsArray[i] = colorsHSVRange.HSVRange(huePixel[i], saturationPixel[i], valuePixel[i]);
            }

            for (int i = 18; i < 36; i++)
            {
                camera2ColorsArray[i-18] = colorsHSVRange.HSVRange(huePixel[i], saturationPixel[i], valuePixel[i]);
            }

            for (int i = 36; i < 54; i++)
            {
                camera3ColorsArray[i - 36] = colorsHSVRange.HSVRange(huePixel[i], saturationPixel[i], valuePixel[i]);
            }

            for (int i = 54; i < 72; i++)
            {
                camera4ColorsArray[i - 54] = colorsHSVRange.HSVRange(huePixel[i], saturationPixel[i], valuePixel[i]);
            }

        }

        private void SolutionFinalString()
        {
            finalSolutionString = "";
            //Order of the faces: Up, Right, Front, Down, Left, Back
            Char[][] orderedFaces = new Char[6][] { upFaceArray, rightFaceArray, frontFaceArray, downFaceArray, leftFaceArray, backFaceArray};
            Char[] centerColor = new Char[6];
            Char[] facesChangedNotation = new Char[6] {'U','R','F','D','L','B'};

            for (int i = 0; i < orderedFaces.Length; i++)
            {
                centerColor[i] = orderedFaces[i][4];
            }

            for (int k = 0; k < centerColor.Length; k++)
            {
                for (int i = 0; i < orderedFaces.Length; i++)
                {
                    for (int j = 0; j < orderedFaces[i].Length; j++)
                    {
                        if (centerColor[k].Equals(orderedFaces[i][j]))
                        {
                            orderedFaces[i][j] = facesChangedNotation[k];
                        }
                    }
                }
            }
            
            for (int i = 0; i < orderedFaces.Length; i++)
            {
                for (int j = 0; j < orderedFaces[i].Length; j++)
                {
                    finalSolutionString += orderedFaces[i][j].ToString();
                }
            }            
        }

        private void CompareColorsAndSaveFinalStringArrays()
        {
            CompareColorsAndSaveToString(camera3ColorsArray, camera4ColorsArray, upFaceArray, "up");
            CompareColorsAndSaveToString(camera1ColorsArray, camera2ColorsArray, downFaceArray, "down");
            CompareColorsAndSaveToStringFrontBack(camera1ColorsArray, camera3ColorsArray, frontFaceArray);
            CompareColorsAndSaveToStringFrontBack(camera2ColorsArray, camera4ColorsArray, backFaceArray);
            CompareColorsAndSaveToStringLeftRight(camera2ColorsArray, camera3ColorsArray, leftFaceArray);
            CompareColorsAndSaveToStringLeftRight(camera1ColorsArray, camera4ColorsArray, rightFaceArray);
        }

        private void butonCam4_Click(object sender, EventArgs e)
        {
            CompareColorsAndSaveFinalStringArrays();
            SolutionFinalString();
        }

        

        private void CheckFinalArrays()
        {
            CompareColorsAndSaveFinalStringArrays();
            Char[][] orderedFaces = new Char[6][] { upFaceArray, rightFaceArray, frontFaceArray, downFaceArray, leftFaceArray, backFaceArray };
            Char[] centerColor = new Char[6];
            Char[] stickersFaceColors = new Char[6] { 'w', 'y', 'r', 'o', 'b', 'g' };
            int[] colorsCount = new int[6] { 0, 0, 0, 0, 0, 0 };
            int totalSum = 0;

            for (int k = 0; k < colorsCount.Length; k++)
            {
                for (int i = 0; i < orderedFaces.Length; i++)
                {
                    for (int j = 0; j < orderedFaces[i].Length; j++)
                    {
                        if (orderedFaces[i][j].Equals(stickersFaceColors[k]))
                        {
                            colorsCount[k]++;
                        }
                    }
                }
            }

            for (int i = 0; i < colorsCount.Length; i++)
            {
                if (colorsCount[i] == 9)
                {
                    totalSum++;
                }
            }

            if (solutionOk == false)
            {
                if (totalSum != 6)
                {
                }
                else
                {
                    imageProcessingCheck = true;
                    SolutionFinalString();
                    SolveCubeMethod();
                    solutionOk = true;
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void resetBTN_Click_1(object sender, EventArgs e)
        {
            resetAutomaticSendScramble();
        }

        private void switchButton_Click(object sender, EventArgs e)
        {
            //switchBoolManulaAutomatic.switchArrays = true;
            if (isConnected)
            {
                DisconnectFromArduino();
            }
            ManualDataAcquisition manualDataCube = new ManualDataAcquisition();
            manualDataCube.Show();
            Hide();
        }

        private void fullCamera_Click(object sender, EventArgs e)
        {
            PixelCalibration takenewPixels = new PixelCalibration();
            takenewPixels.Show();
            Hide();
        }

        private void checkSameSticker(Char[] camOne, Char[] camTwo, Char[] charData, int camOneIndex, int camTwoIndex, int charDataIndex)
        {
            if (camOne[camOneIndex].Equals(camTwo[camTwoIndex]))
            {
                charData[charDataIndex] = camOne[camOneIndex];
            }
            else
            {
                if (!camOne[camOneIndex].Equals('i'))
                {
                    charData[charDataIndex] = camOne[camOneIndex];
                }
                else if (!camTwo[camTwoIndex].Equals('i'))
                {
                    charData[charDataIndex] = camTwo[camTwoIndex];
                }
                else
                {
                    charData[charDataIndex] = 'N';
                }
            }
        }


        private void CompareColorsAndSaveToString(Char[] cameraOne, Char[] cameraTwo, Char[] savedData, string face)
        {

            checkSameSticker(cameraOne, cameraTwo, savedData, 17, 17, 4);
            checkSameSticker(cameraOne, cameraTwo, savedData, 16, 12, 8);
            checkSameSticker(cameraOne, cameraTwo, savedData, 12, 16, 0);

            if (face.Equals("up"))
            {
                savedData[1] = cameraTwo[15];
                savedData[2] = cameraTwo[14];
                savedData[3] = cameraOne[13];
                savedData[5] = cameraTwo[13];
                savedData[6] = cameraOne[14];
                savedData[7] = cameraOne[15];
            } else if (face.Equals("down"))
            {
                savedData[1] = cameraOne[13];
                savedData[2] = cameraOne[14];
                savedData[3] = cameraTwo[15];
                savedData[5] = cameraOne[15];
                savedData[6] = cameraTwo[14];
                savedData[7] = cameraTwo[13];
            }
            
        }

        private void CompareColorsAndSaveToStringFrontBack(Char[] cameraOne, Char[] cameraTwo, Char[] savedData)
        {
            checkSameSticker(cameraOne, cameraTwo, savedData, 5, 11, 4);
            checkSameSticker(cameraOne, cameraTwo, savedData, 4, 6, 6);
            checkSameSticker(cameraOne, cameraTwo, savedData, 0, 10, 2);

            savedData[0] = cameraTwo[8];
            savedData[1] = cameraTwo[9];
            savedData[3] = cameraTwo[7];
            savedData[5] = cameraOne[1];
            savedData[7] = cameraOne[3];
            savedData[8] = cameraOne[2];
        }

        private void CompareColorsAndSaveToStringLeftRight(Char[] cameraOne, Char[] cameraTwo, Char[] savedData)
        {
            checkSameSticker(cameraOne, cameraTwo, savedData, 11, 5, 4);
            checkSameSticker(cameraOne, cameraTwo, savedData, 10, 0, 8);
            checkSameSticker(cameraOne, cameraTwo, savedData, 6, 4, 0);

            savedData[1] = cameraTwo[3];
            savedData[2] = cameraTwo[2];
            savedData[3] = cameraOne[7];
            savedData[5] = cameraTwo[1];
            savedData[6] = cameraOne[8];
            savedData[7] = cameraOne[9];
        }


        //regexFunction
        static string SolutionReplace(string value)
        {
            value = Regex.Replace(value, @"\.+", "");
            value = Regex.Replace(value, @"\s\([^\)]+\)", "");
            value = Regex.Replace(value, @"\s+", " ");
            if (value.Substring(0, 1).Equals(" "))
            {
                value = value.Remove(0, 1);
            }

            return value;
        }

        static int NumberOfMoves(string value)
        {
            int moves;
            value = Regex.Match(value, @"(?<=\().+?(?=\))").Value;
            value = Regex.Match(value, @"\d+").Value;

            if (Int32.TryParse(value, out moves))
            {
                Console.WriteLine("Succes: " + moves);
            }
            else
            {
                Console.WriteLine("Fail!");
            }

            return moves;
        }

        private int IndexOfBeginigChar(string stringToSearch, char x)
        {
            int index;
            return index = stringToSearch.IndexOf(x) + 1;
        }
        private int LengthNumber(string stringToSearch, char x, char y)
        {
            int index;
            return index = stringToSearch.IndexOf(x) - stringToSearch.IndexOf(y) - 1;
        }

        private string ReplaceWhiteSpaces(string incomingString)
        {
            return incomingString.Replace(" ", String.Empty);
        }
        //end of regex

        private void showSolutions_Click(object sender, EventArgs e)
        {
            //  kociembaSolution.Text = SolutionReplace(kociembaFinalStringSolution) + "\r\n" + NumberOfMoves(kociembaFinalStringSolution)+ "\r\nString length: " + SolutionReplace(kociembaFinalStringSolution).Length.ToString();
            //   blindfoldedStringSolution.Text = SolutionReplace(blindfoldedFinalStringSolution) + "\r\n" + NumberOfMoves(blindfoldedFinalStringSolution) + "\r\nString length: " + blindfoldedFinalStringSolution.Length.ToString();
            kociembaTextBox.Text = SolutionReplace(value) + "\r\nMoves: " + NumberOfMoves(value) + " Length: " + SolutionReplace(value).Length;
            bldTextBox.Text = SolutionReplace(bldCornersValue) + "\r\nMoves: " + NumberOfMoves(bldCornersValue) + " Length: " + SolutionReplace(bldCornersValue).Length
                         +"\r\n"+ SolutionReplace(bldEdgesValue) + "\r\nMoves: " + NumberOfMoves(bldEdgesValue) + " Length: " + SolutionReplace(bldEdgesValue).Length;
            Console.WriteLine("Replaced String"+ReplaceWhiteSpaces(blindfoldedSolution.lettersEdges));
        }

    }
}
