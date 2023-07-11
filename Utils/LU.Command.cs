using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Humanizer.DateTimeHumanizeStrategy;
using log4net.Appender;
using System.IO;

namespace LaceLib.Utils
{
    public static partial class LU
    {
        public const char OptionIdicator = '-';
        public const char CommandSeparator = ' ';
        public const char CommandPiper = '|';
        public enum CmdTextType
        {
            Command,
            Option,
            Parameter,
            Argument,
            Pipeline
        }
        public abstract class Command
        {
            public abstract string Name { get; }
            public virtual string ManPath => $"./man/{Name}.txt";
            public virtual bool HasMan => File.Exists(ManPath);
            public virtual string Man => File.ReadAllText(ManPath);
        }
        public static bool IsOption(this string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 1)
            {
                return false;
            }
            return text[1] == OptionIdicator;
        }
    }
}
