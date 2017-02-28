using System.Collections.Generic;

namespace RecordsLang.OutputModel
{
    public class RecordList
    {
        public RecordList(string ns)
        {
            Namespace = ns;
            Records = new List<Record>();
        }

        public string Namespace { get; }

        public List<Record> Records { get; }
    }
}