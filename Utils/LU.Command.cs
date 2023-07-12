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
        public abstract class Command
        {
            public abstract string Name { get; }
            public virtual string ManPath => $"./man/{Name}.txt";
            public virtual bool HasMan => File.Exists(ManPath);
            public virtual string Man => File.ReadAllText(ManPath);
            public virtual HashSet<Option> PossibleOptions { get; private set; }
            public string[] InvalidOptions { get; private set; }
            public int ExitCode { get; private set; }
            public object? Value { get; private set; }
            private Dictionary<Option, string> options = new Dictionary<Option, string>();
            private string[] arguments = new string[0];
            public abstract void Run(ref object? pipe);
            protected void Update(int exitCode, object? value)
            {
                ExitCode = exitCode;
                Value = value;
            }
            public void TakeInput(string[] input)
            {
                bool valid = true, waitingParam = false, preArg = true;
                for (int i = 0; i < input.Length; i++)
                {
                    if (!waitingParam && !input[i].IsOption())
                    {
                        preArg = false;
                    }
                    if (!preArg)
                    {
                        arguments = arguments.Append(input[i]).ToArray();
                        continue;
                    }
                    if (input[i].IsOption())
                    {
                        foreach (Option op in PossibleOptions)
                        {
                            if (!op.Matches(input[i]))
                            {
                                continue;
                            }
                            valid = true;
                            waitingParam = op.TakeParam;
                            options.TryAdd(op, string.Empty);
                            break;
                        }
                        if (!valid)
                        {
                            InvalidOptions = InvalidOptions.Append(input[i]).ToArray();
                        }
                        continue;
                    }
                    if (waitingParam)
                    {
                        options[options.Keys.Last()] = input[i];
                        waitingParam = false;
                    }
                }
            }
        }
        public struct Option
        {
            public readonly string Name;
            public readonly string[] Abbreviation;
            public readonly bool TakeParam;
            public Option(string name, bool takeParam, params string[] abv)
            {
                Name = name;
                Abbreviation = abv;
                TakeParam = takeParam;
            }
            public bool IsValid => !string.IsNullOrEmpty(Name);
            public bool Matches(string text)
            {
                if (text == OptionIndicator + Name)
                {
                    return true;
                }
                foreach (string abv in Abbreviation)
                {
                    if (text == OptionIndicator + abv)
                    {
                        return true;
                    }
                }
                return false;
            }
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
