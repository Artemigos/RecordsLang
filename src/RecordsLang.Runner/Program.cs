using System;

namespace RecordsLang.Runner
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid number of arguments.");
                return 1;
            }

            var input = System.IO.File.ReadAllText(args[0]);

            var r = new Records();
            var data = r.Parse(input);
            var result = r.Transform(data);

            System.IO.File.WriteAllText(args[1], result);

            return 0;
        }
    }
}