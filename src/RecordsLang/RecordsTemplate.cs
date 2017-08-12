using System.IO;
using HandlebarsDotNet;

namespace RecordsLang
{
    public static class RecordsTemplate
    {
        public static string Template(this object records)
        {
            dynamic d = records;
            string templateContent;

            using (var str = typeof(RecordsTransformExport).Assembly.GetManifestResourceStream("RecordsLang.template.hbs"))
            using (var rd = new StreamReader(str))
            {
                templateContent = rd.ReadToEnd();
            }

            var tmpl = Handlebars.Compile(templateContent);
            return tmpl(d);
        }
    }
}