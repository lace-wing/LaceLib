
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
		public const char OptionAbbrevIndicactor = '-';
		public const string OptionNameIdicator = "--";
		public const char CommandSeparator = ' ';
		public const char CommandPiper = '|';
		public const char CommandOutputter = '>';
		public abstract class Command
		{
			public struct Option
			{
				public readonly string Name;
				public readonly string Description;
				public readonly char[] Abbreviation;
				public readonly bool TakeParam;
				public Option(string name, string description, bool takeParam, params char[] abv)
				{
					Name = name;
					Description = description;
					Abbreviation = abv;
					TakeParam = takeParam;
				}
				public bool IsValid => !string.IsNullOrEmpty(Name);
			}

			public class OptionNameDuplicateException : ArgumentException
			{
				public readonly string[] ConflictingOptions;
				public readonly string ConflictingName;
				public OptionNameDuplicateException(string name, params Type[] options)
				{
					ConflictingName = name;
					List<string> types = new List<string>();
					foreach (var type in options)
					{
						types.Add(type.ToString());
					}
					ConflictingOptions = types.ToArray();
				}
				public OptionNameDuplicateException(string name, params Option[] options)
				{
					ConflictingName = name;
                    List<string> types = new List<string>();
                    foreach (var op in options)
                    {
                        types.Add(op.GetType().ToString());
                    }
                    ConflictingOptions = types.ToArray();
                }
			}
			public class OptionAbbrevDuplicateException : ArgumentException
			{
				public readonly string[] ConflictingOptions;
				public readonly string ConflictingAbbrev;
				public OptionAbbrevDuplicateException(string abbrev, params Type[] options)
				{
					ConflictingAbbrev = abbrev;
                    List<string> types = new List<string>();
                    foreach (var type in options)
                    {
                        types.Add(type.ToString());
                    }
                    ConflictingOptions = types.ToArray();
                }
				public OptionAbbrevDuplicateException(string abbrev, params Option[] options)
				{
					ConflictingAbbrev = abbrev;
                    List<string> types = new List<string>();
                    foreach (var op in options)
                    {
                        types.Add(op.GetType().ToString());
                    }
                    ConflictingOptions = types.ToArray();
                }
			}
			public class OptionInvalidException : ArgumentException
			{
				public readonly string OptionName;
				public OptionInvalidException(string hypenedOptionName)
				{
					OptionName = hypenedOptionName;
				}
			}
			public class OptionTooShortException : ArgumentException
			{
				public OptionTooShortException()
				{
				}
			}
			public class OptionArgumentNullException : ArgumentNullException
			{
				public readonly Option ErroredOption;
				public OptionArgumentNullException(Option option)
				{
					ErroredOption = option;
				}
			}

			public abstract string Name { get; }
			public virtual string ManPath => $"./man/{Name}.txt";
			public virtual bool HasMan => File.Exists(ManPath);
			public virtual string Man => File.ReadAllText(ManPath);
			public virtual HashSet<Option> PossibleOptions { get; private set; }
			public string[] InvalidOptions { get; private set; }
			public int ExitCode { get; private set; }
			public object? Value { get; private set; }
			private Dictionary<string, Dictionary<Option, string>> arguments = new Dictionary<string, Dictionary<Option, string>>();
			public abstract void Run(ref object? pipe);
			protected void Update(int exitCode, object? value)
			{
				ExitCode = exitCode;
				Value = value;
			}
			protected bool AddOption(string arg, Option option, string param)
			{
				if (!arguments.ContainsKey(arg))
				{
					return false;
				}
				return arguments[arg].TryAdd(option, param);
			}
			protected bool AddArg(string arg)
			{
				return arguments.TryAdd(arg, new Dictionary<Option, string>());
			}
			protected Option[] MatchOptions(string text)
			{
				if (text.Length < 2 || !text.IsOption())
				{
					throw new OptionTooShortException();
				}
				List<Option> ops = new List<Option>();
				bool isFullName = text.IsOptionFullName();
                foreach (Option op in PossibleOptions)
                {
                    if (isFullName && op.Name == text[2..])
                    {
                        ops.Add(op);
                        break;
                    }
					else
					{
						text = text[1..];
						foreach (char o in text)
						{
							if (op.Abbreviation.Contains(o))
							{
								ops.Add(op);
								continue;
							}
							throw new OptionInvalidException($"{OptionAbbrevIndicactor}{o}");
						}
					}
                    throw new OptionInvalidException(text);
                }
				return ops.ToArray();
			}
			public void TakeInput(string[] input)
			{
				bool validOption = true, isOptionArg = false;
				string lastArg = string.Empty;
				Option lastOption = default;
				for (int i = 0; i < input.Length; i++)
				{
					if (!isOptionArg && !input[i].IsOption())
					{
						lastArg = input[i];
						AddArg(input[i]);
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
							validOption = true;
							isOptionArg = op.TakeParam;
							options.TryAdd(op, string.Empty);
							break;
						}
						if (!validOption)
						{
							InvalidOptions = InvalidOptions.Append(input[i]).ToArray();
						}
						continue;
					}
					if (isOptionArg)
					{
						options[options.Keys.Last()] = input[i];
						isOptionArg = false;
					}
				}
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
		public static bool IsOptionFullName(this string text)
		{
			return text.Length > 2 && text[0..1] == OptionNameIdicator;
		}
	}
}
