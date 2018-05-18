using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsLang.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Dto...");
            var r1 = new Dto();
            Console.WriteLine("Dto created.");

            Console.WriteLine("Creating Model...");
            var r2 = new Model(1, "2", 3L);
            Console.WriteLine("Model created.");

            Console.WriteLine("Creating Default...");
            var r3 = new Default();
            Console.WriteLine("Default created.");
        }
    }
}
