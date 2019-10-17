# Properties Stringifier
[![NuGet](https://img.shields.io/nuget/v/PropertiesStringifier.svg)](https://www.nuget.org/packages/PropertiesStringifier/)
#### A very simple extension method to stringify your model class properties and help in debugging.

When debugging, instead of seing this:
![Without Properties Stringifier](img/without-properties-stringifier.png?raw=true "Without Properties Stringifier")

You will see this:
![With Properties Stringifier](img/with-properties-stringifier.png?raw=true "With Properties Stringifier")

## Installation
Package Manager

`
Install-Package PropertiesStringifier
`

.NET CLI

`
dotnet add package PropertiesStringifier
`

## Usage
Properties Stringifier comes in two flavors:

#### 1 - Overrides the "ToString()"
```csharp
using PropertiesStringifier;

public class Actress
{
    public string Name { get; set; }

    public int Age { get; set; }

    public override string ToString()
    {
        return this.StringifyProperties();
    }
}
```

#### 2 - Inherits from StringifyProperties base class
```csharp
using PropertiesStringifier;

public class Actress : StringifyProperties
{
    public string Name { get; set; }

    public int Age { get; set; }
}
```

That's all. 😊
