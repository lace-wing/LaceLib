using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace LaceLib.Utils
{
    public static partial class LU
    {
        public static string[] ToLower(this string[] array)
        {
            string[] result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i].ToLower();
            }
            return result;
        }
    }
}
