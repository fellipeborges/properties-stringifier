namespace PropertiesStringifier
{
    class PropertyData
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public bool IsUserDefinedType { get; set; }

        public PropertyClassification Classification { get; set; }
    }
}
