# Properties Stringifier
A very simple extension method to stringify your model class properties and help in debugging.

When debugging, instead of seing this:
![Without Properties Stringifier](img/without-properties-stringifier.png?raw=true "Without Properties Stringifier")

You will see this:
![With Properties Stringifier](img/with-properties-stringifier.png?raw=true "With Properties Stringifier")

## Usage
Add the using:
```csharp
using PropertiesStringifier;
```

Overrides the "ToString()" method of your model class:
```csharp
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

That's all.
