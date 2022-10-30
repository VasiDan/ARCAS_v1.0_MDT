﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Kociemba
{
    class Search
    {
        public static readonly bool USE_TWIST_FLIP_PRUN = true;
        public static readonly int PARTIAL_INIT_LEVEL = 0;
        //Options for research purpose.
        internal static readonly bool TRY_PRE_MOVE = true;
        internal static readonly bool TRY_INVERSE = true;
        internal static readonly bool TRY_THREE_AXES = true;
        internal static readonly bool USE_CONJ_PRUN = USE_TWIST_FLIP_PRUN;

        internal static readonly int MAX_DEPTH2 = 13;

        internal static readonly int PRE_IDX_MAX = TRY_PRE_MOVE ? 9 : 1;

        internal static bool inited = false;

        private int[] move = new int[31];

        private int[,] corn0 = new int[6, PRE_IDX_MAX];
        private int[,] ud8e0 = new int[6, PRE_IDX_MAX];

        private CoordCube[] nodeUD = new CoordCube[21];
        private CoordCube[] nodeRL = new CoordCube[21];
        private CoordCube[] nodeFB = new CoordCube[21];

        private CoordCube[,] node0 = new CoordCube[6, PRE_IDX_MAX];

        private sbyte[] facelet = new sbyte[54];

        private long selfSym;
        private int preIdxMax;
        private int conjMask;
        private int urfIdx;
        private int preIdx;
        private int length1;
        private int depth1;
        private int maxDep2;
        private int sol;
        private string solution;
        private long probe;
        private long probeMax;
        private long probeMin;
        private int verbose;
        private CubieCube cc = new CubieCube();

        private bool isRec = false;

        // Verbose_Mask determines if a " . " separates the phase1 and phase2 parts of the solver string like in F' R B R L2 F .U2 U D for example.<br>

        public static readonly int USE_SEPARATOR = 0x1;

        // Verbose_Mask determines if the solution will be inversed to a scramble/state generator.
        public static readonly int INVERSE_SOLUTION = 0x2;

        // Verbose_Mask determines if a tag such as "(21f)" will be appended to the solution.

        public static readonly int APPEND_LENGTH = 0x4;

        // Verbose_Mask determines if guaranteeing the solution to be optimal.
        public static readonly int OPTIMAL_SOLUTION = 0x8;

        public Search()
        {
            for (int i = 0; i < 21; i++)
            {
                nodeUD[i] = new CoordCube();
                nodeRL[i] = new CoordCube();
                nodeFB[i] = new CoordCube();
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < PRE_IDX_MAX; j++)
                {
                    node0[i, j] = new CoordCube();
                }
            }
        }
        /// <summary>
        /// Computes the solver string for a given cube.
        /// </summary>
        /// <param name="facelets">
        ///      is the cube definition string format.<br>
        /// The names of the facelet positions of the cube:
        /// <pre>
        ///             |************|
        ///             |*U1**U2**U3*|
        ///             |************|
        ///             |*U4**U5**U6*|
        ///             |************|
        ///             |*U7**U8**U9*|
        ///             |************|
        /// ************|************|************|************|
        /// *L1**L2**L3*|*F1**F2**F3*|*R1**R2**F3*|*B1**B2**B3*|
        /// ************|************|************|************|
        /// *L4**L5**L6*|*F4**F5**F6*|*R4**R5**R6*|*B4**B5**B6*|
        /// ************|************|************|************|
        /// *L7**L8**L9*|*F7**F8**F9*|*R7**R8**R9*|*B7**B8**B9*|
        /// ************|************|************|************|
        ///             |************|
        ///             |*D1**D2**D3*|
        ///             |************|
        ///             |*D4**D5**D6*|
        ///             |************|
        ///             |*D7**D8**D9*|
        ///             |************|
        /// </pre>
        /// A cube definition string "UBL..." means for example: In position U1 we have the U-color, in position U2 we have the
        /// B-color, in position U3 we have the L color etc. For example, the "super flip" state is represented as <br>
        /// <pre>UBULURUFURURFRBRDRFUFLFRFDFDFDLDRDBDLULBLFLDLBUBRBLBDB</pre>
        /// and the state generated by "F U' F2 D' B U R' F' L D' R' U' L U B' D2 R' F U2 D2" can be represented as <br>
        /// <pre>FBLLURRFBUUFBRFDDFUULLFRDDLRFBLDRFBLUUBFLBDDBUURRBLDDR</pre>
        /// You can also use <seealso cref="cs.min2phase.Tools#fromScramble(java.lang.String s)"/> to convert the scramble string to the
        /// cube definition string.
        /// </param>
        /// <param name="maxDepth">
        ///      defines the maximal allowed maneuver length. For random cubes, a maxDepth of 21 usually will return a
        ///      solution in less than 0.02 seconds on average. With a maxDepth of 20 it takes about 0.1 seconds on average to find a
        ///      solution, but it may take much longer for specific cubes.
        /// </param>
        /// <param name="probeMax">
        ///      defines the maximum number of the probes of phase 2. If it does not return with a solution, it returns with
        ///      an error code.
        /// </param>
        /// <param name="probeMin">
        ///      defines the minimum number of the probes of phase 2. So, if a solution is found within given probes, the
        ///      computing will continue to find shorter solution(s). Btw, if probeMin > probeMax, probeMin will be set to probeMax.
        /// </param>
        /// <param name="verbose">
        ///      determins the format of the solution(s). see USE_SEPARATOR, INVERSE_SOLUTION, APPEND_LENGTH, OPTIMAL_SOLUTION
        /// </param>
        /// <returns> The solution string or an error code:<br>
        ///      Error 1: There is not exactly one facelet of each colour<br>
        ///      Error 2: Not all 12 edges exist exactly once<br>
        ///      Error 3: Flip error: One edge has to be flipped<br>
        ///      Error 4: Not all corners exist exactly once<br>
        ///      Error 5: Twist error: One corner has to be twisted<br>
        ///      Error 6: Parity error: Two corners or two edges have to be exchanged<br>
        ///      Error 7: No solution exists for the given maxDepth<br>
        ///      Error 8: Probe limit exceeded, no solution within given probMax </returns>

        private static readonly object syncLock = new object();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual string Solution(string facelets, int maxDepth, long probeMax, long probeMin, int verbose)
        {
            int check = Verify(facelets);
            if (check != 0)
            {
                return "Error " + Math.Abs(check);
            }
            sol = maxDepth + 1;
            probe = 0;
            this.probeMax = probeMax;
            this.probeMin = Math.Min(probeMin, probeMax);
            this.verbose = verbose;
            solution = null;
            isRec = false;

            Init();

            InitialSearch();
            Console.WriteLine("a trecut prin Solution");
            return (verbose & OPTIMAL_SOLUTION) == 0 ? SearchSol() : Searchopt();
        }


        private void InitialSearch()
        {
            conjMask = (TRY_INVERSE ? 0 : 0x38) | (TRY_THREE_AXES ? 0 : 0x36);
            CubieCube pc = new CubieCube();
            selfSym = cc.SelfSymmetry();
            if (selfSym >> 48 != 0)
            {
                conjMask |= 0x38;
            }
            if ((selfSym >> 16 & 0xffff) != 0)
            {
                conjMask |= 0x12;
            }
            if ((selfSym >> 32 & 0xffff) != 0)
            {
                conjMask |= 0x24;
            }
            preIdxMax = conjMask > 7 ? 1 : PRE_IDX_MAX;
            for (int i = 0; i < 6; i++)
            {
                node0[i, 0].Set(cc);
                corn0[i, 0] = cc.GetCPermSym();
                ud8e0[i, 0] = cc.GetU4Comb() << 16 | cc.GetD4Comb();
                if ((conjMask & 1 << i) == 0)
                {
                    for (int j = 1; j < preIdxMax; j++)
                    {
                        CubieCube.CornMult(CubieCube.moveCube[CubieCube.preMove[j]], cc, pc);
                        CubieCube.EdgeMult(CubieCube.moveCube[CubieCube.preMove[j]], cc, pc);
                        node0[i, j].Set(pc);
                        corn0[i, j] = pc.GetCPermSym();
                        ud8e0[i, j] = pc.GetU4Comb() << 16 | pc.GetD4Comb();
                    }
                }
                cc.URFConjugate();
                if (i % 3 == 2)
                {
                    cc.InvCubieCube();
                }
            }
            selfSym = selfSym & 0xffffffffffffL;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual string Next(long probeMax, long probeMin, int verbose)
        {
            probe = 0;
            this.probeMax = probeMax;
            this.probeMin = Math.Min(probeMin, probeMax);
            solution = null;
            isRec = (this.verbose & OPTIMAL_SOLUTION) == (verbose & OPTIMAL_SOLUTION);
            this.verbose = verbose;
            return (verbose & OPTIMAL_SOLUTION) == 0 ? SearchSol() : Searchopt();
        }

        public static bool IsInited()
        {
            return inited;
        }

        public long NumberOfProbes()
        {
            return probe;
        }

        public int Length()
        {
            return sol;
        }

        // for synchronized


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Init()
        {
            if (!inited)
            {
                CubieCube.InitMove();
                CubieCube.InitSym();
            }
            CoordCube.Init();

            inited = true;
        }

        public int Verify(string facelets)
        {
            int count = 0x000000;
            try
            {
                string center = new string(
                    new char[] {
                        facelets[Util.U5],
                        facelets[Util.R5],
                        facelets[Util.F5],
                        facelets[Util.D5],
                        facelets[Util.L5],
                        facelets[Util.B5]
                    }
                );
                for (int i = 0; i < 54; i++)
                {
                    facelet[i] = (sbyte)center.IndexOf(facelets[i]);
                    if (facelet[i] == -1)
                    {
                        return -1;
                    }
                    count += 1 << (facelet[i] << 2);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            if (count != 0x999999)
            {
                return -1;
            }
            Util.ToCubieCube(facelet, cc);
            return cc.Verify();
        }

        private string SearchSol()
        {
            // Console.WriteLine("a ajuns in search???");
            for (length1 = isRec ? length1 : 0; length1 < sol; length1++)
            {
                // Console.WriteLine("a ajuns in primul FOR??");
                maxDep2 = Math.Min(MAX_DEPTH2, sol - length1);
                for (urfIdx = isRec ? urfIdx : 0; urfIdx < 6; urfIdx++)
                {
                    // Console.WriteLine("ce cauta in al 222 for-ul asta?");
                    if ((conjMask & 1 << urfIdx) != 0)
                    {
                        continue;
                    }
                    for (preIdx = isRec ? preIdx : 0; preIdx < preIdxMax; preIdx++)
                    {
                        //  Console.WriteLine("ce cauta in al 333 for-ul asta?");
                        if (preIdx != 0 && preIdx % 2 == 0)
                        {
                            continue;
                        }
                        node0[urfIdx, preIdx].CalcPruning(true);
                        //Console.WriteLine("dupa NODE");
                        int ssym = (int)(0xffff & selfSym);

                        if (preIdx != 0)
                        {
                            ssym &= (int)CubieCube.moveCubeSym[CubieCube.preMove[preIdx]];
                            // Console.WriteLine(ssym);
                        }
                        depth1 = length1 - (preIdx == 0 ? 0 : 1);
                        if (node0[urfIdx, preIdx].prun <= depth1 && Phase1(node0[urfIdx, preIdx], ssym, depth1, -1) == 0)
                        {
                            //Console.WriteLine("sollll??");
                            return solution == null ? "Error 8" : solution;
                        }
                    }

                }
            }
            return solution == null ? "Error 7" : solution;
        }
        /// <summary>
        /// @return
        ///      0: Found or Probe limit exceeded
        ///      1: Try Next Power
        ///      2: Try Next Axis
        /// </summary>
        private int Phase1(CoordCube node, int ssym, int maxl, int lm)
        {
            if (node.prun == 0 && maxl < 5)
            {
                if (maxl == 0)
                {
                    int ret = InitPhase2();
                    if (ret == 0 || preIdx == 0)
                    {
                        return ret;
                    }
                    preIdx++;
                    ret = Math.Min(InitPhase2(), ret);
                    preIdx--;
                    return ret;
                }
                else
                {
                    return 1;
                }
            }

            int skipMoves = 0;
            int i = 1;
            for (int s = ssym; (s >>= 1) != 0; i++)
            {
                if ((s & 1) == 1)
                {
                    skipMoves |= CubieCube.firstMoveSym[i];
                }
            }

            for (int axis = 0; axis < 18; axis += 3)
            {
                if (axis == lm || axis == lm - 9)
                {
                    continue;
                }
                for (int power = 0; power < 3; power++)
                {
                    int m = axis + power;

                    if (isRec && m != move[depth1 - maxl] || skipMoves != 0 && (skipMoves & 1 << m) != 0)
                    {
                        continue;
                    }

                    int prun = nodeUD[maxl].DoMovePrun(node, m, true);
                    if (prun > maxl)
                    {
                        break;
                    }
                    else if (prun == maxl)
                    {
                        continue;
                    }

                    if (USE_CONJ_PRUN)
                    {
                        prun = nodeUD[maxl].DoMovePrunConj(node, m);
                        if (prun > maxl)
                        {
                            break;
                        }
                        else if (prun == maxl)
                        {
                            continue;
                        }
                    }

                    move[depth1 - maxl] = m;
                    int ret = Phase1(nodeUD[maxl], ssym & (int)CubieCube.moveCubeSym[m], maxl - 1, axis);
                    if (ret == 0)
                    {
                        return 0;
                    }
                    else if (ret == 2)
                    {
                        break;
                    }
                }
            }
            return 1;
        }

        private string Searchopt()
        {
            int maxprun1 = 0;
            int maxprun2 = 0;
            for (int i = 0; i < 6; i++)
            {
                node0[i, 0].CalcPruning(false);
                if (i < 3)
                {
                    maxprun1 = Math.Max(maxprun1, node0[i, 0].prun);
                }
                else
                {
                    maxprun2 = Math.Max(maxprun2, node0[i, 0].prun);
                }
            }
            urfIdx = maxprun2 > maxprun1 ? 3 : 0;
            preIdx = 0;
            for (length1 = isRec ? length1 : 0; length1 < sol; length1++)
            {
                CoordCube ud = node0[0 + urfIdx, 0];
                CoordCube rl = node0[1 + urfIdx, 0];
                CoordCube fb = node0[2 + urfIdx, 0];

                if (ud.prun <= length1 && rl.prun <= length1 && fb.prun <= length1 && Phase1opt(ud, rl, fb, selfSym, length1, -1) == 0)
                {
                    return solution == null ? "Error 8" : solution;
                }
            }
            return solution == null ? "Error 7" : solution;
        }

        /// <summary>
        /// @return
        ///      0: Found or Probe limit exceeded
        ///      1: Try Next Power
        ///      2: Try Next Axis
        /// </summary>

        private int Phase1opt(CoordCube ud, CoordCube rl, CoordCube fb, long ssym, int maxl, int lm)
        {
            if (ud.prun == 0 && rl.prun == 0 && fb.prun == 0 && maxl < 5)
            {
                maxDep2 = maxl + 1;
                depth1 = length1 - maxl;
                return InitPhase2() == 0 ? 0 : 1;
            }

            int skipMoves = 0;
            int i = 1;
            for (long s = ssym; (s >>= 1) != 0; i++)
            {
                if ((s & 1) == 1)
                {
                    skipMoves |= CubieCube.firstMoveSym[i];
                }
            }

            for (int axis = 0; axis < 18; axis += 3)
            {
                if (axis == lm || axis == lm - 9)
                {
                    continue;
                }
                for (int power = 0; power < 3; power++)
                {
                    int m = axis + power;

                    if (isRec && m != move[length1 - maxl] || skipMoves != 0 && (skipMoves & 1 << m) != 0)
                    {
                        continue;
                    }

                    // UD Axis
                    int prun_ud = nodeUD[maxl].DoMovePrun(ud, m, false);
                    if (prun_ud > maxl)
                    {
                        break;
                    }
                    else if (prun_ud == maxl)
                    {
                        continue;
                    }

                    // RL Axis
                    m = CubieCube.urfMove[2, m];

                    int prun_rl = nodeRL[maxl].DoMovePrun(rl, m, false);
                    if (prun_rl > maxl)
                    {
                        break;
                    }
                    else if (prun_rl == maxl)
                    {
                        continue;
                    }

                    // FB Axis
                    m = CubieCube.urfMove[2, m];

                    int prun_fb = nodeFB[maxl].DoMovePrun(fb, m, false);
                    if (prun_ud == prun_rl && prun_rl == prun_fb && prun_fb != 0)
                    {
                        prun_fb++;
                    }

                    if (prun_fb > maxl)
                    {
                        break;
                    }
                    else if (prun_fb == maxl)
                    {
                        continue;
                    }

                    m = CubieCube.urfMove[2, m];

                    move[length1 - maxl] = m;
                    int ret = Phase1opt(nodeUD[maxl], nodeRL[maxl], nodeFB[maxl], ssym & CubieCube.moveCubeSym[m], maxl - 1, axis);
                    if (ret == 0)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// @return
        ///      0: Found or Probe limit exceeded
        ///      1: Try Next Power
        ///      2: Try Next Axis
        /// </summary>

        private int InitPhase2()
        {
            isRec = false;
            if (probe >= (solution == null ? probeMax : probeMin))
            {
                return 0;
            }
            ++probe;
            int cidx = corn0[urfIdx, preIdx] >> 4;
            int csym = corn0[urfIdx, preIdx] & 0xf;
            int mid = node0[urfIdx, preIdx].slice;
            for (int i = 0; i < depth1; i++)
            {
                int m = move[i];
                cidx = CoordCube.CPermMove[cidx, Util.std2ud[CubieCube.SymMove[csym, m]]];
                csym = CubieCube.SymMult[cidx & 0xf, csym];
                cidx >>= 4;

                int cx = CoordCube.UDSliceMove[mid & 0x1ff, m];
                mid = Util.permMult[mid >> 9, cx >> 9] << 9 | cx & 0x1ff;
            }
            mid >>= 9;
            int prun = CoordCube.GetPruning(CoordCube.MCPermPrun, cidx * 24 + CoordCube.MPermConj[mid, csym]);
            if (prun >= maxDep2)
            {
                return prun > maxDep2 ? 2 : 1;
            }

            int u4e = ud8e0[urfIdx, preIdx] >> 16;
            int d4e = ud8e0[urfIdx, preIdx] & 0xffff;
            for (int i = 0; i < depth1; i++)
            {
                int m = move[i];

                int cx = CoordCube.UDSliceMove[u4e & 0x1ff, m];
                u4e = Util.permMult[u4e >> 9, cx >> 9] << 9 | cx & 0x1ff;

                cx = CoordCube.UDSliceMove[d4e & 0x1ff, m];
                d4e = Util.permMult[d4e >> 9, cx >> 9] << 9 | cx & 0x1ff;
            }

            int edge = CubieCube.MtoEPerm[494 - (u4e & 0x1ff) + (u4e >> 9) * 70 + (d4e >> 9) * 1680];
            int esym = edge & 0xf;
            edge >>= 4;

            prun = Math.Max(prun, Math.Max(
                CoordCube.GetPruning(CoordCube.MEPermPrun,
                edge * 24 + CoordCube.MPermConj[mid, esym]),
                CoordCube.GetPruning(CoordCube.EPermCCombPrun,
                edge * 70 + CoordCube.CCombConj[CubieCube.Perm2Comb[cidx], CubieCube.SymMultInv[esym, csym]])));

            if (prun >= maxDep2)
            {
                return prun > maxDep2 ? 2 : 1;
            }

            int lm = 10;
            if (depth1 >= 2 && move[depth1 - 1] / 3 % 3 == move[depth1 - 2] / 3 % 3)
            {
                lm = Util.std2ud[Math.Max(move[depth1 - 1], move[depth1 - 2]) / 3 * 3 + 1];
            }
            else if (depth1 >= 1)
            {
                lm = Util.std2ud[move[depth1 - 1] / 3 * 3 + 1];
                if (move[depth1 - 1] > Util.Fx3)
                {
                    lm = -lm;
                }
            }

            int depth2;
            for (depth2 = maxDep2 - 1; depth2 >= prun; depth2--)
            {
                int ret = Phase2(edge, esym, cidx, csym, mid, depth2, depth1, lm);
                if (ret < 0)
                {
                    break;
                }
                depth2 = depth2 - ret;
                sol = depth1 + depth2;
                if (preIdx != 0)
                {
                    Debug.Assert(depth2 > 0); //If depth2 == 0, the solution is optimal. In this case, we won't try preScramble to find shorter solutions.
                    int axisPre = Util.preMove[preIdx] / 3;
                    int axisLast = move[sol - 1] / 3;
                    if (axisPre == axisLast)
                    {
                        int pow = (Util.preMove[preIdx] % 3 + move[sol - 1] % 3 + 1) % 4;
                        move[sol - 1] = axisPre * 3 + pow;
                    }
                    else if (depth2 > 1 && axisPre % 3 == axisLast % 3 && move[sol - 2] / 3 == axisPre)
                    {
                        int pow = (Util.preMove[preIdx] % 3 + move[sol - 2] % 3 + 1) % 4;
                        move[sol - 2] = axisPre * 3 + pow;
                    }
                    else
                    {
                        move[sol++] = Util.preMove[preIdx];
                    }
                }
                solution = SolutionToString();
            }
            if (depth2 != maxDep2 - 1)
            { //At least one solution has been found.
                maxDep2 = Math.Min(MAX_DEPTH2, sol - length1);
                return probe >= probeMin ? 0 : 1;
            }
            else
            {
                return 1;
            }
        }

        //-1: no solution found
        // X: solution with X moves shorter than expectation. Hence, the length of the solution is  depth - X

        private int Phase2(int eidx, int esym, int cidx, int csym, int mid, int maxl, int depth, int lm)
        {
            if (eidx == 0 && cidx == 0 && mid == 0)
            {
                return maxl;
            }
            int moveMask = lm < 0 ? (1 << (-lm)) : Util.ckmv2bit[lm];
            for (int m = 0; m < 10; m++)
            {
                if ((moveMask >> m & 1) != 0)
                {
                    m += 0x42 >> m & 3;
                    continue;
                }
                int midx = CoordCube.MPermMove[mid, m];
                int cidxx = CoordCube.CPermMove[cidx, CubieCube.SymMoveUD[csym, m]];
                int csymx = CubieCube.SymMult[cidxx & 0xf, csym];
                cidxx >>= 4;
                if (CoordCube.GetPruning(CoordCube.MCPermPrun, cidxx * 24 + CoordCube.MPermConj[midx, csymx]) >= maxl)
                {
                    continue;
                }
                int eidxx = CoordCube.EPermMove[eidx, CubieCube.SymMoveUD[esym, m]];
                int esymx = CubieCube.SymMult[eidxx & 0xf, esym];
                eidxx >>= 4;
                if (CoordCube.GetPruning(CoordCube.EPermCCombPrun, eidxx * 70 + CoordCube.CCombConj[CubieCube.Perm2Comb[cidxx], CubieCube.SymMultInv[esymx, csymx]]) >= maxl)
                {
                    continue;
                }
                if (CoordCube.GetPruning(CoordCube.MEPermPrun, eidxx * 24 + CoordCube.MPermConj[midx, esymx]) >= maxl)
                {
                    continue;
                }
                int eidxi = CubieCube.GetPermSymInv(eidxx, esymx, false);
                int cidxi = CubieCube.GetPermSymInv(cidxx, csymx, true);
                if (CoordCube.GetPruning(CoordCube.EPermCCombPrun, (eidxi >> 4) * 70 + CoordCube.CCombConj[CubieCube.Perm2Comb[cidxi >> 4], CubieCube.SymMultInv[eidxi & 0xf, cidxi & 0xf]]) >= maxl)
                {
                    continue;
                }

                int ret = Phase2(eidxx, esymx, cidxx, csymx, midx, maxl - 1, depth + 1, (lm < 0 && m + lm == -5) ? -lm : m);
                if (ret >= 0)
                {
                    move[depth] = Util.ud2std[m];
                    return ret;
                }
            }
            return -1;
        }

        private string SolutionToString()
        {
            StringBuilder sb = new StringBuilder();
            int urf = (verbose & INVERSE_SOLUTION) != 0 ? (urfIdx + 3) % 6 : urfIdx;
            if (urf < 3)
            {
                for (int s = 0; s < sol; s++)
                {
                    if ((verbose & USE_SEPARATOR) != 0 && s == depth1)
                    {
                        sb.Append(".  ");
                    }
                    sb.Append(Util.move2str[CubieCube.urfMove[urf, move[s]]]).Append(' ');
                }
            }
            else
            {
                for (int s = sol - 1; s >= 0; s--)
                {
                    sb.Append(Util.move2str[CubieCube.urfMove[urf, move[s]]]).Append(' ');
                    if ((verbose & USE_SEPARATOR) != 0 && s == depth1)
                    {
                        sb.Append(".  ");
                    }
                }
            }
            if ((verbose & APPEND_LENGTH) != 0)
            {
                sb.Append("(").Append(sol).Append("f)");
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

    }
}
