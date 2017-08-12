using System;
using NPolyglot.LanguageDesign;
using Sprache;

namespace RecordsLang
{
    public class RecordsPaserExport : ICodedParser
    {
        public string ExportName => "RecordsParser";

        public object Parse(string input)
        {
            var result = RecordsParser.Document.TryParse(input);
            if (result.WasSuccessful)
            {
                return result.Value.Postprocess();
            }

            throw new ArgumentException(result.Message);
        }
    }
}