using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace LaceLib.Helpers
{
    public static partial class LH
    {
        public static string TagColor(this string text, Color color)
        {
            text = text.ReplaceLineEndings("\n");
            text = text.Insert(text.Length, "]");
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (text[i] != '\n')
                {
                    continue;
                }
                text = text.Insert(i + 1, $"[c/{color.Hex3()}:");
                text = text.Insert(i, "]");
            }
            text = text.Insert(0, $"[c/{color.Hex3()}:");
            return text;
        }
    }
}
