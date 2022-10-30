using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Blindfolded
{
    // Sus: (AER), (BQN), (CMJ), (DIF)
    // Jos: (LUG), (HXS), (TWO), (PVK)
    class Corners
    {
        public bool switchCubeArrays;
        public int[] countedCornerss = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public string[] newBufferString = new string[8] { "ULB", "UBR", "URF", "UFL", "FDL", "RDF", "BDR", "LDB" };

        string[,] changedBuffer = new string[8, 3]
        {
            { "ULB","LBU","BUL"},
            { "UBR","BRU","RUB"},
            { "URF","RFU","FUR"},
            { "UFL","FLU","LUF"},
            { "FDL","DLF","LFD"},
            { "RDF","DFR","FRD"},
            { "BDR","DRB","RBD"},
            { "LDB","DBL","BLD"}
        };

        string[,] cornersSolution = new string[8, 3]
        {
            {"A","E","R" },{"B","Q","N" },{"C","M","J" },{"D","I","F" },
            {"L","U","G" },{"P","V","K" },{"T","W","O" },{"H","X","S" }
        };

        int[,] changedBufferInt = new int[8, 3]
        {
            { 0,0,0},{ 0,0,0},{ 0,0,0},{ 0,0,0},
            { 0,0,0},{ 0,0,0},{ 0,0,0},{ 0,0,0}
        };

        public List<string> solutionCornersList = new List<string>();

        bool firstCycle = false;

        public string solution = "";
        public string solution2 = "";
        public int countedEdgeSum = 0;

        public string newBuffer = "";
        public string checkCorner = "";
        string expectedCorner = "";

        public string cornerCycle = "";
        public bool firstTime = true;

        public void PassCorner(int corner)
        {
            countedCornerss[corner] += 1;
        }     

        public void ExpectedCornerOrientation(string buffer)
        {
            var arrays = new CubeArrays();
            arrays.switchArraysForm(switchCubeArrays);

            switch (buffer)
            {
                // colt ULB <=> AER
                case "ULB":
                    checkCorner = arrays.Up[0].ToString() + arrays.Left[0].ToString() + arrays.Back[2].ToString();
                    break;
                case "LBU":
                    checkCorner = arrays.Left[0].ToString() + arrays.Back[2].ToString() + arrays.Up[0].ToString();
                    break;
                case "BUL":
                    checkCorner = arrays.Back[2].ToString() + arrays.Up[0].ToString() + arrays.Left[0].ToString();
                    break;

                // colt UBR <=> BQN
                case "UBR":
                    checkCorner = arrays.Up[2].ToString() + arrays.Back[0].ToString() + arrays.Right[2].ToString();
                    break;
                case "BRU":
                    checkCorner = arrays.Back[0].ToString() + arrays.Right[2].ToString() + arrays.Up[2].ToString();
                    break;
                case "RUB":
                    checkCorner = arrays.Right[2].ToString() + arrays.Up[2].ToString() + arrays.Back[0].ToString();
                    break;

                // colt FRU <=> JMC
                case "FUR":
                    checkCorner = arrays.Front[2].ToString() + arrays.Up[8].ToString() + arrays.Right[0].ToString();
                    break;
                case "RFU":
                    checkCorner = arrays.Right[0].ToString() + arrays.Front[2].ToString() + arrays.Up[8].ToString();
                    break;
                case "URF":
                    checkCorner = arrays.Up[8].ToString() + arrays.Right[0].ToString() + arrays.Front[2].ToString();
                    break;

                // colt FLU <=> IFD
                case "FLU":
                    checkCorner = arrays.Front[0].ToString() + arrays.Left[2].ToString() + arrays.Up[6].ToString();
                    break;
                case "LUF":
                    checkCorner = arrays.Left[2].ToString() + arrays.Up[6].ToString() + arrays.Front[0].ToString();
                    break;
                case "UFL":
                    checkCorner = arrays.Up[6].ToString() + arrays.Front[0].ToString() + arrays.Left[2].ToString();
                    break;

                // colt FDL <=> LUG
                case "FDL":
                    checkCorner = arrays.Front[6].ToString() + arrays.Down[0].ToString() + arrays.Left[8].ToString();
                    break;
                case "DLF":
                    checkCorner = arrays.Down[0].ToString() + arrays.Left[8].ToString() + arrays.Front[6].ToString();
                    break;
                case "LFD":
                    checkCorner = arrays.Left[8].ToString() + arrays.Front[6].ToString() + arrays.Down[0].ToString();
                    break;

                // colt FRD <=> KPV
                case "FRD":
                    checkCorner = arrays.Front[8].ToString() + arrays.Right[6].ToString() + arrays.Down[2].ToString();
                    break;
                case "RDF":
                    checkCorner = arrays.Right[6].ToString() + arrays.Down[2].ToString() + arrays.Front[8].ToString();
                    break;
                case "DFR":
                    checkCorner = arrays.Down[2].ToString() + arrays.Front[8].ToString() + arrays.Right[6].ToString();
                    break;

                // colt RBD <=> OTW
                case "RBD":
                    checkCorner = arrays.Right[8].ToString() + arrays.Back[6].ToString() + arrays.Down[8].ToString();
                    break;
                case "BDR":
                    checkCorner = arrays.Back[6].ToString() + arrays.Down[8].ToString() + arrays.Right[8].ToString();
                    break;
                case "DRB":
                    checkCorner = arrays.Down[8].ToString() + arrays.Right[8].ToString() + arrays.Back[6].ToString();
                    break;

                // colt LDB <=> HXS
                case "LDB":
                    checkCorner = arrays.Left[6].ToString() + arrays.Down[6].ToString() + arrays.Back[8].ToString();
                    break;
                case "DBL":
                    checkCorner = arrays.Down[6].ToString() + arrays.Back[8].ToString() + arrays.Left[6].ToString();
                    break;
                case "BLD":
                    checkCorner = arrays.Back[8].ToString() + arrays.Left[6].ToString() + arrays.Down[6].ToString();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Expected buffer: " + buffer + " checkCorner: " + checkCorner);
        }

        public void CheckCorners()
        {
            for (int i = 0; i < countedCornerss.Length; i++)
            {
                countedEdgeSum += countedCornerss[i];
                if (countedCornerss[i] == 0)
                {
                    newBuffer = newBufferString[i];
                    Console.WriteLine("newBuffer: " + newBuffer);
                    ExpectedCornerOrientation(newBuffer);
                    firstCycle = false;
                    firstTime = false;
                    CornersAlgorithm();
                }

            }
        }
        bool justOnce = true;
        bool skipFirstTime = true;
        public string[] switchedBuffer = new string[3];
        string cornerCheckString = "";

        public void CheckCornerFunction(string buffer, string expected)
        {
            for (int i = 0; i < changedBuffer.GetLength(0); i++)
            {
                if (expected.Equals(changedBuffer[i, 0]))
                {
                    Console.WriteLine("Expected(what should be) ::" + expected + " --- Buffer(what it is): " + buffer);
                    Console.WriteLine("changedBuffer[" + i + "," + 0 + "]: " + changedBuffer[i, 0]);
                    if (buffer.Equals(changedBuffer[i, 1]) || buffer.Equals(changedBuffer[i, 2]))
                    {
                        cornerCheckString = "FLIPED";
                        Console.WriteLine("Corner it's in the right place BUT it's just fliped!");
                        // bufferul trebe sa fie diferit fata de cel initial
                    }
                    else if (buffer.Equals(changedBuffer[i, 0]))
                    {
                        cornerCheckString = "OK";
                        Console.WriteLine(changedBuffer[i, 0] + " -- Corner is RIGHT oriented!");
                    }
                    else
                    {
                        cornerCheckString = "WP";
                        skipFirstTime = true;
                        Console.WriteLine(buffer + " -- Corner is in the wrong place!" + " skipfirst time: " + skipFirstTime);
                    }
                }
            }
            Console.WriteLine(" --------- CornerCheckArray: " + cornerCheckString);

        }

        public void CornersAlgorithm()
        {
            var arrays = new CubeArrays();
            arrays.switchArraysForm(switchCubeArrays);

            string buffer = "";
            string cornerToCheck = "";
            if (firstTime == true)
            {
                Console.WriteLine("--- FIRST TIME ---");
                buffer = arrays.Up[0].ToString() + arrays.Left[0].ToString() + arrays.Back[2].ToString();
                // buffer = "ULB";
                cornerToCheck = buffer;
                CheckCornerFunction(cornerToCheck, "ULB");
                // CheckCornerFunction("RDF", "RDF"); 
                expectedCorner = newBufferString[0];
                cornerCycle = newBufferString[0];
            }
            else
            {
                Console.WriteLine("--- n TIME ---");
                CheckCornerFunction(checkCorner, newBuffer);
                buffer = newBuffer;
                cornerCycle = newBuffer;
            }
            Console.WriteLine("Buffer outside: " + buffer + " cornerCheckString: " + cornerCheckString);

            switch (cornerCheckString)
            {
                case "WP":
                    Console.WriteLine("---------wppp");
                    while (firstCycle == false)
                    {
                        Console.WriteLine("Buffer: " + buffer);

                        for (int i = 0; i < changedBuffer.GetLength(0); i++)
                        {
                            for (int j = 0; j < changedBuffer.GetLength(1); j++)
                            {
                                // Console.WriteLine("[{0},{1}] = {2}", i, j, changedBuffer[i, j]);
                                if (cornerCycle.Equals(changedBuffer[i, j]) && justOnce == true)
                                {
                                    changedBufferInt[i, j] += 10;

                                    Console.WriteLine("Buffer: " + cornerCycle + " Changed buffer [" + i + "," + j + "] :" + changedBufferInt[i, j]);
                                    for (int k = 0; k < switchedBuffer.Length; k++)
                                    {
                                        switchedBuffer[k] = changedBuffer[i, k];
                                        Console.WriteLine("switchedBuffer[" + k + "]: " + switchedBuffer[k]);
                                    }
                                    justOnce = false;
                                }
                            }
                        }

                        if (skipFirstTime == false)
                        {
                            for (int i = 0; i < switchedBuffer.Length; i++)
                            {
                                if (buffer.Equals(switchedBuffer[i]))
                                {
                                    Console.WriteLine("Buffer stop: " + switchedBuffer[i]);
                                    firstCycle = true;
                                    justOnce = true;
                                    skipFirstTime = true;
                                }
                            }
                            Console.WriteLine("Skip first time: " + skipFirstTime.ToString());
                        }

                        switch (buffer)
                        {
                            // colt ULB <=> AER
                            case "ULB":
                                // solution += "---" + " ";
                                buffer = arrays.Up[0].ToString() + arrays.Left[0].ToString() + arrays.Back[2].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 0]++;
                                break;
                            case "LBU":
                                // solution += "---" + " ";
                                buffer = arrays.Left[0].ToString() + arrays.Back[2].ToString() + arrays.Up[0].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 1]++;
                                break;
                            case "BUL":
                                // solution += "---" + " ";
                                buffer = arrays.Back[2].ToString() + arrays.Up[0].ToString() + arrays.Left[0].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 2]++;
                                break;


                            // colt UBR <=> BQN
                            case "UBR":
                                //solution += "B" + " ";
                                solutionCornersList.Add("B");
                                buffer = arrays.Up[2].ToString() + arrays.Back[0].ToString() + arrays.Right[2].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 0]++;
                                break;
                            case "BRU":
                                //solution += "Q" + " ";
                                solutionCornersList.Add("Q");
                                buffer = arrays.Back[0].ToString() + arrays.Right[2].ToString() + arrays.Up[2].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 1]++;
                                break;
                            case "RUB":
                                //solution += "N" + " ";
                                solutionCornersList.Add("N");
                                buffer = arrays.Right[2].ToString() + arrays.Up[2].ToString() + arrays.Back[0].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 2]++;
                                break;

                            // colt FRU <=> JMC
                            case "FUR":
                                //solution += "J" + " ";
                                solutionCornersList.Add("J");
                                buffer = arrays.Front[2].ToString() + arrays.Up[8].ToString() + arrays.Right[0].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 0]++;
                                break;
                            case "RFU":
                                //solution += "M" + " ";
                                solutionCornersList.Add("M");
                                buffer = arrays.Right[0].ToString() + arrays.Front[2].ToString() + arrays.Up[8].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 1]++;
                                break;
                            case "URF":
                                //solution += "C" + " ";
                                solutionCornersList.Add("C");
                                buffer = arrays.Up[8].ToString() + arrays.Right[0].ToString() + arrays.Front[2].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 2]++;
                                break;

                            // colt FLU <=> IFD
                            case "FLU":
                                //solution += "I" + " ";
                                solutionCornersList.Add("I");
                                buffer = arrays.Front[0].ToString() + arrays.Left[2].ToString() + arrays.Up[6].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 0]++;
                                break;
                            case "LUF":
                                // solution += "F" + " ";
                                solutionCornersList.Add("F");
                                buffer = arrays.Left[2].ToString() + arrays.Up[6].ToString() + arrays.Front[0].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 1]++;
                                break;
                            case "UFL":
                                // solution += "D" + " ";
                                solutionCornersList.Add("D");
                                buffer = arrays.Up[6].ToString() + arrays.Front[0].ToString() + arrays.Left[2].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 2]++;
                                break;

                            // colt FDL <=> LUG
                            case "FDL":
                                // solution += "L" + " ";
                                solutionCornersList.Add("L");
                                buffer = arrays.Front[6].ToString() + arrays.Down[0].ToString() + arrays.Left[8].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 0]++;
                                break;
                            case "DLF":
                                // solution += "U" + " ";
                                solutionCornersList.Add("U");
                                buffer = arrays.Down[0].ToString() + arrays.Left[8].ToString() + arrays.Front[6].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 1]++;
                                break;
                            case "LFD":
                                // solution += "G" + " ";
                                solutionCornersList.Add("G");
                                buffer = arrays.Left[8].ToString() + arrays.Front[6].ToString() + arrays.Down[0].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 2]++;
                                break;

                            // colt FRD <=> KPV
                            case "FRD":
                                // solution += "K" + " ";
                                solutionCornersList.Add("K");
                                buffer = arrays.Front[8].ToString() + arrays.Right[6].ToString() + arrays.Down[2].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 0]++;
                                break;
                            case "RDF":
                                // solution += "P" + " ";
                                solutionCornersList.Add("P");
                                buffer = arrays.Right[6].ToString() + arrays.Down[2].ToString() + arrays.Front[8].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 1]++;
                                break;
                            case "DFR":
                                //solution += "V" + " ";
                                solutionCornersList.Add("V");
                                buffer = arrays.Down[2].ToString() + arrays.Front[8].ToString() + arrays.Right[6].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 2]++;
                                break;

                            // colt RBD <=> OTW
                            case "RBD":
                                //solution += "O" + " ";
                                solutionCornersList.Add("O");
                                buffer = arrays.Right[8].ToString() + arrays.Back[6].ToString() + arrays.Down[8].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 0]++;
                                break;
                            case "BDR":
                                //  solution += "T" + " ";
                                solutionCornersList.Add("T");
                                buffer = arrays.Back[6].ToString() + arrays.Down[8].ToString() + arrays.Right[8].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 1]++;
                                break;
                            case "DRB":
                                //solution += "W" + " ";
                                solutionCornersList.Add("W");
                                buffer = arrays.Down[8].ToString() + arrays.Right[8].ToString() + arrays.Back[6].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 2]++;
                                break;

                            // colt LDB <=> HXS
                            case "LDB":
                                //solution += "H" + " ";
                                solutionCornersList.Add("H");
                                buffer = arrays.Left[6].ToString() + arrays.Down[6].ToString() + arrays.Back[8].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 0]++;
                                break;
                            case "DBL":
                                // solution += "X" + " ";
                                solutionCornersList.Add("X");
                                buffer = arrays.Down[6].ToString() + arrays.Back[8].ToString() + arrays.Left[6].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 1]++;
                                break;
                            case "BLD":
                                // solution += "S" + " ";
                                solutionCornersList.Add("S");
                                buffer = arrays.Back[8].ToString() + arrays.Left[6].ToString() + arrays.Down[6].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 2]++;
                                break;

                            default:
                                solution += "++error++";
                                break;
                        }
                        Console.WriteLine("Solutie: " + solution);
                        skipFirstTime = false;

                    }
                    break;
                case "OK":
                    switch (buffer)
                    {
                        // colt ULB <=> AER
                        case "ULB":
                            PassCorner(0);
                            break;
                        case "LBU":
                            PassCorner(0);
                            break;
                        case "BUL":
                            PassCorner(0);
                            break;

                        // colt UBR <=> BQN
                        case "UBR":
                            PassCorner(1);
                            break;
                        case "BRU":
                            PassCorner(1);
                            break;
                        case "RUB":
                            PassCorner(1);
                            break;

                        // colt FRU <=> JMC
                        case "FUR":
                            PassCorner(2);
                            break;
                        case "RFU":
                            PassCorner(2);
                            break;
                        case "URF":
                            PassCorner(2);
                            break;

                        // colt FLU <=> IFD
                        case "FLU":
                            PassCorner(3);
                            break;
                        case "LUF":
                            PassCorner(3);
                            break;
                        case "UFL":
                            PassCorner(3);
                            break;

                        // colt FDL <=> LUG
                        case "FDL":
                            PassCorner(4);
                            break;
                        case "DLF":
                            PassCorner(4);
                            break;
                        case "LFD":
                            PassCorner(4);
                            break;

                        // colt FRD <=> KPV
                        case "FRD":
                            PassCorner(5);
                            break;
                        case "RDF":
                            PassCorner(5);
                            break;
                        case "DFR":
                            PassCorner(5);
                            break;

                        // colt RBD <=> OTW
                        case "RBD":
                            PassCorner(6);
                            break;
                        case "BDR":
                            PassCorner(6);
                            break;
                        case "DRB":
                            PassCorner(6);
                            break;

                        // colt LDB <=> HXS
                        case "LDB":
                            PassCorner(7);
                            break;
                        case "DBL":
                            PassCorner(7);
                            break;
                        case "BLD":
                            PassCorner(7);
                            break;

                        default:
                            solution += "++error++";
                            break;
                    }
                    Console.WriteLine("----------okkk");
                    break;
                case "FLIPED":
                    Console.WriteLine("---------flippppp");
                    int twotimes = 0;
                    while (twotimes < 2)
                    {
                        Console.WriteLine("Buffer: " + buffer);
                        switch (buffer)
                        {
                            // colt ULB <=> AER
                            case "ULB":
                                // solution += "---" + " ";
                                buffer = arrays.Up[0].ToString() + arrays.Left[0].ToString() + arrays.Back[2].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 0]++;
                                break;
                            case "LBU":
                                // solution += "---" + " ";
                                buffer = arrays.Left[0].ToString() + arrays.Back[2].ToString() + arrays.Up[0].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 1]++;
                                break;
                            case "BUL":
                                // solution += "---" + " ";
                                buffer = arrays.Back[2].ToString() + arrays.Up[0].ToString() + arrays.Left[0].ToString();
                                PassCorner(0);
                                changedBufferInt[0, 2]++;
                                break;


                            // colt UBR <=> BQN
                            case "UBR":
                                //solution += "B" + " ";
                                solutionCornersList.Add("B");
                                buffer = arrays.Up[2].ToString() + arrays.Back[0].ToString() + arrays.Right[2].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 0]++;
                                break;
                            case "BRU":
                                //solution += "Q" + " ";
                                solutionCornersList.Add("Q");
                                buffer = arrays.Back[0].ToString() + arrays.Right[2].ToString() + arrays.Up[2].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 1]++;
                                break;
                            case "RUB":
                                //solution += "N" + " ";
                                solutionCornersList.Add("N");
                                buffer = arrays.Right[2].ToString() + arrays.Up[2].ToString() + arrays.Back[0].ToString();
                                PassCorner(1);
                                changedBufferInt[1, 2]++;
                                break;

                            // colt FRU <=> JMC
                            case "FUR":
                                //solution += "J" + " ";
                                solutionCornersList.Add("J");
                                buffer = arrays.Front[2].ToString() + arrays.Up[8].ToString() + arrays.Right[0].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 0]++;
                                break;
                            case "RFU":
                                //solution += "M" + " ";
                                solutionCornersList.Add("M");
                                buffer = arrays.Right[0].ToString() + arrays.Front[2].ToString() + arrays.Up[8].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 1]++;
                                break;
                            case "URF":
                                //solution += "C" + " ";
                                solutionCornersList.Add("C");
                                buffer = arrays.Up[8].ToString() + arrays.Right[0].ToString() + arrays.Front[2].ToString();
                                PassCorner(2);
                                changedBufferInt[2, 2]++;
                                break;

                            // colt FLU <=> IFD
                            case "FLU":
                                //solution += "I" + " ";
                                solutionCornersList.Add("I");
                                buffer = arrays.Front[0].ToString() + arrays.Left[2].ToString() + arrays.Up[6].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 0]++;
                                break;
                            case "LUF":
                                // solution += "F" + " ";
                                solutionCornersList.Add("F");
                                buffer = arrays.Left[2].ToString() + arrays.Up[6].ToString() + arrays.Front[0].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 1]++;
                                break;
                            case "UFL":
                                // solution += "D" + " ";
                                solutionCornersList.Add("D");
                                buffer = arrays.Up[6].ToString() + arrays.Front[0].ToString() + arrays.Left[2].ToString();
                                PassCorner(3);
                                changedBufferInt[3, 2]++;
                                break;

                            // colt FDL <=> LUG
                            case "FDL":
                                // solution += "L" + " ";
                                solutionCornersList.Add("L");
                                buffer = arrays.Front[6].ToString() + arrays.Down[0].ToString() + arrays.Left[8].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 0]++;
                                break;
                            case "DLF":
                                // solution += "U" + " ";
                                solutionCornersList.Add("U");
                                buffer = arrays.Down[0].ToString() + arrays.Left[8].ToString() + arrays.Front[6].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 1]++;
                                break;
                            case "LFD":
                                // solution += "G" + " ";
                                solutionCornersList.Add("G");
                                buffer = arrays.Left[8].ToString() + arrays.Front[6].ToString() + arrays.Down[0].ToString();
                                PassCorner(4);
                                changedBufferInt[4, 2]++;
                                break;

                            // colt FRD <=> KPV
                            case "FRD":
                                // solution += "K" + " ";
                                solutionCornersList.Add("K");
                                buffer = arrays.Front[8].ToString() + arrays.Right[6].ToString() + arrays.Down[2].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 0]++;
                                break;
                            case "RDF":
                                // solution += "P" + " ";
                                solutionCornersList.Add("P");
                                buffer = arrays.Right[6].ToString() + arrays.Down[2].ToString() + arrays.Front[8].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 1]++;
                                break;
                            case "DFR":
                                //solution += "V" + " ";
                                solutionCornersList.Add("V");
                                buffer = arrays.Down[2].ToString() + arrays.Front[8].ToString() + arrays.Right[6].ToString();
                                PassCorner(5);
                                changedBufferInt[5, 2]++;
                                break;

                            // colt RBD <=> OTW
                            case "RBD":
                                //solution += "O" + " ";
                                solutionCornersList.Add("O");
                                buffer = arrays.Right[8].ToString() + arrays.Back[6].ToString() + arrays.Down[8].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 0]++;
                                break;
                            case "BDR":
                                //  solution += "T" + " ";
                                solutionCornersList.Add("T");
                                buffer = arrays.Back[6].ToString() + arrays.Down[8].ToString() + arrays.Right[8].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 1]++;
                                break;
                            case "DRB":
                                //solution += "W" + " ";
                                solutionCornersList.Add("W");
                                buffer = arrays.Down[8].ToString() + arrays.Right[8].ToString() + arrays.Back[6].ToString();
                                PassCorner(6);
                                changedBufferInt[6, 2]++;
                                break;

                            // colt LDB <=> HXS
                            case "LDB":
                                //solution += "H" + " ";
                                solutionCornersList.Add("H");
                                buffer = arrays.Left[6].ToString() + arrays.Down[6].ToString() + arrays.Back[8].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 0]++;
                                break;
                            case "DBL":
                                // solution += "X" + " ";
                                solutionCornersList.Add("X");
                                buffer = arrays.Down[6].ToString() + arrays.Back[8].ToString() + arrays.Left[6].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 1]++;
                                break;
                            case "BLD":
                                // solution += "S" + " ";
                                solutionCornersList.Add("S");
                                buffer = arrays.Back[8].ToString() + arrays.Left[6].ToString() + arrays.Down[6].ToString();
                                PassCorner(7);
                                changedBufferInt[7, 2]++;
                                break;

                            default:
                                solution += "++error++";
                                break;
                        }
                        Console.WriteLine("Solutie: " + solution);
                        twotimes++;
                    }
                    break;
                default:
                    Console.WriteLine("---------default");
                    break;
            }
            CheckCorners();
            Console.WriteLine("Suma: " + countedEdgeSum);
            // return solution;
        }
    }
}
