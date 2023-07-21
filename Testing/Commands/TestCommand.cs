using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaceLib.Utils;
using Terraria;

namespace LaceLib.Testing.Commands
{
    internal class TestCommand : LU.Command
    {
        public override string Name => "test";
        public override HashSet<Option> PossibleOptions => base.PossibleOptions;
        public override void Run(ref object pipe)
        {
            Console.WriteLine("tested");
        }
    }
}
