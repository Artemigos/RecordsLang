using System;
using RecordsLang.OutputModel;
using Sprache;

namespace RecordsLang
{
    public class Records
    {
        public object Parse(string input)
        {
            var result = RecordsParser.Document.TryParse(input);
            if (result.WasSuccessful)
            {
                return result.Value.Postprocess();
            }

            throw new ArgumentException(result.Message);
        }

        public string Transform(object data) =>
            data.Template();
    }
}