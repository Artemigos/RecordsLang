using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordsLang.DslStructure;
using Sprache;

namespace RecordsLang
{
    public class RecordsParser
    {
        public static Parser<string> SimpleIdentifier =>
            Parse.Identifier(
                Parse.Letter,
                Parse.LetterOrDigit);

        public static Parser<string> RecordName =>
            SimpleIdentifier.Token();

        public static Parser<string> FieldName =>
            SimpleIdentifier;

        public static Parser<string> TypeName =>
            SimpleIdentifier;

        public static Parser<string> AttributeName =>
            SimpleIdentifier;

        public static Parser<string> PrototypeName =>
            SimpleIdentifier.Token();

        public static Parser<string> NamespaceKeyword =>
            Parse.String("ns").Text().Token();

        public static Parser<string> UseKeyword =>
            Parse.String("use").Text().Token();

        public static Parser<string> DefaultsKeyword =>
            Parse.String("def").Text().Token();

        public static Parser<string> PrototypeKeyword =>
            Parse.String("proto").Text().Token();

        public static Parser<char> InstructionTerminator =>
            Parse.Char(';').Token();

        public static Parser<char> NamespaceCharacter =>
            Parse.AnyChar.Except(Parse.WhiteSpace.Or(InstructionTerminator));

        public static Parser<string> Namespace =>
            from k in NamespaceKeyword
            from ns in NamespaceCharacter.Many().Text().Token()
            from t in InstructionTerminator
            select ns;

        public static Parser<string> Using =>
            from k in UseKeyword
            from ns in NamespaceCharacter.Many().Text().Token()
            from t in InstructionTerminator
            select ns;

        public static Parser<char> OnAttrPrefix =>
            Parse.Char('+');

        public static Parser<char> OffAttrPrefix =>
            Parse.Char('-');

        public static Parser<char> ValueAttrInfix =>
            Parse.Char(':');

        public static Parser<string> PlainAttrValue =>
            Parse.AnyChar.Except(Parse.WhiteSpace.Or(InstructionTerminator)).Many().Text();

        public static Parser<char> QuotationDelimiter =>
            Parse.Char('"');

        public static Parser<string> QuotedAttrValue =>
            from s in QuotationDelimiter
            from value in Parse.CharExcept('"').Many().Text()
            from e in QuotationDelimiter
            select value;

        public static Parser<string> AttrValue =>
            QuotedAttrValue.Or(PlainAttrValue);

        public static Parser<AttributeDef> OnAttr =>
            from p in OnAttrPrefix
            from name in AttributeName
            select new AttributeDef(name, true);

        public static Parser<AttributeDef> OffAttr =>
            from p in OffAttrPrefix
            from name in AttributeName
            select new AttributeDef(name, false);

        public static Parser<AttributeDef> ValueAttr =>
            from name in AttributeName
            from i in ValueAttrInfix
            from value in AttrValue
            select new AttributeDef(name, value);

        public static Parser<AttributeDef> Attr =>
            OnAttr.Or(OffAttr).Or(ValueAttr);

        public static Parser<IEnumerable<AttributeDef>> AttrList =>
            Attr.DelimitedBy(Parse.WhiteSpace.AtLeastOnce()).Token();

        public static Parser<IEnumerable<AttributeDef>> Defaults =>
            from k in DefaultsKeyword
            from attrs in AttrList
            from t in InstructionTerminator
            select attrs;

        public static Parser<PrototypeDef> Prototype =>
            from k in PrototypeKeyword
            from name in PrototypeName
            from attrs in AttrList
            from t in InstructionTerminator
            select new PrototypeDef(name, attrs);

        public static Parser<char> FieldListStart =>
            Parse.Char('(');

        public static Parser<char> FieldListEnd =>
            Parse.Char(')');

        public static Parser<char> FieldDelimiter =>
            Parse.Char(',');

        public static Parser<FieldDef> Field =>
            from type in TypeName
            from i in Parse.WhiteSpace.AtLeastOnce()
            from name in FieldName
            select new FieldDef(name, type);

        public static Parser<IEnumerable<FieldDef>> FieldList =>
            from s in FieldListStart
            from fields in Field.Token().DelimitedBy(FieldDelimiter)
            from e in FieldListEnd
            select fields;

        public static Parser<RecordDef> DefaultRecord =>
            from name in RecordName
            from fields in FieldList
            from attrs in Parse.WhiteSpace.AtLeastOnce().Then(x => AttrList).Optional()
            from t in InstructionTerminator
            select new RecordDef(null, name, fields, attrs.GetOrElse(new AttributeDef[0]));

        public static Parser<RecordDef> PrototypedRecord =>
            from proto in PrototypeName
            from name in RecordName
            from fields in FieldList
            from attrs in Parse.WhiteSpace.AtLeastOnce().Then(x => AttrList).Optional()
            from t in InstructionTerminator
            select new RecordDef(proto, name, fields, attrs.GetOrElse(new AttributeDef[0]));

        public static Parser<RecordDef> Record =>
            DefaultRecord.Or(PrototypedRecord);

        public static Parser<DocumentDef> Document =>
            from ns in Namespace.Token()
            from uses in Using.Token().Many()
            from defs in Defaults.Token()
            from protos in Prototype.Token().Many()
            from records in Record.Token().Many()
            select new DocumentDef(ns, uses, defs, protos, records);
    }
}
