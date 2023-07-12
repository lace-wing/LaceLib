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
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace LaceLib.Utils
{
    public static partial class LU
    {
        public const char OptionIndicator = '-';
        public const char CommandSeparator = ' ';
        public const char CommandPiper = '|';
        public static string[] PATH = new string[] { "./bin/commands/" };
        public abstract class Command
        {
            public abstract string Name { get; }
            public virtual string ManPath => $"./man/{Name}.txt";
            public virtual bool HasMan => File.Exists(ManPath);
            public virtual string Man => File.ReadAllText(ManPath);
            public int ExitCode { get; private set; }
            public object? Value { get; private set; }
            public virtual Option[] Options { get; private set; }
            public abstract void Run(ref object? pipe);
            protected void Update(int exitCode, object? value)
            {
                ExitCode = exitCode;
                Value = value;
            }
        }
        public struct Option
        {
            public readonly string Name;
            public readonly string[] Abversions;
            public readonly int ParamNum;
            public Option(string name, string[] abv, int paramNum)
            {
                Name = name;
                Abversions = abv;
                ParamNum = paramNum;
            }
            public bool IsValid => !string.IsNullOrEmpty(Name);
        }
        public static bool IsOption(this string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 1)
            {
                return false;
            }
            return text[1] == OptionIndicator;
        }
    }
}
