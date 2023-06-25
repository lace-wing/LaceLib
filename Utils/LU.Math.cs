using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaceLib.Utils
{
    public static partial class LU
    {
        #region Compare
        public static bool Inside(this int value, int min, int max)
        {
            return value > min && value < max;
        }
        public static bool Inside(this float value, float min, float max)
        {
            return value > min && value < max;
        }
        public static bool Inside(this double value, double min, double max)
        {
            return value > min && value < max;
        }
        public static bool Around(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        public static bool Around(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
        public static bool Around(this double value, double min, double max)
        {
            return value >= min && value <= max;
        }
        #endregion
    }
}
