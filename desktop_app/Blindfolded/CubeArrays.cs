using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textexemplu2;

namespace Textexemplu2.Blindfolded
{
    class CubeArrays
    {
        // test 1
        /*
        public char[] Up = new char[9] { 'F', 'R','R','U','U','B','D','D','D' };
        public char[] Front = new char[9] { 'L', 'B', 'L', 'U', 'F', 'L', 'R', 'B', 'R' };
        public char[] Left = new char[9] { 'L', 'R', 'F', 'L', 'L', 'F', 'L', 'L', 'B' };
        public char[] Right = new char[9] { 'B', 'L', 'D', 'D', 'R', 'R', 'D', 'R', 'F' };
        public char[] Back = new char[9] { 'B', 'F', 'U', 'D', 'B', 'F', 'U', 'F', 'U' };
        public char[] Down = new char[9] { 'U', 'U', 'F', 'U', 'D', 'B', 'B', 'D', 'R' };
        */
        // test 2 
        /*
        public char[] Up = new char[9] { 'F', 'B','D','F','U','B','U','L','R' };
        public char[] Front = new char[9] { 'B', 'F', 'U', 'B', 'F', 'L', 'B', 'F', 'L' };
        public char[] Left = new char[9] { 'L', 'D', 'R', 'B', 'L', 'L', 'F', 'R', 'L' };
        public char[] Right = new char[9] { 'F', 'U', 'F', 'D', 'R', 'U', 'D', 'F', 'B' };
        public char[] Back = new char[9] { 'L', 'D', 'U', 'L', 'B', 'R', 'D', 'R', 'D' };
        public char[] Down = new char[9] { 'U', 'U', 'B', 'U', 'D', 'R', 'R', 'D', 'R' };
        */
        //t3
        /*
        public char[] Up = new char[9] { 'R', 'R', 'F', 'F', 'U', 'L', 'D', 'F', 'B' };
        public char[] Front = new char[9] { 'B', 'D', 'R', 'U', 'F', 'U', 'U', 'U', 'R' };
        public char[] Left = new char[9] { 'F', 'R', 'L', 'L', 'L', 'B', 'F', 'R', 'R' };
        public char[] Right = new char[9] { 'D', 'B', 'U', 'L', 'R', 'D', 'D', 'D', 'L' };
        public char[] Back = new char[9] { 'L', 'U', 'U', 'R', 'B', 'F', 'B', 'B', 'L' };
        public char[] Down = new char[9] { 'B', 'F', 'F', 'B', 'D', 'L', 'D', 'D', 'U' };
        */
        //test 4 : U2 F2 L' D2 F2 L B2 R U2 B' R' D L2 B D R U2 F' L2
        //Corners: KW GS CM DI
        //Edges: T AL WC QD RJ VX D
        /*
        public char[] Up = new char[9] { 'F', 'F', 'U', 'B', 'U', 'U', 'F', 'B', 'R' };
        public char[] Front = new char[9] { 'L', 'U', 'U', 'D', 'F', 'D', 'L', 'R', 'D' };
        public char[] Left = new char[9] { 'R', 'L', 'U', 'R', 'L', 'B', 'B', 'L', 'B' };
        public char[] Right = new char[9] { 'F', 'R', 'R', 'R', 'R', 'D', 'R', 'L', 'F' };
        public char[] Back = new char[9] { 'B', 'L', 'D', 'F', 'B', 'F', 'D', 'F', 'L' };
        public char[] Down = new char[9] { 'D', 'B', 'B', 'U', 'D', 'D', 'U', 'U', 'L' };
        */

        //test 5 : R D2 R' B2 D2 R' U2 R' U2 R U' L U2 R D F R' B U B'
        //Corners: GW PJ BH
        //Edges: QX LI BP ES OT HR
        /*
        public char[] Up = new char[9] { 'L', 'L', 'L', 'D', 'U', 'R', 'U', 'R', 'B' };
        public char[] Front = new char[9] { 'F', 'U', 'U', 'F', 'F', 'U', 'R', 'U', 'R' };
        public char[] Left = new char[9] { 'F', 'B', 'L', 'B', 'L', 'U', 'U', 'L', 'D' };
        public char[] Right = new char[9] { 'R', 'F', 'B', 'L', 'R', 'D', 'F', 'B', 'D' };
        public char[] Back = new char[9] { 'D', 'D', 'D', 'F', 'B', 'L', 'F', 'R', 'B' };
        public char[] Down = new char[9] { 'B', 'B', 'U', 'F', 'D', 'R', 'L', 'D', 'R' };
        */

        //test6(superflip): U R2 F B R B2 R U2 L B2 R U' D' R2 F R' L B2 U2 F2
        //corners: -
        //edges: AQ BM CI ED LF PJ TN HR VO WS XG
        /*
        public char[] Up = new char[9] { 'U', 'B', 'U', 'L', 'U', 'R', 'U', 'F', 'U' };
        public char[] Front = new char[9] { 'F', 'U', 'F', 'L', 'F', 'R', 'F', 'D', 'F' };
        public char[] Left = new char[9] { 'L', 'U', 'L', 'B', 'L', 'F', 'L', 'D', 'L' };
        public char[] Right = new char[9] { 'R', 'U', 'R', 'F', 'R', 'B', 'R', 'D', 'R' };
        public char[] Back = new char[9] { 'B', 'U', 'B', 'R', 'B', 'L', 'B', 'D', 'B' };
        public char[] Down = new char[9] { 'D', 'F', 'D', 'L', 'D', 'R', 'D', 'B', 'D' };
        */
        // RDFRULRDRBFURRUFDDULDBFUUUDLFRBDFLUFFFBLLRBDBLRUBBBLLD
        /*
        public char[] Up = new char[9] { 'R', 'D', 'F', 'R', 'U', 'L', 'R', 'D', 'R' };
        public char[] Front = new char[9] { 'U', 'L', 'D', 'B', 'F', 'U', 'U', 'U', 'D' };
        public char[] Left = new char[9] { 'F', 'F', 'B', 'L', 'L', 'R', 'B', 'D', 'B' };
        public char[] Right = new char[9] { 'B', 'F', 'U', 'R', 'R', 'U', 'F', 'D', 'D' };
        public char[] Back = new char[9] { 'L', 'R', 'U', 'B', 'B', 'B', 'L', 'L', 'D' };
        public char[] Down = new char[9] { 'L', 'F', 'R', 'B', 'D', 'F', 'L', 'U', 'F' };
        */

        public char[] Up = new char[9];
        public char[] Front = new char[9];
        public char[] Left = new char[9];
        public char[] Right = new char[9];
        public char[] Back = new char[9];
        public char[] Down = new char[9];
        //public char[] Up = Form2.upFaceArray;
       // public char[] Front = Form2.frontFaceArray;
       // public char[] Left = Form2.leftFaceArray;
      //  public char[] Right = Form2.rightFaceArray;
      //  public char[] Back = Form2.backFaceArray;
      //  public char[] Down = Form2.downFaceArray;
      
        public void switchArraysForm(bool switchArrays)
        {
            if (switchArrays.Equals(true))
            {
                Console.WriteLine("Manual data aquisition");
                Up = ManualDataAcquisition.upFaceArray;
                Front = ManualDataAcquisition.frontFaceArray;
                Left = ManualDataAcquisition.leftFaceArray;
                Right = ManualDataAcquisition.rightFaceArray;
                Back = ManualDataAcquisition.backFaceArray;
                Down = ManualDataAcquisition.downFaceArray;
            } else
            {
                Console.WriteLine("Automatic..");
                Up = Form2.upFaceArray;
                Front = Form2.frontFaceArray;
                Left = Form2.leftFaceArray;
                Right = Form2.rightFaceArray;
                Back = Form2.backFaceArray;
                Down = Form2.downFaceArray;
            }
        }

    }
}
