using NPolyglot.LanguageDesign;

namespace RecordsLang
{
    public class RecordsTransformExport : ICodedTransform
    {
        public string ExportName => "RecordsTransform";

        public string Transform(object data) =>
            data.Template();
    }
}