using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    public static class Extensions
    {
        public static void Increment(this object obj)
        {
            Type objType = obj.GetType();
            if (objType.IsDefined(typeof(IncrementableAttribute), false))
            {
                foreach (var prop in objType.GetProperties(BindingFlags.Public|BindingFlags.Instance))
                {
                    if (prop.PropertyType != typeof(int)) continue;

                    if (!prop.CanRead || !prop.CanWrite) continue;

                    var propGet = prop.GetGetMethod(false);
                    var propSet = prop.GetSetMethod(false);

                    if (propGet == null || propSet == null) continue;

                    int newPropValue = (int)prop.GetValue(obj) + 1;

                    prop.SetValue(obj, newPropValue);
                }
            }
            else
            {
                foreach (var prop in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.IsDefined(typeof(IncrementableAttribute), false)) continue;

                    if (prop.PropertyType != typeof(int)) throw new NonIncrementablePropertyTypeException();

                    if (!prop.CanRead || !prop.CanWrite) continue;

                    var propGet = prop.GetGetMethod(false);
                    var propSet = prop.GetSetMethod(false);

                    if (propGet == null || propSet == null) continue;

                    int newPropValue = (int)prop.GetValue(obj) + 1;

                    prop.SetValue(obj, newPropValue);
                }
            }
        }
    }
}
