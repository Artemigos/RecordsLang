using System;
using System.Linq;
using NUnit.Framework;
using RecordsLang.DslStructure;
using Sprache;

namespace RecordsLang.UT
{
    [TestFixture]
    public class RecordsTests
    {
        [TestCase("+Attr1", "Attr1")]
        [TestCase("+Attr2", "Attr2")]
        public void ParsesOnAttr(string input, string name)
        {
            var result = RecordsParser.Attr.Parse(input);
            Assert.AreEqual(AttributeType.On, result.Type);
            Assert.AreEqual(name, result.Name);
        }

        [TestCase("-Attr1", "Attr1")]
        [TestCase("-Attr2", "Attr2")]
        public void ParsesOffAttr(string input, string name)
        {
            var result = RecordsParser.Attr.Parse(input);
            Assert.AreEqual(AttributeType.Off, result.Type);
            Assert.AreEqual(name, result.Name);
        }

        [TestCase("Attr1:plain", "Attr1", "plain")]
        [TestCase("Attr2:\"quoted val\"", "Attr2", "quoted val")]
        public void ParsesValueAttr(string input, string name, string value)
        {
            var result = RecordsParser.Attr.Parse(input);
            Assert.AreEqual(AttributeType.Value, result.Type);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(value, result.Value);
        }

        [TestCase(
            "+flag1 -flag2 +flag3 someVal1:plain someVal2:\"is quoted\"",
            "flag1", "flag2", "flag3", "someVal1", "someVal2")]
        public void ParsesCorrectAttrNames(string input, params string[] expected)
        {
            var attrs = RecordsParser.AttrList.Parse(input).ToArray();

            Assert.AreEqual(expected.Length, attrs.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], attrs[i].Name);
            }
        }

        [TestCase(
            "+flag1 -flag2 +flag3 someVal1:plain someVal2:\"is quoted\"",
            AttributeType.On, AttributeType.Off, AttributeType.On, AttributeType.Value, AttributeType.Value)]
        public void ParsesCorrectAttrTypes(string input, params AttributeType[] expected)
        {
            var attrs = RecordsParser.AttrList.Parse(input).ToArray();

            Assert.AreEqual(expected.Length, attrs.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], attrs[i].Type);
            }
        }

        [TestCase(
            "+flag1 -flag2 +flag3 someVal1:plain someVal2:\"is quoted\"",
            "", "", "", "plain", "is quoted")]
        public void ParsesCorrectAttrValues(string input, params string[] expected)
        {
            var attrs = RecordsParser.AttrList.Parse(input).ToArray();

            Assert.AreEqual(expected.Length, attrs.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], attrs[i].Value);
            }
        }

        [Test]
        public void ParsesDefaults()
        {
            var defaultsDef = "def +flag1 -flag2 someVal:val;";
            var expected = new AttributeDef[]
            {
                new AttributeDef("flag1", true),
                new AttributeDef("flag2", false),
                new AttributeDef("someVal", "val")
            };

            var parsed = RecordsParser.Defaults.Parse(defaultsDef).ToArray();

            Assert.AreEqual(expected.Length, parsed.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Name, parsed[i].Name);
                Assert.AreEqual(expected[i].Type, parsed[i].Type);
                Assert.AreEqual(expected[i].Value, parsed[i].Value);
            }
        }

        [Test]
        public void ParsesPrototype()
        {
            var protoDef = "proto myProto +flag1 -flag2 someVal:val;";
            var expected = new AttributeDef[]
            {
                new AttributeDef("flag1", true),
                new AttributeDef("flag2", false),
                new AttributeDef("someVal", "val")
            };

            var parsed = RecordsParser.Prototype.Parse(protoDef);
            var attrs = parsed.Attributes.ToArray();

            Assert.AreEqual("myProto", parsed.Name);
            Assert.AreEqual(expected.Length, attrs.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Name, attrs[i].Name);
                Assert.AreEqual(expected[i].Type, attrs[i].Type);
                Assert.AreEqual(expected[i].Value, attrs[i].Value);
            }
        }

        [TestCase("Rec1(int F1, long F2);", null, "Rec1", 2, 0)]
        [TestCase("immutable Rec1(int F1);", "immutable", "Rec1", 1, 0)]
        [TestCase("immutable Rec1(int F1) quoted:\"the value\" -copy;", "immutable", "Rec1", 1, 2)]
        public void ParsesRecords(string input, string proto, string name, int fieldsCount, int attrsCount)
        {
            var parsed = RecordsParser.Record.Parse(input);

            Assert.AreEqual(proto, parsed.PrototypeName);
            Assert.AreEqual(name, parsed.Name);
            Assert.AreEqual(fieldsCount, parsed.Fields.Count());
            Assert.AreEqual(attrsCount, parsed.Attributes.Count());
        }
    }
}
