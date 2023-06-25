using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LaceLib.Helpers
{
    public static partial class LH
    {
        #region TooltipLine
        public static bool HasLine(this List<TooltipLine> lines, string lineName)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lineName == lines[i].Name)
                {
                    return true;
                }
            }
            return false;
        }
        public static int IndexOfLine(this List<TooltipLine> lines, string lineName)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lineName == lines[i].Name)
                {
                    return i;
                }
            }
            return -1;
        }
        public static List<TooltipLine> GetModLines(this List<TooltipLine> lines, Mod mod)
        {
            List<TooltipLine> result = new List<TooltipLine>();
            foreach (TooltipLine line in lines)
            {
                if (line.Mod == mod.Name)
                {
                    result.Add(line);
                }
            }
            return result;
        }
        public static List<TooltipLine> GetModLines(this List<TooltipLine> lines, string modName)
        {
            List<TooltipLine> result = new List<TooltipLine>();
            foreach (TooltipLine line in lines)
            {
                if (line.Mod == modName)
                {
                    result.Add(line);
                }
            }
            return result;
        }
        public static TooltipLine MakeTooltipLine(this string text, Mod mod, string name, Color? color = null, bool isModifier = false, bool isModifierBad = false)
        {
            TooltipLine line = new TooltipLine(mod, name, text);
            line.OverrideColor = color;
            line.IsModifier = isModifier;
            line.IsModifierBad = isModifierBad;
            return line;
        }
        public static class Tooltip
        {
            public static readonly string ItemName = "ItemName";
            public static readonly string Favorite = "Favorite";
            public static readonly string FavoriteDesc = "FavoriteDesc";
            public static readonly string Social = "Social";
            public static readonly string SocialDesc = "SocialDesc";
            public static readonly string Damage = "Damage";
            public static readonly string CritChance = "CritChance";
            public static readonly string Speed = "Speed";
            public static readonly string Knockback = "Knockback";
            public static readonly string FishingPower = "FishingPower";
            public static readonly string NeedsBait = "NeedsBait";
            public static readonly string BaitPower = "BaitPower";
            public static readonly string Equipable = "Equipable";
            public static readonly string WandConsumes = "WandConsumes";
            public static readonly string Quest = "Quest";
            public static readonly string Vanity = "Vanity";
            public static readonly string Defense = "Defense";
            public static readonly string PickPower = "PickPower";
            public static readonly string AxePower = "AxePower";
            public static readonly string HammerPower = "HammerPower";
            public static readonly string TileBoost = "TileBoost";
            public static readonly string HealLife = "HealLife";
            public static readonly string HealMana = "HealMana";
            public static readonly string UseMana = "UseMana";
            public static readonly string Placeable = "Placeable";
            public static readonly string Ammo = "Ammo";
            public static readonly string Consumable = "Consumable";
            public static readonly string Material = "Material";
            public static readonly string AnyTooltip = "Tooltip{0}";
            public static readonly string EtherianManaWarning = "EtherianManaWarning";
            public static readonly string WellFedExpert = "WellFedExpert";
            public static readonly string BuffTime = "BuffTime";
            public static readonly string OneDropLogo = "OneDropLogo";
            public static readonly string PrefixDamage = "PrefixDamage";
            public static readonly string PrefixSpeed = "PrefixSpeed";
            public static readonly string PrefixCritChance = "PrefixCritChance";
            public static readonly string PrefixUseMana = "PrefixUseMana";
            public static readonly string PrefixSize = "PrefixSize";
            public static readonly string PrefixShootSpeed = "PrefixShootSpeed";
            public static readonly string PrefixKnockback = "PrefixKnockback";
            public static readonly string PrefixAccDefense = "PrefixAccDefense";
            public static readonly string PrefixAccMaxMana = "PrefixAccMaxMana";
            public static readonly string PrefixAccCritChance = "PrefixAccCritChance";
            public static readonly string PrefixAccDamage = "PrefixAccDamage";
            public static readonly string PrefixAccMoveSpeed = "PrefixAccMoveSpeed";
            public static readonly string PrefixAccMeleeSpeed = "PrefixAccMeleeSpeed";
            public static readonly string SetBonus = "SetBonus";
            public static readonly string Expert = "Expert";
            public static readonly string SpecialPrice = "SpecialPrice";
            public static readonly string Price = "Price";
        }
        #endregion
    }
}
