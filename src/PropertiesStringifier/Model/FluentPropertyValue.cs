using System.Collections.Generic;

namespace PropertiesStringifier
{
    public class FluentPropertyValue<TSource>
    {
        private readonly TSource _classInstance;
        private readonly List<string> _propertiesList;

        /// <summary>
        /// Creates a new instance of FluentPropertyValue<TSource> and adds a property name to it.
        /// </summary>
        /// <param name="classInstance">The instance of the class of type TSource</param>
        /// <param name="propertyName">Property name to be added</param>
        public FluentPropertyValue(TSource classInstance, string propertyName)
        {
            _classInstance = classInstance;
            _propertiesList = new List<string> { propertyName };
        }

        /// <summary>
        /// Creates a new instance of FluentPropertyValue<TSource> based on a previous instance and adds a property name to it.
        /// </summary>
        /// <param name="fluentPropertyValue">Previous instance of luentPropertyValue<TSource></param>
        /// <param name="propertyName">Property name to be added</param>
        public FluentPropertyValue(FluentPropertyValue<TSource> fluentPropertyValue, string propertyName)
        {
            _classInstance = fluentPropertyValue._classInstance;
            _propertiesList = new List<string>();
            _propertiesList.AddRange(fluentPropertyValue._propertiesList);
            _propertiesList.Add(propertyName);
        }

        /// <summary>
        /// Returns the stringified chosen properties only.
        /// </summary>
        public override string ToString()
        {
            return 
                _classInstance
                .StringifyProperties(p => _propertiesList.Contains(p.Name));
        }
    }
}
