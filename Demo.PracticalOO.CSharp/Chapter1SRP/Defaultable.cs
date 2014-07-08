using System;
using System.Linq.Expressions;

namespace Demo.PracticalOO.CSharp.Chapter1SRP
{
    public abstract class Defaultable<T> where T:class
    {
        public TProperty GetProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue)
        {
            var expression = propertyExpression.Body as MemberExpression;

            if (expression == null)
                return defaultValue;

            var info = GetType().GetProperty(expression.Member.Name);
            if (info == null) 
                return defaultValue;

            var value = info.GetValue(this, null);
            if (ReferenceEquals(value, default(TProperty)) || (value.Equals(default(TProperty))))
                return defaultValue;

            return (TProperty) value;
        }
    }
}
