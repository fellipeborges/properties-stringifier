﻿using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

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
            string stringifiedProperties = 
                obj
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .Select(p =>
                    {
                        var propertyData = GetPropertyData(obj, p);
                        return propertyData;
                    })
                    .Where(p => p.Classification != PropertyClassification.UserDefinedType)
                    .ToList()
                    .Select(propertyData =>
                    {
                        var nameValue = propertyData.Classification switch
                        {
                            PropertyClassification.Default => GetDefaultValue(propertyData),
                            PropertyClassification.NullValue => GetNullValue(propertyData),
                            PropertyClassification.Datetime => GetDatetimeValue(propertyData),
                            PropertyClassification.Array => GetArrayValue(propertyData),
                            PropertyClassification.List => GetListValue(propertyData),
                            _ => GetDefaultValue(propertyData),
                        };

                        return nameValue.ToString();
                    })
                    .Aggregate(new StringBuilder(), (prev, next) => prev.Append(" " + next))
                    .ToString();

            return stringifiedProperties.Trim();
        }

        /// <summary>
        /// Returns the Count of a List.
        /// </summary>
        private static NameValueModel GetListValue(PropertyData propertyData)
        {
            string valueToShow = "Unknown";
            if (propertyData.Value is ICollection collection)
            {
                valueToShow = collection.Count.ToString();
            }

            return new NameValueModel
            {
                Name = $"{propertyData.Name}Count",
                Value = valueToShow
            };
        }

        /// <summary>
        /// Returns the length of an array.
        /// </summary>
        private static NameValueModel GetArrayValue(PropertyData propertyData)
        {
            return new NameValueModel
            {
                Name = $"{propertyData.Name}Count",
                Value = ((Array)propertyData.Value).Length.ToString()
            };
        }

        /// <summary>
        /// Tries to parse the value as a date and if successfull returns it in the format "yyyy-MM-dd".
        /// Otherwise returns the input value itself.
        /// </summary>
        private static NameValueModel GetDatetimeValue(PropertyData propertyData)
        {
            string valueToShow = propertyData.Value.ToString();
            if (DateTime.TryParse(valueToShow, out DateTime resultDate))
            {
                valueToShow = resultDate.ToString("yyyy-MM-dd");
            }

            return new NameValueModel
            {
                Name = propertyData.Name,
                Value = valueToShow
            };
        }

        /// <summary>
        /// Returns the model for a unknown property type
        /// </summary>
        private static NameValueModel GetDefaultValue(PropertyData propertyData)
        {
            return new NameValueModel
            {
                Name = propertyData.Name,
                Value = propertyData.Value.ToString()
            };
        }

        /// <summary>
        /// Returns the model for a null value.
        /// </summary>
        private static NameValueModel GetNullValue(PropertyData propertyData)
        {
            return new NameValueModel
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
            bool isUserDefinedType =
                property.PropertyType.IsClass &&
                property.PropertyType.Assembly.FullName == obj.GetType().Assembly.FullName;

            if (isUserDefinedType)
            {
                return PropertyClassification.UserDefinedType;
            }
            else if (value == null)
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

    }
}