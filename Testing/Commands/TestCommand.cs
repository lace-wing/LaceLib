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
        public override LU.Option[] PossibleOptions => new LU.Option[] { new LU.Option("-period", false, "p", "d") };
        public override void Run(ref object pipe)
        {
            Console.WriteLine("tested");
        }
    }
}
