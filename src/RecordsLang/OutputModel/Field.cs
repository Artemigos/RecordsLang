namespace RecordsLang.OutputModel
{
    public class Field
    {
        public Field(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public string Type { get; }
    }
}