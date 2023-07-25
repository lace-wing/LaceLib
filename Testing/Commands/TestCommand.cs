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
        public override Option[] Options { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override void Run(ref object pipe)
        {
            Console.WriteLine("tested");
        }
        int a;
        int A
        {
            get { return a; }
            set { a = value; }
        }
    }
}
