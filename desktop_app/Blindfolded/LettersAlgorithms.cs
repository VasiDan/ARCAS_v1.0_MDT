using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Blindfolded
{
    class LettersAlgorithms
    {
        string letterA = "L2 R2";

        string letterB = "R' U R U' L2 R2 D R' D' R";
        string letterB2m = "R' D R D' L2 R2 U R' U' R";

        string letterC = "U2 L R' F2 L R'";
        string letterC2m = "D2 L R' B2 L R'";

        string letterD = "L U' L' U L2 R2 D' L D L'";
        string letterD2m = "L D' L' D L2 R2 U' L U L'";

        string letterE = "B L' B' L2 R2 F L F'";
        string letterE2m = "F L' F' L2 R2 B L B'";

        string letterF = "B L2 B' L2 R2 F L2 F'";
        string letterF2m = "F L2 F' L2 R2 B L2 B'";

        string letterG = "B L B' L2 R2 F L' F'";
        string letterG2m = "F L F' L2 R2 B L' B'";

        string letterH = "L' B L B' L2 R2 F L' F' L";
        string letterH2m = "L' F L F' L2 R2 B L' B' L";

        string letterI = "D L R' F R2 F' L' R U R2 U' D' L2 R2";
        string letterI2m = "U L R' B R2 B' L' R D R2 D' U' L2 R2";

        string letterJ = "U R U' L2 R2 D R' D'";
        string letterJ2m = "D R D' L2 R2 U R' U'";

        string letterL = "U' L' U L2 R2 D' L D";
        string letterL2m = "D' L' D L2 R2 U' L U";

        string letterM = "B' R B L2 R2 F' R' F";
        string letterM2m = "F' R F L2 R2 B' R' B";

        string letterN = "R B' R' B L2 R2 F' R F R'";
        string letterN2m = "R F' R' F L2 R2 B' R B R'";

        string letterO = "B' R' B L2 R2 F' R F";
        string letterO2m = "F' R' F L2 R2 B' R B";

        string letterP = "B' R2 B L2 R2 F' R2 F";
        string letterP2m = "F' R2 F L2 R2 B' R2 B";

        string letterQ = "B' R B U R2 U' L2 R2 D R2 D' F' R' F";
        string letterQ2m = "F' R F D R2 D' L2 R2 U R2 U' B' R' B";

        string letterR = "U' L U L2 R2 D' L' D";
        string letterR2m = "D' L D L2 R2 U' L' U";

        string letterS = "L2 R2 U D R2 D' L R' B R2 B' L' R U'";
        string letterS2m = "L2 R2 D U R2 U' L R' F R2 F' L' R D'";

        string letterT = "U R' U' L2 R2 D R D'";
        string letterT2m = "D R' D' L2 R2 U R U'";

        string letterV = "U R2 U' L2 R2 D R2 D'";
        string letterV2m = "D R2 D' L2 R2 U R2 U'";

        string letterW = "L' R B2 L' R D2";
        string letterW2m = "L' R F2 L' R U2";

        string letterX = "U' L2 U L2 R2 D' L2 D";
        string letterX2m = "D' L2 D L2 R2 U' L2 U";

        string parity = "D' L2 D L2 R2 U' L2 U";
        public string parity2m = "U' L2 U L2 R2 D' L2 D";

        string oldPochmannAlgorithm = "R U' R' U' R U R' F' R U R' U' R' F R";

        string cornerB = "R D'";
        string cornerB2b = "D R'";

        string cornerC = "F";
        string cornerC2b = "F'";

        string cornerD = "F R'";
        string cornerD2b = "R F'";

        string cornerF = "F2";
        //string cornerF2b = "F2";

        string cornerG = "F2 R'";
        string cornerG2b = "R F2";

        string cornerH = "D2";
        //string cornerH2b = "D2";

        string cornerI = "F' D";
        string cornerI2b = "D' F";

        string cornerJ = "F2 D";
        string cornerJ2b = "D' F2";

        string cornerK = "F D";
        string cornerK2b = "D' F'";

        string cornerL = "D";
        string cornerL2b = "D'";

        string cornerM = "R'";
        string cornerM2b = "R";

        string cornerN = "R2";
        //string cornerN2b = "R2";

        string cornerO = "R";
        string cornerO2b = "R'";

        string cornerP = "";
        string cornerP2b = "";

        string cornerQ = "R' F";
        string cornerQ2b = "F' R";

        string cornerS = "D' R";
        string cornerS2b = "R' D";

        string cornerT = "D'";
        string cornerT2b = "D";

        string cornerU = "F'";
        string cornerU2b = "F";

        string cornerV = "D' F'";
        string cornerV2b = "F D";

        string cornerW = "D2 F'";
        string cornerW2b = "F D2";

        string cornerX = "D F'";
        string cornerX2b = "F D'";

        public string finalSolutionEdges = "";
        public string finalSolutionCorners = "";
        public string lettersEdges = "";
        public string lettersCorners = "";
        public string totalEM = "", totalCM = "";

        public bool parityCheck = false;
        public int totalMovesBLD = 0, totalLettersCorners = 0, totalLettersEdges = 0;

        public void FinalSolutionCornersAndEdges(bool switchArrays)
        {
            finalSolutionEdges = "";
            finalSolutionCorners = "";

            var cornersFunction = new Corners();
            var edgesFuntion = new Edges();

            int edgesMoves = 0, cornersMoves = 0, parityMoves = 0;

            cornersFunction.switchCubeArrays = switchArrays;
            edgesFuntion.switchCubeArrays = switchArrays;
            cornersFunction.CornersAlgorithm();
            edgesFuntion.EdgesAlgorithm();

            var cornersList = cornersFunction.solutionCornersList;
            var edgesList = edgesFuntion.solutionEdgesList;
            var cornersString = String.Join(" ", cornersList);
            var edgesString = String.Join(" ", edgesList);

            
            lettersCorners = cornersString;

            if (cornersList.Count % 2 == edgesList.Count % 2)
            {
                if (cornersList.Count % 2 != 0)
                {
                    Console.WriteLine("Numar IMPAR de litere -> aplicam algoritmul de paritate");
                    parityCheck = true;
                }
                else
                {
                    Console.WriteLine("Numar PAR de litere -> NU aplicam algoritmul de paritate");
                    parityCheck = false;
                }

            }
            else
            {
                Console.WriteLine("Suma Colturilor != suma muchilor!!! fatal error!");
            }

            for (int i = 0; i < edgesList.Count; i++)
            {
                if (i % 2 != 0)
                {
                    switch (edgesList[i])
                    {
                        case "W":
                            edgesList[i] = "C";
                            break;
                        case "C":
                            edgesList[i] = "W";
                            break;
                        case "I":
                            edgesList[i] = "S";
                            break;
                        case "S":
                            edgesList[i] = "I";
                            break;
                        default:
                            break;

                    }
                }
            }
            var edgesStringAfter = String.Join(" ", edgesList);

            lettersEdges = edgesStringAfter;

            for (int i = 0; i < cornersList.Count; i++)
            {
                switch (cornersList[i])
                {
                    case "B":
                        finalSolutionCorners += cornerB + " " + oldPochmannAlgorithm + " " + cornerB2b + " ";
                        break;
                    case "C":
                        finalSolutionCorners += cornerC + " " + oldPochmannAlgorithm + " " + cornerC2b + " ";
                        break;
                    case "D":
                        finalSolutionCorners += cornerD + " " + oldPochmannAlgorithm + " " + cornerD2b + " ";
                        break;
                    case "F":
                        finalSolutionCorners += cornerF + " " + oldPochmannAlgorithm + " " + cornerF + " ";
                        break;
                    case "G":
                        finalSolutionCorners += cornerG + " " + oldPochmannAlgorithm + " " + cornerG2b + " ";
                        break;
                    case "H":
                        finalSolutionCorners += cornerH + " " + oldPochmannAlgorithm + " " + cornerH + " ";
                        break;
                    case "I":
                        finalSolutionCorners += cornerI + " " + oldPochmannAlgorithm + " " + cornerI2b + " ";
                        break;
                    case "J":
                        finalSolutionCorners += cornerJ + " " + oldPochmannAlgorithm + " " + cornerJ2b + " ";
                        break;
                    case "K":
                        finalSolutionCorners += cornerK + " " + oldPochmannAlgorithm + " " + cornerK2b + " ";
                        break;
                    case "L":
                        finalSolutionCorners += cornerL + " " + oldPochmannAlgorithm + " " + cornerL2b + " ";
                        break;
                    case "M":
                        finalSolutionCorners += cornerM + " " + oldPochmannAlgorithm + " " + cornerM2b + " ";
                        break;
                    case "N":
                        finalSolutionCorners += cornerN + " " + oldPochmannAlgorithm + " " + cornerN + " ";
                        break;
                    case "O":
                        finalSolutionCorners += cornerO + " " + oldPochmannAlgorithm + " " + cornerO2b + " ";
                        break;
                    case "P":
                        finalSolutionCorners += cornerP + " " + oldPochmannAlgorithm + " " + cornerP2b + " ";
                        break;
                    case "Q":
                        finalSolutionCorners += cornerQ + " " + oldPochmannAlgorithm + " " + cornerQ2b + " ";
                        break;
                    case "S":
                        finalSolutionCorners += cornerS + " " + oldPochmannAlgorithm + " " + cornerS2b + " ";
                        break;
                    case "T":
                        finalSolutionCorners += cornerT + " " + oldPochmannAlgorithm + " " + cornerT2b + " ";
                        break;
                    case "U":
                        finalSolutionCorners += cornerU + " " + oldPochmannAlgorithm + " " + cornerU2b + " ";
                        break;
                    case "V":
                        finalSolutionCorners += cornerV + " " + oldPochmannAlgorithm + " " + cornerV2b + " ";
                        break;
                    case "W":
                        finalSolutionCorners += cornerW + " " + oldPochmannAlgorithm + " " + cornerW2b + " ";
                        break;
                    case "X":
                        finalSolutionCorners += cornerX + " " + oldPochmannAlgorithm + " " + cornerX2b + " ";
                        break;
                    default:
                        break;

                }
            }

            for (int i = 0; i < edgesList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    switch (edgesList[i])
                    {
                        case "A":
                            finalSolutionEdges += letterA + " ";
                            break;
                        case "B":
                            finalSolutionEdges += letterB + " ";
                            break;
                        case "C":
                            finalSolutionEdges += letterC + " ";
                            break;
                        case "D":
                            finalSolutionEdges += letterD + " ";
                            break;
                        case "E":
                            finalSolutionEdges += letterE + " ";
                            break;
                        case "F":
                            finalSolutionEdges += letterF + " ";
                            break;
                        case "G":
                            finalSolutionEdges += letterG + " ";
                            break;
                        case "H":
                            finalSolutionEdges += letterH + " ";
                            break;
                        case "I":
                            finalSolutionEdges += letterI + " ";
                            break;
                        case "J":
                            finalSolutionEdges += letterJ + " ";
                            break;
                        case "L":
                            finalSolutionEdges += letterL + " ";
                            break;
                        case "M":
                            finalSolutionEdges += letterM + " ";
                            break;
                        case "N":
                            finalSolutionEdges += letterN + " ";
                            break;
                        case "O":
                            finalSolutionEdges += letterO + " ";
                            break;
                        case "P":
                            finalSolutionEdges += letterP + " ";
                            break;
                        case "Q":
                            finalSolutionEdges += letterQ + " ";
                            break;
                        case "R":
                            finalSolutionEdges += letterR + " ";
                            break;
                        case "S":
                            finalSolutionEdges += letterS + " ";
                            break;
                        case "T":
                            finalSolutionEdges += letterT + " ";
                            break;
                        case "V":
                            finalSolutionEdges += letterV + " ";
                            break;
                        case "W":
                            finalSolutionEdges += letterW + " ";
                            break;
                        case "X":
                            finalSolutionEdges += letterX + " ";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (edgesList[i])
                    {
                        case "A":
                            finalSolutionEdges += letterA + " ";
                            break;
                        case "B":
                            finalSolutionEdges += letterB2m + " ";
                            break;
                        case "C":
                            finalSolutionEdges += letterC2m + " ";
                            break;
                        case "D":
                            finalSolutionEdges += letterD2m + " ";
                            break;
                        case "E":
                            finalSolutionEdges += letterE2m + " ";
                            break;
                        case "F":
                            finalSolutionEdges += letterF2m + " ";
                            break;
                        case "G":
                            finalSolutionEdges += letterG2m + " ";
                            break;
                        case "H":
                            finalSolutionEdges += letterH2m + " ";
                            break;
                        case "I":
                            finalSolutionEdges += letterI2m + " ";
                            break;
                        case "J":
                            finalSolutionEdges += letterJ2m + " ";
                            break;
                        case "L":
                            finalSolutionEdges += letterL2m + " ";
                            break;
                        case "M":
                            finalSolutionEdges += letterM2m + " ";
                            break;
                        case "N":
                            finalSolutionEdges += letterN2m + " ";
                            break;
                        case "O":
                            finalSolutionEdges += letterO2m + " ";
                            break;
                        case "P":
                            finalSolutionEdges += letterP2m + " ";
                            break;
                        case "Q":
                            finalSolutionEdges += letterQ2m + " ";
                            break;
                        case "R":
                            finalSolutionEdges += letterR2m + " ";
                            break;
                        case "S":
                            finalSolutionEdges += letterS2m + " ";
                            break;
                        case "T":
                            finalSolutionEdges += letterT2m + " ";
                            break;
                        case "V":
                            finalSolutionEdges += letterV2m + " ";
                            break;
                        case "W":
                            finalSolutionEdges += letterW2m + " ";
                            break;
                        case "X":
                            finalSolutionEdges += letterX2m + " ";
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("Corners Length: " + edgesList.Count);
            Console.WriteLine("Corners String: " + cornersString);
            Console.WriteLine("Edges String Before: " + edgesString);
            Console.WriteLine("Edges String After: " + edgesStringAfter);

            edgesMoves = finalSolutionEdges.Count(Char.IsLetter);
            cornersMoves = finalSolutionCorners.Count(Char.IsLetter);
            parityMoves = parity2m.Count(Char.IsLetter);

            totalLettersCorners = lettersCorners.Count(Char.IsLetter);
            totalLettersEdges = lettersEdges.Count(Char.IsLetter);
            totalEM = edgesMoves.ToString();
            totalCM = cornersMoves.ToString();

            Console.WriteLine("-----Soutie finala-----\n\r");
            if (parityCheck == true)
            {
                Console.WriteLine("Edges:" + edgesMoves + "\r\n" + finalSolutionEdges + "\r\n" + "Parity: " + parityMoves + "\r\n" + parity2m + "\r\n" + "\r\nCorners: " + cornersMoves + "\r\n" + finalSolutionCorners);
                totalMovesBLD = edgesMoves + parityMoves + cornersMoves;
                Console.WriteLine("Total moves: " + totalMovesBLD);
                Console.WriteLine("\r\n Finala mare cu paritate: ");
                Console.WriteLine("---" + finalSolutionEdges + parity2m + finalSolutionCorners + "(" + totalMovesBLD + "f)");
            }
            else
            {
                Console.WriteLine("Edges:" + edgesMoves + "\r\n" + finalSolutionEdges + "\r\nCorners: " + cornersMoves + "\r\n" + finalSolutionCorners);
                totalMovesBLD = edgesMoves + cornersMoves;
                Console.WriteLine("Total moves: " + totalMovesBLD);
                Console.WriteLine("\r\n Finala mare fara paritate: ");
                Console.WriteLine("---" + finalSolutionEdges + finalSolutionCorners + "(" + totalMovesBLD + "f)");
            }
        }

    }
}
