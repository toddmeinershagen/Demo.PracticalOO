using System;
using System.Collections;
using System.Linq.Expressions;

namespace Demo.PracticalOO.CSharp.Chapter1SRP
{
    public static class ObjectExtensions
    {
        public static TDefault GetProperty<TDefault>(this object self, string property, TDefault defaultValue)
        {
            var info = self.GetType().GetProperty(property);
            if (info == null) return defaultValue;
            return (TDefault)info.GetValue(self, null);
        }

        public static TDefault Fetch<TDefault>(this IDictionary self, object key, TDefault defaultValue)
        {
            object value;
            if (self.TryGet(key, out value))
                return (TDefault)value;

            return defaultValue;
        }

        public static bool TryGet(this IDictionary self, object key, out object value)
        {
            try
            {
                value = self[key];
                return true;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }
    }
}
