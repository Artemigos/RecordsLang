using System.Collections.Generic;

namespace RecordsLang.DslStructure
{
    public class RecordDef
    {
        public RecordDef(string prototypeName, string name, IEnumerable<FieldDef> fields, IEnumerable<AttributeDef> attributes)
        {
            PrototypeName = prototypeName;
            Name = name;
            Fields = fields;
            Attributes = attributes;
        }

        public string PrototypeName { get; }

        public string Name { get; }

        public IEnumerable<FieldDef> Fields { get; }

        public IEnumerable<AttributeDef> Attributes { get; }
    }
}