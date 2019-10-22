using System.Collections.Generic;

namespace PropertiesStringifier
{
    public class FluentPropertyValue<TSource>
    {
        public TSource ClassInstance { get; set; }
        public List<string> PropertiesList { get; set; }

        /// <summary>
        /// Creates a new instance of FluentPropertyValue<TSource> and adds a property name to it.
        /// </summary>
        /// <param name="classInstance">The instance of the class of type TSource</param>
        /// <param name="propertyName">Property name to be added</param>
        public FluentPropertyValue(TSource classInstance, string propertyName)
        {
            ClassInstance = classInstance;
            PropertiesList = new List<string> { propertyName };
        }

        /// <summary>
        /// Creates a new instance of FluentPropertyValue<TSource> based on a previous instance and adds a property name to it.
        /// </summary>
        /// <param name="fluentPropertyValue">Previous instance of FluentPropertyValue<TSource></param>
        /// <param name="propertyName">Property name to be added</param>
        public FluentPropertyValue(FluentPropertyValue<TSource> fluentPropertyValue, string propertyName)
        {
            ClassInstance = fluentPropertyValue.ClassInstance;
            PropertiesList = new List<string>();
            PropertiesList.AddRange(fluentPropertyValue.PropertiesList);
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
                .StringifyProperties(p => PropertiesList.Contains(p.Name));
        }
    }
}
