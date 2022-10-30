using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textexemplu2
{
    class HSVColorRange
    {
        public char HSVRange(int hue, int saturation, int value)
        {
            char finalcolor;

            if (hue >= 0 && hue <= 180 && saturation >= 0 && saturation <= 80 && value >= 50 && value <= 255)
            {
                finalcolor = 'w';
            }
            else if (hue >= 17 && hue <= 36 && saturation >= 30 && saturation <= 255 && value >= 0 && value <= 255)
            {
                finalcolor = 'y';
            }
            else if (hue >= 0 && hue <= 16 && saturation >= 50 && saturation <= 255 && value >= 50 && value <= 255)
            {
                finalcolor = 'o';
            }
            else if (hue >= 120 && hue <= 177 && saturation >= 55 && saturation <= 255 && value >= 0 && value <= 255)
            {
                finalcolor = 'r';
            }
            else if (hue >= 40 && hue <= 94 && saturation >= 50 && saturation <= 255 && value >= 0 && value <= 255)
            {
                finalcolor = 'g';
            }
            else if (hue >= 95 && hue <= 160 && saturation >= 50 && saturation <= 255 && value >= 0 && value <= 255)
            {
                finalcolor = 'b';
            }
            else
                finalcolor = 'i';

            return finalcolor;
        }
    }
}
