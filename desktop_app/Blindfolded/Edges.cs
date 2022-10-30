using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textexemplu2;

namespace Textexemplu2.Blindfolded
{
    // Sus: (AER), (BQN), (CMJ), (DIF)
    // Jos: (LUG), (HXS), (TWO), (PVK)
    class Edges
    {
        public bool switchCubeArrays;

        public int[] countedEdges = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public string[] newBufferString = new string[12] { "UB", "UR", "UF", "UL", "FL", "RF", "BR", "LB", "FD", "RD", "BD", "LD" };

        string[,] changedBuffer = new string[12, 2]
        {
            { "UB","BU"},{ "UR","RU"},{ "UF","FU"},{ "UL","LU"},{ "FL","LF"},{ "RF","FR"},
            { "BR","RB"},{ "LB","BL"},{ "FD","DF"},{ "RD","DR"},{ "BD","DB"},{ "LD","DL"}
        };

        string[,] cornersSolution = new string[12, 2]
        {
            {"A","Q" },{"B","M" },{"C","I" },{"D","E" },{"L","F" },{"P","J" },
            {"T","N" },{"H","R" },{"K","U" },{"O","V" },{"S","W" },{"G","X" }
        };

        int[,] changedBufferInt = new int[12, 2]
        {
            { 0,0},{ 0,0},{ 0,0},{ 0,0},{ 0,0},{ 0,0},
            { 0,0},{ 0,0},{ 0,0},{ 0,0},{ 0,0},{ 0,0}
        };

        public List<string> solutionEdgesList = new List<string>();

        bool firstCycle = false;

        public string solution = "";
        public string solution2 = "";
        public int countedEdgeSum = 0;

        public string newBuffer = "";
        public string checkEdges = "";
        string expectedEdge = "";

        public string edgeCycle = "";
        public bool firstTime = true;

        public void PassEdge(int edge)
        {
            countedEdges[edge] += 1;
        }

        public void ExpectedEdgesOrientation(string buffer)
        {
            var arrays = new CubeArrays();
            arrays.switchArraysForm(switchCubeArrays);

            switch (buffer)
            {
                // colt UB <=> AQ
                case "UB":
                    checkEdges = arrays.Up[1].ToString() + arrays.Back[1].ToString();
                    break;
                case "BU":
                    checkEdges = arrays.Back[1].ToString() + arrays.Up[1].ToString();
                    break;

                // colt UR <=> BM
                case "UR":
                    checkEdges = arrays.Up[5].ToString() + arrays.Right[1].ToString();
                    break;
                case "RU":
                    checkEdges = arrays.Right[1].ToString() + arrays.Up[5].ToString();
                    break;

                // colt UF <=> CI
                case "UF":
                    checkEdges = arrays.Up[7].ToString() + arrays.Front[1].ToString();
                    break;
                case "FU":
                    checkEdges = arrays.Front[1].ToString() + arrays.Up[7].ToString();
                    break;

                // colt UL <=> DE
                case "UL":
                    checkEdges = arrays.Up[3].ToString() + arrays.Left[1].ToString();
                    break;
                case "LU":
                    checkEdges = arrays.Left[1].ToString() + arrays.Up[3].ToString();
                    break;

                // colt FL <=> LF
                case "FL":
                    checkEdges = arrays.Front[3].ToString() + arrays.Left[5].ToString();
                    break;
                case "LF":
                    checkEdges = arrays.Left[5].ToString() + arrays.Front[3].ToString();
                    break;

                // colt RF <=> PJ
                case "RF":
                    checkEdges = arrays.Right[3].ToString() + arrays.Front[5].ToString();
                    break;
                case "FR":
                    checkEdges = arrays.Front[5].ToString() + arrays.Right[3].ToString();
                    break;

                // colt BR <=> TN
                case "BR":
                    checkEdges = arrays.Back[3].ToString() + arrays.Right[5].ToString();
                    break;
                case "RB":
                    checkEdges = arrays.Right[5].ToString() + arrays.Back[3].ToString();
                    break;

                // colt LB <=> HR
                case "LB":
                    checkEdges = arrays.Left[3].ToString() + arrays.Back[5].ToString();
                    break;
                case "BL":
                    checkEdges = arrays.Back[5].ToString() + arrays.Left[3].ToString();
                    break;

                // colt FD <=> KU
                case "FD":
                    checkEdges = arrays.Front[7].ToString() + arrays.Down[1].ToString();
                    break;
                case "DF":
                    checkEdges = arrays.Down[1].ToString() + arrays.Front[7].ToString();
                    break;

                // colt RD <=> OV
                case "RD":
                    checkEdges = arrays.Right[7].ToString() + arrays.Down[5].ToString();
                    break;
                case "DR":
                    checkEdges = arrays.Down[5].ToString() + arrays.Right[7].ToString();
                    break;

                // colt BD <=> SW
                case "BD":
                    checkEdges = arrays.Back[7].ToString() + arrays.Down[7].ToString();
                    break;
                case "DB":
                    checkEdges = arrays.Down[7].ToString() + arrays.Back[7].ToString();
                    break;

                // colt LD <=> GX
                case "LD":
                    checkEdges = arrays.Left[7].ToString() + arrays.Down[3].ToString();
                    break;
                case "DL":
                    checkEdges = arrays.Down[3].ToString() + arrays.Left[7].ToString();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Expected buffer: " + buffer + " checkCorner: " + checkEdges);
        }

        public void CheckEdges()
        {
            for (int i = 0; i < countedEdges.Length; i++)
            {
                countedEdgeSum += countedEdges[i];
                if (countedEdges[i] == 0)
                {
                    newBuffer = newBufferString[i];
                    Console.WriteLine("newBuffer: " + newBuffer);
                    ExpectedEdgesOrientation(newBuffer);
                    firstCycle = false;
                    firstTime = false;
                    EdgesAlgorithm();
                }

            }
        }
        bool justOnce = true;
        bool skipFirstTime = true;
        public string[] switchedBuffer = new string[2];
        string edgesCheckString = "";

        public void CheckEdgesFunction(string buffer, string expected)
        {
            for (int i = 0; i < changedBuffer.GetLength(0); i++)
            {
                //  Console.WriteLine(" ----Expected: "+ expected+ "----"+changedBuffer[i, 0]);
                if (expected.Equals(changedBuffer[8, 1]))
                {
                    edgesCheckString = "WP";
                    skipFirstTime = true;
                }
                if (expected.Equals(changedBuffer[i, 0]))
                {
                    Console.WriteLine("Expected(what should be) ::" + expected + " --- Buffer(what it is): " + buffer);
                    Console.WriteLine("changedBuffer[" + i + "," + 0 + "]: " + changedBuffer[i, 0]);
                    if (buffer.Equals(changedBuffer[i, 1]))
                    {
                        edgesCheckString = "FLIPED";
                        Console.WriteLine("Corner it's in the right place BUT it's just fliped!");
                        // bufferul trebe sa fie diferit fata de cel initial
                    }
                    else if (buffer.Equals(changedBuffer[i, 0]))
                    {
                        edgesCheckString = "OK";
                        Console.WriteLine(changedBuffer[i, 0] + " -- Corner is RIGHT oriented!");
                    }
                    else
                    {
                        edgesCheckString = "WP";
                        skipFirstTime = true;
                        Console.WriteLine(buffer + " -- Corner is in the wrong place!" + " skipfirst time: " + skipFirstTime);
                    }
                }
            }
            Console.WriteLine(" --------- CornerCheckArray: " + edgesCheckString);

        }

        public void EdgesAlgorithm()
        {
            var arrays = new CubeArrays();
            arrays.switchArraysForm(switchCubeArrays);

            string buffer = "";
            string edgeToCheck = "";
            if (firstTime == true)
            {
                Console.WriteLine("--- FIRST TIME ---");
                buffer = arrays.Down[1].ToString() + arrays.Front[7].ToString();
                //buffer = "DF";
                edgeToCheck = buffer;
                CheckEdgesFunction(edgeToCheck, "DF");
                // CheckCornerFunction("RDF", "RDF"); 
                expectedEdge = newBufferString[8];
                edgeCycle = newBufferString[8];
            }
            else
            {
                Console.WriteLine("--- n TIME ---");
                CheckEdgesFunction(checkEdges, newBuffer);
                buffer = newBuffer;
                edgeCycle = newBuffer;
            }
            Console.WriteLine("Buffer outside: " + buffer + " cornerCheckString: " + edgesCheckString);

            switch (edgesCheckString)
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
                                if (edgeCycle.Equals(changedBuffer[i, j]) && justOnce == true)
                                {
                                    changedBufferInt[i, j] += 10;

                                    Console.WriteLine("Buffer: " + edgeCycle + " Changed buffer [" + i + "," + j + "] :" + changedBufferInt[i, j]);
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
                            // colt UB <=> AQ
                            case "UB":
                                //solution += "A" + " ";
                                solutionEdgesList.Add("A");
                                buffer = arrays.Up[1].ToString() + arrays.Back[1].ToString();
                                PassEdge(0);
                                break;
                            case "BU":
                                //solution += "Q" + " ";
                                solutionEdgesList.Add("Q");
                                buffer = arrays.Back[1].ToString() + arrays.Up[1].ToString();
                                PassEdge(0);
                                break;

                            // colt UR <=> BM
                            case "UR":
                                //solution += "B" + " ";
                                solutionEdgesList.Add("B");
                                buffer = arrays.Up[5].ToString() + arrays.Right[1].ToString();
                                PassEdge(1);
                                break;
                            case "RU":
                                // solution += "M" + " ";
                                solutionEdgesList.Add("M");
                                buffer = arrays.Right[1].ToString() + arrays.Up[5].ToString();
                                PassEdge(1);
                                break;

                            // colt UF <=> CI
                            case "UF":
                                //solution += "C" + " ";
                                solutionEdgesList.Add("C");
                                buffer = arrays.Up[7].ToString() + arrays.Front[1].ToString();
                                PassEdge(2);
                                break;
                            case "FU":
                                //solution += "I" + " ";
                                solutionEdgesList.Add("I");
                                buffer = arrays.Front[1].ToString() + arrays.Up[7].ToString();
                                PassEdge(2);
                                break;

                            // colt UL <=> DE
                            case "UL":
                                //solution += "D" + " ";
                                solutionEdgesList.Add("D");
                                buffer = arrays.Up[3].ToString() + arrays.Left[1].ToString();
                                PassEdge(3);
                                break;
                            case "LU":
                                //solution += "E" + " ";
                                solutionEdgesList.Add("E");
                                buffer = arrays.Left[1].ToString() + arrays.Up[3].ToString();
                                PassEdge(3);
                                break;

                            // colt FL <=> LF
                            case "FL":
                                //solution += "L" + " ";
                                solutionEdgesList.Add("L");
                                buffer = arrays.Front[3].ToString() + arrays.Left[5].ToString();
                                PassEdge(4);
                                break;
                            case "LF":
                                // solution += "F" + " ";
                                solutionEdgesList.Add("F");
                                buffer = arrays.Left[5].ToString() + arrays.Front[3].ToString();
                                PassEdge(4);
                                break;

                            // colt RF <=> PJ
                            case "RF":
                                //solution += "P" + " ";
                                solutionEdgesList.Add("P");
                                buffer = arrays.Right[3].ToString() + arrays.Front[5].ToString();
                                PassEdge(5);
                                break;
                            case "FR":
                                //solution += "J" + " ";
                                solutionEdgesList.Add("J");
                                buffer = arrays.Front[5].ToString() + arrays.Right[3].ToString();
                                PassEdge(5);
                                break;

                            // colt BR <=> TN
                            case "BR":
                                // solution += "T" + " ";
                                solutionEdgesList.Add("T");
                                buffer = arrays.Back[3].ToString() + arrays.Right[5].ToString();
                                PassEdge(6);
                                break;
                            case "RB":
                                // solution += "N" + " ";
                                solutionEdgesList.Add("N");
                                buffer = arrays.Right[5].ToString() + arrays.Back[3].ToString();
                                PassEdge(6);
                                break;

                            // colt LB <=> HR
                            case "LB":
                                //solution += "H" + " ";
                                solutionEdgesList.Add("H");
                                buffer = arrays.Left[3].ToString() + arrays.Back[5].ToString();
                                PassEdge(7);
                                break;
                            case "BL":
                                //solution += "R" + " ";
                                solutionEdgesList.Add("R");
                                buffer = arrays.Back[5].ToString() + arrays.Left[3].ToString();
                                PassEdge(7);
                                break;

                            // colt FD <=> KU
                            case "FD":
                                // solution += "---" + " ";
                                buffer = arrays.Front[7].ToString() + arrays.Down[1].ToString();
                                PassEdge(8);
                                break;
                            case "DF":
                                // solution += "---" + " ";
                                buffer = arrays.Down[1].ToString() + arrays.Front[7].ToString();
                                PassEdge(8);
                                break;

                            // colt RD <=> OV
                            case "RD":
                                // solution += "O" + " ";
                                solutionEdgesList.Add("O");
                                buffer = arrays.Right[7].ToString() + arrays.Down[5].ToString();
                                PassEdge(9);
                                break;
                            case "DR":
                                // solution += "V" + " ";
                                solutionEdgesList.Add("V");
                                buffer = arrays.Down[5].ToString() + arrays.Right[7].ToString();
                                PassEdge(9);
                                break;

                            // colt BD <=> SW
                            case "BD":
                                //solution += "S" + " ";
                                solutionEdgesList.Add("S");
                                buffer = arrays.Back[7].ToString() + arrays.Down[7].ToString();
                                PassEdge(10);
                                break;
                            case "DB":
                                //solution += "W" + " ";
                                solutionEdgesList.Add("W");
                                buffer = arrays.Down[7].ToString() + arrays.Back[7].ToString();
                                PassEdge(10);
                                break;

                            // colt LD <=> GX
                            case "LD":
                                // solution += "G" + " ";
                                solutionEdgesList.Add("G");
                                buffer = arrays.Left[7].ToString() + arrays.Down[3].ToString();
                                PassEdge(11);
                                break;
                            case "DL":
                                //solution += "X" + " ";
                                solutionEdgesList.Add("X");
                                buffer = arrays.Down[3].ToString() + arrays.Left[7].ToString();
                                PassEdge(11);
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
                        // colt UB <=> AQ
                        case "UB":
                            PassEdge(0);
                            break;
                        case "BU":
                            PassEdge(0);
                            break;

                        // colt UR <=> BM
                        case "UR":
                            PassEdge(1);
                            break;
                        case "RU":
                            PassEdge(1);
                            break;

                        // colt UF <=> CI
                        case "UF":
                            PassEdge(2);
                            break;
                        case "FU":
                            PassEdge(2);
                            break;

                        // colt UL <=> DE
                        case "UL":
                            PassEdge(3);
                            break;
                        case "LU":
                            PassEdge(3);
                            break;

                        // colt FL <=> LF
                        case "FL":
                            PassEdge(4);
                            break;
                        case "LF":
                            PassEdge(4);
                            break;

                        // colt RF <=> PJ
                        case "RF":
                            PassEdge(5);
                            break;
                        case "FR":
                            PassEdge(5);
                            break;

                        // colt BR <=> TN
                        case "BR":
                            PassEdge(6);
                            break;
                        case "RB":
                            PassEdge(6);
                            break;

                        // colt LB <=> HR
                        case "LB":
                            PassEdge(7);
                            break;
                        case "BL":
                            PassEdge(7);
                            break;

                        // colt FD <=> KU
                        case "FD":
                            PassEdge(8);
                            break;
                        case "DF":
                            PassEdge(8);
                            break;

                        // colt RD <=> OV
                        case "RD":
                            PassEdge(9);
                            break;
                        case "DR":
                            PassEdge(9);
                            break;

                        // colt BD <=> SW
                        case "BD":
                            PassEdge(10);
                            break;
                        case "DB":
                            PassEdge(10);
                            break;

                        // colt LD <=> GX
                        case "LD":
                            PassEdge(11);
                            break;
                        case "DL":
                            PassEdge(11);
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
                            // colt UB <=> AQ
                            case "UB":
                                //solution += "A" + " ";
                                solutionEdgesList.Add("A");
                                buffer = arrays.Up[1].ToString() + arrays.Back[1].ToString();
                                PassEdge(0);
                                break;
                            case "BU":
                                //solution += "Q" + " ";
                                solutionEdgesList.Add("Q");
                                buffer = arrays.Back[1].ToString() + arrays.Up[1].ToString();
                                PassEdge(0);
                                break;

                            // colt UR <=> BM
                            case "UR":
                                //solution += "B" + " ";
                                solutionEdgesList.Add("B");
                                buffer = arrays.Up[5].ToString() + arrays.Right[1].ToString();
                                PassEdge(1);
                                break;
                            case "RU":
                                // solution += "M" + " ";
                                solutionEdgesList.Add("M");
                                buffer = arrays.Right[1].ToString() + arrays.Up[5].ToString();
                                PassEdge(1);
                                break;

                            // colt UF <=> CI
                            case "UF":
                                //solution += "C" + " ";
                                solutionEdgesList.Add("C");
                                buffer = arrays.Up[7].ToString() + arrays.Front[1].ToString();
                                PassEdge(2);
                                break;
                            case "FU":
                                //solution += "I" + " ";
                                solutionEdgesList.Add("I");
                                buffer = arrays.Front[1].ToString() + arrays.Up[7].ToString();
                                PassEdge(2);
                                break;

                            // colt UL <=> DE
                            case "UL":
                                //solution += "D" + " ";
                                solutionEdgesList.Add("D");
                                buffer = arrays.Up[3].ToString() + arrays.Left[1].ToString();
                                PassEdge(3);
                                break;
                            case "LU":
                                //solution += "E" + " ";
                                solutionEdgesList.Add("E");
                                buffer = arrays.Left[1].ToString() + arrays.Up[3].ToString();
                                PassEdge(3);
                                break;

                            // colt FL <=> LF
                            case "FL":
                                //solution += "L" + " ";
                                solutionEdgesList.Add("L");
                                buffer = arrays.Front[3].ToString() + arrays.Left[5].ToString();
                                PassEdge(4);
                                break;
                            case "LF":
                                // solution += "F" + " ";
                                solutionEdgesList.Add("F");
                                buffer = arrays.Left[5].ToString() + arrays.Front[3].ToString();
                                PassEdge(4);
                                break;

                            // colt RF <=> PJ
                            case "RF":
                                //solution += "P" + " ";
                                solutionEdgesList.Add("P");
                                buffer = arrays.Right[3].ToString() + arrays.Front[5].ToString();
                                PassEdge(5);
                                break;
                            case "FR":
                                //solution += "J" + " ";
                                solutionEdgesList.Add("J");
                                buffer = arrays.Front[5].ToString() + arrays.Right[3].ToString();
                                PassEdge(5);
                                break;

                            // colt BR <=> TN
                            case "BR":
                                // solution += "T" + " ";
                                solutionEdgesList.Add("T");
                                buffer = arrays.Back[3].ToString() + arrays.Right[5].ToString();
                                PassEdge(6);
                                break;
                            case "RB":
                                // solution += "N" + " ";
                                solutionEdgesList.Add("N");
                                buffer = arrays.Right[5].ToString() + arrays.Back[3].ToString();
                                PassEdge(6);
                                break;

                            // colt LB <=> HR
                            case "LB":
                                //solution += "H" + " ";
                                solutionEdgesList.Add("H");
                                buffer = arrays.Left[3].ToString() + arrays.Back[5].ToString();
                                PassEdge(7);
                                break;
                            case "BL":
                                //solution += "R" + " ";
                                solutionEdgesList.Add("R");
                                buffer = arrays.Back[5].ToString() + arrays.Left[3].ToString();
                                PassEdge(7);
                                break;

                            // colt FD <=> KU
                            case "FD":
                                // solution += "---" + " ";
                                buffer = arrays.Front[7].ToString() + arrays.Down[1].ToString();
                                PassEdge(8);
                                break;
                            case "DF":
                                // solution += "---" + " ";
                                buffer = arrays.Down[1].ToString() + arrays.Front[7].ToString();
                                PassEdge(8);
                                break;

                            // colt RD <=> OV
                            case "RD":
                                // solution += "O" + " ";
                                solutionEdgesList.Add("O");
                                buffer = arrays.Right[7].ToString() + arrays.Down[5].ToString();
                                PassEdge(9);
                                break;
                            case "DR":
                                // solution += "V" + " ";
                                solutionEdgesList.Add("V");
                                buffer = arrays.Down[5].ToString() + arrays.Right[7].ToString();
                                PassEdge(9);
                                break;

                            // colt BD <=> SW
                            case "BD":
                                //solution += "S" + " ";
                                solutionEdgesList.Add("S");
                                buffer = arrays.Back[7].ToString() + arrays.Down[7].ToString();
                                PassEdge(10);
                                break;
                            case "DB":
                                //solution += "W" + " ";
                                solutionEdgesList.Add("W");
                                buffer = arrays.Down[7].ToString() + arrays.Back[7].ToString();
                                PassEdge(10);
                                break;

                            // colt LD <=> GX
                            case "LD":
                                // solution += "G" + " ";
                                solutionEdgesList.Add("G");
                                buffer = arrays.Left[7].ToString() + arrays.Down[3].ToString();
                                PassEdge(11);
                                break;
                            case "DL":
                                //solution += "X" + " ";
                                solutionEdgesList.Add("X");
                                buffer = arrays.Down[3].ToString() + arrays.Left[7].ToString();
                                PassEdge(11);
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
            CheckEdges();
            Console.WriteLine("Suma: " + countedEdgeSum);
            //return solution;
        }
    }
}
