using System;
using System.Linq.Expressions;

namespace PropertiesStringifier
{
    public static class StringifyFluentExtension
    {
        public static FluentPropertyValue<TSource> StringifyThisProperty<TSource, TProperty>(this TSource obj, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            string propertyName = GetName(propertyLambda);
            return new FluentPropertyValue<TSource>(obj, propertyName);
        }

        public static FluentPropertyValue<TSource> AndThisProperty<TSource, TProperty>(this FluentPropertyValue<TSource> fluentPropertuValue, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            string propertyName = GetName(propertyLambda);
            return new FluentPropertyValue<TSource>(fluentPropertuValue, propertyName);
        }

        private static string GetName<TSource, TProperty>(Expression<Func<TSource, TProperty>> exp)
        {
            if (!(exp.Body is MemberExpression body))
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }
    }
}
