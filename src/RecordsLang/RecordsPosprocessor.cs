using System;
using System.Collections.Generic;
using System.Linq;
using RecordsLang.DslStructure;
using RecordsLang.OutputModel;

namespace RecordsLang
{
    public static class RecordsPosprocessor
    {
        public static RecordList Postprocess(this DocumentDef doc)
        {
            var result = new RecordList(doc.Namespace);
            result.Usings.AddRange(doc.Usings);

            foreach (var rec in doc.Records)
            {
                var flags = new List<string>();
                var attrs = new Dictionary<string, string>();

                var inproc = ApplyAttibuteModifications(flags, attrs, doc.Defaults);

                var proto = doc.Prototypes.FirstOrDefault(x => x.Name == rec.PrototypeName);
                if (proto != null)
                {
                    inproc = ApplyAttibuteModifications(inproc.Item1, inproc.Item2, proto.Attributes);
                }

                inproc = ApplyAttibuteModifications(inproc.Item1, inproc.Item2, rec.Attributes);

                var fields = rec.Fields.Select(x => new Field(x.Name, x.Type)).ToArray();
                result.Records.Add(new Record(rec.Name, inproc.Item1.ToArray(), inproc.Item2, fields));
            }

            return result;
        }

        private static Tuple<List<string>, Dictionary<string, string>> ApplyAttibuteModifications(
            List<string> flags,
            Dictionary<string, string> values,
            IEnumerable<AttributeDef> modifications)
        {
            var flagResult = new List<string>(flags);
            var valResult = new Dictionary<string, string>(values);

            foreach (var mod in modifications)
            {
                if (mod.Type == AttributeType.On)
                {
                    if (!flagResult.Contains(mod.Name))
                    {
                        flagResult.Add(mod.Name);
                    }

                    continue;
                }

                if (mod.Type == AttributeType.Off)
                {
                    if (flagResult.Contains(mod.Name))
                    {
                        flagResult.Remove(mod.Name);
                        continue;
                    }

                    if (valResult.ContainsKey(mod.Name))
                    {
                        valResult.Remove(mod.Name);
                    }

                    continue;
                }

                if (mod.Type == AttributeType.Value)
                {
                    valResult[mod.Name] = mod.Value;
                }
            }

            return new Tuple<List<string>, Dictionary<string, string>>(flagResult, valResult);
        }
    }
}