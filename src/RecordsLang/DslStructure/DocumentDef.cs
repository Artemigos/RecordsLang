using System.Collections.Generic;

namespace RecordsLang.DslStructure
{
    public class DocumentDef
    {
        public DocumentDef(string ns, IEnumerable<string> usings, IEnumerable<AttributeDef> defaults, IEnumerable<PrototypeDef> prototypes, IEnumerable<RecordDef> records)
        {
            Namespace = ns;
            Usings = usings;
            Defaults = defaults;
            Prototypes = prototypes;
            Records = records;
        }

        public string Namespace { get; }

        public IEnumerable<string> Usings { get; }

        public IEnumerable<AttributeDef> Defaults { get; }

        public IEnumerable<PrototypeDef> Prototypes { get; }

        public IEnumerable<RecordDef> Records { get; }
    }
}