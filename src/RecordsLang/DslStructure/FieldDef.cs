﻿namespace RecordsLang.DslStructure
{
    public class FieldDef
    {
        public FieldDef(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public string Type { get; }
    }
}