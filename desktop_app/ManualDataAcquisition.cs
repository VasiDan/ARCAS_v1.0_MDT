using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Textexemplu2.Kociemba;
using Textexemplu2.Blindfolded;

namespace Textexemplu2
{
    public partial class ManualDataAcquisition : Form
    {
        private string blindfoldedFinalStringSolution;
        private string kociembaFinalStringSolution;

        // serialConnection and regex
        bool isConnected = false;
        String[] ports;
        SerialPort port;

        int kociembaCountMoves, bldCornersCountMoves, bldEdgesCountMoves, kociembaCountFullString, bldCornersCountFullString, bldEdgesCountFullString;
        bool firstSendKociemba = false, firstSendBLDCorners = false, firstSendBLDEdges = false;


        string value = "U2 L2 B2 R  U2 L' D2 U2 R  .  U  F  D' R' F  L2 D  B' L  U  L2 (20f)";
        string bldvalue = "U2 R2 F' R2 F L2 R2 B' R2 B U' L2 U L2 R2 D' L2 D L D' L' D L2 R2 U' L U L' R' U R U' L2 R2 D R' D' R D' L D L2 R2 U' L' U U' L' U L2 R2 D' L D L' R F2 L' R U2 L' R B2 L' R D2 D R' D' L2 R2 U R U' U R2 U' L2 R2 D R2 D' D R' D' L2 R2 U R U' F' D R U' R' U' R U R' F' R U R' U' R' F R D' F F2 R' R U' R' U' R U R' F' R U R' U' R' F R R F2 R' F R U' R' U' R U R' F' R U R' U' R' F R F' R D' R U' R' U' R U R' F' R U R' U' R' F R D F R U' R' U' R U R' F' R U R' U' R' F R F' D F' R U' R' U' R U R' F' R U R' U' R' F R F D'  R U' R' U' R U R' F' R U R' U' R' F R  D' F' R U' R' U' R U R' F' R U R' U' R' F R F D (234f)";
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
        private bool checkTheColorsOfTheCube = false;

        //
        static String frontFaceString, backFaceString, leftFaceString, rightFaceString, upFaceString, downFaceString;

        public static Char[] upFaceArray = new Char[9];
        public static Char[] downFaceArray = new Char[9];
        public static Char[] frontFaceArray = new Char[9];
        public static Char[] backFaceArray = new Char[9] ;
        public static Char[] leftFaceArray = new Char[9];
        public static Char[] rightFaceArray = new Char[9];

        // array to save on solve
        public static Char[] upFaceArray1 = new Char[9];
        public static Char[] downFaceArray1 = new Char[9] ;
        public static Char[] frontFaceArray1 = new Char[9];
        public static Char[] backFaceArray1 = new Char[9] ;
        public static Char[] leftFaceArray1 = new Char[9] ;
        public static Char[] rightFaceArray1 = new Char[9];
        Char[][] orderedFaces1 = new Char[6][] { upFaceArray1, rightFaceArray1, frontFaceArray1, downFaceArray1, leftFaceArray1, backFaceArray1 };

        //Order of the faces: Up, Right, Front, Down, Left, Back
        Char[][] orderedFaces = new Char[6][] { upFaceArray, rightFaceArray, frontFaceArray, downFaceArray, leftFaceArray, backFaceArray};
        String[] faces = new String[6] { upFaceString, rightFaceString, frontFaceString, downFaceString, leftFaceString, backFaceString};
        string finalSolutionString = "";

        public bool solutionOk = false;
        public bool solutionsSend = false;

        public ManualDataAcquisition()
        {
            InitializeComponent();
            chosenColor.BackColor = Color.White;
            button3.Enabled = false;
            automaticRB.Checked = true;
            motorSpeedTrackBar.Value = 9;
            motorSpeedLabel.Text = "Motors speed: 250 RPM";
            motorSpeedLabel.ForeColor = Color.ForestGreen;

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
                MessageBox.Show("Error: "+ result);
            }
            else
            {
                kociembaFinalStringSolution = result;
                kociembaTextBox.Text = result;
                Console.WriteLine("totul e ok :)");
                BLDSolutionAndShowText();
            }          
            //kociembaFinalStringSolution = result;
        }
        //

        // BLD 
        private void BLDSolutionAndShowText()
        {
            blindfoldedSolution.FinalSolutionCornersAndEdges(true);

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

            bldTextBox.AppendText("\r\n" + blindfoldedFinalStringSolution);
            bldTextBox.AppendText("\r\nEdges: -" + blindfoldedSolution.lettersEdges + "-" + blindfoldedSolution.totalLettersEdges + "-\r\n");
            bldTextBox.AppendText("\r\nCorners: -" + blindfoldedSolution.lettersCorners + "-" + blindfoldedSolution.totalLettersCorners + "-\r\n");

            if (solutionsSend == false)
            {
                SendKociembaStringToArduino();
                solutionsSend = true;
            }
        }
        //

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
                    "BC:[" + bldCornersCountFullString + "]-[" + blindfoldedCornersFullString + "]" +
                    "BE:[" + bldEdgesCountFullString + "]-[" + blindfoldedEdgesFullString + "]\r\n");
            }

            if (firstSendKociemba == true)
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

        private void resetDefaults()
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
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);           port.Open();
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
            resetDefaults();
        }

        // send kociemba, bld
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
                port.Write("#GRPC" + "GRP" + "#\n");
            }
        }

        private void releaseTheCubeBTN_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
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

        private void motorSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            int motorsSpeed = motorSpeedTrackBar.Value + 160;
            motorSpeedLabel.Text = "Motors speed: " + motorsSpeed.ToString() + " RPM";

            if (motorsSpeed < 210)
            {
                motorSpeedLabel.ForeColor = Color.Orange;
            }
            else if (motorsSpeed >= 210 && motorsSpeed <= 250)
            {
                motorSpeedLabel.ForeColor = Color.ForestGreen;
            }
            else
            {
                motorSpeedLabel.ForeColor = Color.Red;
            }

            if (isConnected)
            {
                port.Write("#SPDM" + motorsSpeed.ToString() + "#\n");
            }
        }

        // rest button
        void resetAutomaticSendScramble()
        {
            solutionOk = false;
            solutionsSend = false;
        }

        private void resetBTN_Click_1(object sender, EventArgs e)
        {
            //clear all
            ClearAllFacesAndArrays();
            //
            resetAutomaticSendScramble();
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

        private void SolveCube_Click(object sender, EventArgs e)
        {
            SolveCubeMethod();   
        }


        private char pickedColor = 'w';

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectFromArduino();
            }
            Form2 automaticDataCube = new Form2();
            Close();
            automaticDataCube.Show();
            
        }

        private void transparentButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'n';
            chosenColor.BackColor = Color.Transparent;
        }

        private void redButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'r';
            chosenColor.BackColor = Color.Red;
        }

        private void orangeButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'o';
            chosenColor.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void blueButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'b';
            chosenColor.BackColor = Color.FromArgb(3, 146, 255);
        }

        private void greenButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'g';
            chosenColor.BackColor = Color.FromArgb(73, 226, 93);
        }

        private void whiteButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'w';
            chosenColor.BackColor = Color.White;
        }

        private void yellowButton_Click(object sender, EventArgs e)
        {
            pickedColor = 'y';
            chosenColor.BackColor = Color.FromArgb(247, 255, 0);
        }

        private void front1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front1, 2,0);
        }

        private void front2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front2, 2,1);
        }

        private void front3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front3, 2,2);
        }

        private void front4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front4, 2,3);  
        }

        private void front5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front5, 2,4);            
        }

        private void front6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front6, 2,5); 
        }

        private void front7_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front7, 2,6);
        }

        private void front8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front8, 2,7);
        }

        private void front9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, front9, 2,8);
        }

        private void right1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right1, 1,0);
        }

        private void right2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right2, 1,1); 
        }

        private void right3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right3, 1,2); 
        }

        private void right4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right4, 1,3); 
        }

        private void right5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right5, 1,4);  
        }

        private void right6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right6, 1,5);   
        }

        private void right7_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right7, 1,6);  
        }
        private void right8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right8, 1,7);
        }

        private void right9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, right9, 1,8);
        }

        private void left1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left1, 4,0); 
        }

        private void left2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left2, 4,1);
        }

        private void left3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left3, 4,2);      
        }

        private void left4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left4, 4,3);
        }

        private void left5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left5, 4,4);  
        }

        private void left6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left6, 4,5);
        }

        private void left7_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left7, 4,6);   
        }
        private void left8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left8, 4,7);
        }

        private void left9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, left9, 4,8); 
        }

        private void back1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back1, 5,0);  
        }

        private void back2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back2, 5,1);
        }

        private void back3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back3, 5,2); 
        }

        private void back4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back4, 5,3);
        }

        private void back5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back5, 5,4);            
        }

        private void back6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back6, 5,5);            
        }

        private void back7_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back7, 5,6);
        }
        private void back8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back8, 5,7);            
        }

        private void back9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, back9, 5,8);           
        }

        private void up1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up1, 0,0);            
        }

        private void up2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up2, 0 , 1);           
        }

        private void up3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up3, 0,2);
            
        }

        private void up4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up4, 0,3);
            
        }

        private void up5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up5, 0,4);
           
        }

        private void up6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up6, 0,5);
            
        }

        private void up7_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up7, 0,6);
           
        }
        private void up8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up8, 0,7);
            
        }

        private void up9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, up9, 0,8);
           
        }

        private void down1_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down1, 3,0);
        }

        private void down2_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down2, 3,1);
        }

        private void down3_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down3, 3,2);
        }

        private void down4_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down4, 3,3);
        }

        private void down5_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down5, 3,4);
        }

        private void down6_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down6, 3,5);
        }

        private void down7_Click(object sender, EventArgs e)
        {
           ChangeLabelsNumber(pickedColor, down7, 3,6);
        }

        private void down8_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down8, 3,7);
        }

        private void down9_Click(object sender, EventArgs e)
        {
            ChangeLabelsNumber(pickedColor, down9, 3,8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update arrays
            colorToArrayAndStrings();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //clear all
            ClearAllFacesAndArrays();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArraysToString();
            SolveCubeMethod();
        }

        Boolean countCheck = true;

        private void ChangeLabelsNumber(Char labelColor, Button buttonColorToCheck, int arrayFace, int faceNumber)
        {

            int[] labelsArraySum = new int[6] { 9,9,9,9,9,9 };
            int[] labelsArrayDif = new int[6] { 0, 0, 0, 0, 0, 0};

            Button[] cubeStickerButtons = new Button[54] {
                up1,up2,up3,up4,up5,up6,up7,up8,up9,
                right1,right2,right3,right4,right5,right6,right7,right8,right9,
                front1,front2,front3,front4,front5,front6,front7,front8,front9,
                down1,down2,down3,down4,down5,down6,down7,down8,down9,
                left1,left2,left3,left4,left5,left6,left7,left8,left9,
                back1,back2,back3,back4,back5,back6,back7,back8,back9};

            Label[] allLabels = new Label[7] { redLabel, orangeLabel, blueLabel,greenLabel,whiteLabel,yellowLabel,transparentLabel};            
            int currentLabel = 0;
            String curentStikerColor = "";

            // CubeBackroundColorToChar(buttonColorToCheck.BackColor);
            
            if (!buttonColorToCheck.BackColor.Equals(chosenColor.BackColor))
            {
                switch (labelColor)
                {
                    case 'w':
                        currentLabel = 4;
                        break;
                    case 'y':
                        currentLabel = 5;
                        break;
                    case 'r':
                        currentLabel = 0;
                        break;
                    case 'o':
                        currentLabel = 1;
                        break;
                    case 'b':
                        currentLabel = 2;
                        break;
                    case 'g':
                        currentLabel = 3;
                        break;
                    case 'n':
                        currentLabel = 6;
                        break;
                    default:
                        break;
                }

            }
            if (!buttonColorToCheck.BackColor.Equals(chosenColor.BackColor))
            {
                if (Int32.Parse(allLabels[currentLabel].Text) > 0 && allLabels[currentLabel]!=transparentLabel)
                {
                    buttonColorToCheck.BackColor = chosenColor.BackColor;
                } else if (allLabels[currentLabel] == transparentLabel)
                {
                    buttonColorToCheck.BackColor = chosenColor.BackColor;
                }
                

            }

            for (int j = 0; j < cubeStickerButtons.Length; j++)
            {
                if (cubeStickerButtons[j].BackColor.Equals(Color.Red))
                {
                    
                    labelsArrayDif[0]++;
                    labelsArraySum[0]=9-labelsArrayDif[0];
                    redLabel.Text = (9- labelsArrayDif[0]).ToString();
                }
                else if (cubeStickerButtons[j].BackColor.Equals(Color.FromArgb(255, 128, 0)))
                {
                    labelsArrayDif[1]++;
                    labelsArraySum[1] = 9 - labelsArrayDif[1];
                    orangeLabel.Text = (9 - labelsArrayDif[1]).ToString();
                }
                else if (cubeStickerButtons[j].BackColor.Equals(Color.FromArgb(3, 146, 255)))
                {
                    labelsArrayDif[2]++;
                    labelsArraySum[2] = 9 - labelsArrayDif[2];
                    blueLabel.Text = (9 - labelsArrayDif[2]).ToString();
                }
                else if (cubeStickerButtons[j].BackColor.Equals(Color.FromArgb(73, 226, 93)))
                {
                    labelsArrayDif[3]++;
                    labelsArraySum[3] = 9 - labelsArrayDif[3];
                    greenLabel.Text = (9 - labelsArrayDif[3]).ToString();
                }
                else if (cubeStickerButtons[j].BackColor.Equals(Color.White))
                {
                    labelsArrayDif[4]++;
                    labelsArraySum[4] = 9 - labelsArrayDif[4];
                    whiteLabel.Text = (9 - labelsArrayDif[4]).ToString();
                }
                else if (cubeStickerButtons[j].BackColor.Equals(Color.FromArgb(247, 255, 0)))
                {
                    labelsArrayDif[5]++;
                    labelsArraySum[5] = 9 - labelsArrayDif[5];
                    yellowLabel.Text = (9 - labelsArrayDif[5]).ToString();
                }
            }

            int sumDif = 0;

            for (int i = 0; i < 6; i++)
            {
                sumDif += labelsArrayDif[i];
                if (allLabels[i].Text.Equals("8") && labelsArraySum[i] == 9)
                {
                    allLabels[i].Text = "9";
                }


               if (labelsArraySum[i].Equals(0))
                {
                    allLabels[i].ForeColor=Color.Red;
                } else
                {
                    allLabels[i].ForeColor = Color.Black;
                }
            }

            transparentLabel.Text = (54 - sumDif).ToString();

            if (transparentLabel.Text.Equals("0"))
            {
                button3.Enabled = true;
            } else
            {
                button3.Enabled = false;
            }

            orderedFaces1[arrayFace][faceNumber] = labelColor;

        }

        private void colorToArrayAndStrings()
        {
            //Order of the faces: Up, Right, Front, Down, Left, Back

            for (int i = 0; i < orderedFaces1.Length; i++)
            {
                faces[i] = "";
                for (int j = 0; j < orderedFaces1[i].Length; j++)
                {
                    faces[i] += orderedFaces1[i][j] + (j == (orderedFaces1[i].Length - 1) ? "" : ",");
                }
            }
            
        }


        private void ClearAllFacesAndArrays()
        {
            Label[] allLabels = new Label[7] { redLabel, orangeLabel, blueLabel, greenLabel, whiteLabel, yellowLabel, transparentLabel};
            Button[] cubeStickerButtons = new Button[54] {
                up1,up2,up3,up4,up5,up6,up7,up8,up9,
                right1,right2,right3,right4,right5,right6,right7,right8,right9,
                front1,front2,front3,front4,front5,front6,front7,front8,front9,
                down1,down2,down3,down4,down5,down6,down7,down8,down9,
                left1,left2,left3,left4,left5,left6,left7,left8,left9,
                back1,back2,back3,back4,back5,back6,back7,back8,back9};


            for (int i = 0; i < cubeStickerButtons.Length; i++)
            {
                cubeStickerButtons[i].BackColor = Color.Transparent;
            }

            for (int i = 0; i < orderedFaces1.Length; i++)
            {
                for (int j = 0; j < orderedFaces1[i].Length; j++)
                {
                    orderedFaces1[i][j] = 'n';
                }
            }

            for (int k = 0; k < faces.Length; k++)
            {
                faces[k] = "";
                allLabels[k].Text = "9";
                allLabels[k].ForeColor = Color.Black;
            }
            allLabels[6].Text = "54";
        }



        private void ArraysToString()
        {
            finalSolutionString = "";
            //Order of the faces: Up, Right, Front, Down, Left, Back
            Char[] centerColor = new Char[6];
            Char[] facesChangedNotation = new Char[6] { 'U', 'R', 'F', 'D', 'L', 'B' };

            for (int i = 0; i < orderedFaces.Length; i++)
            {
                for (int j = 0; j < orderedFaces[i].Length; j++)
                {
                    orderedFaces[i][j] = orderedFaces1[i][j];
                }
            }

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


    }
}
