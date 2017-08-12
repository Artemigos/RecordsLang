using RecordsLang.OutputModel;
using Sprache;
using NPolyglot.LanguageDesign;
using System;

namespace RecordsLang
{
    public class RecordsTransformExport : ICodedTransform
    {
        public string ExportName => "RecordsTransform";

        public string Transform(object data) =>
            data.Template();
    }
}