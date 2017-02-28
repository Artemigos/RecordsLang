using System.Collections.Generic;
using System.Linq;

namespace RecordsLang.OutputModel
{
    public class Record
    {
        public Record(string name, string[] enabledFlags, Dictionary<string, string> attributes, Field[] fields)
        {
            Name = name;
            EnabledFlags = enabledFlags;
            Attributes = attributes;
            Fields = fields;
        }

        public string Name { get; }

        public string[] EnabledFlags { get; }

        public Dictionary<string, string> Attributes { get; }

        public Field[] Fields { get; }

        public bool DefaultConstructor =>
            EnabledFlags.Contains("defaultConstructor");

        public bool CopyConstructor =>
            EnabledFlags.Contains("copyConstructor");

        public bool DataConstructor =>
            EnabledFlags.Contains("dataConstructor");

        public bool Set =>
            EnabledFlags.Contains("set");

        public bool Serializable =>
            EnabledFlags.Contains("serializable");

        public string Base =>
            Attributes.ContainsKey("base") ? Attributes["base"] : null;

        public string[] EqualsSelected =>
            Attributes.ContainsKey("valEquals")
                ? Attributes["valEquals"].Split(',')
                : Fields.Select(x => x.Name).ToArray();

        public new bool Equals =>
            Attributes.ContainsKey("valEquals") || EnabledFlags.Contains("valEquals");

        public bool AnyDerived =>
            Base != null;
    }
}