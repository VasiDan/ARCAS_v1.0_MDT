using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Kociemba
{
    class Tools
    {
        private static Random gen = new Random();

        private static void ReadFully(sbyte[] arr, BinaryReader input)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                arr[i] = input.ReadSByte();
                // Console.WriteLine("Read00 ++" + arr[i]);
            }
        }

        private static void Read(char[] arr, BinaryReader input)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                arr[i] = input.ReadChar();
                // Console.WriteLine("Read0 ++" + i + ": " + arr[i]);
            }
        }

        private static void Read(int[] arr, BinaryReader input)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                arr[i] = input.ReadInt32();
                //Console.WriteLine("Read1 ++" + i + ": " + arr[i]);
            }
        }

        private static void Read(char[,] arr, BinaryReader input)
        {
            for (int i = 0, leng = arr.GetLength(0); i < leng; i++)
            {
                for (int j = 0, len = arr.GetLength(1); j < len; j++)
                {
                    arr[i, j] = input.ReadChar();
                    // Console.WriteLine("Read2 ++"+i+","+j+": " + arr[i,j]);
                }
            }
        }

        private static void WriteFully(sbyte[] arr, BinaryWriter output)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                output.Write(arr[i]);
                //  Console.WriteLine("Write1 ++" + i + ": " + arr[i].ToString());
            }
        }

        private static void Write(char[] arr, BinaryWriter output)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                output.Write(arr[i]);
                //Console.WriteLine("Write1 ++" + i + ": " + arr[i].ToString());
            }
        }

        private static void Write(int[] arr, BinaryWriter output)
        {
            for (int i = 0, len = arr.Length; i < len; i++)
            {
                output.Write(arr[i]);
                //  Console.WriteLine("Write2 ++" + i + ": " + arr[i].ToString());
            }

        }


        private static void Write(char[,] arr, BinaryWriter output)
        {
            for (int i = 0, leng = arr.GetLength(0); i < leng; i++)
            {
                for (int j = 0, len = arr.GetLength(1); j < len; j++)
                {
                    output.Write(arr[i, j]);
                    //  Console.WriteLine("Write3 ++" + i+","+j + ": " + arr[i,j].ToString());
                }
            }
        }

        protected internal Tools()
        {
        }

        /// <summary>
        /// initializing from cached tables(move table, pruning table, etc.)
        /// </summary>       

        public static void InitFrom(BinaryReader input)
        {
            Console.WriteLine("A intrat in initForm");
            if (Search.inited)
            {
                return;
            }
            CubieCube.InitMove();
            CubieCube.InitSym();
            Read(CubieCube.FlipS2R, input);
            Read(CubieCube.TwistS2R, input);
            Read(CubieCube.EPermS2R, input);
            Read(CubieCube.MtoEPerm, input);
            ReadFully(CubieCube.Perm2Comb, input);
            Read(CoordCube.TwistMove, input);
            Read(CoordCube.FlipMove, input);
            Read(CoordCube.UDSliceMove, input);
            Read(CoordCube.UDSliceConj, input);
            Read(CoordCube.CPermMove, input);
            Read(CoordCube.EPermMove, input);
            Read(CoordCube.MPermMove, input);
            Read(CoordCube.MPermConj, input);
            Read(CoordCube.CCombMove, input);
            Read(CoordCube.CCombConj, input);
            Read(CoordCube.MCPermPrun, input);
            Read(CoordCube.MEPermPrun, input);
            Read(CoordCube.EPermCCombPrun, input);
            Read(CoordCube.UDSliceTwistPrun, input);
            Read(CoordCube.UDSliceFlipPrun, input);
            if (Search.USE_TWIST_FLIP_PRUN)
            {
                Read(CubieCube.FlipS2RF, input);
                Read(CoordCube.TwistFlipPrun, input);
            }
            Search.inited = true;
            Console.WriteLine("A iesit din initForm");
        }

        public static void SaveTo(BinaryWriter output)
        {
            Console.WriteLine("A intrat in saveTo");
            Search.Init();
            Write(CubieCube.FlipS2R, output); //          672
            Write(CubieCube.TwistS2R, output); // +        648
            Write(CubieCube.EPermS2R, output); // +      5,536
            Write(CubieCube.MtoEPerm, output); // +     80,640
            WriteFully(CubieCube.Perm2Comb, output); // +      2,768
            Write(CoordCube.TwistMove, output); // +     11,664
            Write(CoordCube.FlipMove, output); // +     12,096
            Write(CoordCube.UDSliceMove, output); // +     17,820
            Write(CoordCube.UDSliceConj, output); // +      7,920
            Write(CoordCube.CPermMove, output); // +     99,648
            Write(CoordCube.EPermMove, output); // +     55,360
            Write(CoordCube.MPermMove, output); // +        480
            Write(CoordCube.MPermConj, output); // +        768
            Write(CoordCube.CCombMove, output);                // +      1,400
            Write(CoordCube.CCombConj, output); // +      2,240
            Write(CoordCube.MCPermPrun, output); // +     33,216
            Write(CoordCube.MEPermPrun, output); // +     33,216
            Write(CoordCube.EPermCCombPrun, output); // +     96,880                                //
            Write(CoordCube.UDSliceTwistPrun, output); // +     80,192
            Write(CoordCube.UDSliceFlipPrun, output); // +     83,160
            if (Search.USE_TWIST_FLIP_PRUN)
            { // =    626,324 Bytes
                Write(CubieCube.FlipS2RF, output); // +      5,376
                Write(CoordCube.TwistFlipPrun, output); // +    331,776
            } // =    963,476 Bytes            
            Console.WriteLine("A iesit din saveTo");
        }

        // Set Random Source
        public static void SetRandomSource(Random gen)
        {
            Tools.gen = gen;
        }

        public static string RandomCube()
        {
            return RandomState(STATE_RANDOM, STATE_RANDOM, STATE_RANDOM, STATE_RANDOM);
        }

        private static int ResolveOri(sbyte[] arr, int basevar)
        {
            int sum = 0, idx = 0, lastUnknown = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == -1)
                {
                    arr[i] = (sbyte)gen.Next(basevar);
                    lastUnknown = i;
                }
                sum += arr[i];
            }
            if (sum % basevar != 0 && lastUnknown != -1)
            {
                arr[lastUnknown] = (sbyte)((30 + arr[lastUnknown] - sum) % basevar);
            }
            for (int i = 0; i < arr.Length - 1; i++)
            {
                idx *= basevar;
                idx += arr[i];
            }
            return idx;
        }

        private static int CountUnknown(sbyte[] arr)
        {
            if (arr == STATE_SOLVED)
            {
                return 0;
            }
            int cnt = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == -1)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        private static int ResolvePerm(sbyte[] arr, int cntU, int parity)
        {
            if (arr == STATE_SOLVED)
            {
                return 0;
            }
            else if (arr == STATE_RANDOM)
            {
                return parity == -1 ? gen.Next(2) : parity;
            }
            sbyte[] val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != -1)
                {
                    val[arr[i]] = -1;
                }
            }
            int idx = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (val[i] != -1)
                {
                    int j = gen.Next(idx + 1);
                    sbyte temp = val[i];
                    val[idx++] = val[j];
                    val[j] = temp;
                }
            }
            int last = -1;
            for (idx = 0; idx < arr.Length && cntU > 0; idx++)
            {
                if (arr[idx] == -1)
                {
                    if (cntU == 2)
                    {
                        last = idx;
                    }
                    arr[idx] = val[--cntU];
                }
            }
            int p = Util.GetNParity(GetNPerm(arr, arr.Length), arr.Length);
            if (p == 1 - parity && last != -1)
            {
                sbyte temp = arr[idx - 1];
                arr[idx - 1] = arr[last];
                arr[last] = temp;
            }
            return p;
        }

        internal static int GetNPerm(sbyte[] arr, int n)
        {
            int idx = 0;
            for (int i = 0; i < n; i++)
            {
                idx *= (n - i);
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[i])
                    {
                        idx++;
                    }
                }
            }
            return idx;
        }

        protected internal static readonly sbyte[] STATE_RANDOM = null;
        protected internal static readonly sbyte[] STATE_SOLVED = new sbyte[0];

        protected internal static string RandomState(sbyte[] cp, sbyte[] co, sbyte[] ep, sbyte[] eo)
        {
            int parity;
            int cntUE = ep == STATE_RANDOM ? 12 : CountUnknown(ep);
            int cntUC = cp == STATE_RANDOM ? 8 : CountUnknown(cp);
            int cpVal, epVal;
            if (cntUE < 2)
            { //ep != STATE_RANDOM
                if (ep == STATE_SOLVED)
                {
                    epVal = parity = 0;
                }
                else
                {
                    parity = ResolvePerm(ep, cntUE, -1);
                    epVal = GetNPerm(ep, 12);
                }
                if (cp == STATE_SOLVED)
                {
                    cpVal = 0;
                }
                else if (cp == STATE_RANDOM)
                {
                    do
                    {
                        cpVal = gen.Next(40320);
                    } while (Util.GetNParity(cpVal, 8) != parity);
                }
                else
                {
                    ResolvePerm(cp, cntUC, parity);
                    cpVal = GetNPerm(cp, 8);
                }
            }
            else
            { //ep != STATE_SOLVED
                if (cp == STATE_SOLVED)
                {
                    cpVal = parity = 0;
                }
                else if (cp == STATE_RANDOM)
                {
                    cpVal = gen.Next(40320);
                    parity = Util.GetNParity(cpVal, 8);
                }
                else
                {
                    parity = ResolvePerm(cp, cntUC, -1);
                    cpVal = GetNPerm(cp, 8);
                }
                if (ep == STATE_RANDOM)
                {
                    do
                    {
                        epVal = gen.Next(479001600);
                    } while (Util.GetNParity(epVal, 12) != parity);
                }
                else
                {
                    ResolvePerm(ep, cntUE, parity);
                    epVal = GetNPerm(ep, 12);
                }
            }
            return Util.ToFaceCube(new CubieCube(cpVal, co == STATE_RANDOM ? gen.Next(2187) : (co == STATE_SOLVED ? 0 : ResolveOri(co, 3)), epVal, eo == STATE_RANDOM ? gen.Next(2048) : (eo == STATE_SOLVED ? 0 : ResolveOri(eo, 2))));
        }

        public static String RandomLastLayer()
        {
            return RandomState(
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0 },
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7, 8, 9, 10, 11 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0 });
        }

        public static String RandomLastSlot()
        {
            return RandomState(
                       new sbyte[] { -1, -1, -1, -1, -1, 5, 6, 7 },
                       new sbyte[] { -1, -1, -1, -1, -1, 0, 0, 0 },
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7, -1, 9, 10, 11 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0, -1, 0, 0, 0 });
        }

        public static String RandomZBLastLayer()
        {
            return RandomState(
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0 },
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7, 8, 9, 10, 11 },
                       STATE_SOLVED);
        }

        public static String RandomCornerOfLastLayer()
        {
            return RandomState(
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0 },
                       STATE_SOLVED,
                       STATE_SOLVED);
        }

        public static String RandomEdgeOfLastLayer()
        {
            return RandomState(
                       STATE_SOLVED,
                       STATE_SOLVED,
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7, 8, 9, 10, 11 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0 });
        }

        public static String RandomCrossSolved()
        {
            return RandomState(
                       STATE_RANDOM,
                       STATE_RANDOM,
                       new sbyte[] { -1, -1, -1, -1, 4, 5, 6, 7, -1, -1, -1, -1 },
                       new sbyte[] { -1, -1, -1, -1, 0, 0, 0, 0, -1, -1, -1, -1 });
        }

        public static String RandomEdgeSolved()
        {
            return RandomState(
                       STATE_RANDOM,
                       STATE_RANDOM,
                       STATE_SOLVED,
                       STATE_SOLVED);
        }

        public static String RandomCornerSolved()
        {
            return RandomState(
                       STATE_SOLVED,
                       STATE_SOLVED,
                       STATE_RANDOM,
                       STATE_RANDOM);
        }

        public static string SuperFlip()
        {
            return Util.ToFaceCube(new CubieCube(0, 0, 0, 2047));
        }


        public static string FfromScramble(int[] scramble)
        {
            CubieCube c1 = new CubieCube();
            CubieCube c2 = new CubieCube();
            CubieCube tmp;
            for (int i = 0; i < scramble.Length; i++)
            {
                CubieCube.CornMult(c1, CubieCube.moveCube[scramble[i]], c2);
                CubieCube.EdgeMult(c1, CubieCube.moveCube[scramble[i]], c2);
                tmp = c1;
                c1 = c2;
                c2 = tmp;
            }
            return Util.ToFaceCube(c1);
        }

        /**
    * Convert a scramble string to its cube definition string.
    *
    * @param s  scramble string. Only outer moves (U, R, F, D, L, B, U2, R2, ...) are recognized.
    * @return cube definition string, which represent the state generated by the scramble<br>
    */
        public static String fromScramble(String s)
        {
            int[] arr = new int[s.Length];
            int j = 0;
            int axis = -1;
            for (int i = 0, length = s.Length; i < length; i++)
            {
                switch (s[i])
                {
                    case 'U': axis = 0; break;
                    case 'R': axis = 3; break;
                    case 'F': axis = 6; break;
                    case 'D': axis = 9; break;
                    case 'L': axis = 12; break;
                    case 'B': axis = 15; break;
                    case ' ':
                        if (axis != -1)
                        {
                            arr[j++] = axis;
                        }
                        axis = -1;
                        break;
                    case '2': axis++; break;
                    case '\'': axis += 2; break;
                    default: continue;
                }

            }
            if (axis != -1) arr[j++] = axis;
            int[] ret = new int[j];
            while (--j >= 0)
            {
                ret[j] = arr[j];
            }
            return FfromScramble(ret);
        }

        /**
         * Check whether the cube definition string s represents a solvable cube.
         *
         * @param facelets is the cube definition string , see {@link cs.min2phase.Search#solution(java.lang.String facelets, int maxDepth, long timeOut, long timeMin, int verbose)}
         * @return 0: Cube is solvable<br>
         *         -1: There is not exactly one facelet of each colour<br>
         *         -2: Not all 12 edges exist exactly once<br>
         *         -3: Flip error: One edge has to be flipped<br>
         *         -4: Not all 8 corners exist exactly once<br>
         *         -5: Twist error: One corner has to be twisted<br>
         *         -6: Parity error: Two corners or two edges have to be exchanged
         */
        public static int Verify(string facelets)
        {
            return new Search().Verify(facelets);
        }



    }
}
