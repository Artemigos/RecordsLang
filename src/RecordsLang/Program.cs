using System;
using System.IO;
using HandlebarsDotNet;
using RecordsLang.OutputModel;
using Sprache;

namespace RecordsLang
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

            var input = File.ReadAllText(args[0]);
            var parsed = Parse(input);
            var templated = ApplyTemplate(parsed);
            File.WriteAllText(args[1], templated);

            return 0;
        }

        public static object Parse(string input)
        {
            var result = RecordsParser.Document.TryParse(input);
            if (result.WasSuccessful)
            {
                return result.Value.Postprocess();
            }

            throw new ArgumentException(result.Message);
        }

        public static string ApplyTemplate(object records)
        {
            dynamic d = records;
            string templateContent;

            using (var str = typeof(Program).Assembly.GetManifestResourceStream("RecordsLang.template.hbs"))
            using (var rd = new StreamReader(str))
            {
                templateContent = rd.ReadToEnd();
            }

            var tmpl = Handlebars.Compile(templateContent);
            return tmpl(d);
        }
    }
}