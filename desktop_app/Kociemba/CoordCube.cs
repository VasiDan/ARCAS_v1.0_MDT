using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Kociemba
{
    class CoordCube
    {
        internal static int N_MOVES = 18;
        internal static int N_MOVES2 = 10;

        internal static int N_SLICE = 495;
        internal static int N_TWIST = 2187;
        internal static int N_TWIST_SYM = 324;
        internal static int N_FLIP = 2048;
        internal static int N_FLIP_SYM = 336;
        internal static int N_PERM = 40320;
        internal static int N_PERM_SYM = 2768;
        internal static int N_MPERM = 24;
        internal static int N_COMB = 70;

        //XMove = Move Table
        //XPrun = Pruning Table
        //XConj = Conjugate Table

        // phase1
        internal static char[,] UDSliceMove = new char[N_SLICE, N_MOVES];
        internal static char[,] TwistMove = new char[N_TWIST_SYM, N_MOVES];
        internal static char[,] FlipMove = new char[N_FLIP_SYM, N_MOVES];
        internal static char[,] UDSliceConj = new char[N_SLICE, 8];
        internal static int[] UDSliceTwistPrun = new int[N_SLICE * N_TWIST_SYM / 8 + 1];
        internal static int[] UDSliceFlipPrun = new int[N_SLICE * N_FLIP_SYM / 8 + 1];
        internal static int[] TwistFlipPrun = Search.USE_TWIST_FLIP_PRUN ? new int[N_FLIP * N_TWIST_SYM / 8 + 1] : null;

        // phase2
        internal static char[,] CPermMove = new char[N_PERM_SYM, N_MOVES];
        internal static char[,] EPermMove = new char[N_PERM_SYM, N_MOVES2];
        internal static char[,] MPermMove = new char[N_MPERM, N_MOVES2];
        internal static char[,] MPermConj = new char[N_MPERM, 16];
        internal static char[,] CCombMove = new char[N_COMB, N_MOVES2];
        internal static char[,] CCombConj = new char[N_COMB, 16];
        internal static int[] MCPermPrun = new int[N_MPERM * N_PERM_SYM / 8 + 1];
        internal static int[] MEPermPrun = new int[N_MPERM * N_PERM_SYM / 8 + 1];
        internal static int[] EPermCCombPrun = new int[N_COMB * N_PERM_SYM / 8 + 1];

        // 0: not initialized, 1: partially initialized, 2: finished

        internal static int initLevel = 0;

        internal static void Init()
        {
            if (initLevel == 2)
            {
                return;
            }
            if (initLevel == 0)
            {
                CubieCube.InitPermSym2Raw();

                InitCPermMove();
                InitEPermMove();
                InitMPermMoveConj();
                InitCombMoveConj();

                CubieCube.InitFlipSym2Raw();
                CubieCube.InitTwistSym2Raw();
                InitFlipMove();
                InitTwistMove();
                InitUDSliceMoveConj();
            }
            if (InitPruning(initLevel == 0))
            {
                initLevel = 2;
            }
            else
            {
                initLevel = 1;
            }
        }

        internal static bool InitPruning(bool isFirst)
        {
            bool initedPrun = true;
            initedPrun = (initedPrun || isFirst) && InitMEPermPrun();
            initedPrun = (initedPrun || isFirst) && InitMCPermPrun();
            initedPrun = (initedPrun || isFirst) && InitPermCombPrun();
            initedPrun = (initedPrun || isFirst) && InitSliceTwistPrun();
            initedPrun = (initedPrun || isFirst) && InitSliceFlipPrun();
            if (Search.USE_TWIST_FLIP_PRUN)
            {
                initedPrun = (initedPrun || isFirst) && InitTwistFlipPrun();
            }
            // Console.WriteLine(initedPrun);
            return initedPrun;
        }

        internal static void SetPruning(int[] table, int index, int value)
        {
            // table[index >> 3] = (int)Math.Pow(table[index >> 3], value << ((index & 7) << 2));
            table[index >> 3] ^= value << ((index & 7) << 2);
            //Console.WriteLine(index + "+++" + value + "\n\r");
        }

        internal static int GetPruning(int[] table, int index)
        {
            // Console.Write(UDSliceMove);
            //   Console.WriteLine(table + "+++" + index + "\n\r");
            return table[index >> 3] >> ((index & 7) << 2) & 0xf;
        }

        internal static void InitUDSliceMoveConj()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_SLICE; i++)
            {
                c.SetUDSlice(i);
                for (int j = 0; j < N_MOVES; j += 3)
                {
                    CubieCube.EdgeMult(c, CubieCube.moveCube[j], d);
                    UDSliceMove[i, j] = (char)d.GetUDSlice();
                }
                for (int j = 0; j < 16; j += 2)
                {
                    CubieCube.EdgeConjugate(c, CubieCube.SymMultInv[0, j], d);
                    UDSliceConj[i, j >> 1] = (char)(d.GetUDSlice() & 0x1ff);
                }
            }
            for (int i = 0; i < N_SLICE; i++)
            {
                for (int j = 0; j < N_MOVES; j += 3)
                {
                    int udslice = UDSliceMove[i, j];
                    for (int k = 1; k < 3; k++)
                    {
                        int cx = UDSliceMove[udslice & 0x1ff, j];
                        udslice = Util.permMult[udslice >> 9, cx >> 9] << 9 | cx & 0x1ff;
                        UDSliceMove[i, j + k] = (char)udslice;
                    }
                }
            }
        }

        internal static void InitFlipMove()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_FLIP_SYM; i++)
            {
                c.SetFlip(CubieCube.FlipS2R[i]);
                for (int j = 0; j < N_MOVES; j++)
                {
                    CubieCube.EdgeMult(c, CubieCube.moveCube[j], d);
                    FlipMove[i, j] = (char)d.GetFlipSym();
                }
            }
        }

        internal static void InitTwistMove()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_TWIST_SYM; i++)
            {
                c.SetTwist(CubieCube.TwistS2R[i]);
                for (int j = 0; j < N_MOVES; j++)
                {
                    CubieCube.CornMult(c, CubieCube.moveCube[j], d);
                    TwistMove[i, j] = (char)d.GetTwistSym();
                }
            }
        }

        internal static void InitCPermMove()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_PERM_SYM; i++)
            {
                c.SetCPerm(CubieCube.EPermS2R[i]);
                for (int j = 0; j < N_MOVES; j++)
                {
                    CubieCube.CornMult(c, CubieCube.moveCube[Util.ud2std[j]], d);
                    CPermMove[i, j] = (char)d.GetCPermSym();
                }
            }
        }

        internal static void InitEPermMove()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_PERM_SYM; i++)
            {
                c.SetEPerm(CubieCube.EPermS2R[i]);
                for (int j = 0; j < N_MOVES2; j++)
                {
                    CubieCube.EdgeMult(c, CubieCube.moveCube[Util.ud2std[j]], d);
                    EPermMove[i, j] = (char)d.getEPermSym();
                }
            }
        }

        internal static void InitMPermMoveConj()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_MPERM; i++)
            {
                c.SetMPerm(i);
                for (int j = 0; j < N_MOVES2; j++)
                {
                    CubieCube.EdgeMult(c, CubieCube.moveCube[Util.ud2std[j]], d);
                    MPermMove[i, j] = (char)d.GetMPerm();
                }
                for (int j = 0; j < 16; j++)
                {
                    CubieCube.EdgeConjugate(c, CubieCube.SymMultInv[0, j], d);
                    MPermConj[i, j] = (char)d.GetMPerm();
                }
            }
        }

        internal static void InitCombMoveConj()
        {
            CubieCube c = new CubieCube();
            CubieCube d = new CubieCube();
            for (int i = 0; i < N_COMB; i++)
            {
                c.SetCComb(i);
                for (int j = 0; j < N_MOVES2; j++)
                {
                    CubieCube.CornMult(c, CubieCube.moveCube[Util.ud2std[j]], d);
                    CCombMove[i, j] = (char)d.GetCComb();
                }
                for (int j = 0; j < 16; j++)
                {
                    CubieCube.CornConjugate(c, CubieCube.SymMultInv[0, j], d);
                    CCombConj[i, j] = (char)d.GetCComb();
                }
            }
        }

        internal static bool HasZero(int val)
        {
            return ((val - 0x11111111) & ~val & 0x88888888) != 0;
        }

        internal static bool InitRawSymPrun(int[] PrunTable, char[,] RawMove, char[,] RawConj, char[,] SymMove, char[] SymState, int PrunFlag)
        {
            int SYM_SHIFT = PrunFlag & 0xf;
            int SYM_E2C_MAGIC = ((PrunFlag >> 4) & 1) == 1 ? CubieCube.SYM_E2C_MAGIC : 0x00000000;
            bool IS_PHASE2 = ((PrunFlag >> 5) & 1) == 1;
            int INV_DEPTH = PrunFlag >> 8 & 0xf;
            int MAX_DEPTH = PrunFlag >> 12 & 0xf;
            int MIN_DEPTH = PrunFlag >> 16 & 0xf;

            int SYM_MASK = (1 << SYM_SHIFT) - 1;
            bool ISTFP = RawMove == null;
            int N_RAW = ISTFP ? N_FLIP : RawMove.GetLength(0);
            int N_SIZE = N_RAW * SymMove.GetLength(0);
            int N_MOVES = IS_PHASE2 ? 10 : 18;
            int NEXT_AXIS_MAGIC = N_MOVES == 10 ? 0x42 : 0x92492;
            /* Console.WriteLine(SYM_SHIFT+"\n\r"+ SYM_E2C_MAGIC + "\n\r" +
                 IS_PHASE2 + "\n\r" + INV_DEPTH + "\n\r" + MAX_DEPTH +
                 "\n\r" + MIN_DEPTH + "\n\r" + SYM_MASK + "\n\r" + N_RAW + "\n\r" +
                 N_SIZE + "\n\r"+N_MOVES + "\n\r"+NEXT_AXIS_MAGIC + "\n\r");
                 */
            // Console.WriteLine(N_RAW + "---" + N_SIZE);
            // Console.WriteLine(PrunTable);
            int depth = GetPruning(PrunTable, N_SIZE) - 1;
            int done = 0;

            if (depth == -1)
            {
                for (int i = 0; i < N_SIZE / 8 + 1; i++)
                {
                    PrunTable[i] = 0x11111111;
                }
                //  Console.WriteLine("in");
                SetPruning(PrunTable, 0, 0 ^ 1);
                // Console.WriteLine("dupa");
                depth = 0;
                done = 1;
            }

            int SEARCH_DEPTH = Search.PARTIAL_INIT_LEVEL > 0 ?
                Math.Min(Math.Max(depth + 1, MIN_DEPTH), MAX_DEPTH) : MAX_DEPTH;

            while (depth < SEARCH_DEPTH)
            {
                int mask = (int)((depth + 1) * 0x11111111 ^ 0xffffffff);
                //Console.WriteLine(mask);
                for (int i = 0; i < PrunTable.Length; i++)
                {
                    int value = PrunTable[i] ^ mask;
                    value &= value >> 1;
                    PrunTable[i] += value & (value >> 2) & 0x11111111;
                    // Console.WriteLine(PrunTable[i]);
                }

                bool inv = depth > INV_DEPTH;
                int select = inv ? (depth + 2) : depth;
                int selArrMask = select * 0x11111111;
                int check = inv ? depth : (depth + 2);
                depth++;
                int xorVal = depth ^ (depth + 1);
                int val = 0;
                for (int i = 0; i < N_SIZE; i++, val >>= 4)
                {
                    if ((i & 7) == 0)
                    {
                        val = PrunTable[i >> 3];
                        if (!HasZero(val ^ selArrMask))
                        {
                            i += 7;
                            continue;
                        }
                    }
                    if ((val & 0xf) != select)
                    {
                        continue;
                    }
                    int raw = i % N_RAW;
                    int sym = i / N_RAW;
                    int flip = 0, fsym = 0;
                    if (ISTFP)
                    {
                        flip = CubieCube.FlipR2S[raw];
                        fsym = flip & 7;
                        flip >>= 3;
                    }

                    for (int m = 0; m < N_MOVES; m++)
                    {
                        int symx = SymMove[sym, m];
                        int rawx;
                        if (ISTFP)
                        {
                            rawx = CubieCube.FlipS2RF[FlipMove[flip, CubieCube.Sym8Move[m << 3 | fsym]] ^ fsym ^ (symx & SYM_MASK)];
                        }
                        else
                        {
                            rawx = RawConj[RawMove[raw, m] & 0x1ff, symx & SYM_MASK];

                        }
                        symx >>= SYM_SHIFT;
                        int idx = symx * N_RAW + rawx;
                        int prun = GetPruning(PrunTable, idx);
                        if (prun != check)
                        {
                            if (prun < depth - 1)
                            {
                                m += NEXT_AXIS_MAGIC >> m & 3;
                            }
                            continue;
                        }
                        done++;
                        if (inv)
                        {
                            SetPruning(PrunTable, i, xorVal);
                            break;
                        }
                        SetPruning(PrunTable, idx, xorVal);
                        for (int j = 1, symState = SymState[symx]; (symState >>= 1) != 0; j++)
                        {
                            if ((symState & 1) != 1)
                            {
                                continue;
                            }
                            int idxx = symx * N_RAW;
                            if (ISTFP)
                            {
                                idxx += CubieCube.FlipS2RF[CubieCube.FlipR2S[rawx] ^ j];
                            }
                            else
                            {
                                idxx += RawConj[rawx, j ^ (SYM_E2C_MAGIC >> (j << 1) & 3)];
                            }
                            if (GetPruning(PrunTable, idxx) == check)
                            {
                                SetPruning(PrunTable, idxx, xorVal);
                                done++;
                            }
                        }
                    }
                }
                // Console.WriteLine(depth+"---"+ done+ "---"+Stopwatch.StartNew().Elapsed.TotalMilliseconds / 1e6d);
            }
            return Search.PARTIAL_INIT_LEVEL > 1 || depth == MAX_DEPTH;
        }

        internal static bool InitTwistFlipPrun()
        {
            // Console.WriteLine(UDSliceMove);
            return InitRawSymPrun(TwistFlipPrun, null, null, TwistMove, CubieCube.SymStateTwist, 0x19603);
        }

        internal static bool InitSliceTwistPrun()
        {
            return InitRawSymPrun(UDSliceTwistPrun, UDSliceMove, UDSliceConj, TwistMove, CubieCube.SymStateTwist, 0x69603);
        }

        internal static bool InitSliceFlipPrun()
        {
            return InitRawSymPrun(UDSliceFlipPrun, UDSliceMove, UDSliceConj, FlipMove, CubieCube.SymStateFlip, 0x69603);
        }

        internal static bool InitMEPermPrun()
        {
            return InitRawSymPrun(MEPermPrun, MPermMove, MPermConj, EPermMove, CubieCube.SymStatePerm, 0x7c724);
        }

        internal static bool InitMCPermPrun()
        {
            return InitRawSymPrun(MCPermPrun, MPermMove, MPermConj, CPermMove, CubieCube.SymStatePerm, 0x8ea34);
        }

        internal static bool InitPermCombPrun()
        {
            return InitRawSymPrun(EPermCCombPrun, CCombMove, CCombConj, EPermMove, CubieCube.SymStatePerm, 0x7c824);
        }

        internal int twist;
        internal int tsym;
        internal int flip;
        internal int fsym;
        internal int slice;
        internal int prun;

        internal int twistc;
        internal int flipc;

        internal CoordCube()
        {
        }

        internal virtual void Set(CoordCube node)
        {
            this.twist = node.twist;
            this.tsym = node.tsym;
            this.flip = node.flip;
            this.fsym = node.fsym;
            this.slice = node.slice;
            this.prun = node.prun;

            if (Search.USE_CONJ_PRUN)
            {
                this.twistc = node.twistc;
                this.flipc = node.flipc;
            }
        }

        internal virtual void CalcPruning(bool isPhase1)
        {
            prun = Math.Max(Math.Max(GetPruning(UDSliceTwistPrun, twist * N_SLICE + UDSliceConj[slice & 0x1ff, tsym]), GetPruning(UDSliceFlipPrun, flip * N_SLICE + UDSliceConj[slice & 0x1ff, fsym])), Search.USE_TWIST_FLIP_PRUN ? GetPruning(TwistFlipPrun, twist << 11 | CubieCube.FlipS2RF[flip << 3 | (fsym ^ tsym)]) : 0);
        }

        internal virtual void Set(CubieCube cc)
        {
            twist = cc.GetTwistSym();
            flip = cc.GetFlipSym();
            slice = cc.GetUDSlice();
            tsym = twist & 7;
            twist = twist >> 3;
            fsym = flip & 7;
            flip = flip >> 3;

            if (Search.USE_CONJ_PRUN)
            {
                CubieCube pc = new CubieCube();
                CubieCube.CornConjugate(cc, 1, pc);
                CubieCube.EdgeConjugate(cc, 1, pc);
                twistc = pc.GetTwistSym();
                flipc = pc.GetFlipSym();
            }
        }

        /// <summary>
        /// @return
        ///      0: Success
        ///      1: Try Next Power
        ///      2: Try Next Axis
        /// </summary>

        internal virtual int DoMovePrun(CoordCube cc, int m, bool isPhase1)
        {
            slice = UDSliceMove[cc.slice & 0x1ff, m] & 0x1ff;

            flip = FlipMove[cc.flip, CubieCube.Sym8Move[m << 3 | cc.fsym]];
            fsym = (flip & 7) ^ cc.fsym;
            flip >>= 3;

            twist = TwistMove[cc.twist, CubieCube.Sym8Move[m << 3 | cc.tsym]];
            tsym = (twist & 7) ^ cc.tsym;
            twist >>= 3;

            prun = Math.Max(
                Math.Max(
                    GetPruning(UDSliceTwistPrun, twist * N_SLICE + UDSliceConj[slice, tsym]),
                    GetPruning(UDSliceFlipPrun, flip * N_SLICE + UDSliceConj[slice, fsym])),
                Search.USE_TWIST_FLIP_PRUN ? GetPruning(TwistFlipPrun,
                twist << 11 | CubieCube.FlipS2RF[flip << 3 | (fsym ^ tsym)]) : 0);
            // Console.WriteLine(prun);
            return prun;
        }

        internal virtual int DoMovePrunConj(CoordCube cc, int m)
        {
            m = CubieCube.SymMove[3, m];
            flipc = FlipMove[cc.flipc >> 3, CubieCube.Sym8Move[m << 3 | cc.flipc & 7]] ^ (cc.flipc & 7);
            twistc = TwistMove[cc.twistc >> 3, CubieCube.Sym8Move[m << 3 | cc.twistc & 7]] ^ (cc.twistc & 7);
            return GetPruning(TwistFlipPrun, (twistc >> 3) << 11 | CubieCube.FlipS2RF[flipc ^ (twistc & 7)]);
        }
    }
}
