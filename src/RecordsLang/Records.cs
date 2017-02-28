using RecordsLang.OutputModel;
using Sprache;
using NPolyglot.LanguageDesign;
using System;

namespace RecordsLang
{
    public class Records : ICodedParser, ICodedTransform
    {
        string ICodedParser.ExportName => "RecordsParser";

        object ICodedParser.Parse(string input)
        {
            var result = RecordsParser.Document.TryParse(input);
            if (result.WasSuccessful)
            {
                return result.Value.Postprocess();
            }

            throw new ArgumentException(result.Message);
        }

        string ICodedTransform.ExportName => "RecordsTransform";

        string ICodedTransform.Transform(object data) =>
            data.Template();
    }
}