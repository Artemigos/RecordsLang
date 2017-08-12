using RecordsLang.OutputModel;
using Sprache;
using NPolyglot.LanguageDesign;
using System;

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