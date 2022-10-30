using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2.Kociemba
{
    class Util
    {
        // Moves
        internal static sbyte Ux1 = 0;
        internal static sbyte Ux2 = 1;
        internal static sbyte Ux3 = 2;

        internal static sbyte Rx1 = 3;
        internal static sbyte Rx2 = 4;
        internal static sbyte Rx3 = 5;

        internal static sbyte Fx1 = 6;
        internal static sbyte Fx2 = 7;
        internal static sbyte Fx3 = 8;

        internal static sbyte Dx1 = 9;
        internal static sbyte Dx2 = 10;
        internal static sbyte Dx3 = 11;

        internal static sbyte Lx1 = 12;
        internal static sbyte Lx2 = 13;
        internal static sbyte Lx3 = 14;

        internal static sbyte Bx1 = 15;
        internal static sbyte Bx2 = 16;
        internal static sbyte Bx3 = 17;

        //Facelets
        internal static sbyte U1 = 0;
        internal static sbyte U2 = 1;
        internal static sbyte U3 = 2;
        internal static sbyte U4 = 3;
        internal static sbyte U5 = 4;
        internal static sbyte U6 = 5;
        internal static sbyte U7 = 6;
        internal static sbyte U8 = 7;
        internal static sbyte U9 = 8;

        internal static sbyte R1 = 9;
        internal static sbyte R2 = 10;
        internal static sbyte R3 = 11;
        internal static sbyte R4 = 12;
        internal static sbyte R5 = 13;
        internal static sbyte R6 = 14;
        internal static sbyte R7 = 15;
        internal static sbyte R8 = 16;
        internal static sbyte R9 = 17;

        internal static sbyte F1 = 18;
        internal static sbyte F2 = 19;
        internal static sbyte F3 = 20;
        internal static sbyte F4 = 21;
        internal static sbyte F5 = 22;
        internal static sbyte F6 = 23;
        internal static sbyte F7 = 24;
        internal static sbyte F8 = 25;
        internal static sbyte F9 = 26;

        internal static sbyte D1 = 27;
        internal static sbyte D2 = 28;
        internal static sbyte D3 = 29;
        internal static sbyte D4 = 30;
        internal static sbyte D5 = 31;
        internal static sbyte D6 = 32;
        internal static sbyte D7 = 33;
        internal static sbyte D8 = 34;
        internal static sbyte D9 = 35;

        internal static sbyte L1 = 36;
        internal static sbyte L2 = 37;
        internal static sbyte L3 = 38;
        internal static sbyte L4 = 39;
        internal static sbyte L5 = 40;
        internal static sbyte L6 = 41;
        internal static sbyte L7 = 42;
        internal static sbyte L8 = 43;
        internal static sbyte L9 = 44;

        internal static sbyte B1 = 45;
        internal static sbyte B2 = 46;
        internal static sbyte B3 = 47;
        internal static sbyte B4 = 48;
        internal static sbyte B5 = 49;
        internal static sbyte B6 = 50;
        internal static sbyte B7 = 51;
        internal static sbyte B8 = 52;
        internal static sbyte B9 = 53;

        //Colors
        internal static sbyte U = 0;
        internal static sbyte R = 1;
        internal static sbyte F = 2;
        internal static sbyte D = 3;
        internal static sbyte L = 4;
        internal static sbyte B = 5;

        internal static readonly sbyte[,] cornerFacelet = new sbyte[,]
        {
            {U9, R1, F3},{U7, F1, L3}, {U1, L1, B3},{U3, B1, R3},
            {D3, F9, R7},{D1, L9, F7}, {D7, B9, L7}, {D9, R9, B7}
        };

        internal static readonly sbyte[,] edgeFacelet = new sbyte[,]
        {
              {U6, R2}, {U8, F2}, {U4, L2}, {U2, B2},
              {D6, R8}, {D2, F8},{D4, L8},  {D8, B8},
              {F6, R4}, {F4, L6}, {B6, L4}, {B4, R6}
        };

        internal static int[,] Cnk = new int[13, 13];
        internal static int[,] permMult = new int[24, 24];
        internal static string[] move2str = new string[] { "U ", "U2", "U'", "R ", "R2", "R'", "F ", "F2", "F'", "D ", "D2", "D'", "L ", "L2", "L'", "B ", "B2", "B'" };
        internal static int[] preMove = new int[] { -1, Rx1, Rx3, Fx1, Fx3, Lx1, Lx3, Bx1, Bx3 };
        internal static int[] ud2std = new int[] { Ux1, Ux2, Ux3, Rx2, Fx2, Dx1, Dx2, Dx3, Lx2, Bx2, Rx1, Rx3, Fx1, Fx3, Lx1, Lx3, Bx1, Bx3 };
        internal static int[] std2ud = new int[18];
        internal static int[] ckmv2bit = new int[11];

        internal static void ToCubieCube(sbyte[] face, CubieCube ccRet)
        {
            sbyte orientation, column1, column2;
            for (int i = 0; i < 8; i++)
            {
                ccRet.ca[i] = 0; //invalidate corners
            }
            for (int i = 0; i < 12; i++)
            {
                ccRet.ea[i] = 0; //invalidate edges
            }
            for (sbyte i = 0; i < 8; i++)
            {
                // get the colors of the cubie at corner i, starting with U/D
                for (orientation = 0; orientation < 3; orientation++)
                {
                    if (face[cornerFacelet[i, orientation]] == U || face[cornerFacelet[i, orientation]] == D)
                    {
                        break;
                    }
                }

                column1 = face[cornerFacelet[i, (orientation + 1) % 3]];
                column2 = face[cornerFacelet[i, (orientation + 2) % 3]];

                for (sbyte j = 0; j < 8; j++)
                {
                    if (column1 == cornerFacelet[j, 1] / 9 && column2 == cornerFacelet[j, 2] / 9)
                    {
                        // in cornerposition i we have cornercubie j
                        ccRet.ca[i] = (sbyte)(orientation % 3 << 3 | j);
                        break;
                    }
                }
            }

            for (sbyte i = 0; i < 12; i++)
            {
                for (sbyte j = 0; j < 12; j++)
                {
                    if (face[edgeFacelet[i, 0]] == edgeFacelet[j, 0] / 9 && face[edgeFacelet[i, 1]] == edgeFacelet[j, 1] / 9)
                    {
                        ccRet.ea[i] = (sbyte)(j << 1);
                        break;
                    }
                    if (face[edgeFacelet[i, 0]] == edgeFacelet[j, 1] / 9 && face[edgeFacelet[i, 1]] == edgeFacelet[j, 0] / 9)
                    {
                        ccRet.ea[i] = (sbyte)(j << 1 | 1);
                        break;
                    }
                }
            }
        }

        internal static string ToFaceCube(CubieCube cc)
        {
            char[] face = new char[54];
            char[] ts = new char[] { 'U', 'R', 'F', 'D', 'L', 'B' };
            for (int i = 0; i < 54; i++)
            {
                face[i] = ts[i / 9];
            }
            for (sbyte c = 0; c < 8; c++)
            {
                int j = cc.ca[c] & 0X7; // cornercubie with index j is at corner position with index c
                int orientation = cc.ca[c] >> 3; // Orientation of this cubie
                for (sbyte n = 0; n < 3; n++)
                {
                    face[cornerFacelet[c, (n + orientation) % 3]] = ts[cornerFacelet[j, n] / 9];
                }
            }
            for (sbyte e = 0; e < 12; e++)
            {
                int j = cc.ea[e] >> 1; // edgecubie with index j is at edge position with index e
                int orientation = cc.ea[e] & 1; // Orientation of this cubie
                for (sbyte n = 0; n < 2; n++)
                {
                    face[edgeFacelet[e, (n + orientation) % 2]] = ts[edgeFacelet[j, n] / 9];
                }
            }
            return new string(face);

        }

        internal static int GetNParity(int index, int n)
        {
            int parity = 0;
            for (int i = n - 2; i >= 0; i--)
            {
                parity ^= index % (n - i);
                index /= (n - i);
            }
            return parity & 1;
        }

        internal static sbyte SetValue(int value0, int value, bool isEdge)
        {
            return (sbyte)(isEdge ? (value << 1 | value0 & 1) : (value | value0 & 0xf8));
        }

        internal static int GetValue(int value0, bool isEdge)
        {
            return isEdge ? value0 >> 1 : value0 & 7;
        }

        internal static void SetNPerm(sbyte[] array, int index, int n, bool isEdge)
        {
            long value = unchecked((long)0xFEDCBA9876543210L);
            long extract = 0;
            for (int p = 2; p <= n; p++)
            {
                extract = extract << 4 | index % p;
                index /= p;
            }
            for (int i = 0; i < n - 1; i++)
            {
                int v = ((int)extract & 0xf) << 2;
                extract >>= 4;
                array[i] = SetValue(array[i], (int)(value >> v & 0xf), isEdge);
                long m = (1L << v) - 1;
                value = value & m | value >> 4 & ~m;
            }
            array[n - 1] = SetValue(array[n - 1], (int)(value & 0xf), isEdge);
        }

        internal static int GetNPerm(sbyte[] array, int n, bool isEdge)
        {
            int index = 0;
            long value = unchecked((long)0xFEDCBA9876543210L);
            for (int i = 0; i < n - 1; i++)
            {
                int v = GetValue(array[i], isEdge) << 2;
                index = (n - i) * index + (int)(value >> v & 0x7);
                value -= 0x1111111111111110L << v;
            }
            return index;
        }

        internal static int GetComb(sbyte[] array, int mask, bool isEdge)
        {
            int end = array.Length - 1;
            int indexComb = 0, indexPerm = 0, r = 4, val = 0x0123;
            for (int i = end; i >= 0; i--)
            {
                int perm = GetValue(array[i], isEdge);
                if ((perm & 0xc) == mask)
                {
                    int v = (perm & 3) << 2;
                    indexPerm = r * indexPerm + (val >> v & 0xf);
                    val -= 0x0111 >> (12 - v);
                    indexComb += Cnk[i, r--];
                }
            }
            return indexPerm << 9 | Cnk[array.Length, 4] - 1 - indexComb;
        }

        internal static void SetComb(sbyte[] array, int index, int mask, bool isEdge)
        {
            int end = array.Length - 1;
            int r = 4, fill = end, val = 0x0123;
            int indexComb = Cnk[array.Length, 4] - 1 - (index & 0x1ff);
            int indexPerm = index >> 9;
            int extract = 0;
            for (int p = 2; p <= 4; p++)
            {
                extract = extract << 4 | indexPerm % p;
                indexPerm /= p;
            }
            for (int i = end; i >= 0; i--)
            {
                if (indexComb >= Cnk[i, r])
                {
                    indexComb -= Cnk[i, r--];
                    int v = (extract & 0xf) << 2;
                    extract >>= 4;
                    array[i] = SetValue(array[i], val >> v & 3 | mask, isEdge);
                    int m = (1 << v) - 1;
                    val = val & m | val >> 4 & ~m;
                }
                else
                {
                    if ((fill & 0xc) == mask)
                    {
                        fill -= 4;
                    }
                    array[i] = SetValue(array[i], fill--, isEdge);
                }
            }
        }

        static Util()
        {
            for (int i = 0; i < 18; i++)
            {
                std2ud[ud2std[i]] = i;
            }
            for (int i = 0; i < 10; i++)
            {
                int ix = ud2std[i] / 3;
                ckmv2bit[i] = 0;
                for (int j = 0; j < 10; j++)
                {
                    int jx = ud2std[j] / 3;
                    ckmv2bit[i] |= ((ix == jx) || ((ix % 3 == jx % 3) && (ix >= jx)) ? 1 : 0) << j;
                }
            }
            ckmv2bit[10] = 0;
            for (int i = 0; i < 13; i++)
            {
                Cnk[i, 0] = Cnk[i, i] = 1;
                for (int j = 1; j < i; j++)
                {
                    Cnk[i, j] = Cnk[i - 1, j - 1] + Cnk[i - 1, j];
                }
            }
            sbyte[] arr1 = new sbyte[4];
            sbyte[] arr2 = new sbyte[4];
            sbyte[] arr3 = new sbyte[4];
            for (int i = 0; i < 24; i++)
            {
                SetNPerm(arr1, i, 4, false);
                for (int j = 0; j < 24; j++)
                {
                    SetNPerm(arr2, j, 4, false);
                    for (int k = 0; k < 4; k++)
                    {
                        arr3[k] = arr1[arr2[k]];
                    }
                    permMult[i, j] = GetNPerm(arr3, 4, false);
                }
            }

        }

    };
}
