using System.Collections.Generic;

namespace RecordsLang.DslStructure
{
    public class PrototypeDef
    {
        public PrototypeDef(string name, IEnumerable<AttributeDef> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; }

        public IEnumerable<AttributeDef> Attributes { get; }
    }
}