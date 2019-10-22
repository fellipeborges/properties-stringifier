using System.Collections.Generic;

namespace PropertiesStringifier
{
    public class FluentIgnorePropertyValue<TSource>
    {
        public TSource ClassInstance { get; set; }
        public List<string> PropertiesList { get; set; }

        /// <summary>
        /// Creates a new instance of FluentIgnorePropertyValue<TSource> and adds to it a property name to be ignored .
        /// </summary>
        /// <param name="classInstance">The instance of the class of type TSource</param>
        /// <param name="propertyName">Property name to be ignored</param>
        public FluentIgnorePropertyValue(TSource classInstance, string propertyName)
        {
            ClassInstance = classInstance;
            PropertiesList = new List<string> { propertyName };
        }

        /// <summary>
        /// Creates a new instance of FluentIgnorePropertyValue<TSource> based on a previous instance and adds to it a property name to be ignored.
        /// </summary>
        /// <param name="fluentIgnoredPropertyValue">Previous instance of FluentIgnorePropertyValue<TSource></param>
        /// <param name="propertyName">Property name to be added</param>
        public FluentIgnorePropertyValue(FluentIgnorePropertyValue<TSource> fluentIgnoredPropertyValue, string propertyName)
        {
            ClassInstance = fluentIgnoredPropertyValue.ClassInstance;
            PropertiesList = new List<string>();
            PropertiesList.AddRange(fluentIgnoredPropertyValue.PropertiesList);
            PropertiesList.Add(propertyName);
        }

        /// <summary>
        /// Returns the stringified chosen properties only.
        /// </summary>
        public override string ToString()
        {
            return
                this
                .ClassInstance
                .StringifyProperties(p => 
                    !p.IsUserDefinedType &&
                    !PropertiesList.Contains(p.Name)
                );
        }
    }
}
