using System;

namespace PropertiesStringifier
{
    public abstract class StringifyProperties
    {
        public override string ToString()
        {
            return this.StringifyProperties();
        }
    }
}
