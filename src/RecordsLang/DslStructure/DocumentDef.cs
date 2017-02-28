using System.Collections.Generic;

namespace RecordsLang.DslStructure
{
    public class DocumentDef
    {
        public DocumentDef(string ns, IEnumerable<AttributeDef> defaults, IEnumerable<PrototypeDef> prototypes, IEnumerable<RecordDef> records)
        {
            Namespace = ns;
            Defaults = defaults;
            Prototypes = prototypes;
            Records = records;
        }

        public string Namespace { get; }

        public IEnumerable<AttributeDef> Defaults { get; }

        public IEnumerable<PrototypeDef> Prototypes { get; }

        public IEnumerable<RecordDef> Records { get; }
    }
}