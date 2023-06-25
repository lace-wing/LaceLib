using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static LaceLib.Helpers.LH;
using static Terraria.GameContent.Bestiary.On_BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;
using Terraria.UI;
using System.Threading;
using Terraria.ModLoader.IO;

namespace LaceLib.Helpers
{
    public static partial class LH
    {
        #region Chat tag
        public static string AddColorTag(this string text, Color color)
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
        #endregion

        #region Localized text
        public static string GenericLocKey(Mod mod, object catagory, object content, string dataName)
        {
            return $"Mods.{mod.Name}.{catagory.GetType()}.{content.GetType()}.{dataName}";
        }
        public static string GenericLocKey(Mod mod, string catagoryName, object content, string dataName)
        {
            return $"Mods.{mod.Name}.{catagoryName}.{content.GetType()}.{dataName}";
        }
        public static class LocCatagory
        {
            #region Vanilla catagories
            public static readonly string DamageClasses = "DamageClasses";
            public static readonly string InfoDisplays = "InfoDisplays";
            public static readonly string Biomes = "Biomes";
            public static readonly string Buffs = "Buffs";
            public static readonly string Items = "Items";
            public static readonly string NPCs = "NPCs";
            public static readonly string Prefixes = "Prefixes";
            public static readonly string Projectiles = "Projectiles";
            public static readonly string ResourceDisplaySets = "ResourceDisplaySets";
            public static readonly string Tiles = "Tiles";
            public static readonly string Keybinds = "Keybinds";
            public static readonly string Configs = "Configs";
            #endregion

            #region Custom catagories
            public static readonly string Common = "Common";
            public static readonly string Messages = "Messages";
            public static readonly string Terms = "Terms";
            public static readonly string ExtraTooltip = "ExtraTooltip";
            #endregion
        }
        public static class LocData
        {
            #region Vanilla data
            public static readonly string DisplayName = "DisplayName";
            public static readonly string Tooltip = "Tooltip";
            public static readonly string Label = "Label";
            public static readonly string Header = "Header";
            public static readonly string MapEntry = "MapEntry";
            public static readonly string ContainerName = "ContainerName";
            #endregion

            #region Custom data
            public static readonly string MapEntry_Locked = "MapEntry_Locked";
            public static readonly string MapEntry_Inactive = "MapEntry_Inactive";
            public static readonly string DisplayName_Inactive = "DisplayName_Inactive";
            #endregion
        }
        #endregion
    }
}