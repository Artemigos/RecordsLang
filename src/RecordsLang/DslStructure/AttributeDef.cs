namespace RecordsLang.DslStructure
{
    public class AttributeDef
    {
        public AttributeDef(string name, string value)
        {
            Name = name;
            Value = value;
            Type = AttributeType.Value;
        }

        public AttributeDef(string name, bool isOn)
        {
            Name = name;
            Value = string.Empty;
            Type = isOn ? AttributeType.On : AttributeType.Off;
        }

        public string Name { get; }

        public string Value { get; }

        public AttributeType Type { get; }
    }
}