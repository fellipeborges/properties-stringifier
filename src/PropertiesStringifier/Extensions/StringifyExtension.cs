using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace PropertiesStringifier
{
    public static class StringifyExtension
    {
        /// <summary>
        /// Stringifies a class properties in the format "Property: Value".
        /// User defined types are ignored.
        /// </summary>
        public static string StringifyProperties(this object obj)
        {
            var notUserDefinedTypeProperties = StringifyProperties(obj, x => !x.IsUserDefinedType);
            return notUserDefinedTypeProperties;
        }

        /// <summary>
        /// Stringifies a class properties in the format "Property: Value" and applies a predicate to filter the properties.
        /// </summary>
        internal static string StringifyProperties(this object obj, Func<PropertyData, bool> predicate)
        {
            try
            {
                string stringifiedProperties =
                    obj
                        .GetType()
                        .GetProperties()
                        .ToList()
                        .Select(p => GetPropertyData(obj, p))
                        .Where(predicate)
                        .Select(propertyData => GetNameValueByClassification(propertyData).ToString())
                        .Aggregate("", (prev, next) => $"{prev} {next}")
                        .ToString()
                        .Trim();

                return stringifiedProperties;
            }
            catch (Exception ex)
            {
                return $"Failed to stringify properties: {ex.Message}";
            }
        }

        /// <summary>
        /// Returns an instance of NameValue model based on the classification.
        /// </summary>
        private static NameValue GetNameValueByClassification(PropertyData propertyData)
        {
            NameValue nameValue;

            switch (propertyData.Classification)
            {
                case PropertyClassification.Default:
                    nameValue = GetDefaultValue(propertyData);
                    break;

                case PropertyClassification.NullValue:
                    nameValue = GetNullValue(propertyData);
                    break;

                case PropertyClassification.Datetime:
                    nameValue = GetDatetimeValue(propertyData);
                    break;

                case PropertyClassification.Array:
                    nameValue = GetArrayValue(propertyData);
                    break;

                case PropertyClassification.List:
                    nameValue = GetListValue(propertyData);
                    break;

                default:
                    nameValue = GetDefaultValue(propertyData);
                    break;
            }

            return nameValue;
        }

        /// <summary>
        /// Returns the Count of a List.
        /// </summary>
        private static NameValue GetListValue(PropertyData propertyData)
        {
            string valueToShow = "Unknown";
            if (propertyData.Value is ICollection collection)
            {
                valueToShow = collection.Count.ToString();
            }

            return new NameValue
            {
                Name = $"{propertyData.Name}Count",
                Value = valueToShow
            };
        }

        /// <summary>
        /// Returns the length of an array.
        /// </summary>
        private static NameValue GetArrayValue(PropertyData propertyData)
        {
            return new NameValue
            {
                Name = $"{propertyData.Name}Count",
                Value = ((Array)propertyData.Value).Length.ToString()
            };
        }

        /// <summary>
        /// Tries to parse the value as a date and if successfull returns it in the format "yyyy-MM-dd" or "yyyy-MM-dd hh:mm:ss".
        /// Otherwise returns the input value itself.
        /// </summary>
        private static NameValue GetDatetimeValue(PropertyData propertyData)
        {
            string valueToShow = propertyData.Value.ToString();
            if (DateTime.TryParse(valueToShow, out DateTime resultDate))
            {
                string format = "yyyy-MM-dd hh:mm:ss";
                if (resultDate.Hour == 0 && resultDate.Minute == 0 && resultDate.Second == 0)
                {
                    format = "yyyy-MM-dd";
                }

                valueToShow = resultDate.ToString(format);
            }

            return new NameValue
            {
                Name = propertyData.Name,
                Value = valueToShow
            };
        }

        /// <summary>
        /// Returns the model for a unknown property type
        /// </summary>
        private static NameValue GetDefaultValue(PropertyData propertyData)
        {
            return new NameValue
            {
                Name = propertyData.Name,
                Value = propertyData.Value.ToString()
            };
        }

        /// <summary>
        /// Returns the model for a null value.
        /// </summary>
        private static NameValue GetNullValue(PropertyData propertyData)
        {
            return new NameValue
            {
                Name = propertyData.Name,
                Value = "null"
            };
        }

        /// <summary>
        /// Analyses a property and the object to return a instance of "PropertyData" model with its information.
        /// </summary>
        private static PropertyData GetPropertyData(object obj, PropertyInfo property)
        {
            return new PropertyData
            {
                Name = property.Name,
                Value = property.GetValue(obj, null),
                IsUserDefinedType = GetIfIsUserDefinedType(obj, property),
                Classification = GetPropertyClassification(obj, property)
            };
        }

        /// <summary>
        /// Returns the property classification based on its value and type.
        /// </summary>
        private static PropertyClassification GetPropertyClassification(object obj, PropertyInfo property)
        {
            object value = property.GetValue(obj, null);
            string typeName = property.PropertyType.ToString().ToLower();

            if (value == null)
            {
                return PropertyClassification.NullValue;
            }
            else if (typeName.Contains("datetime"))
            {
                return PropertyClassification.Datetime;
            }
            else if (property.PropertyType.IsArray)
            {
                return PropertyClassification.Array;
            }
            else if (typeName.Contains("generic.list"))
            {
                return PropertyClassification.List;
            }

            return PropertyClassification.Default;
        }

        /// <summary>
        /// Returns the property classification based on its value and type.
        /// </summary>
        private static bool GetIfIsUserDefinedType(object obj, PropertyInfo property)
        {
            bool isUserDefinedType =
                property.PropertyType.IsClass &&
                property.PropertyType.Assembly.FullName == obj.GetType().Assembly.FullName;

            return isUserDefinedType;
        }
    }
}
