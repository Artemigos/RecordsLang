# RecordsLang

This is a language allowing to defing pure data classes in a concise way. It's built on top of [NPolyglot](https://github.com/Artemigos/NPolyglot.Core).

## Installation

`Install-Package RecordsLang -Pre`

**NOTE:** Visual Studio caches the list of available build actions, so it might be necessary to unload and load the project.

## Setting up a records file

The records are built during compile time and for that to kick in the files need to be set up properly:

* create a text file
* set the file's build action to `Records`

## Syntax

Each records file is divided into four sections:

1. Namespace declaration
1. List of default attributes
1. List of record prototypes
1. The records themselves

The order can't be changed

### Namespace declaration

This section is **required**.

```
ns My.Namespace;
```

This will translate to:

```csharp
namespace My.Namespace
{
    // ...
}
```

### List of default attributes

This section is **required**.

First let's talk about attributes. They are modifiers, that control how the generated classes will look like. There are 2 types: flags and values. Flags are off by default and have to be turned on if needed. Values are not defined by default.

To turn on a flag use `+flagName`. To set a value use `valueName:value`. If a value contains spaces use `valueName:"value with spaces"`.

Now back to default attributes - this section defines flags and values that by default will be on all records:

```
def +set +defaultConstructor;
```

This makes sure that by default, all records will have setters on their properties and will have a default constructor.

### List of records prototypes

Besides the defaults, it is possible to have named prototypes, that define different sets of attributes and can be applied to specific records.

A prototype can look like this:

```
proto MyPrototype -set +dataConstructor +serializable;
```

Prototypes don't ignore the defaults, they **override** them.

There's a `-attributeName` syntax, that *unsets* a flag or *removes* a value. Also setting a value to somethig different will scrap the previous value.

A prototype can the be used like this:

```
MyPrototype Record(int Field1);
```

### List of records

A record definition consist of an optional prototype, name, list of fields and list of local attributes. First let's look at an example:

```
PrototypeName RecordName(TypeName1 FieldName1, TypeName2 FieldName2) +flag value:someValue;
```

First we have the prototype name, which is optional. If skipped no prototype is used.

Then we have the record name, which should be self explanatory.

After that there's the list of field enclosed in parentheses. Each field consists of a type followed by a name. Fields are separated with a comma.

At the end there's a list of attributes that apply to this record and override things defined anywhere else.

## More on attributes

### Evaluation of attributes

For each record:

* By default all flags are disbaled and all values are not set
* Attributes from the `def` list are applied
* If record has a prototype, it's attributes are applied while changing flag states and values defined previously
* Record's local attributes are applied while changing flag states and values defined previously

The result is what will be generated in code.

### Available attributes

* `+set` flag - constrols whether generated properties will have a setter
* `+defaultConstructor` flag - controls whether a default constructor will be generated
* `+dataConstructor` flag - controls whether a constructor accepting all fields as parameters will be generated
* `+copyConstructor` flag - controls whether a copy constructor will be generated
* `+serializable` flag - controls whether a serializable attribute will be added to the class
* `+valEquals` flag - controls whether equality for the class will be overriden (this includes `Equals`, `GetHashCode`, `operator ==`, `operator !=`)
* `valEquals:Field1,Field2` value - same as the equivalent flag, but only specified fields are used for equality comparison. When both the flag and the value are defined, the value takes priority
* `base:BaseClass` value - allows defining a base class
